/*
 * Created by SharpDevelop.
 * User: oferfrid
 * Date: 26/09/2011
 * Time: 21:21
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;

using OctoTip.OctoTipExperiments.Core.Base;
using OctoTip.OctoTipExperiments.Core.Attributes;
using OctoTip.OctoTipExperiments.Core.Interfaces;

namespace OctoTip.OctoTipExperiments.Protocols
{
	/// <summary>
	/// Description of MPNProtocol.
	/// </summary>
	[ProtocolAttribute("Preform an MIC Evaluarion Of coulture")]
	public class MICProtocol:Protocol
	{
		public MICProtocol()
		{
		}
		
		
		public static new List<Type> ProtocolStates()
		{
			return new List<Type>{typeof(WaitState), typeof(PrepareState)};
		}
		
		
		protected override void OnProtocolStart()
		{
			throw new NotImplementedException();
		}
		
		protected override void OnProtocolEnd()
		{
			throw new NotImplementedException();
		}
	}
}
