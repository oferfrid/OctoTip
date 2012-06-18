/*
 * Created by SharpDevelop.
 * User: oferfrid
 * Date: 06/05/2012
 * Time: 12:27
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
	/// Description of KCPStartKillState.
	/// </summary>
	[State("Dilut 2 AMP","Dilut Sample To AMP")]
	public class KCPStartKillState:RunRobotState,IRestartableState
	{
		
		
		int LicInd;
		int NumberOfSamples;
		int ONStartIndex;

		
		public KCPStartKillState(int LicInd,int NumberOfSamples,int ONStartIndex):base()
		{
			this.LicInd = LicInd;
			this.NumberOfSamples = NumberOfSamples;
			this.ONStartIndex = ONStartIndex;
		}
		
		public void Restart()
		{
			this.Start();
		}

		protected override RobotJob BeforeRobotRun()
		{
			List<RobotJobParameter> RJP = new List<RobotJobParameter>(2);
			
			LicPos LP = Utils.Ind2LicPos(LicInd);
			
			//ImportVariable(Liconic6PlateCart#Liconic6PlatePos#NumberOfSamples#SampleFirstLocation,"D:\OctoTip\Protocols\KillCurvePlaiting\Scripts\StartData.csv",0#0#0#0#0,"1#1#2#22#1",0,1,0,1,1);
			RJP.Add(new RobotJobParameter("Liconic6PlateCart",RobotJobParameter.ParameterType.Number,LP.Cart));
			RJP.Add(new RobotJobParameter("Liconic6PlatePos",RobotJobParameter.ParameterType.Number,LP.Pos));
			RJP.Add(new RobotJobParameter("NumberOfSamples",RobotJobParameter.ParameterType.Number,NumberOfSamples));
			RJP.Add(new RobotJobParameter("ONStartIndex",RobotJobParameter.ParameterType.Number,ONStartIndex));			
			
			RobotJob RJ = new RobotJob(
				@"D:\OctoTip\Protocols\KillCurvePlaiting\Scripts\StartKill.esc",RJP);
			
			return RJ;
		}
		
		protected override void AfterRobotRun()
		{
			
		}
		
		
		#region static
		public static new List<Type> NextStates()
		{
			return new List<Type>{typeof(KCPSampleState)};
		}
		#endregion
	}
}


