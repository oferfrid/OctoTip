/*
 * Created by SharpDevelop.
 * User: oferfrid
 * Date: 08/04/2012
 * Time: 14:29
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
	/// Description of CADilut.
	/// </summary>
	[State("Dilut","Dilut in exponential phase")]
	public class CADilut:RunRobotState,IRestartableState
	{
	
		int LicInd;
		int Dilut2WellInd;
		
		public CADilut(int LicInd,int Dilut2WellInd):base()
		{
			this.LicInd = LicInd;
			this.Dilut2WellInd = Dilut2WellInd;
		}

		public void Restart()
		{
			this.Start();
		}
		
		protected override RobotJob BeforeRobotRun()
		{
			List<RobotJobParameter> RJP = new List<RobotJobParameter>(2);
			
			LicPos LP = Utils.Ind2LicPos(LicInd);
			
			RJP.Add(new RobotJobParameter("Liconic6PlateCart",RobotJobParameter.ParameterType.Number,LP.Cart));
			RJP.Add(new RobotJobParameter("Liconic6PlatePos",RobotJobParameter.ParameterType.Number,LP.Pos));
			RJP.Add(new RobotJobParameter("Dilut2WellInd",RobotJobParameter.ParameterType.Number,Dilut2WellInd));
			
			RobotJob RJ = new RobotJob(
				@"D:\OctoTip\Protocols\CyclingInAmp\Scripts\DilutbLac.esc",RJP);
			
			return RJ;
		}
		
		protected override void AfterRobotRun()
		{
			
		}
		
		
		#region static
		public static new List<Type> NextStates()
		{
			return new List<Type>{typeof(CAGrowToON)};
		}
		#endregion
		
	}
}
