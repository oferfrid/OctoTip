/*
 * Created by SharpDevelop.
 * User: Tecan
 * Date: 18/10/2011
 * Time: 16:48
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using OctoTip.OctoTipExperiments.Core.Attributes;
using OctoTip.OctoTipExperiments.Core.Base;

namespace Evo1
{
	/// <summary>
	/// Description of EvoProtocolParameters.
	/// </summary>
	public class EvoProtocolParameters:ProtocolParameters
	{
		[ProtocolParameterAtribute("Amp Eppendorf Position Index")]
		public int AmpEppendorfInd;
		
	}
}
