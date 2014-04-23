/*
 * Created by SharpDevelop.
 * User: oferfrid
 * Date: 23/04/2014
 * Time: 10:50
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;
using System.Linq;
using OctoTip.Lib;
using OctoTip.Lib.ExperimentsCore.Attributes;
using OctoTip.Lib.ExperimentsCore.Base;
using OctoTip.Lib.Utils;

namespace SerialDilutionONEvolution
{
	/// <summary>
	/// Description of SDONEReadState.
	/// </summary>
	public class SDONEReadState:ReadState
	{
		int LicPlateInd;
		int Well;
		protected double _WellOD;
		
		public SDONEReadState(int LicPlateInd,int Well):base()
		{
			this.LicPlateInd = LicPlateInd;
			this.Well = Well;
		}
		
		protected override OctoTip.Lib.RobotJob BeforeRobotRun()
		{
			
			List<RobotJobParameter> RJP = new List<RobotJobParameter>(2);
			
			LicPos LP = Utils.Ind2LicPos(LicPlateInd);

			//ImportVariable(Lic24PlateCart#Lic24PlatePos#Dilution348Ind#FromWell#FreezeInd,"D:\OctoTip\Protocols\SerialDilutionONEvolution\Scripts\DiluteData.csv",0#0#0#0#0,"1#1#1#1#0",0,1,0,1,1);


			RJP.Add(new RobotJobParameter("Lic24PlateCart",RobotJobParameter.ParameterType.Number,LP.Cart));
			RJP.Add(new RobotJobParameter("Lic24PlatePos",RobotJobParameter.ParameterType.Number,LP.Pos));
			RobotJob RJ = new RobotJob(
				@"D:\OctoTip\Protocols\SerialDilutionONEvolution\Scripts\Dilute.esc",RJP,0.5);
			return RJ;
		}
		
		protected override void AfterRobotRun()
		{
			Dictionary<int,List<double>> MeasurementsResults = this.GetMeasurementsResults(24);
			_WellOD = MeasurementsResults[Well].Average();
			throw new NotImplementedException();
		}
	}
}
