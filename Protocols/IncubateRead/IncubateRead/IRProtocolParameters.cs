/*
 * Created by SharpDevelop.
 * User: Irit
 * Date: 18/12/11
 * Time: 11:37 AM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using OctoTip.Lib.ExperimentsCore.Attributes;
using OctoTip.Lib.ExperimentsCore.Base;

namespace IncubateRead
{
	/// <summary>
	/// Description of IRProtocolParameters.
	/// </summary>
	public class IRProtocolParameters:ProtocolParameters
	{
		[ProtocolParameterAtribute("Index in Liconic","17",true)]
		public int LicInd;
		[ProtocolParameterAtribute("Total Growth time (hr)","24",true)]
		public double TotalTime;
		[ProtocolParameterAtribute("Frequency of reading (min)","2",true)]
		public double ReadFrequency;
		[ProtocolParameterAtribute("Start in round ","1",true)]
		public int StartRound;
		[ProtocolParameterAtribute("Results file path and name",@"D:\OctoTip\Protocols\IncubateRead\Output\Out.csv")]
		public string OutputFile;
		
		public override bool IsValid()
		{
			return true;
		}
		
		public override string GetErrorMessage()
		{
			return String.Empty;
		}
	}
}
