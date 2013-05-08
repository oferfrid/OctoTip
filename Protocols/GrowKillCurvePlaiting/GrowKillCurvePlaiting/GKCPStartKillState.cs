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

namespace GrowKillCurvePlaiting
{
	/// <summary>
	/// Description of GKCPStartKillState.
	/// </summary>
	[State("Dilut 2 AMP","Dilut Sample To AMP")]
	public class GKCPStartKillState:RunRobotState,IRestartableState
	{
		
		
		int LicInd;
		int NumberOfSamples;
		int AMPPosision;

		
		public GKCPStartKillState(int LicInd,int NumberOfSamples,int AMPPosision):base()
		{
			this.LicInd = LicInd;
			this.NumberOfSamples = NumberOfSamples;
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
//ImportVariable(Lic24PlateCart#Lic24PlatePos#NumberOfSamples#NumberOfExpSamples#AMPPosision,"D:\OctoTip\Protocols\GrowKillCurvePlaiting\Scripts\StartKillData.csv",0#0#0#0#0,"1#1#4#2#24",0,1,0,1,1);
			RJP.Add(new RobotJobParameter("Lic24PlateCart",RobotJobParameter.ParameterType.Number,LP.Cart));
			RJP.Add(new RobotJobParameter("Lic24PlatePos",RobotJobParameter.ParameterType.Number,LP.Pos));
			RJP.Add(new RobotJobParameter("NumberOfSamples",RobotJobParameter.ParameterType.Number,NumberOfSamples));
			RJP.Add(new RobotJobParameter("AMPPosision",RobotJobParameter.ParameterType.Number,AMPPosision));
			
			
			RobotJob RJ = new RobotJob(
				@"D:\OctoTip\Protocols\GrowKillCurvePlaiting\Scripts\StartKill.esc",RJP);
			
			return RJ;
		}
		
		protected override void AfterRobotRun()
		{
			
		}
		
		
		#region static
		public static new List<Type> NextStates()
		{
			return new List<Type>{typeof(GKCPSampleState)};
		}
		#endregion
	}
}


