/*
 * Created by SharpDevelop.
 * User: Tecan
 * Date: 20/09/2012
 * Time: 15:25
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;
using OctoTip.Lib;
using OctoTip.Lib.ExperimentsCore.Attributes;
using OctoTip.Lib.ExperimentsCore.Base;
using OctoTip.Lib.ExperimentsCore.Interfaces;

namespace CalibrateL1210Growth
{
	/// <summary>
	/// Description of CLGSampleState.
	/// </summary>
	[State("SampleN","Sample MPN")]
	public class CLGSampleState:RunRobotState,IRestartableState
	{
		
		int LicInd;
		
		public CLGSampleState(int LicInd)
		{
			this.LicInd=LicInd;
		}
		
		#region static
		public static new List<Type> NextStates()
		{
			return new List<Type>{typeof(CLGReadState)};
		}
		#endregion
		
		protected override OctoTip.Lib.RobotJob BeforeRobotRun()
		{
			List<RobotJobParameter> RJP = new List<RobotJobParameter>(2);
			
			LicPos LP = Utils.Ind2LicPos(LicInd);

			RJP.Add(new RobotJobParameter("Liconic96PlateCart",RobotJobParameter.ParameterType.Number,LP.Cart));
			RJP.Add(new RobotJobParameter("Liconic96PlatePos",RobotJobParameter.ParameterType.Number,LP.Pos));

			
			RobotJob RJ = new RobotJob(
				@"D:\OctoTip\Protocols\CalibrateL1210Growth\Scripts\SampleN.esc",RJP);
			
			return RJ;
		}
		
		protected override void AfterRobotRun()
		{
			
		}
		
		public void Restart()
		{
			this.Start();
		}
	}
}
