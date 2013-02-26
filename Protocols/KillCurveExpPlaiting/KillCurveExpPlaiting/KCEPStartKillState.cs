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

namespace KillCurveExpPlaiting
{
	/// <summary>
	/// Description of KCEPStartKillState.
	/// </summary>
	[State("Dilut 2 AMP","Dilut Sample To AMP")]
	public class KCEPStartKillState:RunRobotState,IRestartableState
	{
		
		
		int LicInd;
		int NumberOfSamples;
		int NumberOfExpSamples;
		int AMPPosision;

		
		public KCEPStartKillState(int LicInd,int NumberOfSamples,int NumberOfExpSamples,int AMPPosision):base()
		{
			this.LicInd = LicInd;
			this.NumberOfSamples = NumberOfSamples;
			this.NumberOfExpSamples = NumberOfExpSamples;
			this.AMPPosision = AMPPosision;
		}
		
		public void Restart()
		{
			this.Start();
		}

		protected override RobotJob BeforeRobotRun()
		{
			List<RobotJobParameter> RJP = new List<RobotJobParameter>(2);
			
			LicPos LP = Utils.Ind2LicPos(LicInd);
//ImportVariable(Lic24PlateCart#Lic24PlatePos#NumberOfSamples#NumberOfExpSamples#AMPPosision,"D:\OctoTip\Protocols\KillCurveExpPlaiting\Scripts\StartKillData.csv",0#0#0#0#0,"1#1#4#2#24",0,1,0,1,1);
			RJP.Add(new RobotJobParameter("Lic24PlateCart",RobotJobParameter.ParameterType.Number,LP.Cart));
			RJP.Add(new RobotJobParameter("Lic24PlatePos",RobotJobParameter.ParameterType.Number,LP.Pos));
			RJP.Add(new RobotJobParameter("NumberOfSamples",RobotJobParameter.ParameterType.Number,NumberOfSamples));
			RJP.Add(new RobotJobParameter("NumberOfExpSamples",RobotJobParameter.ParameterType.Number,NumberOfExpSamples));
			RJP.Add(new RobotJobParameter("AMPPosision",RobotJobParameter.ParameterType.Number,AMPPosision));
			
			
			RobotJob RJ = new RobotJob(
				@"D:\OctoTip\Protocols\KillCurveExpPlaiting\Scripts\StartKill.esc",RJP);
			
			return RJ;
		}
		
		protected override void AfterRobotRun()
		{
			
		}
		
		
		#region static
		public static new List<Type> NextStates()
		{
			return new List<Type>{typeof(KCEPSampleState)};
		}
		#endregion
	}
}


