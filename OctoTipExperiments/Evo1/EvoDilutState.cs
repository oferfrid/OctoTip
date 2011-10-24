/*
 * Created by SharpDevelop.
 * User: Tecan
 * Date: 18/10/2011
 * Time: 16:55
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;
using OctoTip.OctoTipExperiments.Core.Attributes;
using OctoTip.OctoTipExperiments.Core.Base;
using OctoTip.OctoTipLib;

namespace Evo1
{
	/// <summary>
	/// Description of EvoDilut.
	/// </summary>
	[State("Dilut","Dilut")]
	public class EvoDilutState:RunRobotState
	{
		
		#region static 
		public static new List<Type> NextStates()
		{
			return new List<Type>{typeof(EvoGrow2ReadState)};
		}
		#endregion
		EvoProtocol RunningInEvoProtocol;
		int FromWell ;
		int ToWell ;
		int PlateInd;
		public EvoDilutState(EvoProtocol RunningInEvoProtocol,int PlateInd,int FromWell ,int ToWell):base((Protocol)RunningInEvoProtocol)
		{
			this.RunningInEvoProtocol = RunningInEvoProtocol;
			this.FromWell = FromWell;
			this.ToWell = ToWell;
			this.PlateInd = PlateInd;
		}
		
		protected override RobotJob BeforeRobotRun()
		{
			List<RobotJobParameter> RJP = new List<RobotJobParameter>(4);
			
			LicPos LP = Utils.Ind2LicPos(PlateInd);
			
			RJP.Add(new RobotJobParameter("Lic6Cart",RobotJobParameter.ParameterType.Number,LP.Cart));
			RJP.Add(new RobotJobParameter("Lic6Pos",RobotJobParameter.ParameterType.Number,LP.Pos));
			RJP.Add(new RobotJobParameter("FromWell",RobotJobParameter.ParameterType.Number,FromWell));
			RJP.Add(new RobotJobParameter("ToWell",RobotJobParameter.ParameterType.Number,ToWell));
			        
			

			
			RobotJob RJ = new RobotJob(@"D:\OctoTip\SampleData\Evo1\EvoDilut1.esc",RJP);
			
			return RJ;
		}
		
		protected override void AfterRobotRun()
		{
			//throw new NotImplementedException();
		}
	}
}
