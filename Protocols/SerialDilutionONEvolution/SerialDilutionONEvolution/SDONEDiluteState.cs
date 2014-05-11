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
using OctoTip.Lib.Utils;

namespace SerialDilutionONEvolution
{
	/// <summary>
	/// Description of SDONEDiluteState.
	/// </summary>
	[State("Dilut","Dilute main evolution batch culture")]
	public class SDONEDiluteState:RunRobotState,IRestartableState
	{
		int LicPlateInd;
		int FromWell;
		int DiluteUsing384PlatePos;
		int FreezeInd;
		
		public SDONEDiluteState(int LicPlateInd,int FromWell,int DiluteUsing384PlatePos,int FreezeInd):base()
		{
			this.LicPlateInd=LicPlateInd;
			this.FromWell=FromWell;
			this.DiluteUsing384PlatePos = DiluteUsing384PlatePos;
			this.FreezeInd=FreezeInd;
		}
		protected override RobotJob BeforeRobotRun()
		{
			List<RobotJobParameter> RJP = new List<RobotJobParameter>(4);
			
			LicPos LP = Utils.Ind2LicPos(LicPlateInd);
			

//ImportVariable(Lic24PlateCart#Lic24PlatePos#Dilution348Ind#FromWell#FreezeInd,"D:\OctoTip\Protocols\SerialDilutionONEvolution\Scripts\DiluteData.csv",0#0#0#0#0,"1#1#1#1#0",0,1,0,1,1);


			RJP.Add(new RobotJobParameter("Lic24PlateCart",RobotJobParameter.ParameterType.Number,LP.Cart));
			RJP.Add(new RobotJobParameter("Lic24PlatePos",RobotJobParameter.ParameterType.Number,LP.Pos));
			RJP.Add(new RobotJobParameter("FromWell",RobotJobParameter.ParameterType.Number,FromWell));
			RJP.Add(new RobotJobParameter("Dilution384Index",RobotJobParameter.ParameterType.Number,DiluteUsing384PlatePos));
			RJP.Add(new RobotJobParameter("FreezeInd",RobotJobParameter.ParameterType.Number,FreezeInd));

			
			RobotJob RJ = new RobotJob(
				@"D:\OctoTip\Protocols\SerialDilutionONEvolution\Scripts\Dilute.esc",RJP,0.9);
			
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
			return new List<Type>{typeof(SDONEWait4StationaryState),typeof(SDONEReadWellBackgroundState)};
		}
		#endregion
	}
}
