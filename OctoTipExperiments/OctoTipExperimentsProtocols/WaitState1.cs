/*
 * Created by SharpDevelop.
 * User: oferfrid
 * Date: 09/10/2011
 * Time: 14:04
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;
using OctoTip.OctoTipExperiments.Core.Attributes;
using OctoTip.OctoTipExperiments.Core.Base;

namespace OctoTip.OctoTipExperiments
{
	/// <summary>
	/// Description of WaitState1.
	/// </summary>
	[State("Wait1","WaitUntil time")]
	public class WaitState1:WaitState
	{
		public WaitState1():base()
		{
		}
		
		public WaitState1(Protocol RunningInProtocol ,DateTime WaitUntil):base( RunningInProtocol , WaitUntil)
		{
			
		}
		
		
		protected override void OnWaitStart()
		{
			this.RunningInProtocol.OnDisplayedDataChange(new ProtocolDisplayedDataChangeEventArgs("started1"));
		}
		
		protected override void OnWaitEnd()
		{
			this.RunningInProtocol.OnDisplayedDataChange(new ProtocolDisplayedDataChangeEventArgs("ended1"));
		}
		
		#region static
		public static new List<Type> NextStates()
		{
			return new List<Type>{typeof(RoboRunState1)};
		}
	#endregion
	}
}
