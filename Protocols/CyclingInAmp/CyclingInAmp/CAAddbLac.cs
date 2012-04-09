/*
 * Created by SharpDevelop.
 * User: oferfrid
 * Date: 08/04/2012
 * Time: 12:32
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;
using OctoTip.Lib;
using OctoTip.Lib.ExperimentsCore.Attributes;
using OctoTip.Lib.ExperimentsCore.Base;

namespace CyclingInAmp
{
	/// <summary>
	/// Description of CAAddbLac.
	/// </summary>
	[State("Add b-Lac","Add b-Lac to stop Killing")]
	public class CAAddbLac:RunRobotState
	{
		int LicInd;
		int Add2WellInd;
		
		public CAAddbLac(int LicInd,int Add2WellInd):base()
		{
			this.LicInd = LicInd;
			this.Add2WellInd = Add2WellInd;
		}

		protected override RobotJob BeforeRobotRun()
		{
			List<RobotJobParameter> RJP = new List<RobotJobParameter>(2);
			
			LicPos LP = Utils.Ind2LicPos(LicInd);
			
			RJP.Add(new RobotJobParameter("Liconic6PlateCart",RobotJobParameter.ParameterType.Number,LP.Cart));
			RJP.Add(new RobotJobParameter("Liconic6PlatePos",RobotJobParameter.ParameterType.Number,LP.Pos));
			RJP.Add(new RobotJobParameter("Add2WellInd",RobotJobParameter.ParameterType.Number,Add2WellInd));
			
			RobotJob RJ = new RobotJob(
				@"D:\OctoTip\Protocols\CyclingInAmp\Scripts\AddbLac.esc",RJP);
			
			return RJ;
		}
		
		protected override void AfterRobotRun()
		{
			
		}
		
		
		#region static
		public static new List<Type> NextStates()
		{
			return new List<Type>{typeof(CAGetOD)};
		}
		#endregion
		
		
	}
}