/*
 * Created by SharpDevelop.
 * User: oferfrid
 * Date: 11/05/2012
 * Time: 14:40
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;
using OctoTip.Lib;
using OctoTip.Lib.ExperimentsCore.Attributes;
using OctoTip.Lib.ExperimentsCore.Base;
using OctoTip.Lib.ExperimentsCore.Interfaces;

namespace KillCurvePlaiting
{
	/// <summary>
	/// Description of KCPEndGrowState.
	/// </summary>
	[State("End ON Grow","End of  ON grow")]
	public class KCPEndGrowState:RunRobotState,IRestartableState
	{
		int LicONInd;
		public KCPEndGrowState(int LicONInd):base()
		{
			this.LicONInd = LicONInd;
		}
		
		public void Restart()
		{
			this.Start();
		}

		protected override RobotJob BeforeRobotRun()
		{
			List<RobotJobParameter> RJP = new List<RobotJobParameter>(2);
			
			LicPos LP = Utils.Ind2LicPos(LicONInd);
			
			//ImportVariable(Liconic96PlateCart#Liconic96PlatePos,"D:\OctoTip\Protocols\KillCurvePlaiting\Scripts\StartGrowData.csv",0#0,"1#1",0,1,0,1,1);
			RJP.Add(new RobotJobParameter("Liconic96PlateCart",RobotJobParameter.ParameterType.Number,LP.Cart));
			RJP.Add(new RobotJobParameter("Liconic96PlatePos",RobotJobParameter.ParameterType.Number,LP.Pos));
			
			RobotJob RJ = new RobotJob(
				@"D:\OctoTip\Protocols\KillCurvePlaiting\Scripts\EndGrow.esc",RJP);
			
			return RJ;
		}
		
		protected override void AfterRobotRun()
		{
			
		}
		
		
		#region static
		public static new List<Type> NextStates()
		{
			return new List<Type>{typeof(KCPStartKillState)};
		}
		#endregion
	}

}

