/*
 * Created by SharpDevelop.
 * User: Tecan
 * Date: 18/10/2011
 * Time: 16:09
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;
using OctoTip.Lib.ExperimentsCore.Attributes;
using OctoTip.Lib.ExperimentsCore.Base;
using OctoTip.Lib;


namespace Evo1
{
	/// <summary>
	/// Description of EvoStarterState.
	/// </summary>
	[State("Start","Start the Evolution Experiment")]
	public class EvoStarterState:RunRobotState
	{
		#region static
		public static new List<Type> NextStates()
		{
			return new List<Type>{typeof(EvoKillReadState)};
		}
		#endregion
		
		int PlateInd ;
		
		EvoProtocol RunningInEvoProtocol;
		public EvoStarterState(EvoProtocol RunningInEvoProtocol,int PlateInd):base((Protocol)RunningInEvoProtocol)
		{
			this.RunningInEvoProtocol = RunningInEvoProtocol;
			this.PlateInd =PlateInd;
		}
		
		protected override RobotJob BeforeRobotRun()
		{
			List<RobotJobParameter> RJP = new List<RobotJobParameter>(3);
			
			LicPos LP = Utils.Ind2LicPos(PlateInd);
			
			RJP.Add(new RobotJobParameter("Lic6Cart",RobotJobParameter.ParameterType.Number,LP.Cart));
			RJP.Add(new RobotJobParameter("Lic6Pos",RobotJobParameter.ParameterType.Number,LP.Pos));
			RJP.Add(new RobotJobParameter("AMPWellInd",RobotJobParameter.ParameterType.Number,RunningInEvoProtocol.EvoProtocolParameters.AMPEppendorfInd));
			        
			RobotJob RJ = new RobotJob(@"D:\OctoTip\SampleData\Evo1\EvoStarter.esc",RJP);
			
			return RJ;
		}
		
		protected override void AfterRobotRun()
		{
			
		}
	}
}
