/*
 * Created by SharpDevelop.
 * User: Tecan
 * Date: 18/10/2012
 * Time: 14:33
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using OctoTip.Lib.ExperimentsCore.Attributes;
using OctoTip.Lib.ExperimentsCore.Base;

namespace SerialDilutionONEvolution
{
	/// <summary>
	/// Description of SDONEProtocol.
	/// </summary>
	[Protocol("Serial Dilution ON Evolution","Ofer Fridman","Serial Dilution Over night Evolution experiment")]
	public class SDONEProtocol:Protocol
	{
		FileInfo ProtocolStateFile;
		LogInGoogleDocs myLogInGoogleDocs;
		
		public new SDONEProtocolParameters ProtocolParameters
		{
			get{return (SDONEProtocolParameters) base.ProtocolParameters;}
			set{base.ProtocolParameters = value;}
		}
		
		
		public SDONEProtocol(SDONEProtocolParameters ProtocolParameters):base((ProtocolParameters)ProtocolParameters)
		{
			//create protocol File
			string LogName =  ProtocolParameters.Name + "_" + DateTime.Now.ToString("yyyyMMddHHmm") ;
			ProtocolStateFile = new FileInfo(ProtocolParameters.OutputFilePath + LogName+".txt");
			myLogInGoogleDocs = new LogInGoogleDocs(LogName,this.ProtocolParameters.SharedResourcesFilePath);
			ReportProtocolState(0,string.Format("Creating Protoclo {0} ({1}), using parameters: \n{2}",ProtocolParameters.Name,this.GetType().Name,ProtocolParameters.ToString()));
			
		}
		
		protected override void DoWork()
		{
			
			
			ReportProtocolState(ProtocolParameters.Cycle,string.Format("Starting Protocol {0}({1})",ProtocolParameters.Name,this.GetType().Name));
			
			
			while((ProtocolParameters.CurentWell<24)&& !this.ShouldStop)
			{
				DateTime StartCycleTime = DateTime.Now;
				int freezeIndex = 0;
				bool freez = (Array.IndexOf(ProtocolParameters.FreezeWells,ProtocolParameters.CurentWell)>0);
				if(freez)
				{
					//Log(ProtocolParameters.Name.ToString() +" " + ProtocolParameters.Cycle.ToString() + " " + ProtocolParameters.LicPlatePosition.ToString() + " " + ProtocolParameters.CurentWell.ToString());
					//myLogInGoogleDocs.Log(ProtocolParameters.Name.ToString() +" " + ProtocolParameters.Cycle.ToString() + " " + ProtocolParameters.LicPlatePosition.ToString() + " " + ProtocolParameters.CurentWell.ToString());
					
						freezeIndex = LocalUtils.GetNextFreezPos(ProtocolParameters.SharedResourcesFilePath,string.Format("{0}-Cycle:{1}(Plate:{2} Well:{3})",ProtocolParameters.Name, ProtocolParameters.Cycle,ProtocolParameters.LicPlatePosition,ProtocolParameters.CurentWell));
						
					if (freezeIndex>24)
					{
						ReportProtocolState(ProtocolParameters.Cycle,string.Format("No freeze in pos:{0} (> 24)",freezeIndex));
						freezeIndex = 0;
					}
				}
				if(ProtocolParameters.CurentWell<24)
				{
					//dilute
					
					ReportProtocolState(ProtocolParameters.Cycle,string.Format("Dilute from plate {0} Well {1} after {2:0.00} Hours freeze:{3}",ProtocolParameters.LicPlatePosition ,ProtocolParameters.CurentWell,24,freezeIndex));
					ChangeState(new SDONEDiluteState(ProtocolParameters.LicPlatePosition,ProtocolParameters.CurentWell,freezeIndex));
					ProtocolParameters.CurentWell++;
				}
				else
				{
					ReportProtocolState(ProtocolParameters.Cycle,string.Format("No more wells plate {0} Well {1}",ProtocolParameters.LicPlatePosition ,ProtocolParameters.CurentWell));
					this.RequestStop();
					break;
					
				}
				
			TimeSpan TimeFromDilution = (DateTime.Now - StartCycleTime);
			double TotalHours4Dilution =  (TimeSpan.FromHours(ProtocolParameters.Time4Dilution) - TimeFromDilution).TotalHours;
				
				
				ReportProtocolState(ProtocolParameters.Cycle,string.Format("wait for first next day. plate {0} Well {1}, {2:0.00} Hours",ProtocolParameters.LicPlatePosition ,ProtocolParameters.CurentWell,TotalHours4Dilution));
				ChangeState(new SDONEWait24State(TotalHours4Dilution));
			}
		}
		
		
		public void ReportProtocolState(int Cycle,string Messege)
		{
			string LogMessege = string.Format("({0}):{1}",Cycle,Messege);
			
			try
			{
				using (StreamWriter sw = ProtocolStateFile.AppendText())
			{
				sw.WriteLine("({0}){1}:\t{2}",Cycle,DateTime.Now,Messege);
				sw.Flush();
			}
			myLogInGoogleDocs.Log(LogMessege);
			DisplayData(LogMessege);
			}
			catch(Exception e)
			{
				myProtocolLogger.Add("Error:" + e.ToString());
			}
			myProtocolLogger.Add(LogMessege);
			
		}
		
		#region static
		
		
		public static new List<Type> ProtocolStates()
		{
			return new List<Type>{
				typeof(SDONEDiluteState),
				typeof(SDONEWait24State),
			};
		}
		#endregion
	}
}
