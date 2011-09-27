/*
 * Created by SharpDevelop.
 * User: oferfrid
 * Date: 26/09/2011
 * Time: 20:19
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;

using OctoTip.OctoTipExperiments.Interfaces;

namespace OctoTip.OctoTipExperiments.Base
{
	/// <summary>
	/// Description of Protocol.
	/// </summary>
	public abstract class Protocol:IProtocol
	{
		
		public abstract void Start();
	}
}
