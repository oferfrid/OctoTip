/*
 * Created by SharpDevelop.
 * User: Tecan
 * Date: 18/10/2011
 * Time: 16:17
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;
using OctoTip.OctoTipExperiments.Core.Attributes;
using OctoTip.OctoTipExperiments.Core.Base;

namespace Evo1
{
	/// <summary>
	/// Description of EvoGrow2State.
	/// </summary>
	[State("Grow 2","Grow After Dilution")]
	public class EvoGrow2State:WaitState
	{
		#region static
		public static new List<Type> NextStates()
		{
			return new List<Type>{typeof(EvoGrow2ReadState)};
		}
		#endregion
		EvoProtocol RunningInEvoProtocol;
		public EvoGrow2State(EvoProtocol RunningInEvoProtocol):base((Protocol)RunningInEvoProtocol)
		{
			this.RunningInEvoProtocol = RunningInEvoProtocol;
		}
		
		protected override void OnWaitStart()
		{
			throw new NotImplementedException();
		}
		
		protected override void OnWaitEnd()
		{
			throw new NotImplementedException();
		}
	}
}
