/*
 * Created by SharpDevelop.
 * User: oferfrid
 * Date: 05/04/2012
 * Time: 17:15
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;
using OctoTip.Lib;
using OctoTip.Lib.ExperimentsCore.Attributes;
using OctoTip.Lib.ExperimentsCore.Base;
using OctoTip.Lib.ExperimentsCore.Interfaces;

namespace CyclingInAmp
{
	/// <summary>
	/// Description of CAStartState.
	/// </summary>
	[State("Start","Dilution of bacteria to AMP at the first time")]
	public class CAStart:RunRobotState,IRestartableState
	{
		int LicInd;
		
		public CAStart(int LicInd):base()
		{
			this.LicInd = LicInd;
		}

		
		public void Restart()
		{
			this.DoWork();
		}
		
		protected override RobotJob BeforeRobotRun()
		{
			List<RobotJobParameter> RJP = new List<RobotJobParameter>(2);
			
			LicPos LP = Utils.Ind2LicPos(LicInd);
			
			 
			RJP.Add(new RobotJobParameter("Liconic6PlateCart",RobotJobParameter.ParameterType.Number,LP.Cart));
			RJP.Add(new RobotJobParameter("Liconic6PlatePos",RobotJobParameter.ParameterType.Number,LP.Pos));
			
			
			RobotJob RJ = new RobotJob(
				@"D:\OctoTip\Protocols\CyclingInAmp\Scripts\Start.esc",RJP);
			
			return RJ;
			
		}
		
		protected override void AfterRobotRun()
		{
			
		}
		
		
		#region static
		public static new List<Type> NextStates()
		{
			return new List<Type>{typeof(CAKill)};
		}
		#endregion
		
		
		
		
	}
	
}
