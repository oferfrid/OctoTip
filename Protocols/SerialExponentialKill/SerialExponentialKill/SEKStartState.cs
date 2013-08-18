/*
 * Created by SharpDevelop.
 * User: Tecan
 * Date: 18/10/2012
 * Time: 14:37
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;

using OctoTip.Lib;
using OctoTip.Lib.ExperimentsCore.Attributes;
using OctoTip.Lib.ExperimentsCore.Base;
using OctoTip.Lib.ExperimentsCore.Interfaces;

namespace SerialExponentialKill
{
	/// <summary>
	/// Description of SEKStartState.
	/// </summary>
	[State("Start","Put First Plate in Liconic")]
	public class SEKStartState:RunRobotState,IRestartableState
	{
		int LicPlateInd;
		
		public SEKStartState(int LicPlateInd):base()
		{
			this.LicPlateInd=LicPlateInd;
		}
		protected override RobotJob BeforeRobotRun()
		{
			List<RobotJobParameter> RJP = new List<RobotJobParameter>(2);
			
			LicPos LP = Utils.Ind2LicPos(LicPlateInd);
			
//ImportVariable(Lic24PlateCart#Lic24PlatePos,"D:\OctoTip\Protocols\SerialExponentialKill\Scripts\StartData.csv",0#0,"1#1",0,1,0,1,1);

			RJP.Add(new RobotJobParameter("Lic24PlateCart",RobotJobParameter.ParameterType.Number,LP.Cart));
			RJP.Add(new RobotJobParameter("Lic24PlatePos",RobotJobParameter.ParameterType.Number,LP.Pos));
			
			
			RobotJob RJ = new RobotJob(
				@"D:\OctoTip\Protocols\SerialExponentialKill\Scripts\Start.esc",RJP,0.9);
			
			return RJ;
		}
		
		protected override void AfterRobotRun()
		{
			//throw new NotImplementedException();
		}
		
		public void Restart()
		{
			this.Start();
		}
		
		
		#region static
		public static new List<Type> NextStates()
		{
			return new List<Type>{typeof(SEKReadBackroundState)};
		}
		#endregion
	}
}
