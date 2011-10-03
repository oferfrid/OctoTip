/*
 * Created by SharpDevelop.
 * User: oferfrid
 * Date: 02/10/2011
 * Time: 14:28
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using OctoTip.OctoTipExperiments.Core.Attributes;
using OctoTip.OctoTipExperiments.Core.Interfaces;

namespace OctoTip.OctoTipExperiments.Protocols
{
	/// <summary>
	/// Description of MPNProtocolParameters.
	/// </summary>
	
	public struct MPNProtocolParameters :IProtocolParameters
	{
		[ProtocolParameterAtribute("Wait Time")]
		public int UpdateTime;
		[ProtocolParameterAtribute("Name")]
		public string Name;
		[ProtocolParameterAtribute("Sampling Time array")]
		public double[] SampleTime;
	}
}
