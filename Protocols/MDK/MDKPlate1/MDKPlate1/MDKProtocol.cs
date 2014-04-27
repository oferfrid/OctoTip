/*
 * Created by SharpDevelop.
 * User: Tecan
 * Date: 11/02/2014
 * Time: 11:28
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

namespace MDKPlate1
{
	/// <summary>
	/// Description of MDKPlate1Protocol.
	/// </summary>
	[Protocol("MDK Plate 1","Asher","Preform MDKPlate1 on single plate")]
	public class MDKProtocol:Protocol
	{
		
		FileInfo ProtocolStateFile;
		
		
		public new MDKProtocolParameters ProtocolParameters
		{
			get{
				return (MDKProtocolParameters) base.ProtocolParameters;
			}
			set{
				base.ProtocolParameters = value;
			}
		}
		
		
		public MDKProtocol(MDKProtocolParameters ProtocolParameters):base((ProtocolParameters)ProtocolParameters)
		{
			string LogName =  ProtocolParameters.Name + "_" + DateTime.Now.ToString("yyyyMMddHHmm") ;
			ProtocolStateFile = new FileInfo(ProtocolParameters.OutputFilePath + LogName+".txt");
			ReportProtocolState(0,string.Format("Creating Protocol {0} ({1}), using parameters: \n{2}",ProtocolParameters.Name,this.GetType().Name,ProtocolParameters.ToString()));
			
		}
		
		
		
		protected override void DoWork()
		{
			ReportProtocolState(ProtocolParameters.InoculateCycle,string.Format("Starting Protocol {0}({1})",ProtocolParameters.Name,this.GetType().Name));
			
			
			if (ProtocolParameters.InoculateCycle==0)
			{
				ReportProtocolState(0,string.Format(@"Prepering plate {0} with logarithmic scale concentrations ({1}μg/ml-{2}μg/ml)",ProtocolParameters.LicPlatePosition));
				
				this.ChangeState(new MDKPreparePlateState(ProtocolParameters.LicPlatePosition,ProtocolParameters.mu()));
			}
			
			//Total time between inoculations (including inoculation time)+ casting
			
			double TotIncTime = (ProtocolParameters.MaxTime-ProtocolParameters.MinTime)/7;
			int MIC = 0;
			
			if(ProtocolParameters.MIC)
			{
				MIC = 1;
				TotIncTime	= (ProtocolParameters.MaxTime-ProtocolParameters.MinTime)/6;
				
				ReportProtocolState(ProtocolParameters.InoculateCycle,string.Format("Inoculating MIC row in plate {1}",ProtocolParameters.LicPlatePosition));
				
				this.ChangeState(new MDKInoculateState(ProtocolParameters.LicPlatePosition,ProtocolParameters.GermIndex,1));
				
			}
			
			double InoculateLength;
			double Time2Incubate;
			
			DateTime StartInoculte = new DateTime();
			
			//Inoculation loop, except last row
			
			ProtocolParameters.InoculateCycle++;
			
			while(ProtocolParameters.InoculateCycle<8-MIC&&!this.ShouldStop)
			{
				StartInoculte = DateTime.Now;
				
				ReportProtocolState(ProtocolParameters.InoculateCycle,string.Format("Inoculating row {0} in plate {1}",(ProtocolParameters.InoculateCycle+MIC),ProtocolParameters.LicPlatePosition));
				
				this.ChangeState(new MDKInoculateState(ProtocolParameters.LicPlatePosition,ProtocolParameters.GermIndex,(ProtocolParameters.InoculateCycle+MIC)));
				
				InoculateLength = (DateTime.Now-StartInoculte).TotalHours;
				
				//Incubation
								
				Time2Incubate  = TotIncTime-InoculateLength;
				ReportProtocolState(ProtocolParameters.InoculateCycle,string.Format("Plate {0} in incubator for {1:0.0} Minutes",ProtocolParameters.LicPlatePosition,Time2Incubate*60));
				
				this.ChangeState(new MDKIncubateState(Time2Incubate));
				
				
				ProtocolParameters.InoculateCycle++;
			}
			
			//Inoculation - last row
						
			ReportProtocolState(ProtocolParameters.InoculateCycle,string.Format("Inoculating row {0} in plate {1}",(ProtocolParameters.InoculateCycle+MIC),ProtocolParameters.LicPlatePosition));
				
			this.ChangeState(new MDKInoculateState(ProtocolParameters.LicPlatePosition,ProtocolParameters.GermIndex,(ProtocolParameters.InoculateCycle+MIC)));
						
			//Incubation
								
			ReportProtocolState(9,string.Format("Plate {0} in incubatorfor {1:0.0} Minutes",ProtocolParameters.LicPlatePosition,ProtocolParameters.MinTime*60));
			
			this.ChangeState(new MDKIncubateState(ProtocolParameters.MinTime));
				
			
			//Antibiotic deactivation and final incubation
			
			ReportProtocolState(9,string.Format("Deactivating antibiotic in plate {0}",ProtocolParameters.LicPlatePosition));
				
			this.ChangeState(new MDKDeactivateState(ProtocolParameters.LicPlatePosition,ProtocolParameters.BLacIndex,MIC));
			
			ReportProtocolState(ProtocolParameters.InoculateCycle,string.Format("Plate {0} in incubatorfor {1:0.0} Minutes",ProtocolParameters.LicPlatePosition,ProtocolParameters.FinIncTime*60));
				
			this.ChangeState(new MDKFinalIncubateState(ProtocolParameters.FinIncTime));
			
		}
		
		public void ReportProtocolState(int InoculateCycle,string Messege)
		{
			string LogMessege = string.Format("({0}):{1}",InoculateCycle,Messege);
			
			
				this.ProtocolLog(LogMessege,LoggingEntery.EnteryTypes.Informational);
				DisplayData(LogMessege);
		}
		
		#region static
		
		
		public static new List<Type> ProtocolStates()
		{
			return new List<Type>{
				typeof(MDKDeactivateState),
				typeof(MDKFinalIncubateState),
				typeof(MDKIncubateState),
				typeof(MDKInoculateState),
				typeof(MDKPreparePlateState)};
			#endregion
		}
	}
}