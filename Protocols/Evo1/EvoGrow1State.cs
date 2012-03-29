/*
 * Created by SharpDevelop.
 * User: Tecan
 * Date: 18/10/2011
 * Time: 16:15
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
	/// Description of Grow1State.
	/// </summary>
	[State("Grow 1","Grow After b-Lac")]
	public class EvoGrow1State:WaitState
	{
		#region static
		public static new List<Type> NextStates()
		{
			return new List<Type>{typeof(EvoGrow1ReadState)};
		}
		#endregion
		EvoProtocol RunningInEvoProtocol;
		public EvoGrow1State(EvoProtocol RunningInEvoProtocol):base((Protocol)RunningInEvoProtocol,DateTime.Now.AddMinutes( RunningInEvoProtocol.EvoProtocolParameters.TimeBetweenReads))
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
