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
	public abstract class GKCPDilut:RunRobotState,IRestartableState
	{
		
		int LicInd;
		int NumberOfSamplesToDilute;
		int Row;
		string SharedResourcesFilePath;
		
	
			
			public GKCPDilut(int LicInd,int Row,int NumberOfSamplesToDilute,string SharedResourcesFilePath):base()
		{
			this.LicInd = LicInd;
			this.NumberOfSamplesToDilute = NumberOfSamplesToDilute;
			this.Row = Row;
			this.SharedResourcesFilePath = SharedResourcesFilePath;
			
		}
		
		public void Restart()
		{
			this.Start();
		}

		protected override RobotJob BeforeRobotRun()
		{
			List<RobotJobParameter> RJP = new List<RobotJobParameter>(2);
			
			LicPos LP = Utils.Ind2LicPos(LicInd);
			
			int DiluteUsing384PlateIndex = LocalUtils.GetNext384Index(SharedResourcesFilePath);
			int DiluteUsing384PlatePos = LocalUtils.GetNext384Pos(DiluteUsing384PlateIndex);
			
			//increase the index off 384 in the number of samples
			for (int i=0;i<(NumberOfSamplesToDilute-1);i++)
			{
				LocalUtils.GetNext384Pos(LocalUtils.GetNext384Index(SharedResourcesFilePath));
			}
			
			//ImportVariable(Lic24PlateCart#Lic24PlatePos#Dilution348Ind#Row#NumberOfSamplesToDilute,"D:\OctoTip\Protocols\GrowKillCurvePlaiting\Scripts\DiluteData.csv",0#0#0#0#0,"1#1#1#2#4",0,1,0,1,1);
			RJP.Add(new RobotJobParameter("Lic24PlateCart",RobotJobParameter.ParameterType.Number,LP.Cart));
			RJP.Add(new RobotJobParameter("Lic24PlatePos",RobotJobParameter.ParameterType.Number,LP.Pos));
			RJP.Add(new RobotJobParameter("Dilution348Ind",RobotJobParameter.ParameterType.Number,DiluteUsing384PlatePos));
			RJP.Add(new RobotJobParameter("Row",RobotJobParameter.ParameterType.Number,Row));
			RJP.Add(new RobotJobParameter("NumberOfSamplesToDilute",RobotJobParameter.ParameterType.Number,NumberOfSamplesToDilute));
			
			
			
			RobotJob RJ = new RobotJob(
				@"D:\OctoTip\Protocols\GrowKillCurvePlaiting\Scripts\Dilute.esc",RJP);
			
			return RJ;
		}
		
		
		
		protected override void AfterRobotRun()
		{
			
		}
		
		
		
	}
}


