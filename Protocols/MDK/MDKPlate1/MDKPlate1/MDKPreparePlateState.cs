/*
 * Created by SharpDevelop.
 * User: Tecan
 * Date: 11/02/2014
 * Time: 09:04
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;
using OctoTip.Lib;
using OctoTip.Lib.ExperimentsCore.Attributes;
using OctoTip.Lib.ExperimentsCore.Base;
using OctoTip.Lib.ExperimentsCore.Interfaces;

namespace MDKPlate1
{
	/// <summary>
	/// Description of MDKPreparePlate.
	/// </summary>
	/// 
	[State("Preparation","Preparation of the antibiotic gradient")]
	public class MDKPreparePlateState:RunRobotState,IRestartableState
	{
		
		int LicPlateInd;
		
		public MDKPreparePlateState(int LicPlateInd):base()
		{
			this.LicPlateInd = LicPlateInd;
		}
		
		public void Restart()
		{
			Start();
		}
		
		protected override RobotJob BeforeRobotRun()
		{
			
			LicPos LP = Utils.Ind2LicPos(LicPlateInd);
			//ImportVariable(Lic96PlateCart#Lic96PlatePos#AntibioMinFrac#AntibioMaxFrac,"D:\OctoTip\Protocols\MDK\MDKPlate1\Scripts\PreparePlate.csv",0#0#0#0,"1#1#0.05#0.5",0,1,0,1,1);
			System.Collections.Generic.List<RobotJobParameter> RJP = new List<RobotJobParameter>(4);
			RJP.Add(new RobotJobParameter("Lic96PlateCart",RobotJobParameter.ParameterType.Number,LP.Cart));
			RJP.Add(new RobotJobParameter("Lic96PlatePos",RobotJobParameter.ParameterType.Number,LP.Pos));
			
			RobotJob RJ = new RobotJob(@"D:\OctoTip\Protocols\MDK\MDKPlate1\Scripts\PrepareLogPlate.esc",RJP,0.2);
			return RJ;
		}
		
		protected override void AfterRobotRun()
		{
			//throw new NotImplementedException();
		}
		
		
		#region static
		public static new List<Type> NextStates()
		{
			return new List<Type>{typeof(MDKInoculateState)};
		}
		#endregion
	}
}
