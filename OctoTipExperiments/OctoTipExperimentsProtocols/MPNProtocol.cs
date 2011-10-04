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
	[ProtocolAttribute("Preform an MPN Evaluarion Of coulture")]
	public class MPNProtocol:Protocol
	{
		
		#region static
		public static new List<Type> ProtocolStates()
		{
			return new List<Type>{ typeof(WaitState)};
		}
		#endregion
		
		
		public MPNProtocol():base()
		{
			
		}
		
		public MPNProtocol(MPNProtocolParameters Parameters):base((ProtocolParameters)Parameters)
		{
			
		}
		
		public override void OnStatusChanged(ProtocolStatusChangeEventArgs e)
		{
			// Do any specific processing here.

			// Call the base class event invocation method.
			base.OnStatusChanged(e);
		}
		
		protected override void OnProtocolStart()
		{
			//throw new NotImplementedException();
			this.CurentState = new WaitState(this,0);
		}
		
		protected override void OnProtocolEnd()
		{
			//throw new NotImplementedException();
			this.CurentState = null;
		}
	}
}
