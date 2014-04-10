/*
 * Created by SharpDevelop.
 * User: oferfrid
 * Date: 04/04/2012
 * Time: 15:59
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using OctoTip.Lib.ExperimentsCore.Attributes;
using OctoTip.Lib.ExperimentsCore.Base;

namespace CyclingInAmp
{
	/// <summary>
	/// Description of CAProtocolParameters.
	/// </summary>
	public class CAProtocolParameters:ProtocolParameters
	{
		public CAProtocolParameters()
		{
		}	
		[ProtocolParameterAtribute("Run Start","true",true)]
		public bool RunStart;
		[ProtocolParameterAtribute("Indexes in Liconic","1,2,3",true)]
		public int[] LicInds;
		[ProtocolParameterAtribute("Kill Time (Hours)","4",true)]
		public double KillTime;
		[ProtocolParameterAtribute("Frequency of reading (min)","10",true)]
		public double ReadFrequency;
		[ProtocolParameterAtribute("Absolut OD 2 Dilut","0.1",true)]
		public double AbsolutOD2Dilut;
		[ProtocolParameterAtribute("Time To ON after dilution (Hours)","5",true)]
		public double Time2ON;
		[ProtocolParameterAtribute("384 Plate Empty Ind file",@"D:\OctoTip\Protocols\CyclingInAmp\Scripts\384WellInd.csv")]
		public string Empty384WellIndFilePath;
		[ProtocolParameterAtribute("Log file path",@"D:\OctoTip\Protocols\CyclingInAmp\Output\")]
		public string OutputFilePath;
		[ProtocolParameterAtribute("Cycle Index","0",true)]
		public int CycleInd;
		[ProtocolParameterAtribute("Plate Ind","1",true)]
		public int PlateInd;
		[ProtocolParameterAtribute("Freezing Index","1",true)]
		public int FreezeInd;
	}
	
		public override bool IsValid()
		{
			//TODO:Really test 4 validity!
			return true;
		}
		
		public override string GetErrorMessege()
		{
			return string.Empty;
		}
}
