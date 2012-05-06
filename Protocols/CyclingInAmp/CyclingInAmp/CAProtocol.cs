/*
 * Created by SharpDevelop.
 * User: oferfrid
 * Date: 04/04/2012
 * Time: 15:58
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;
using System.IO;
using OctoTip.Lib.ExperimentsCore.Attributes;
using OctoTip.Lib.ExperimentsCore.Base;

namespace CyclingInAmp
{
	/// <summary>
	/// Description of CAProtocol.
	/// </summary>
	[Protocol("Cyclic AMP","Ofer Fridman","Cyclic Evolution In AMP with b-lac")]
	public class CAProtocol:Protocol
	{
		public new CAProtocolParameters ProtocolParameters
		{
			get{return (CAProtocolParameters) base.ProtocolParameters;}
			set{base.ProtocolParameters = value;}
		}
		
		public CAProtocol(CAProtocolParameters ProtocolParameters):base((ProtocolParameters)ProtocolParameters)
		{
			//create protocol File
			
			ProtocolStateFile = new FileInfo(ProtocolParameters.OutputFilePath + ProtocolParameters.Name + "_" + DateTime.Now.ToString("yyyyMMddHHmm") +".txt");
			ReportProtocolState(0,string.Format("Creating Protoclo {0} ({1}), using parameters: \n{2}",ProtocolParameters.Name,this.GetType().Name,ProtocolParameters.ToString()));
			
		}
		
		
		
		
		protected override void DoWork( )
		{
			

			ReportProtocolState(ProtocolParameters.CycleInd,string.Format("Starting Protocol {0}({1})",ProtocolParameters.Name,this.GetType().Name));
			if(ProtocolParameters.RunStart)
			{
			this.ChangeState(new CAStart(ProtocolParameters.LicInds[0]));
			}
			
			while(!this.ShouldStop)
			{
				ProtocolParameters.CycleInd++;
				
				ReportProtocolState(ProtocolParameters.CycleInd,string.Format("End of dilution Starting the Kill state ({0:0.0} hours)",ProtocolParameters.KillTime));
				ChangeState(new CAKill(ProtocolParameters.KillTime));
				
				
				int WellInd = ((ProtocolParameters.CycleInd-1)%3)*2+1;
				int LiconicInd =ProtocolParameters.LicInds[ProtocolParameters.PlateInd - 1];
				ReportProtocolState(ProtocolParameters.CycleInd,string.Format("End of Kill state and start of b-Lac add state on plate {0} to well ind={1} ",LiconicInd,WellInd ));
				ChangeState(new CAAddbLac(LiconicInd,WellInd));
				ReportProtocolState(ProtocolParameters.CycleInd,"End b-Lac addition");
				// whait for OD
				double OD;
				ReportProtocolState(ProtocolParameters.CycleInd,string.Format("Waiting for OD > {0:0.000} in plate {1} to well ind={2}",ProtocolParameters.AbsolutOD2Dilut,LiconicInd,WellInd));
				do
				{
					CAGetOD _CAGetOD = new CAGetOD(ProtocolParameters.Empty384WellIndFilePath,LiconicInd,WellInd,ProtocolParameters.OutputFilePath);
					ChangeState(_CAGetOD);
					OD = _CAGetOD.GetReadResult();
					ReportProtocolState(ProtocolParameters.CycleInd,string.Format("OD={0:0.000}",OD));
					if(OD<ProtocolParameters.AbsolutOD2Dilut)
					{
					ChangeState(new CAGrowToOD(ProtocolParameters.ReadFrequency/60));
					}
				}while(OD<ProtocolParameters.AbsolutOD2Dilut && !this.ShouldStop);
				
				ReportProtocolState(ProtocolParameters.CycleInd,string.Format(" OD = {0:0.000} > {1:0.000} in plate {2} to well ind={3} starting Dilution ",OD,ProtocolParameters.AbsolutOD2Dilut,LiconicInd,WellInd));
				ChangeState(new CADilut(LiconicInd,WellInd+1));
				ReportProtocolState(ProtocolParameters.CycleInd,string.Format("Dilution Ended Witing to ON ({0:0.0} hours)",ProtocolParameters.Time2ON));
				ChangeState(new CAGrowToON(ProtocolParameters.Time2ON));
				if ((ProtocolParameters.CycleInd-1)%3==2 && !this.ShouldStop)
				{//replace plate
					int NewLiconicInd =ProtocolParameters.LicInds[ProtocolParameters.PlateInd++];
					ReportProtocolState(ProtocolParameters.CycleInd,string.Format("Incubation Ended. diluting to AMP from plate {0} to new Plate {1}, freezing ON at position {2}",LiconicInd,NewLiconicInd,ProtocolParameters.FreezeInd));	
					ChangeState(new CADilutb2AmpInNewPlate(LiconicInd,NewLiconicInd,ProtocolParameters.FreezeInd++));
				}
				else
				{
					ReportProtocolState(ProtocolParameters.CycleInd,string.Format("Incubation Ended. diluting to AMP plate {0}",LiconicInd));	
					int Dilut2WellInd = ((ProtocolParameters.CycleInd-1)%3)*2+3;
					ChangeState(new CADilutb2Amp(LiconicInd,Dilut2WellInd,ProtocolParameters.CycleInd));
				}
				
			}
		}
		
		FileInfo ProtocolStateFile;
		public void ReportProtocolState(int CycleInd,string Messege)
		{
			using (StreamWriter sw = ProtocolStateFile.AppendText())
			{
				sw.WriteLine("({0}){1}:\t{2}",CycleInd,DateTime.Now,Messege);
				sw.Flush();
			}
			DisplayData(Messege);
			myProtocolLogger.Add(Messege);
		}
		
		#region static
		
		
		public static new List<Type> ProtocolStates()
		{
			return new List<Type>{
				typeof(CAAddbLac),
				typeof(CADilut),
				typeof(CADilutb2Amp),
				typeof(CADilutb2AmpInNewPlate),
				typeof(CAGrowToOD),
				typeof(CAGrowToON),
				typeof(CAKill),
				typeof(CAStart),
				typeof(CAGetOD)
			};
		}
		#endregion
	}
}
