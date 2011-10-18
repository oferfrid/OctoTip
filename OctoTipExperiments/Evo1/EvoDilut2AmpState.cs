/*
 * Created by SharpDevelop.
 * User: Tecan
 * Date: 18/10/2011
 * Time: 16:18
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
	/// Description of EvoDilut2AmpState.
	/// </summary>
	[State("2AMP","Dilut to AMP")]
	public class EvoDilut2AmpState:RunRobotState
	{
		#region static 
		
		public static new List<Type> NextStates()
		{
			return new List<Type>{typeof(EvoKillReadState)};
		}
		#endregion
		
		EvoProtocol RunningInEvoProtocol;
		public EvoDilut2AmpState(EvoProtocol RunningInEvoProtocol):base((Protocol)RunningInEvoProtocol)
		{
			this.RunningInEvoProtocol = RunningInEvoProtocol;
		}
		
		protected override OctoTip.OctoTipLib.RobotJob BeforeRobotRun()
		{
			throw new NotImplementedException();
		}
		
		protected override void AfterRobotRun()
		{
			throw new NotImplementedException();
		}
	}
}
