/*
 * Created by SharpDevelop.
 * User: Tecan
 * Date: 11/02/2014
 * Time: 09:35
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

namespace MDKPlate1
{
	/// <summary>
	/// Description of Class1.
	/// </summary>
	/// 
	[State("Inoculation","Inoculation of a row in plate")]
	public class MDKInoculateState:RunRobotState,IRestartableState
	{
		//Lic96PlateCart,Lic96PlatePos,GermIndex,InoculationRow
		
		int LicPlateInd;
		int GermIndex;
		int InoculationRow;
		
		public MDKInoculateState(int LicPlateInd,int GermIndex,int InoculationRow):base()
		{
			this.LicPlateInd = LicPlateInd;
			this.GermIndex = GermIndex;
			this.InoculationRow = InoculationRow;	
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
			RJP.Add(new RobotJobParameter("GermIndex",RobotJobParameter.ParameterType.Number,GermIndex));
			RJP.Add(new RobotJobParameter("InoculationRow",RobotJobParameter.ParameterType.Number,InoculationRow));
				
			RobotJob RJ = new RobotJob(@"D:\OctoTip\Protocols\MDK\MDKPlate1\Scripts\Inoculate.esc",RJP,0.9);
			return RJ;
		}
		
		protected override void AfterRobotRun()
		{
			//throw new NotImplementedException();
		}
		
		#region static
		public static new List<Type> NextStates()
		{
			return new List<Type>{typeof(MDKIncubateState)};
		}
		#endregion
	}
}
