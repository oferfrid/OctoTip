﻿/*
 * Created by SharpDevelop.
 * User: Tecan
 * Date: 11/02/2014
 * Time: 10:50
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

namespace MDKPlate2
{
	/// <summary>
	/// Description of MDKDeactivateState.
	/// </summary>
	/// 
	[State("Deactivation","Deactivation of antibiotic in plate")]
	public class MDKDeactivateState:RunRobotState,IRestartableState
	{
		//Lic96PlateCart,Lic96PlatePos,BLacIndex
		int LicPlateInd;
		int BLacIndex;
		
		public MDKDeactivateState(int LicPlateInd,int BLacIndex):base()
		{
			this.LicPlateInd = LicPlateInd;
			this.BLacIndex = BLacIndex;
		}
		
		public void Restart()
		{
			Start();
		}
		
		protected override RobotJob BeforeRobotRun()
		{
			LicPos LP = Utils.Ind2LicPos(LicPlateInd);
			System.Collections.Generic.List<RobotJobParameter> RJP = new List<RobotJobParameter>(4);
			RJP.Add(new RobotJobParameter("Lic96PlateCart",RobotJobParameter.ParameterType.Number,LP.Cart));
			RJP.Add(new RobotJobParameter("Lic96PlatePos",RobotJobParameter.ParameterType.Number,LP.Pos));
			RJP.Add(new RobotJobParameter("BLacIndex",RobotJobParameter.ParameterType.Number,BLacIndex));
				
			RobotJob RJ = new RobotJob(@"D:\OctoTip\Protocols\MDK\MDKPlate2\Scripts\Deactivate.esc",RJP,0.9);
			return RJ;
		}
		
		protected override void AfterRobotRun()
		{
			//throw new NotImplementedException();
		}
		
		#region static
		public static new List<Type> NextStates()
		{
			return new List<Type>{typeof(MDKFinalIncubateState)};
		}
		#endregion
	}
}
