/*
 * Created by SharpDevelop.
 * User: Tecan
 * Date: 18/10/2011
 * Time: 16:17
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;
using System.Linq;
using OctoTip.Lib.ExperimentsCore.Attributes;
using OctoTip.Lib.ExperimentsCore.Base;
using OctoTip.Lib;

namespace Evo1
{
	/// <summary>
	/// Description of EvoGrow2ReadState.
	/// </summary>
	[State("Grow 2 read","Grow After Dilution read")]
	public class EvoGrow2ReadState:ReadState
	{
		#region static
		public static new List<Type> NextStates()
		{
			return new List<Type>{typeof(EvoGrow2State),typeof(EvoDilut2AmpState)};
		}
		#endregion
		EvoProtocol RunningInEvoProtocol;
		int PlateInd;
		int WellInd;
		public EvoGrow2ReadState(EvoProtocol RunningInEvoProtocol,int PlateInd,int WellInd):base((Protocol)RunningInEvoProtocol)
		{
			this.RunningInEvoProtocol = RunningInEvoProtocol;
			this.PlateInd = PlateInd;
			this.WellInd = WellInd;
		}
		
		protected override RobotJob BeforeRobotRun()
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
			
			double MeanOD = GetMeasurementsResults()[WellInd].Average();
			RunningInEvoProtocol.CurentOD = MeanOD;
		}
		
		
	}
}
