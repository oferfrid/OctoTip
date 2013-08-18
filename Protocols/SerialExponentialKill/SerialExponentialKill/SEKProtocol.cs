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

namespace SerialExponentialKill
{
	/// <summary>
	/// Description of SEKProtocol.
	/// </summary>
	[Protocol("Serial Exponential Kill","Orit Gefen","Serial Exponential Kill for type 2")]
	public class SEKProtocol:Protocol
	{
		FileInfo ProtocolStateFile;
		LogInGoogleDocs myLogInGoogleDocs;
		
		public new SEKProtocolParameters ProtocolParameters
		{
			get{return (SEKProtocolParameters) base.ProtocolParameters;}
			set{base.ProtocolParameters = value;}
		}
		
		
		public SEKProtocol(SEKProtocolParameters ProtocolParameters):base((ProtocolParameters)ProtocolParameters)
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
			
			
			ReportProtocolState(ProtocolParameters.Cycle,string.Format("Insert first plate to pos:{0}",ProtocolParameters.LicPlatePosition ));
			ChangeState(new SEKStartState(ProtocolParameters.LicPlatePosition));	
			
			while((ProtocolParameters.CurentWell<24 || ProtocolParameters.LicPlatePositions.Length>0)&& !this.ShouldStop)
			{
				Wait4OD();
				int freezeIndex = 0;
				bool freez = (Array.IndexOf(ProtocolParameters.FreezeWells,ProtocolParameters.CurentWell)>0);
				if(freez)
				{
					freezeIndex = LocalUtils.GetNextFreezPos(ProtocolParameters.SharedResourcesFilePath,string.Format("{0}-Cycle:{1}(Plate:{2} Well:{3})",ProtocolParameters.Name, ProtocolParameters.Cycle,ProtocolParameters.LicPlatePosition,ProtocolParameters.CurentWell));
					if (freezeIndex>24)
					{
						ReportProtocolState(ProtocolParameters.Cycle,string.Format("No freeze in pos:{0} (> 24)",freezeIndex));
						freezeIndex = 0;
					}
				}
				
							
				
				ReportProtocolState(ProtocolParameters.Cycle,string.Format("Add Amp to plate {0} Well {1} and freez in pos {2}",ProtocolParameters.LicPlatePosition ,ProtocolParameters.CurentWell,freezeIndex));
				ChangeState(new AddAmpState(ProtocolParameters.LicPlatePosition,ProtocolParameters.CurentWell,freezeIndex,ProtocolParameters.AMPPosition));
					
				ReportProtocolState(ProtocolParameters.Cycle,string.Format("Kiling in plate {0} Well {1} for {2:0.00} (Hours)",ProtocolParameters.LicPlatePosition ,ProtocolParameters.CurentWell,ProtocolParameters.KillTime));
				ChangeState(new KillState(ProtocolParameters.LicPlatePosition,ProtocolParameters.CurentWell,freezeIndex,ProtocolParameters.AMPPosition));
			
				
				
				TimeSpan GrowthTime = DateTime.Now - StartGrowTime;
				int DiluteUsing384PlateIndex = LocalUtils.GetNext384Index(ProtocolParameters.SharedResourcesFilePath);
				int DiluteUsing384PlatePos = LocalUtils.GetNext384Pos(DiluteUsing384PlateIndex);
				
				
				if(ProtocolParameters.CurentWell<24)
				{
					//dilute in same plate
					
					ReportProtocolState(ProtocolParameters.Cycle,string.Format("Dilute from plate {0} Well {1} after {2:0.00} Hours (using 384 {3} Position) freeze:{4}",ProtocolParameters.LicPlatePosition ,ProtocolParameters.CurentWell,GrowthTime.TotalHours,DiluteUsing384PlatePos,freezeIndex));
					ChangeState(new SEKDiluteState(ProtocolParameters.LicPlatePosition,ProtocolParameters.CurentWell,freezeIndex ,DiluteUsing384PlatePos));
					ProtocolParameters.CurentWell++;
				}
				else
				{
					//dilute to new plate
					int NewPlateInd = ProtocolParameters.LicPlatePositions[0];
					ReportProtocolState(ProtocolParameters.Cycle,string.Format("Dilute from plate {0} to Plate {1} after {2:0.00} Hours (using 384 {3} Position) freeze:{4}",ProtocolParameters.LicPlatePosition ,NewPlateInd,GrowthTime.TotalHours,DiluteUsing384PlatePos,freezeIndex));
					ChangeState(new SEKDilute2NewPlateState(ProtocolParameters.LicPlatePosition,NewPlateInd,freezeIndex,DiluteUsing384PlatePos));
					//remove old plate index from list
					ProtocolParameters.LicPlatePositions = ProtocolParameters.LicPlatePositions.Skip(1).ToArray();
					ProtocolParameters.LicPlatePosition = NewPlateInd;
					ProtocolParameters.CurentWell = 1;
				}
				ProtocolParameters.Cycle++;
				StartGrowTime = DateTime.Now;
				//
				//while(false);
				
				
			}
			
			
		}
		
