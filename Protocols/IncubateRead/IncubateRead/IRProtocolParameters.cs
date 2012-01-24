/*
 * Created by SharpDevelop.
 * User: Irit
 * Date: 18/12/11
 * Time: 11:37 AM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using OctoTip.OctoTipExperiments.Core.Attributes;
using OctoTip.OctoTipExperiments.Core.Base;

namespace IncubateRead
{
	/// <summary>
	/// Description of IRProtocolParameters.
	/// </summary>
	public class IRProtocolParameters:ProtocolParameters
	{
		[ProtocolParameterAtribute("Index in Liconic","1")]
		public int LicInd;
		[ProtocolParameterAtribute("Total Growth time (hr)","24")]
		public double TotalTime;
		[ProtocolParameterAtribute("Frequency of reading (min)","10")]
		public double ReadFrequency;
		[ProtocolParameterAtribute("Results file path and name",@"c:\")]
		public string Path;
	}
}
