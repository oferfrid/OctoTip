/*
 * Created by SharpDevelop.
 * User: oferfrid
 * Date: 06/05/2012
 * Time: 12:09
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using OctoTip.Lib.ExperimentsCore.Attributes;
using OctoTip.Lib.ExperimentsCore.Base;

namespace KillCurveExpPlaiting
{
	/// <summary>
	/// Description of KCEPProtocol.
	/// </summary>
	[Protocol("Kill Curve Exp Plaiting","Ofer Fridman","Kill courve of Exponential by plaiting.")]
	public class KCEPProtocol:Protocol
	{
		DateTime KillStartTime;
		LogInGoogleDocs myLogInGoogleDocs;
		
		public new KCEPProtocolParameters ProtocolParameters
		{
			get{return (KCEPProtocolParameters) base.ProtocolParameters;}
			set{base.ProtocolParameters = value;}
		}
		
		public KCEPProtocol(KCEPProtocolParameters ProtocolParameters):base((ProtocolParameters)ProtocolParameters)
		{
			//create protocol File
			string LogName =  ProtocolParameters.Name + "_" + DateTime.Now.ToString("yyyyMMddHHmm") ;
			myLogInGoogleDocs = new LogInGoogleDocs(LogName,this.ProtocolParameters.SharedResourcesFilePath);
			ProtocolStateFile = new FileInfo(ProtocolParameters.OutputFilePath + ProtocolParameters.Name + "_" + DateTime.Now.ToString("yyyyMMddHHmm") +".txt");
			ReportProtocolState(0,string.Format("Creating Protoclo {0} ({1}), using parameters: \n{2}",ProtocolParameters.Name,this.GetType().Name,ProtocolParameters.ToString()));
			
		}
		
		
		protected override void DoWork( )
		{
			ReportProtocolState(ProtocolParameters.SampleIndex,string.Format("Starting Protocol {0}({1})",ProtocolParameters.Name,this.GetType().Name));
			if (!ProtocolParameters.StartInKill)
			{
				//Wait for grow1
				ReportProtocolState(ProtocolParameters.SampleIndex,string.Format("Starting grow1 in plate index {0}",ProtocolParameters.LicPlatePosition));
				ChangeState(new KCEPGrow1State(ProtocolParameters.Grow1Time));
				
				//Dilute sampels 1
				ReportProtocolState(ProtocolParameters.SampleIndex,string.Format("Starting Diulute 1 in plate index {0})",ProtocolParameters.LicPlatePosition));
				ChangeState(new KCEPDilut1State(ProtocolParameters.LicPlatePosition,ProtocolParameters.NumberOfSamples,ProtocolParameters.SharedResourcesFilePath));
				
				
				//Wait BG OD
				Whait4OD(2);
				
				//Dilute Exponential
				ReportProtocolState(ProtocolParameters.SampleIndex,string.Format("Starting Diulute of exponential in plate index {0}",ProtocolParameters.LicPlatePosition));
				ChangeState(new KCEPDilut2State(ProtocolParameters.LicPlatePosition,ProtocolParameters.NumberOfExpSamples,ProtocolParameters.SharedResourcesFilePath));

				//Wait BG OD
				Whait4OD(3);
			}
			
			if (ProtocolParameters.SampleIndex == 0)
			{
			//Add Amp for exponential  and start kill
			KillStartTime = DateTime.Now;
			ReportProtocolState(ProtocolParameters.SampleIndex,string.Format("Starting Kill for {0} samples in plate index {1}",ProtocolParameters.NumberOfSamples,ProtocolParameters.LicPlatePosition));
			ChangeState(new KCEPStartKillState(ProtocolParameters.LicPlatePosition,ProtocolParameters.NumberOfSamples,ProtocolParameters.NumberOfExpSamples,ProtocolParameters.AMPPosision));
			}
			
			
			while(!this.ShouldStop & (ProtocolParameters.SampleIndex < ProtocolParameters.SampleTimes.Length ))
			{				
				
				double TimeOfWait  =  ProtocolParameters.SampleTimes[ProtocolParameters.SampleIndex] - TimeFromStart().TotalMinutes;
				if (TimeOfWait>0)
				{
					
					ReportProtocolState(ProtocolParameters.SampleIndex,string.Format("start whait for {0:0.0} Minutes to {1:0.0} time point",TimeOfWait,ProtocolParameters.SampleTimes[ProtocolParameters.SampleIndex ] ));
					ChangeState(new KCEPWaitState(TimeOfWait));
				}
				
				
				int SampleEppendorfInd = 0;
				for (int i=0;i<(ProtocolParameters.NumberOfSamples);i++)
				{
					SampleEppendorfInd = LocalUtils.GetNextFreezPos(ProtocolParameters.SharedResourcesFilePath,string.Format("{0}:t{1}, S{2} - {3}",ProtocolParameters.Name, ProtocolParameters.SampleIndex , (i+1),DateTime.Now));
				}
				

				int firstSampleEppendorfInd = SampleEppendorfInd - ProtocolParameters.NumberOfSamples+1;
				
				ReportProtocolState(ProtocolParameters.SampleIndex,string.Format("Sampleling from plate {0} to Eppendorf {1} + {2} samples timestamp {3:0.0} starting at {4:0.0} Minutes",ProtocolParameters.LicPlatePosition,firstSampleEppendorfInd,ProtocolParameters.NumberOfSamples, ProtocolParameters.SampleTimes[ProtocolParameters.SampleIndex] ,TimeFromStart().TotalMinutes));
				ChangeState(new KCEPSampleState(ProtocolParameters.LicPlatePosition,ProtocolParameters.NumberOfSamples,firstSampleEppendorfInd));
				
				
				ReportProtocolState(ProtocolParameters.SampleIndex,string.Format("end Sampleling at {0:0.0} Minutes",TimeFromStart().TotalMinutes));
				
				
				ProtocolParameters.SampleIndex++;
				
			}
		}
		
		private void Whait4OD(int Row)
		{
			double[] BackroundOD ;
			ReportProtocolState(ProtocolParameters.SampleIndex,string.Format("read Backround from plate {0} row {1}",ProtocolParameters.LicPlatePosition,Row));
			
			KCEPReadBackroundState ReadBackroundState = new KCEPReadBackroundState(ProtocolParameters.LicPlatePosition,Row,ProtocolParameters.NumberOfExpSamples);
			ChangeState(ReadBackroundState);
			
			BackroundOD = ReadBackroundState.BackroundOD;
			ReportProtocolState(ProtocolParameters.SampleIndex,string.Format("End reading Backround from plate {0} row,{1} ODs={2}",ProtocolParameters.LicPlatePosition,Row ,string.Join(",", BackroundOD)));
			
			//Wait for the first exponential well to reach Dilution Net OD
			
			double[] NetOD = new double[ProtocolParameters.NumberOfExpSamples];
			//Wait for the first exponential well to reach Dilution Net OD
			ReportProtocolState(ProtocolParameters.SampleIndex,string.Format("wait for first OD read for plate {0} Row {1}, {2:0.00} Hours",ProtocolParameters.LicPlatePosition ,Row,ProtocolParameters.Time4TheFirstODTest));
			ChangeState(new KCEPWait2ODReadState(ProtocolParameters.Time4TheFirstODTest));
			do
			{
				ReportProtocolState(ProtocolParameters.SampleIndex,string.Format("read OD from plate {0}",ProtocolParameters.LicPlatePosition));
				KCEPReadODState ReadODState = new KCEPReadODState(ProtocolParameters.LicPlatePosition,Row,ProtocolParameters.NumberOfExpSamples);
				ChangeState(ReadODState);
				
				double[] OD = ReadODState.OD;
				for(int i=0;i<OD.Length;i++)
				{
					NetOD[i] = OD[i]-BackroundOD[i];
				}
				
				ReportProtocolState(ProtocolParameters.SampleIndex,string.Format("End reading OD from plate {0} Row {1} Net OD={2} (OD={3})",ProtocolParameters.LicPlatePosition ,Row,string.Join(",", NetOD),string.Join(",", OD)));

				if(NetOD.Max()<=ProtocolParameters.NetODtoDilute)
				{
					double TimeTillNextRead = GetTimeTillNextRead(NetOD.Max(),ProtocolParameters.NetODtoDilute);
					ReportProtocolState(ProtocolParameters.SampleIndex,string.Format("wait for OD read for plate {0} Row {1}, {2:0.00} min",ProtocolParameters.LicPlatePosition ,Row,TimeTillNextRead));
					ChangeState(new KCEPWait2ODReadState(TimeTillNextRead/60));
				}
			}
			while(NetOD.Max()<ProtocolParameters.NetODtoDilute && !this.ShouldStop);
			
		}
		
		FileInfo ProtocolStateFile;
		
		private double GetTimeTillNextRead(double NetOD,double TargetOD)
		{
			double TimeTillNextRead = -(ProtocolParameters.MaxTimeBetweenODreads - ProtocolParameters.MinTimeBetweenODreads)/(ProtocolParameters.NetODtoDilute)*NetOD + ProtocolParameters.MaxTimeBetweenODreads;
			return TimeTillNextRead;
			
		}

		private TimeSpan TimeFromStart()
		{
			return (DateTime.Now - KillStartTime);
		}
		
		
		public void ReportProtocolState(int SampleID,string Messege)
		{
			string LogMessege = string.Format("({0}):{1}",SampleID,Messege);
			
			try
			{
				using (StreamWriter sw = ProtocolStateFile.AppendText())
				{
					sw.WriteLine("({0}){1}:\t{2}",SampleID,DateTime.Now,Messege);
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
				typeof(KCEPGrow1State),
				typeof(KCEPDilut1State),
				typeof(KCEPDilut2State),
				typeof(KCEPStartKillState),
				typeof(KCEPSampleState),
				typeof(KCEPWaitState),
				typeof(KCEPReadBackroundState),
				typeof(KCEPWait2ODReadState),
				typeof(KCEPReadODState)
			};
		}
		#endregion
		
	}
}