		private void Wait4OD()
		{
				double BackroundOD;
				double OD;
				
				DateTime StartGrowTime = DateTime.Now;
				
				ReportProtocolState(ProtocolParameters.Cycle,string.Format("read Backround from plate {0} Well {1}",ProtocolParameters.LicPlatePosition ,ProtocolParameters.CurentWell));
				SEKReadBackroundState ReadBackroundState = new SEKReadBackroundState(ProtocolParameters.LicPlatePosition,ProtocolParameters.CurentWell);
				ChangeState(ReadBackroundState);
				
				BackroundOD = ReadBackroundState.BackroundOD;
				ReportProtocolState(ProtocolParameters.Cycle,string.Format("End reading Backround from plate {0} Well {1} OD={2:0.0000}",ProtocolParameters.LicPlatePosition ,ProtocolParameters.CurentWell,BackroundOD));

				ReportProtocolState(ProtocolParameters.Cycle,string.Format("wait for first OD read for plate {0} Well {1}, {2:0.00} Hours",ProtocolParameters.LicPlatePosition ,ProtocolParameters.CurentWell,ProtocolParameters.Time4TheFirstODTest));
				ChangeState(new SEKWait2ODState(ProtocolParameters.Time4TheFirstODTest));
				do
				{
					ReportProtocolState(ProtocolParameters.Cycle,string.Format("read OD from plate {0} Well {1}",ProtocolParameters.LicPlatePosition ,ProtocolParameters.CurentWell));
					SEKReadODState ReadODState = new SEKReadODState(ProtocolParameters.LicPlatePosition,ProtocolParameters.CurentWell);
					ChangeState(ReadODState);
					
					OD = ReadODState.OD;
					ReportProtocolState(ProtocolParameters.Cycle,string.Format("End reading OD from plate {0} Well {1} Net OD={2:0.0000} (OD={3:0.0000})",ProtocolParameters.LicPlatePosition ,ProtocolParameters.CurentWell,OD-BackroundOD,OD));

					if(((OD - BackroundOD)<=ProtocolParameters.NetODtoDilute )&& !this.ShouldStop)
					{
						double TimeTillNextRead = GetTimeTillNextRead((OD - BackroundOD),ProtocolParameters.NetODtoDilute);
						ReportProtocolState(ProtocolParameters.Cycle,string.Format("wait for OD read for plate {0} Well {1}, {2:0.00} min",ProtocolParameters.LicPlatePosition ,ProtocolParameters.CurentWell,TimeTillNextRead));
						ChangeState(new SEKWait2ODState(TimeTillNextRead/60));
					}
				}
				while((OD - BackroundOD)<ProtocolParameters.NetODtoDilute && !this.ShouldStop);
			
				TimeSpan TimeOfGrow = DateTime.Now- StartGrowTime;
				
				ReportProtocolState(ProtocolParameters.Cycle,string.Format("Net OD {0:0.000}>{1:0.000} in plate {2} Well {3}, after {2:0.00} Hours",(OD - BackroundOD),ProtocolParameters.NetODtoDilute,ProtocolParameters.LicPlatePosition ,ProtocolParameters.CurentWell,TimeOfGrow.TotalHours));
		}
		
		private double GetTimeTillNextRead(double NetOD,double TargetOD)
		{
			double TimeTillNextRead = -(ProtocolParameters.MaxTimeBetweenODreads - ProtocolParameters.MinTimeBetweenODreads)/(ProtocolParameters.NetODtoDilute)*NetOD + ProtocolParameters.MaxTimeBetweenODreads;
			return TimeTillNextRead;
						
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
				typeof(SEKDiluteState),
				typeof(SEKDilute2NewPlateState),
				typeof(SEKWait2ODState),
				typeof(SEKReadBackroundState),
				typeof(SEKReadODState),
				typeof(SEKStartState)
			};
		}
		#endregion
	}
}
