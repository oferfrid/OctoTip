﻿/*
 * Created by SharpDevelop.
 * User: oferfrid
 * Date: 26/09/2011
 * Time: 21:21
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using OctoTip.OctoTipExperiments.Base;
using OctoTip.OctoTipExperiments.Attributes;
using OctoTip.OctoTipExperiments.Interfaces;

namespace OctoTip.OctoTipExperiments.Protocols
{
	/// <summary>
	/// Description of MPNProtocol.
	/// </summary>
	[ProtocolAttribute("Preform an MPN Evaluarion Of coulture")]
	public class MPNProtocol:Protocol
	{
		public MPNProtocol()
		{
		}
		
		public override void Start()
		{
			throw new NotImplementedException();
		}
	}
}