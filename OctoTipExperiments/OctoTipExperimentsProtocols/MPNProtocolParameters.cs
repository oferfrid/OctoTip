﻿/*
 * Created by SharpDevelop.
 * User: oferfrid
 * Date: 02/10/2011
 * Time: 14:28
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using OctoTip.OctoTipExperiments.Core.Attributes;
using OctoTip.OctoTipExperiments.Core.Base;

namespace OctoTip.OctoTipExperiments.Protocols
{
	/// <summary>
	/// Description of MPNProtocolParameters.
	/// </summary>
	
	public class MPNProtocolParameters :ProtocolParameters
	{
		[ProtocolParameterAtribute("wait per cycle (minuts)")]
		public double[] WaitTimes;
	}
}