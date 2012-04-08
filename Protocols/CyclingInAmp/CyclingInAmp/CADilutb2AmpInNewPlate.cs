/*
 * Created by SharpDevelop.
 * User: oferfrid
 * Date: 08/04/2012
 * Time: 15:55
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
	/// Description of CADilutb2AmpInNewPlate.
	/// </summary>
	[State("Dilut 2 new AMP","Dilut ON to AMP in palte ")]
	public class CADilutb2AmpInNewPlate:RunRobotState
	{
		
		
		int LicInd;
		int NewLicInd;
		int FreezeIndex;
		
		public CADilutb2AmpInNewPlate(int LicInd,int NewLicInd,int FreezeIndex):base()
		{
			this.LicInd = LicInd;
			this.NewLicInd = 	NewLicInd ;
			this.FreezeIndex =FreezeIndex;
		}

		protected override RobotJob BeforeRobotRun()
		{
			List<RobotJobParameter> RJP = new List<RobotJobParameter>(2);
			
			LicPos LP = Utils.Ind2LicPos(LicInd);
			LicPos NLP = Utils.Ind2LicPos(NewLicInd);
			
			
			RJP.Add(new RobotJobParameter("Liconic6PlateCart",RobotJobParameter.ParameterType.Number,LP.Cart));
			RJP.Add(new RobotJobParameter("Liconic6PlatePos",RobotJobParameter.ParameterType.Number,LP.Pos));
			RJP.Add(new RobotJobParameter("LiconicNew6PlateCart",RobotJobParameter.ParameterType.Number,NLP.Cart));
			RJP.Add(new RobotJobParameter("LiconicNew6PlatePos",RobotJobParameter.ParameterType.Number,NLP.Pos));
			RJP.Add(new RobotJobParameter("FreezeIndex",RobotJobParameter.ParameterType.Number,FreezeIndex));
			
			RobotJob RJ = new RobotJob(
				@"D:\OctoTip\Protocols\CyclingInAmp\Scripts\Dilutb2AmpInNewPlate.esc",RJP);
			
			return RJ;
		}
		
		protected override void AfterRobotRun()
		{
			
		}
		
		
		#region static
		public static new List<Type> NextStates()
		{
			return new List<Type>{typeof(CAKill)};
		}
		#endregion

	}
}
