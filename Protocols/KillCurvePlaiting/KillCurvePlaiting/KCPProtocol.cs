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

namespace KillCurvePlaiting
{
	/// <summary>
	/// Description of KCPProtocol.
	/// </summary>
	[Protocol("Cyclic AMP","Ofer Fridman","Cyclic Evolution In AMP with b-lac")]
	public class KCPProtocol:Protocol
	{
		DateTime StartTime;
		
		public new KCPProtocolParameters ProtocolParameters
		{
			get{return (KCPProtocolParameters) base.ProtocolParameters;}
			set{base.ProtocolParameters = value;}
		}
		
		public KCPProtocol(KCPProtocolParameters ProtocolParameters):base((ProtocolParameters)ProtocolParameters)
		{
			//create protocol File
			
			ProtocolStateFile = new FileInfo(ProtocolParameters.OutputFilePath + ProtocolParameters.Name + "_" + DateTime.Now.ToString("yyyyMMddHHmm") +".txt");
			ReportProtocolState(0,string.Format("Creating Protoclo {0} ({1}), using parameters: \n{2}",ProtocolParameters.Name,this.GetType().Name,ProtocolParameters.ToString()));
			
		}
		
		
		protected override void DoWork( )
		{
			ReportProtocolState(ProtocolParameters.SampleIndex,string.Format("Starting Protocol {0}({1})",ProtocolParameters.Name,this.GetType().Name));
			if(ProtocolParameters.RunStart)
			{
				
				this.ChangeState(new KCPStartKillState(ProtocolParameters.Plate6Ind,ProtocolParameters.NumberOfSamples));
			}
			StartTime = DateTime.Now;
			

			bool IsFirst = true;
			
			while(!this.ShouldStop & ProtocolParameters.SampleIndex>ProtocolParameters.SampleTimes.Length )
			{
				ProtocolParameters.SampleIndex++;
				
				int SampleEppendorfInd = ProtocolParameters.SampleEppendorfInd;
				ProtocolParameters.SampleEppendorfInd += ProtocolParameters.NumberOfSamples;
				
				
				
				ReportProtocolState(ProtocolParameters.SampleIndex,string.Format("Sampleling from plate {0} to Eppendorf {1} + {2} samples time stemp {3:0.0} starting at {4:0.0} Minuts",ProtocolParameters.Plate6Ind,SampleEppendorfInd,ProtocolParameters.NumberOfSamples, ProtocolParameters.SampleTimes[ProtocolParameters.SampleIndex - 1] ,TimeFromStart().TotalMinutes));
				ChangeState(new KCPSampleState(ProtocolParameters.Plate6Ind,SampleEppendorfInd,ProtocolParameters.NumberOfSamples,IsFirst));
				IsFirst = false;
				ReportProtocolState(ProtocolParameters.SampleIndex,string.Format("end Sampleling at {0:0.0} Minuts",TimeFromStart().TotalMinutes));
				
				
				double TimeOfWait  =  ProtocolParameters.SampleTimes[ProtocolParameters.SampleIndex - 1] - TimeFromStart().TotalMinutes;
				if (TimeOfWait>0)
				{
					
				ReportProtocolState(ProtocolParameters.SampleIndex,string.Format("start whait for {0:0.0} Minuts to {1:0.0} time point",TimeOfWait,ProtocolParameters.SampleTimes[ProtocolParameters.SampleIndex] ));
				ChangeState(new KCPWaitState(TimeOfWait));
				}
				
				
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
				typeof(KCPStartKillState),
				typeof(KCPSampleState),
				typeof(KCPWaitState)
				
			};
		}
		#endregion
		
	}
}
