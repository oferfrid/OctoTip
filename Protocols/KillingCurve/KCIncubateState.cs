/*
 * Created by SharpDevelop.
 * User: Irit
 * Date: 12/01/12
 * Time: 12:20 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;
using OctoTip.Lib.ExperimentsCore.Attributes;
using OctoTip.Lib.ExperimentsCore.Base;

namespace KillingCurve
{
	/// <summary>
	/// Description of KCIncubateState.
	/// </summary>
	[State("Incubate cultures","Incubates culture and MPN plates")]
	public class KCIncubateState:WaitState
	{
		new KCProtocol RunningInProtocol
		{
			get
			{return (KCProtocol) base.RunningInProtocol;}
		}
		
		public KCIncubateState(KCProtocol RunningInProtocol,DateTime NextTaskTime)
			                  :base((Protocol)RunningInProtocol, NextTaskTime)
		{
		}
		
		protected override void OnWaitStart()
		{
//			throw new NotImplementedException();
		}
		
		protected override void OnWaitEnd()
		{
//			throw new NotImplementedException();
		}
		
		#region static
		public static new List<Type> NextStates()
		{
			return new List<Type>{typeof(KCMPNState)
								 ,typeof(KCReadPlateState)};
		}
		#endregion
	}
}
