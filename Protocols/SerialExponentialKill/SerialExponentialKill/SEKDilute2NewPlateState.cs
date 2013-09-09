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
	[State("Dilut 2 new","Dilute main evolution batch culture 2 new plate")]
	public class SEKDilute2NewPlateState:RunRobotState,IRestartableState
	{
		int LicFromPlateInd;
		int LicToPlateInd;
		public SEKDilute2NewPlateState(int LicFromPlateInd,int LicToPlateInd ):base()
		{
			this.LicFromPlateInd = LicToPlateInd;
			this.LicToPlateInd =  LicToPlateInd;
		}
		protected override RobotJob BeforeRobotRun()
		{
			List<RobotJobParameter> RJP = new List<RobotJobParameter>(2);
			
			LicPos LPFrom = Utils.Ind2LicPos(LicFromPlateInd);
			LicPos LPTo = Utils.Ind2LicPos(LicToPlateInd);

//ImportVariable(Lic24PlateCart#Lic24PlatePos#Lic24PlateCartNew#Lic24PlatePosNew,"D:\OctoTip\Protocols\SerialExponentialKill\Scripts\DiluteDataChangePlate.csv",0#0#0#0,"1#1#1#1",0,1,0,1,1);

			RJP.Add(new RobotJobParameter("Lic24PlateCart",RobotJobParameter.ParameterType.Number,LPFrom.Cart));
			RJP.Add(new RobotJobParameter("Lic24PlatePos",RobotJobParameter.ParameterType.Number,LPFrom.Pos));
			RJP.Add(new RobotJobParameter("Lic24PlateCartNew",RobotJobParameter.ParameterType.Number,LPTo.Cart));
			RJP.Add(new RobotJobParameter("Lic24PlatePosNew",RobotJobParameter.ParameterType.Number,LPTo.Pos));

			RobotJob RJ = new RobotJob(
				@"D:\OctoTip\Protocols\SerialExponentialKill\Scripts\DiluteNewPlate.esc",RJP,0.9);
			
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
			return new List<Type>{typeof(SEKReadBackroundState)};
		}
		#endregion
	}
}
