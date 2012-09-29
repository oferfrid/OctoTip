/*
 * Created by SharpDevelop.
 * User: Tecan
 * Date: 20/09/2012
 * Time: 15:43
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;
using System.IO;
using OctoTip.Lib.ExperimentsCore.Attributes;
using OctoTip.Lib.ExperimentsCore.Base;

namespace CalibrateL1210Growth
{
	/// <summary>
	/// Description of CLGProtocol.
	/// </summary>
	[Protocol("L1210Cal","Sivan","Calibrate L1210 Growth")]
	public class CLGProtocol:Protocol
	{
		FileInfo ProtocolStateFile;
		
		public new CLGProtocolParameters ProtocolParameters
		{
			get{return (CLGProtocolParameters) base.ProtocolParameters;}
			set{base.ProtocolParameters = value;}
		}
		
		
		public CLGProtocol(CLGProtocolParameters ProtocolParameters):base((ProtocolParameters)ProtocolParameters)
		{
			ProtocolStateFile = new FileInfo(ProtocolParameters.OutputFilePath + ProtocolParameters.Name + "_" + DateTime.Now.ToString("yyyyMMddHHmm") +".txt");
			ReportProtocolState(0,0,string.Format("Creating Protoclo {0} ({1}), using parameters: \n{2}",ProtocolParameters.Name,this.GetType().Name,ProtocolParameters.ToString()));
			
		}
		
		protected override void DoWork()
		{
			
			ReportProtocolState(0,0,string.Format("Starting Protocol {0}({1})",ProtocolParameters.Name,this.GetType().Name));
			ReportProtocolState(0,0,string.Format("Starting first read"));
			this.ChangeState(new CLGReadState());
			
			for(int SampleIndex = 0;SampleIndex<ProtocolParameters.NumOfSamples;SampleIndex++)
			{
				ReportProtocolState(SampleIndex,0,string.Format("Starting sample in plate {0}",ProtocolParameters.Liconic96Plate));
				this.ChangeState(new CLGSampleState(ProtocolParameters.Liconic96Plate++));
				for (int ReadIndex=0;ReadIndex<ProtocolParameters.NumReadsBetweenSamples;ReadIndex++)
				{
					ReportProtocolState(SampleIndex,ReadIndex,string.Format("Starting Read"));
					this.ChangeState(new CLGReadState());
					ReportProtocolState(SampleIndex,ReadIndex,string.Format("Starting Incubate for {0:0.00} minutes",ProtocolParameters.ReadFrequency));
					this.ChangeState(new CLGIncubateState(ProtocolParameters.ReadFrequency));
				}
				
			}	
			
		}
		
		public void ReportProtocolState(int SampleID,int ReadID,string Messege)
		{
			using (StreamWriter sw = ProtocolStateFile.AppendText())
			{
				sw.WriteLine("({0}-{1}){2}:\t{3}",SampleID,ReadID,DateTime.Now,Messege);
				sw.Flush();
			}
			DisplayData(Messege);
			myProtocolLogger.Add(Messege);
		}
		
		
		
		#region static
		
		
		public static new List<Type> ProtocolStates()
		{
			return new List<Type>{
				typeof(CLGIncubateState),
				typeof(CLGReadState),
				typeof(CLGSampleState)
			};
		}
		#endregion
	}
}
