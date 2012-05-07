/*
 * Created by SharpDevelop.
 * User: oferfrid
 * Date: 07/05/2012
 * Time: 09:12
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
	/// Description of KCPSumpleState.
	/// </summary>
	[State("Sample","Sample bacteria to ")]
	public class KCPSampleState:RunRobotState,IRestartableState
	{
		
		
		int LicInd;
		int SampleEppendorfInd;
		int NumberOfSamples;
		bool IsFirst;

		
		public KCPSampleState(int LicInd,int SampleEppendorfInd,int NumberOfSamples,bool IsFirst):base()
		{
			this.LicInd = LicInd;
			this.SampleEppendorfInd = SampleEppendorfInd;
			this.NumberOfSamples =NumberOfSamples;
			this.IsFirst = IsFirst;

		}
		
		public void Restart()
		{
			this.Start();
		}

		protected override RobotJob BeforeRobotRun()
		{
			List<RobotJobParameter> RJP = new List<RobotJobParameter>(2);
			
			LicPos LP = Utils.Ind2LicPos(LicInd);

///				@"ImportVariable(Liconic6PlateCart#Liconic6PlatePos#NumberOfSamples#FirstSampleInd#IsFirst,"D:\OctoTip\Protocols\KillCurvePlaiting\Scripts\TakeSampleData.csv",0#0#0#0#0,"1#1#2#1#0",0,1,0,1,1);",RJP);

			RJP.Add(new RobotJobParameter("Liconic6PlateCart",RobotJobParameter.ParameterType.Number,LP.Cart));
			RJP.Add(new RobotJobParameter("Liconic6PlatePos",RobotJobParameter.ParameterType.Number,LP.Pos));
			RJP.Add(new RobotJobParameter("NumberOfSamples",RobotJobParameter.ParameterType.Number,NumberOfSamples));
			RJP.Add(new RobotJobParameter("FirstSampleInd",RobotJobParameter.ParameterType.Number,SampleEppendorfInd));
			RJP.Add(new RobotJobParameter("IsFirst",RobotJobParameter.ParameterType.Number, IsFirst ? 1 : 0));

			
			RobotJob RJ = new RobotJob(
				@"D:\OctoTip\Protocols\KillCurvePlaiting\Scripts\TakeSample.esc",RJP);
			
			return RJ;
		}
		
		protected override void AfterRobotRun()
		{
			
		}
		
		
		#region static
		public static new List<Type> NextStates()
		{
			return new List<Type>{typeof(KCPWaitState)};
		}
		#endregion
	}
}
