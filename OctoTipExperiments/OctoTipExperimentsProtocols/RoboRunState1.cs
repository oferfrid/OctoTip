/*
 * Created by SharpDevelop.
 * User: oferfrid
 * Date: 09/10/2011
 * Time: 18:37
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;
using OctoTip.OctoTipExperiments.Core.Attributes;
using OctoTip.OctoTipExperiments.Core.Base;
using OctoTip.OctoTipLib;

namespace OctoTip.OctoTipExperiments
{
	/// <summary>
	/// Description of Roborun.
	/// </summary>
		[State("RunRobot","Runing!")]
	public class RoboRunState1:RunRobotState
	{
		MPNProtocol RunningInMPNProtocol;
		public RoboRunState1():base()
		{
		}
		public RoboRunState1(MPNProtocol RunningInProtocol):base((Protocol)RunningInProtocol)
		{
			this.RunningInMPNProtocol = RunningInProtocol;
		}
		
		protected override RobotJob BeforeRobotRun()
		{
			List<RobotJobParameter> RP= new List<RobotJobParameter>();
			RP.Add(new RobotJobParameter("Washes",RobotJobParameter.ParameterType.Number,RunningInMPNProtocol.MPNProtocolParameters.Whashs));
			
			OctoTip.OctoTipLib.RobotJob RJ = new OctoTip.OctoTipLib.RobotJob(@"..\..\..\SampleData\TestScripts\Test\" + "TempDo.esc",RP);
			//RJ.CreateScript();
			
			
			Random r = new Random();
			RJ.Priority = (double)r.Next()/int.MaxValue;
			
			return RJ;
		}
		
		#region static
		public static new List<Type> NextStates()
		{
			return new List<Type>{typeof(WaitState2)};
		}
		
		#endregion	
		
		protected override void AfterRobotRun()
		{
			//throw new NotImplementedException();
		}
	}
}
