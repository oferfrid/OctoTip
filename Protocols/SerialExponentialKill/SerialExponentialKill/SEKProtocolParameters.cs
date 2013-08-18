/*
 * Created by SharpDevelop.
 * User: oferfrid
 * Date: 01/11/2012
 * Time: 16:22
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using OctoTip.Lib.ExperimentsCore.Attributes;
using OctoTip.Lib.ExperimentsCore.Base;

namespace SerialExponentialKill
{
	/// <summary>
	/// Description of SEKProtocolParameters.
	/// </summary>
	public class SEKProtocolParameters:ProtocolParameters
	{
		public SEKProtocolParameters()
		{
		}
		[ProtocolParameterAtribute("Cycle","0",true)]
		public int Cycle;
		[ProtocolParameterAtribute("Liconic curent plate Index","1",true)]
		public int LicPlatePosition;
		[ProtocolParameterAtribute("Liconic next plates Indexes","2,3",true)]
		public int[] LicPlatePositions;
		[ProtocolParameterAtribute("Curent well","1",true)]
		public int CurentWell;
		[ProtocolParameterAtribute("Wells to freeze","2,10,24",true)]
		public int[] FreezeWells;
		[ProtocolParameterAtribute("Time Till the first OD (Hours)","1",true)]
		public double Time4TheFirstODTest;
		[ProtocolParameterAtribute("Min Time between OD reads (min)","5",true)]
		public double MinTimeBetweenODreads;
		[ProtocolParameterAtribute("Max Time between OD reads (min)","30",true)]
		public double MaxTimeBetweenODreads;
		[ProtocolParameterAtribute("AMP Position","23",true)]
		public int AMPPosition;
		[ProtocolParameterAtribute("Killing Time (Hours)","2",true)]
		public double KillTime;
		[ProtocolParameterAtribute("b-Lac Position","23",true)]
		public int BlacPosition;
		[ProtocolParameterAtribute("Net OD to dilute","0.06",true)]
		public double NetODtoDilute;		
		[ProtocolParameterAtribute("Log file path",@"D:\OctoTip\Protocols\SerialExponentialKill\Output\")]
		public string OutputFilePath;
		[ProtocolParameterAtribute("Shared Resources file path",@"D:\OctoTip\Protocols\SerialExponentialKill\SharedResources\")]
		public string SharedResourcesFilePath;
		
	}
}
