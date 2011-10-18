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
	/// Description of WaitState2.
	/// </summary>
	[State("Wait2","WaitTime time")]
	public class WaitState2:WaitState
	{
		public WaitState2():base()
		{
		}
		
		public WaitState2(Protocol RunningInProtocol ,TimeSpan WaitTime):base( RunningInProtocol , 	WaitTime)
		{
			
			
		}
		
		
		protected override void OnWaitStart()
		{
			this.RunningInProtocol.OnDisplayedDataChange(new ProtocolDisplayedDataChangeEventArgs("started2"));
		}
		
		protected override void OnWaitEnd()
		{
			this.RunningInProtocol.OnDisplayedDataChange(new ProtocolDisplayedDataChangeEventArgs("ended2"));
		}
		
		#region  static
		public static new List<Type> NextStates()
		{
			return new List<Type>{typeof(RoboRunState1)};
		}
		#endregion
	}
}
