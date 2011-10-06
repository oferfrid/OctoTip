/*
 * Created by SharpDevelop.
 * User: oferfrid
 * Date: 04/10/2011
 * Time: 14:30
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;

using OctoTip.OctoTipExperiments.Core.Attributes;
using OctoTip.OctoTipExperiments.Core.Interfaces;

namespace OctoTip.OctoTipExperiments.Core.Base
{
	/// <summary>
	/// Description of ProtocolParameters.
	/// </summary>
	public abstract class ProtocolParameters:IProtocolParameters
	{
		[ProtocolParameterAtribute("Name")]
		public string Name;
	}
}
