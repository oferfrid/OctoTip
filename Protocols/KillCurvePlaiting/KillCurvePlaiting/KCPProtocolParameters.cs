/*
 * Created by SharpDevelop.
 * User: oferfrid
 * Date: 06/05/2012
 * Time: 12:15
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using OctoTip.Lib.ExperimentsCore.Attributes;
using OctoTip.Lib.ExperimentsCore.Base;

namespace KillCurvePlaiting
{
	/// <summary>
	/// Description of KCPProtocolParameters.
	/// </summary>
	public class KCPProtocolParameters:ProtocolParameters
	{		public KCPProtocolParameters()
		{
		}	
		[ProtocolParameterAtribute("Run Start","true",true)]
		public bool RunStart;
		[ProtocolParameterAtribute("Start eppendorf Index","1",true)]
		public int SampleEppendorfInd;
		[ProtocolParameterAtribute("Sample times (minutes)","4",true)]
		public double[] SampleTimes;
		[ProtocolParameterAtribute("Number Of Samples","1",true)]
		public int NumberOfSamples;
		[ProtocolParameterAtribute("Sample Position in Liconic","1",true)]
		public int Plate6Ind;
		[ProtocolParameterAtribute("Start in sample","0",true)]
		public int SampleIndex;
		[ProtocolParameterAtribute("Log file path",@"D:\OctoTip\Protocols\CyclingInAmp\Output\")]
		public string OutputFilePath;
		
		
	}
}
