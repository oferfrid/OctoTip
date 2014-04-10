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
namespace MDKPlate1
{
	/// <summary>
	/// Description of MDKPlate1Protocol.
	/// </summary>
	[Protocol("MDK Plate 1","Asher","Preform MDKPlate1 on single plate")]
	public class MDKProtocol:Protocol
	{
		
		FileInfo ProtocolStateFile;
		LogInGoogleDocs myLogInGoogleDocs;
		
		
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
			myLogInGoogleDocs = new LogInGoogleDocs(LogName,this.ProtocolParameters.SharedResourcesFilePath);
			ReportProtocolState(0,string.Format("Creating Protocol {0} ({1}), using parameters: \n{2}",ProtocolParameters.Name,this.GetType().Name,ProtocolParameters.ToString()));
			
		}
		
		
		
		protected override void DoWork()
		{
			ReportProtocolState(ProtocolParameters.InoculateCycle,string.Format("Starting Protocol {0}({1})",ProtocolParameters.Name,this.GetType().Name));
			
			
			if (ProtocolParameters.InoculateCycle==0)
			{
				ReportProtocolState(0,string.Format(@"Prepering plate {0} with logarithmic scale concentrations (twofold between adjacent columns)",ProtocolParameters.LicPlatePosition));
				
				this.ChangeState(new MDKPreparePlateState(ProtocolParameters.LicPlatePosition));
			}
			
			//Total time between inoculations (including inoculation time)+ casting
			
			double TotIncTime = (ProtocolParameters.MaxTime-ProtocolParameters.MinTime)/7;
			double InoculateLength;
			double Time2Incubate;
			DateTime StartInoculte = new DateTime();
			
			//Inoculation loop, rows 1-7
			
			ProtocolParameters.InoculateCycle++;
			while(ProtocolParameters.InoculateCycle<8&&!this.ShouldStop)
			{
				StartInoculte = DateTime.Now;
				
				ReportProtocolState(ProtocolParameters.InoculateCycle,string.Format("Inoculating row {0} in plate {1}",ProtocolParameters.InoculateCycle,ProtocolParameters.LicPlatePosition));
				
				this.ChangeState(new MDKInoculateState(ProtocolParameters.LicPlatePosition,ProtocolParameters.GermIndex,ProtocolParameters.InoculateCycle));
				
				InoculateLength = (DateTime.Now-StartInoculte).TotalHours;
				
				//Incubation
								
				Time2Incubate  = TotIncTime-InoculateLength;
				ReportProtocolState(ProtocolParameters.InoculateCycle,string.Format("Plate {0} in incubator for {1:0.0} Minutes",ProtocolParameters.LicPlatePosition,Time2Incubate*60));
				
				this.ChangeState(new MDKIncubateState(Time2Incubate));
				
				
				ProtocolParameters.InoculateCycle++;
			}
			
			//Inoculation - row 8
						
			ReportProtocolState(ProtocolParameters.InoculateCycle,string.Format("Inoculating row {0} in plate {1}",ProtocolParameters.InoculateCycle,ProtocolParameters.LicPlatePosition));
				
			this.ChangeState(new MDKInoculateState(ProtocolParameters.LicPlatePosition,ProtocolParameters.GermIndex,ProtocolParameters.InoculateCycle));
						
			//Incubation
								
			ReportProtocolState(9,string.Format("Plate {0} in incubatorfor {1:0.0} Minutes",ProtocolParameters.LicPlatePosition,ProtocolParameters.MinTime*60));
			
			this.ChangeState(new MDKIncubateState(ProtocolParameters.MinTime));
				
			
			//Antibiotic deactivation and final incubation
			
			ReportProtocolState(9,string.Format("Deactivating antibiotic in plate {0}",ProtocolParameters.LicPlatePosition));
				
			this.ChangeState(new MDKDeactivateState(ProtocolParameters.LicPlatePosition,ProtocolParameters.BLacIndex,ProtocolParameters.MIC));
			
			ReportProtocolState(ProtocolParameters.InoculateCycle,string.Format("Plate {0} in incubatorfor {1:0.0} Minutes",ProtocolParameters.LicPlatePosition,ProtocolParameters.FinIncTime*60));
				
			this.ChangeState(new MDKFinalIncubateState(ProtocolParameters.FinIncTime));
			
		}
		
		public void ReportProtocolState(int InoculateCycle,string Messege)
		{
			string LogMessege = string.Format("({0}):{1}",InoculateCycle,Messege);
			
			try
			{
				using (StreamWriter sw = ProtocolStateFile.AppendText())
				{
					sw.WriteLine("({0}){1}:\t{2}",InoculateCycle,DateTime.Now,Messege);
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
				typeof(MDKDeactivateState),
				typeof(MDKFinalIncubateState),
				typeof(MDKIncubateState),
				typeof(MDKInoculateState),
				typeof(MDKPreparePlateState)};
			#endregion
		}
	}
}