/*
 * Created by SharpDevelop.
 * User: Tecan
 * Date: 18/10/2011
 * Time: 16:15
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;
using OctoTip.Lib.ExperimentsCore.Attributes;
using OctoTip.Lib.ExperimentsCore.Base;
using OctoTip.Lib;

namespace Evo1
{
	/// <summary>
	/// Description of EvoAddbLac.
	/// </summary>
	[State("Add b-Lac","Add b-Lac To Amp Well")]
	public class EvoAddbLacState:RunRobotState
	{
		#region static 
		
		public static new List<Type> NextStates()
		{
			return new List<Type>{typeof(EvoGrow1ReadState)};
		}
		#endregion
		
		EvoProtocol RunningInEvoProtocol;
		
		int PlateInd;
		int WellInd;
		public EvoAddbLacState(EvoProtocol RunningInEvoProtocol,int PlateInd,int WellInd):base((Protocol)RunningInEvoProtocol)
		{
			this.RunningInEvoProtocol = RunningInEvoProtocol;
			this.PlateInd = PlateInd;
			this.WellInd  = WellInd;
		}
		
		
		protected override RobotJob BeforeRobotRun()
		{
			List<RobotJobParameter> RJP = new List<RobotJobParameter>(2);
			
			LicPos LP = Utils.Ind2LicPos(PlateInd);
			
			RJP.Add(new RobotJobParameter("Lic6Cart",RobotJobParameter.ParameterType.Number,LP.Cart));
			RJP.Add(new RobotJobParameter("Lic6Pos",RobotJobParameter.ParameterType.Number,LP.Pos));
			RJP.Add(new RobotJobParameter("WellInd",RobotJobParameter.ParameterType.Number,WellInd));
			RJP.Add(new RobotJobParameter("BLacWellInd",RobotJobParameter.ParameterType.Number,RunningInEvoProtocol.EvoProtocolParameters.bLacEppendorfInd));
			        
			RobotJob RJ = new RobotJob(@"D:\OctoTip\SampleData\Evo1\EvoAddbLac.esc",RJP);
			
			return RJ;
		}
		
		protected override void AfterRobotRun()
		{
			//throw new NotImplementedException();
		}
	}
}
