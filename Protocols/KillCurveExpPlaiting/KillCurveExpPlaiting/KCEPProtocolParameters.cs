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

namespace KillCurveExpPlaiting
{
	/// <summary>
	/// Description of KCEPProtocolParameters.
	/// </summary>
	public class KCEPProtocolParameters:ProtocolParameters
	{		public KCEPProtocolParameters()
		{
		}	
		[ProtocolParameterAtribute("Run Grow","true",true)]
		public bool RunGrow;
		[ProtocolParameterAtribute("Run Start","true",true)]
		public bool RunStart;
		[ProtocolParameterAtribute("Start eppendorf Index","1",true)]
		public int SampleEppendorfInd;
		[ProtocolParameterAtribute("Sample times (minutes)","10,15,20",true)]
		public double[] SampleTimes;
		[ProtocolParameterAtribute("Number Of Samples","1",true)]
		public int NumberOfSamples;
		[ProtocolParameterAtribute("Sample Position in Liconic","1",true)]
		public int Sample6IndInLiconic;
		[ProtocolParameterAtribute("ON Position in Liconic","2",true)]
		public int ON96IndInLiconic;
		[ProtocolParameterAtribute("ON start well index","1",true)]
		public int ONStartwellIndex;
		[ProtocolParameterAtribute("Start in sample","0",true)]
		public int SampleIndex;
		[ProtocolParameterAtribute("Hours 2 Grow 2 ON ","5",true)]
		public double Hours2Grow2ON;
		[ProtocolParameterAtribute("Log file path",@"D:\OctoTip\Protocols\KillCurveExpPlaiting\Output\")]
		public string OutputFilePath;
		[ProtocolParameterAtribute("AMP Posision in block","24",true)]
		public int AMPPosision;
		
		
		
		
	}
}
