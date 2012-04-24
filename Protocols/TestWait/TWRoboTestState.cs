/*
 * Created by SharpDevelop.
 * User: Tecan
 * Date: 20/04/2012
 * Time: 10:00
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;
using OctoTip.Lib;
using OctoTip.Lib.ExperimentsCore.Attributes;
using OctoTip.Lib.ExperimentsCore.Base;
using OctoTip.Lib.ExperimentsCore.Interfaces;

namespace TestWait
{
	/// <summary>
	/// Description of TWRoboTestState.
	/// </summary>
	[State("RobotTest","Not mutch")]
	public class TWRoboTestState:RunRobotState,IRestartableState
	{
		
		
		protected override RobotJob BeforeRobotRun()
		{
			RobotJob rj = new RobotJob(@"D:\OctoTip\Protocols\TestWait\scripts\Test.esc",0.2);
			
			List<RobotJobParameter> RJP = new List<RobotJobParameter>(2);
			RJP.Add(new RobotJobParameter("loop",RobotJobParameter.ParameterType.Number,1));
			rj.RobotJobParameters = RJP;
			return rj;
		}
		
		protected override void AfterRobotRun()
		{
			//throw new NotImplementedException();
		}
		
			
		#region static
		public static new List<Type> NextStates()
		{
			return new List<Type>{typeof(TWWait1State)};
		}
		#endregion
		
		public void Restart()
		{
			this.Start();
		}
	}
}
