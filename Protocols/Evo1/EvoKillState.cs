/*
 * Created by SharpDevelop.
 * User: Tecan
 * Date: 18/10/2011
 * Time: 16:13
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;
using OctoTip.Lib.ExperimentsCore.Attributes;
using OctoTip.Lib.ExperimentsCore.Base;


namespace Evo1
{
	/// <summary>
	/// Description of EvoKillState.
	/// </summary>
	[State("Kill","Kill In AMP")]
	public class EvoKillState:WaitState
	{
		#region static
		public static new List<Type> NextStates()
		{
			return new List<Type>{typeof(EvoKillReadState),typeof(EvoAddbLacState)};
		}
		#endregion
		EvoProtocol RunningInEvoProtocol;
		public EvoKillState(EvoProtocol RunningInEvoProtocol,DateTime WaitUntil):base((Protocol)RunningInEvoProtocol , WaitUntil)
		{
			this.RunningInEvoProtocol = RunningInEvoProtocol;
		}
		
		protected override void OnWaitStart()
		{
			//throw new NotImplementedException();
		}
		
		protected override void OnWaitEnd()
		{
			//throw new NotImplementedException();
		}
	}
}
