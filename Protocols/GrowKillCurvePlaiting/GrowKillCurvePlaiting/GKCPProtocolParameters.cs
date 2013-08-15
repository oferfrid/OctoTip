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

namespace GrowKillCurvePlaiting
{
	/// <summary>
	/// Description of GKCPProtocolParameters.
	/// </summary>
	public class GKCPProtocolParameters:ProtocolParameters
	{		public GKCPProtocolParameters()
		{
		}
		[ProtocolParameterAtribute("Sample Position in Liconic","34",true)]
		public int LicPlatePosition;
		
		[ProtocolParameterAtribute("Number of samples","3",true)]
		public int NumberOfSamples;
		
		[ProtocolParameterAtribute("Grow 1 (Hr)","12",true)]
		public double Grow1Time;
		
		[ProtocolParameterAtribute("Time wait for First OD after dilution (Hr)","8",true)]
		public double Time4TheFirstODTest;
		
		[ProtocolParameterAtribute("Net OD to dilute","0.7",true)]
		public double NetODtoDilute;
		[ProtocolParameterAtribute("Min Time between OD reads (min)","5",true)]
		public double MinTimeBetweenODreads;
		[ProtocolParameterAtribute("Max Time between OD reads (min)","60",true)]
		public double MaxTimeBetweenODreads;
		[ProtocolParameterAtribute("Start in kill","false",true)]
		public bool StartInKill;
		
		[ProtocolParameterAtribute("Sample times (minutes)","0,30,105,180,255,330,405,480",true)]
		public double[] SampleTimes;
		[ProtocolParameterAtribute("Curent Sample Index","0",true)]
		public int SampleIndex;

		[ProtocolParameterAtribute("Log file path",@"D:\OctoTip\Protocols\GrowKillCurvePlaiting\Output\")]
		public string OutputFilePath;
		[ProtocolParameterAtribute("AMP Posision in block","24",true)]
		public int AMPPosision;
		
		[ProtocolParameterAtribute("Shared Resources file path",@"D:\OctoTip\Protocols\GrowKillCurvePlaiting\SharedResources\")]
		public string SharedResourcesFilePath;
		
		
	}
}
