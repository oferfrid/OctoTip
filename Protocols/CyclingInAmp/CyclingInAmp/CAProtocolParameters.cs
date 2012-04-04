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
		[ProtocolParameterAtribute("Index in Liconic","17",true)]
		public int LicInd;
		[ProtocolParameterAtribute("Total Growth time (hr)","24",true)]
		public double TotalTime;
		[ProtocolParameterAtribute("Frequency of reading (min)","2",true)]
		public double ReadFrequency;
		[ProtocolParameterAtribute("Results file path and name",@"D:\OctoTip\Protocols\IncubateRead\Output\Out.csv")]
		public string OutputFile;
		
		
	}
}
