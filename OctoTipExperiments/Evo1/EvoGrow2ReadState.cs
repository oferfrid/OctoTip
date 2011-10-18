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
	/// Description of EvoGrow2ReadState.
	/// </summary>
	[State("Grow 2 read","Grow After Dilution read")]
	public class EvoGrow2ReadState:ReadState
	{
		#region static
		public static new List<Type> NextStates()
		{
			return new List<Type>{typeof(EvoGrow2State),typeof(EvoDilut2AmpState)};
		}
		#endregion
		EvoProtocol RunningInEvoProtocol;
		public EvoGrow2ReadState(EvoProtocol RunningInEvoProtocol):base((Protocol)RunningInEvoProtocol)
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
