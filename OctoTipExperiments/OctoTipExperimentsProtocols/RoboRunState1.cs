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
using OctoTip.OctoTipExperiments.Core.Base;
using OctoTip.OctoTipLib;

namespace OctoTip.OctoTipExperiments
{
	/// <summary>
	/// Description of Roborun.
	/// </summary>
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
		
		public override RobotJob GetRobotJob()
		{
			List<RobotJobParameter> RP= new List<RobotJobParameter>();
			RP.Add(new RobotJobParameter("v1",RobotJobParameter.ParameterType.Number,RunningInMPNProtocol.MPNProtocolParameters.Whash));
			RP.Add(new RobotJobParameter("v2",RobotJobParameter.ParameterType.String,"dfasd"));
			
			OctoTip.OctoTipLib.RobotJob RJ = new OctoTip.OctoTipLib.RobotJob(@"D:\OctoTip\SampleData\" + "Temp.esc",RP);
			//RJ.CreateScript();
			
			
			Random r = new Random();
			RJ.Priority = (double)r.Next()/int.MaxValue;
			
			return RJ;
		}
		
		public static new List<Type> NextStates()
		{
			return new List<Type>{typeof(WaitState1)};
		}
	}
}
