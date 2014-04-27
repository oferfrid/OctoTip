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
using OctoTip.Lib.Logging;

namespace SerialDilutionONEvolution
{
	/// <summary>
	/// Description of SDONEProtocol.
	/// </summary>
	[Protocol("Serial Dilution ON Evolution","Ofer Fridman","Serial Dilution Over night Evolution experiment")]
	public class SDONEProtocol:Protocol
	{
		
		public new SDONEProtocolParameters ProtocolParameters
		{
			get{return (SDONEProtocolParameters) base.ProtocolParameters;}
			set{base.ProtocolParameters = value;}
		}
		
		
		public SDONEProtocol(SDONEProtocolParameters ProtocolParameters):base((ProtocolParameters)ProtocolParameters)
		{
			string Title = 	"Creating Protocol";
			string Messege = string.Format("Creating Protoco1 {0} ({1}), using parameters: \n{2}",ProtocolParameters.Name,this.GetType().Name,ProtocolParameters.ToString());				
			ReportProtocolState(Title,Messege);
			
		}
		
		protected override void DoWork()
		{
			string Title = 	"Starting Protocol";
			string Messege = string.Format("Starting Protocol {0}({1})",ProtocolParameters.Name,this.GetType().Name);
			ReportProtocolState(Title,Messege);
			
			while((ProtocolParameters.CurentWell<24) && !this.ShouldStop)
			{
				DateTime StartCycleTime = DateTime.Now;
				int freezeIndex = 0;
				bool freez = (Array.IndexOf(ProtocolParameters.FreezeWells,ProtocolParameters.CurentWell)>0);
				if(freez)
				{
					//Log(ProtocolParameters.Name.ToString() +" " + ProtocolParameters.Cycle.ToString() + " " + ProtocolParameters.LicPlatePosition.ToString() + " " + ProtocolParameters.CurentWell.ToString());
					//myLogInGoogleDocs.Log(ProtocolParameters.Name.ToString() +" " + ProtocolParameters.Cycle.ToString() + " " + ProtocolParameters.LicPlatePosition.ToString() + " " + ProtocolParameters.CurentWell.ToString());
					
					freezeIndex = LocalUtils.GetNextFreezPos(ProtocolParameters.SharedResourcesFilePath,string.Format("{0}{1}-Cycle:{2}(Plate:{3} Well:{4})",DateTime.Now.ToString(),ProtocolParameters.Name, ProtocolParameters.Cycle,ProtocolParameters.LicPlatePosition,ProtocolParameters.CurentWell));
					
					if (freezeIndex>24)
					{
						Title = "Unable to freeze";
						ReportProtocolState(Title,string.Format("No freeze in pos:{0} (> 24)",freezeIndex));
						freezeIndex = 0;
					}
				}
				if(ProtocolParameters.CurentWell<24)
				{
					//dilute
					 Title = "Dilute";
					 Messege = string.Format("Dilute from plate {0} Well {1} after {2:0.00} Hours freeze:{3}",ProtocolParameters.LicPlatePosition ,ProtocolParameters.CurentWell,24,freezeIndex);
					ReportProtocolState(Title,Messege);
					ChangeState(new SDONEDiluteState(ProtocolParameters.LicPlatePosition,ProtocolParameters.CurentWell,freezeIndex));
					ProtocolParameters.CurentWell++;
					ProtocolParameters.Cycle++;
				}
				else
				{
					ReportProtocolState("No more wells in plate",string.Format("No more wells in plate {0} Well {1}",ProtocolParameters.LicPlatePosition ,ProtocolParameters.CurentWell));
					this.RequestStop();
					break;
					
				}
				
				
				if (this.ProtocolParameters.UseReader)
				{
					DateTime StartWait4StationaryState = DateTime.Now;
					
					ReportProtocolState("Read background",string.Format("Read background from plate {0} Well {1}",ProtocolParameters.LicPlatePosition ,ProtocolParameters.CurentWell));
					SDONEReadWellBackgroundState ReadBackgroundState =new SDONEReadWellBackgroundState(ProtocolParameters.LicPlatePosition,ProtocolParameters.CurentWell);
					ChangeState(ReadBackgroundState);
					double BackgroundOD = ReadBackgroundState.BackgroundWellOD;
					ReportProtocolState("End of read Background",string.Format("End of read Background from plate {0} Well {1} Background OD:{2:0.000}",ProtocolParameters.LicPlatePosition ,ProtocolParameters.CurentWell,BackgroundOD));
					
					while( ((DateTime.Now - StartCycleTime).TotalHours < ProtocolParameters.Time4Dilution) && !this.ShouldStop)
					{
						//wait
						double TimeTillNextCycel =  ProtocolParameters.Time4Dilution - (DateTime.Now - StartCycleTime).TotalHours;
						double TimeTillNextRead = Math.Min(ProtocolParameters.TimeBetweenReads, TimeTillNextCycel);
						ReportProtocolState("Wait for next read",string.Format("Wait for next read. plate {0} Well {1}, {2:0.00} Hours Reading in {3:0.00} Hours",ProtocolParameters.LicPlatePosition ,ProtocolParameters.CurentWell,TimeTillNextCycel,TimeTillNextRead));
						ChangeState(new SDONEWait4ReadState(TimeTillNextRead));
						//read
						ReportProtocolState("Read well",string.Format("Read well from plate {0} Well {1}",ProtocolParameters.LicPlatePosition ,ProtocolParameters.CurentWell));
						SDONEReadWellState ReadWellState =new SDONEReadWellState(ProtocolParameters.LicPlatePosition,ProtocolParameters.CurentWell);
						ChangeState(ReadWellState);
						double WellOD = ReadWellState.WellOD;
						ReportProtocolState("End of OD read",string.Format("End of OD read from plate {0} Well {1} OD:{2:0.000}(absolute={3:0.000})",ProtocolParameters.LicPlatePosition ,ProtocolParameters.CurentWell,WellOD-BackgroundOD,WellOD));
					}
				}
				else
				{
					double TotalHours4Dilution =  (DateTime.Now - StartCycleTime).TotalHours;
					ReportProtocolState("Wait for next round",string.Format("Wait for next round. plate {0} Well {1}, {2:0.00} Hours",ProtocolParameters.LicPlatePosition ,ProtocolParameters.CurentWell,TotalHours4Dilution));
					ChangeState(new SDONEWait4StationaryState(TotalHours4Dilution));
				}
				
			}
		}
		
		
		public void ReportProtocolState(string Title,string Messege)
		{
			string LogMessege = string.Format("({0}):{1}",ProtocolParameters.Cycle,Messege);
			
			this.ProtocolLog(Title,LogMessege, LoggingEntery.EnteryTypes.Informational);
			
		}
		
		#region static
		
		
		public static new List<Type> ProtocolStates()
		{
			return new List<Type>{
				typeof(SDONEDiluteState),
				typeof(SDONEWait4StationaryState),
				typeof(SDONEWait4ReadState),
				typeof(SDONEReadWellBackgroundState),
				typeof(SDONEReadWellState)
			};
		}
		#endregion
	}
}
