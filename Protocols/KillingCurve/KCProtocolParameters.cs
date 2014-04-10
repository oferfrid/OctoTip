/*
 * Created by SharpDevelop.
 * User: Irit
 * Date: 09/01/12
 * Time: 5:41 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using OctoTip.Lib.ExperimentsCore.Attributes;
using OctoTip.Lib.ExperimentsCore.Base;

namespace KillingCurve
{
	/// <summary>
	/// Description of KCProtocolParameters.
	/// </summary>
	public class KCProtocolParameters:ProtocolParameters
	{
		[ProtocolParameterAtribute("Index in liconic of the culture 6 wells plate","1")]
		public int CultureLicInd;
		[ProtocolParameterAtribute("Culture Eppendorf","1")]
		public int CultureEppendorfInd;
		[ProtocolParameterAtribute("Perform inoculation","true")]
		public bool PerformInoc;
		[ProtocolParameterAtribute("Culture well","1")]
		public int WellInd;
		[ProtocolParameterAtribute("First MPN plate index ","2")]
		public int MPNLicInd;
		[ProtocolParameterAtribute("Sampling Times (hr)","0, 0.5, 1")]
		public double[] SamplingTimesArray;
		[ProtocolParameterAtribute("Delay in MPN plate reading (hr)","12")]
		public double ReadAfter;
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
}
