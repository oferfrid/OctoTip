/*
 * Created by SharpDevelop.
 * User: Tecan
 * Date: 18/10/2011
 * Time: 16:18
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
	/// Description of EvoDilut2AmpState.
	/// </summary>
	[State("2AMP","Dilut to AMP")]
	public class EvoDilut2AmpState:RunRobotState
	{
		#region static
		
		public static new List<Type> NextStates()
		{
			return new List<Type>{typeof(EvoKillReadState)};
		}
		#endregion
		
		EvoProtocol RunningInEvoProtocol;
		int FromPlateInd;
		int ToPlateInd;
		int PlateInd;
		int FromWell ;
		int ToWell;
		int FreezWellInd;
		int AMPWellInd;
		public EvoDilut2AmpState(EvoProtocol RunningInEvoProtocol,
		                         int PlateInd,
		                         int FromWell ,
		                         int ToWell,
		                         int FreezWellInd,
		                         int AMPWellInd):base((Protocol)RunningInEvoProtocol)
		{
			this.RunningInEvoProtocol = RunningInEvoProtocol;
			this.PlateInd = PlateInd;
			this.FromWell=FromWell ;
			this.ToWell = ToWell;
			this.FreezWellInd = FreezWellInd;
			this.AMPWellInd = AMPWellInd;
		}
		
		public EvoDilut2AmpState(EvoProtocol RunningInEvoProtocol,
		                         int FromPlateInd,
		                         int ToPlateInd,
		                         int FromWell ,
		                         int ToWell,
		                         int FreezWellInd,
		                         int AMPWellInd):base((Protocol)RunningInEvoProtocol)
		{
			this.RunningInEvoProtocol = RunningInEvoProtocol;
			this.FromPlateInd =FromPlateInd;
			this.ToPlateInd=ToPlateInd;
			this.FromWell =FromWell;
			this.ToWell=ToWell;
			this.FreezWellInd = FreezWellInd;
			this.AMPWellInd = AMPWellInd;
		}
		
		protected override RobotJob BeforeRobotRun()
		{
			RobotJob RJ;
			if(FromPlateInd==0)//do not move plate
			{
				List<RobotJobParameter> RJP = new List<RobotJobParameter>(6);
				
				LicPos LP = Utils.Ind2LicPos(PlateInd);
				
				RJP.Add(new RobotJobParameter("Lic6Cart",RobotJobParameter.ParameterType.Number,LP.Cart));
				RJP.Add(new RobotJobParameter("Lic6Pos",RobotJobParameter.ParameterType.Number,LP.Pos));
				RJP.Add(new RobotJobParameter("FromWell",RobotJobParameter.ParameterType.Number,FromWell));
				RJP.Add(new RobotJobParameter("ToWell",RobotJobParameter.ParameterType.Number,ToWell));
				RJP.Add(new RobotJobParameter("FreezWellInd",RobotJobParameter.ParameterType.Number,FreezWellInd));
				RJP.Add(new RobotJobParameter("AMPWellInd",RobotJobParameter.ParameterType.Number,AMPWellInd));
				
				RJ = new RobotJob(@"D:\OctoTip\SampleData\Evo1\EvoDilutToAmp.esc",RJP);
				
			}
			else
			{
				List<RobotJobParameter> RJP = new List<RobotJobParameter>(8);
				
				LicPos FromLP = Utils.Ind2LicPos(FromPlateInd);
				LicPos ToLP = Utils.Ind2LicPos(ToPlateInd);
				
				RJP.Add(new RobotJobParameter("Lic6Cart",RobotJobParameter.ParameterType.Number,FromLP.Cart));
				RJP.Add(new RobotJobParameter("Lic6Pos",RobotJobParameter.ParameterType.Number,FromLP.Pos));
				RJP.Add(new RobotJobParameter("ToLic6Cart",RobotJobParameter.ParameterType.Number,ToLP.Cart));
				RJP.Add(new RobotJobParameter("ToLic6Pos",RobotJobParameter.ParameterType.Number,ToLP.Pos));
				RJP.Add(new RobotJobParameter("FromWell",RobotJobParameter.ParameterType.Number,FromWell));
				RJP.Add(new RobotJobParameter("ToWell",RobotJobParameter.ParameterType.Number,ToWell));
				RJP.Add(new RobotJobParameter("FreezWellInd",RobotJobParameter.ParameterType.Number,FreezWellInd));
				RJP.Add(new RobotJobParameter("AMPWellInd",RobotJobParameter.ParameterType.Number,AMPWellInd));
				
				//Lic6Cart#Lic6Pos#ToLic6Cart#ToLic6Pos#FromWell#ToWell#FreezWellInd#AMPWellInd

				
				RJ = new RobotJob(@"D:\OctoTip\SampleData\Evo1\EvoDilutToAmpMovePlate.esc",RJP);
				
				
			}
			
			return RJ;
			
		}
		
		protected override void AfterRobotRun()
		{
			//throw new NotImplementedException();
		}
	}
}
