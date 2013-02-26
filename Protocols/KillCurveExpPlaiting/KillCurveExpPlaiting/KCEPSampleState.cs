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

namespace KillCurveExpPlaiting
{
	/// <summary>
	/// Description of KCEPSumpleState.
	/// </summary>
	[State("Sample","Sample bacteria to ")]
	public class KCEPSampleState:RunRobotState,IRestartableState
	{
		
		
		int LicInd;
		int NumberOfSamples;
		int FirstSampleEppendorfInd;


		
		public KCEPSampleState(int LicInd, int NumberOfSamples,int FirstSampleEppendorfInd ):base()
		{
			this.LicInd = LicInd;
			this.NumberOfSamples =NumberOfSamples;
			this.FirstSampleEppendorfInd =FirstSampleEppendorfInd;
		}
		
		public void Restart()
		{
			this.Start();
		}

		protected override RobotJob BeforeRobotRun()
		{
			List<RobotJobParameter> RJP = new List<RobotJobParameter>(4);
			
			LicPos LP = Utils.Ind2LicPos(LicInd);
//ImportVariable(Liconic24PlateCart#Liconic24PlatePos#NumberOfSamples#FirstSampleEppendorfInd,"D:\OctoTip\Protocols\KillCurveExpPlaiting\Scripts\TakeSampleData.csv",0#0#0#0,"1#1#2#1",0,1,0,1,1);


			RJP.Add(new RobotJobParameter("Liconic24PlateCart",RobotJobParameter.ParameterType.Number,LP.Cart));
			RJP.Add(new RobotJobParameter("Liconic24PlatePos",RobotJobParameter.ParameterType.Number,LP.Pos));
			RJP.Add(new RobotJobParameter("NumberOfSamples",RobotJobParameter.ParameterType.Number,NumberOfSamples));
			RJP.Add(new RobotJobParameter("FirstSampleEppendorfInd",RobotJobParameter.ParameterType.Number,FirstSampleEppendorfInd));
		
			RobotJob RJ = new RobotJob(
				@"D:\OctoTip\Protocols\KillCurveExpPlaiting\Scripts\TakeSample.esc",RJP);
			
			return RJ;
		}
		
		protected override void AfterRobotRun()
		{
			
		}
		
		
		#region static
		public static new List<Type> NextStates()
		{
			return new List<Type>{typeof(KCEPWaitState)};
		}
		#endregion
	}
}
