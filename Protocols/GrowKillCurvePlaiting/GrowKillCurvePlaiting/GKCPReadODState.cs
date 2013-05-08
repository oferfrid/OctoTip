/*
 * Created by SharpDevelop.
 * User: oferfrid
 * Date: 01/11/2012
 * Time: 18:32
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;
using System.Linq;
using OctoTip.Lib;
using OctoTip.Lib.ExperimentsCore.Attributes;
using OctoTip.Lib.ExperimentsCore.Base;
using OctoTip.Lib.ExperimentsCore.Interfaces;

namespace GrowKillCurvePlaiting
{
	/// <summary>
	/// Description of ReadBGState.
	/// </summary>
	[State("Read OD","Read well OD")]
	public class GKCPReadODState:ReadState,IRestartableState
	{
		double[] _OD;
		int LicPlateInd;
		int RowInd;
		int NumberOfExpSamples;
		
		public GKCPReadODState(int LicPlateInd , int RowInd,int NumberOfExpSamples):base()
		{
			this.LicPlateInd = LicPlateInd;
			this.RowInd = RowInd;
			this.NumberOfExpSamples = NumberOfExpSamples;
			
			_OD = new double[NumberOfExpSamples];
			
		}
		
		protected override RobotJob BeforeRobotRun()
		{
			List<RobotJobParameter> RJP = new List<RobotJobParameter>(2);
			
			LicPos LP = Utils.Ind2LicPos(LicPlateInd);
			

			//ImportVariable(Lic24PlateCart#Lic24PlatePos#Dilution348Ind#FromWell#FreezeInd,"D:\OctoTip\Protocols\GrowKillCurvePlaiting\Scripts\DiluteData.csv",0#0#0#0#0,"1#1#1#1#0",0,1,0,1,1);
			RJP.Add(new RobotJobParameter("Lic24PlateCart",RobotJobParameter.ParameterType.Number,LP.Cart));
			RJP.Add(new RobotJobParameter("Lic24PlatePos",RobotJobParameter.ParameterType.Number,LP.Pos));
			
			RobotJob RJ = new RobotJob(
				@"D:\OctoTip\Protocols\GrowKillCurvePlaiting\Scripts\ReadPlate.esc",RJP,0.7);
			
			return RJ;
		}
		
		protected override void AfterRobotRun()
		{
			Dictionary<int, List<double>> MeasurementsResults = this.GetMeasurementsResults(24);
			for (int i=0;i<NumberOfExpSamples;i++)
			{
				int WellInd = (RowInd-1)*4 + i + 1;
				_OD[i] = MeasurementsResults[WellInd].Average();
			}
			
		}
		
		public double[] OD
		{
			get { return _OD; }
			
		}
		public void Restart()
		{
			this.Start();
		}
		
		#region static
		public static new List<Type> NextStates()
		{
			return new List<Type>{typeof(GKCPWait2ODReadState),typeof(GKCPStartKillState)};
		}
		#endregion
		
	}
}
