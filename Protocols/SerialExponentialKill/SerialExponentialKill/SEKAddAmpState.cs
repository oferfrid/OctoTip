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

namespace SerialExponentialKill
{
	/// <summary>
	/// Description of SEKDiluteState.
	/// </summary>
	[State("Add AMP","Add AMP and freez if needed")]
	public class SEKAddAmpState:RunRobotState,IRestartableState
	{
		int LicPlateInd;
		int CurentWell;
		int FreezeInd;
		int AMPPos;
		public SEKAddAmpState(int LicPlateInd,int CurentWell,int FreezeInd, int AMPPos ):base()
		{
			this.LicPlateInd=LicPlateInd;
			this.CurentWell=CurentWell;
			this.FreezeInd=FreezeInd;
			this.AMPPos = AMPPos;
		}
		protected override RobotJob BeforeRobotRun()
		{
			List<RobotJobParameter> RJP = new List<RobotJobParameter>(2);
			
			LicPos LP = Utils.Ind2LicPos(LicPlateInd);
			

//ImportVariable(Lic24PlateCart#Lic24PlatePos#AmpPos#FreezePos#CurrWell,"D:\OctoTip\Protocols\SerialExponentialKill\Scripts\StartKillData.csv",0#0#0#0#0,"1#1#1#0#1",0,1,0,1,1);


			RJP.Add(new RobotJobParameter("Lic24PlateCart",RobotJobParameter.ParameterType.Number,LP.Cart));
			RJP.Add(new RobotJobParameter("Lic24PlatePos",RobotJobParameter.ParameterType.Number,LP.Pos));
			RJP.Add(new RobotJobParameter("AmpPos",RobotJobParameter.ParameterType.Number,AMPPos));
			RJP.Add(new RobotJobParameter("FreezePos",RobotJobParameter.ParameterType.Number,FreezeInd));
			RJP.Add(new RobotJobParameter("CurrWell",RobotJobParameter.ParameterType.Number,CurentWell));
			

			
			RobotJob RJ = new RobotJob(
				@"D:\OctoTip\Protocols\SerialExponentialKill\Scripts\StartKill.esc",RJP,0.9);
			
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
			return new List<Type>{typeof(SEKKillState)};
		}
		#endregion
	}
}
