/*
 * Created by SharpDevelop.
 * User: Tecan
 * Date: 18/10/2011
 * Time: 16:14
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;
using System.Linq;
using OctoTip.OctoTipExperiments.Core.Attributes;
using OctoTip.OctoTipExperiments.Core.Base;
using OctoTip.OctoTipLib;

namespace Evo1
{
	/// <summary>
	/// Description of EvoKillReadState.
	/// </summary>
	[State("Kill Read","Kill Read")]
	public class EvoKillReadState:ReadState
	{
		#region static
		public static new List<Type> NextStates()
		{
			return new List<Type>{typeof(EvoKillState)};
		}
		#endregion
		EvoProtocol RunningInEvoProtocol;
		int PlateInd;
		int WellInd;
		public EvoKillReadState(EvoProtocol RunningInEvoProtocol,int PlateInd,int WellInd):base((Protocol)RunningInEvoProtocol)
		{
			this.RunningInEvoProtocol = RunningInEvoProtocol;
			this.PlateInd = PlateInd;
			this.WellInd = WellInd;
		}
		
		
		protected override OctoTip.OctoTipLib.RobotJob BeforeRobotRun()
		{
			
			List<RobotJobParameter> RJP = new List<RobotJobParameter>(2);
			
			LicPos LP = Utils.Ind2LicPos(PlateInd);
			
			RJP.Add(new RobotJobParameter("Lic6Cart",RobotJobParameter.ParameterType.Number,LP.Cart));
			RJP.Add(new RobotJobParameter("Lic6Pos",RobotJobParameter.ParameterType.Number,LP.Pos));
			        
			RobotJob RJ = new RobotJob(@"D:\OctoTip\SampleData\Evo1\EvoRead2OD.esc",RJP);
			
			return RJ;
			
		}
		
		protected override void AfterRobotRun()
		{
			
			double MeanOD = this.GetMeasurementsResults()[WellInd].Average();
			RunningInEvoProtocol.CurentOD = MeanOD;
		}
		
		
	}
}
