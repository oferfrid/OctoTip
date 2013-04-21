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
		[ProtocolParameterAtribute("Sample Position in Liconic","34",true)]
		public int LicPlatePosition;
		
		[ProtocolParameterAtribute("Number of samples","4",true)]
		public int NumberOfSamples;
		
		[ProtocolParameterAtribute("Number of Exponential samples","2",true)]
		public int NumberOfExpSamples;
		
		[ProtocolParameterAtribute("Grow 1 (Hr)","12",true)]
		public double Grow1Time;
		
		[ProtocolParameterAtribute("Time wait for First OD after dilution (Hr)","4",true)]
		public double Time4TheFirstODTest;
		
		[ProtocolParameterAtribute("Net OD to dilute","0.025",true)]
		public double NetODtoDilute;
		[ProtocolParameterAtribute("Min Time between OD reads (min)","5",true)]
		public double MinTimeBetweenODreads;
		[ProtocolParameterAtribute("Max Time between OD reads (min)","30",true)]
		public double MaxTimeBetweenODreads;
		[ProtocolParameterAtribute("Start in kill","false",true)]
		public bool StartInKill;
		
		[ProtocolParameterAtribute("Sample times (minutes)","0,30,110,300,500",true)]
		public double[] SampleTimes;
		[ProtocolParameterAtribute("Curent Sample Index","0",true)]
		public int SampleIndex;

		[ProtocolParameterAtribute("Log file path",@"D:\OctoTip\Protocols\KillCurveExpPlaiting\Output\")]
		public string OutputFilePath;
		[ProtocolParameterAtribute("AMP Posision in block","24",true)]
		public int AMPPosision;
		
		[ProtocolParameterAtribute("Shared Resources file path",@"D:\OctoTip\Protocols\KillCurveExpPlaiting\SharedResources\")]
		public string SharedResourcesFilePath;
		
		
	}
}
