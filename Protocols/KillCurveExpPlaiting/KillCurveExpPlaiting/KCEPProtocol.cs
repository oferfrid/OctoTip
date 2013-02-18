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
using OctoTip.Lib.ExperimentsCore.Attributes;
using OctoTip.Lib.ExperimentsCore.Base;

namespace KillCurveExpPlaiting
{
	/// <summary>
	/// Description of KCEPProtocol.
	/// </summary>
	[Protocol("Kill Curve Plaiting","Ofer Fridman","Kill courve by plaiting.")]
	public class KCEPProtocol:Protocol
	{
		DateTime StartTime;
		
		public new KCEPProtocolParameters ProtocolParameters
		{
			get{return (KCEPProtocolParameters) base.ProtocolParameters;}
			set{base.ProtocolParameters = value;}
		}
		
		public KCEPProtocol(KCEPProtocolParameters ProtocolParameters):base((ProtocolParameters)ProtocolParameters)
		{
			//create protocol File
			
			ProtocolStateFile = new FileInfo(ProtocolParameters.OutputFilePath + ProtocolParameters.Name + "_" + DateTime.Now.ToString("yyyyMMddHHmm") +".txt");
			ReportProtocolState(0,string.Format("Creating Protoclo {0} ({1}), using parameters: \n{2}",ProtocolParameters.Name,this.GetType().Name,ProtocolParameters.ToString()));
			
		}
		
		
		protected override void DoWork( )
		{
			ReportProtocolState(ProtocolParameters.SampleIndex,string.Format("Starting Protocol {0}({1})",ProtocolParameters.Name,this.GetType().Name));
			
			
			if(ProtocolParameters.RunGrow)
			{
				ReportProtocolState(ProtocolParameters.SampleIndex,string.Format("Starting grow in plate index {0})",ProtocolParameters.ON96IndInLiconic));
				this.ChangeState(new KCEPStartGrowState(ProtocolParameters.ON96IndInLiconic));
				ReportProtocolState(ProtocolParameters.SampleIndex,string.Format("Wating for ON ({0:0.0} Hours) in plate index {1})",ProtocolParameters.Hours2Grow2ON,ProtocolParameters.ON96IndInLiconic));
				this.ChangeState(new KCEPGrowState(ProtocolParameters.Hours2Grow2ON));
				ReportProtocolState(ProtocolParameters.SampleIndex,string.Format("End Grow in plate index {0})",ProtocolParameters.ON96IndInLiconic));
				this.ChangeState(new KCEPEndGrowState(ProtocolParameters.ON96IndInLiconic));
				
			}
			
			
			if(ProtocolParameters.RunStart)
			{
				ReportProtocolState(ProtocolParameters.SampleIndex,string.Format("Starting Kill for {0} samples in plate index {1})",ProtocolParameters.NumberOfSamples,ProtocolParameters.Sample6IndInLiconic));
				this.ChangeState(new KCEPStartKillState(ProtocolParameters.Sample6IndInLiconic,ProtocolParameters.NumberOfSamples,ProtocolParameters.ONStartwellIndex,ProtocolParameters.AMPPosision));
			}
			StartTime = DateTime.Now;
			
			while(!this.ShouldStop & ProtocolParameters.SampleIndex<=ProtocolParameters.SampleTimes.Length )
			{
				int SampleEppendorfInd = ProtocolParameters.SampleEppendorfInd;
				ProtocolParameters.SampleEppendorfInd += ProtocolParameters.NumberOfSamples;
				
				
				if (0==ProtocolParameters.SampleIndex)
				{
					ReportProtocolState(ProtocolParameters.SampleIndex,string.Format("Sampleling from plate {0} to Eppendorf {1} + {2} samples timestamp {3:0.0} starting at {4:0.0} Minutes",ProtocolParameters.Sample6IndInLiconic,SampleEppendorfInd,ProtocolParameters.NumberOfSamples, 0 ,TimeFromStart().TotalMinutes));
					ChangeState(new KCEPSampleState(ProtocolParameters.Sample6IndInLiconic,SampleEppendorfInd,ProtocolParameters.NumberOfSamples,true));
				}
				else
				{
					ReportProtocolState(ProtocolParameters.SampleIndex,string.Format("Sampleling from plate {0} to Eppendorf {1} + {2} samples timestamp {3:0.0} starting at {4:0.0} Minutes",ProtocolParameters.Sample6IndInLiconic,SampleEppendorfInd,ProtocolParameters.NumberOfSamples, ProtocolParameters.SampleTimes[ProtocolParameters.SampleIndex - 1] ,TimeFromStart().TotalMinutes));
					ChangeState(new KCEPSampleState(ProtocolParameters.Sample6IndInLiconic,SampleEppendorfInd,ProtocolParameters.NumberOfSamples,false));
				}
				
				ReportProtocolState(ProtocolParameters.SampleIndex,string.Format("end Sampleling at {0:0.0} Minutes",TimeFromStart().TotalMinutes));
				
				double TimeOfWait  =  ProtocolParameters.SampleTimes[ProtocolParameters.SampleIndex] - TimeFromStart().TotalMinutes;
				if (TimeOfWait>0)
				{
					
					ReportProtocolState(ProtocolParameters.SampleIndex,string.Format("start whait for {0:0.0} Minutes to {1:0.0} time point",TimeOfWait,ProtocolParameters.SampleTimes[ProtocolParameters.SampleIndex ] ));
					ChangeState(new KCEPWaitState(TimeOfWait));
				}
				ProtocolParameters.SampleIndex++;
				
			}
		}
		
		FileInfo ProtocolStateFile;
		
		private TimeSpan TimeFromStart()
		{
			return (DateTime.Now - StartTime);
		}
		
		
		public void ReportProtocolState(int SampleID,string Messege)
		{
			using (StreamWriter sw = ProtocolStateFile.AppendText())
			{
				sw.WriteLine("({0}){1}:\t{2}",SampleID,DateTime.Now,Messege);
				sw.Flush();
			}
			DisplayData(Messege);
			myProtocolLogger.Add(Messege);
		}
		
		#region static
		
		
		public static new List<Type> ProtocolStates()
		{
			return new List<Type>{
				typeof(KCEPStartKillState),
				typeof(KCEPSampleState),
				typeof(KCEPWaitState),
				typeof(KCEPStartGrowState),
				typeof(KCEPEndGrowState),
				typeof(KCEPGrowState)
					
			};
		}
		#endregion
		
	}
}
