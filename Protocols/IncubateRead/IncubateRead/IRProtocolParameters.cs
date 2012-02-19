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
		[ProtocolParameterAtribute("Index in Liconic","1",true)]
		public int LicInd;
		[ProtocolParameterAtribute("Total Growth time (hr)","24",true)]
		public double TotalTime;
		[ProtocolParameterAtribute("Frequency of reading (min)","10",true)]
		public double ReadFrequency;
		[ProtocolParameterAtribute("Plate type","6",true)]
		public int PlateType;
		[ProtocolParameterAtribute("364 Plate Position Index [1:3]","1",true)]
		public int Plate364PlatePositionIndex;
//		[ProtocolParameterAtribute("Results file path and name",@"c:\")]
//		public string Path;
	}
}
