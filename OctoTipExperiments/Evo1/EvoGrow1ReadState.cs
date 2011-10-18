/*
 * Created by SharpDevelop.
 * User: Tecan
 * Date: 18/10/2011
 * Time: 16:16
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
	/// Description of EvoGrow1ReadState.
	/// </summary>
	[State("Grow 1 Read","Dilut to AMP")]
	public class EvoGrow1ReadState:ReadState
	{
		#region static
		public static new List<Type> NextStates()
		{
			return new List<Type>{typeof(EvoGrow1State),typeof(EvoDilutState)};
		}
		#endregion
		EvoProtocol RunningInEvoProtocol;
		public EvoGrow1ReadState(EvoProtocol RunningInEvoProtocol):base((Protocol)RunningInEvoProtocol)
		{
			this.RunningInEvoProtocol = RunningInEvoProtocol;
		}
		
		
		protected override OctoTip.OctoTipLib.RobotJob BeforeRobotRun()
		{
			throw new NotImplementedException();
		}
		
		protected override void AfterRobotRun(System.Collections.Generic.Dictionary<int, System.Collections.Generic.List<double>> MeasurementsResults)
		{
			throw new NotImplementedException();
		}
	}
}
