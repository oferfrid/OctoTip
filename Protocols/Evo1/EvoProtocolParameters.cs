/*
 * Created by SharpDevelop.
 * User: Tecan
 * Date: 18/10/2011
 * Time: 16:48
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;

using OctoTip.Lib.ExperimentsCore.Attributes;
using OctoTip.Lib.ExperimentsCore.Base;



namespace Evo1
{
	/// <summary>
	/// Description of EvoProtocolParameters.
	/// </summary>
	public class EvoProtocolParameters:ProtocolParameters
	{
		[ProtocolParameterAtribute("AMP Eppendorf Position Index","24")]
		public int AMPEppendorfInd;
		[ProtocolParameterAtribute("b-Lac Eppendorf Position Index","23")]
		public int bLacEppendorfInd;
		[ProtocolParameterAtribute("Freez Eppendorf Position Indexes","1,2,3,4")]
		public int[] FreezAmpEppendorfInds;
		[ProtocolParameterAtribute("Time between reads (minuts)","15")]
		public double TimeBetweenReads;
		[ProtocolParameterAtribute("Killing Time (minuts)","400")]
		public int KillingTime;
		[ProtocolParameterAtribute("First Plate Position index [1 34]","1")]
		public int FirstPlateInd;
		[ProtocolParameterAtribute("Net OD For first dilution","0.05")]
		public double NetODFirstDilution;
		[ProtocolParameterAtribute("Net OD For Amp dilution","0.1")]
		public double NetODAMPDilution;
		
		
	}
}
