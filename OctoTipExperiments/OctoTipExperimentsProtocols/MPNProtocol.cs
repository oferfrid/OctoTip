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
	[Protocol("Preform an MPN Evaluarion Of coulture")]
	public class MPNProtocol:Protocol
	{
		
		#region static
		public static new List<Type> ProtocolStates()
		{
			return new List<Type>{ typeof(WaitState),typeof(IncubateState)};
		}
		#endregion
		
		int Cycle =0;
		
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
			
			while(Cycle<MPNProtocolParameters.WaitTimes.Length)
			{
				this.ChangeState( new WaitState(this,MPNProtocolParameters.WaitTimes[Cycle]));
				CurentState.DoWork();
				Cycle++;
				if (!(Cycle<MPNProtocolParameters.WaitTimes.Length))
				{
					break;
				}
				this.ChangeState( new IncubateState(this,MPNProtocolParameters.WaitTimes[Cycle]));
				CurentState.DoWork();
				Cycle++;
			}
			
			
			
		}
		
		
		
		protected MPNProtocolParameters MPNProtocolParameters
		{
			get{return (MPNProtocolParameters)this.ProtocolParameters;}
		}
	}
}
