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

namespace SerialDilutionEvolution
{
	/// <summary>
	/// Description of SDEDiluteState.
	/// </summary>
	[State("Dilut","Dilute main evolution batch culture")]
	public class SDEDiluteState:RunRobotState,IRestartableState
	{
		int LicPlateInd;
		int FromWell;
		int FreezeInd;
		int Dilution348Ind;
		public SDEDiluteState(int LicPlateInd,int FromWell,int FreezeInd, int Dilution348Ind ):base()
		{
			this.LicPlateInd=LicPlateInd;
			this.FromWell=FromWell;
			this.FreezeInd=FreezeInd;
			this.Dilution348Ind = Dilution348Ind;
		}
		protected override RobotJob BeforeRobotRun()
		{
			List<RobotJobParameter> RJP = new List<RobotJobParameter>(2);
			
			LicPos LP = Utils.Ind2LicPos(LicPlateInd);
			

//ImportVariable(Lic24PlateCart#Lic24PlatePos#Dilution348Ind#FromWell#FreezeInd,"D:\OctoTip\Protocols\SerialDilutionEvolution\Scripts\DiluteData.csv",0#0#0#0#0,"1#1#1#1#0",0,1,0,1,1);

			RJP.Add(new RobotJobParameter("Liconic24PlateCart",RobotJobParameter.ParameterType.Number,LP.Cart));
			RJP.Add(new RobotJobParameter("Liconic24PlatePos",RobotJobParameter.ParameterType.Number,LP.Pos));
			RJP.Add(new RobotJobParameter("Dilution348Ind",RobotJobParameter.ParameterType.Number,Dilution348Ind));
			RJP.Add(new RobotJobParameter("FromWell",RobotJobParameter.ParameterType.Number,FromWell));
			RJP.Add(new RobotJobParameter("FreezeInd",RobotJobParameter.ParameterType.Number,FreezeInd));

			
			RobotJob RJ = new RobotJob(
				@"D:\OctoTip\Protocols\SerialDilutionEvolution\Scripts\Dilute.esc",RJP);
			
			return RJ;
		}
		
		protected override void AfterRobotRun()
		{
			throw new NotImplementedException();
		}
		
		public void Restart()
		{
			this.Start();
		}
		
		
		#region static
		public static new List<Type> NextStates()
		{
			return new List<Type>{typeof(SDEReadBackroundState)};
		}
		#endregion
	}
}
