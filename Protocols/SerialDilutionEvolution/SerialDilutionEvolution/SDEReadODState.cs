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
using OctoTip.Lib.ExperimentsCore.Attributes;
using OctoTip.Lib.ExperimentsCore.Base;
using OctoTip.Lib.ExperimentsCore.Interfaces;

namespace SerialDilutionEvolution
{
	/// <summary>
	/// Description of ReadBGState.
	/// </summary>
	[State("Read OD","Read well OD")]
	public class SDEReadODState:ReadState,IRestartableState
	{
		double _OD;
		int WellInd;
		public SDEReadODState(int WellInd):base()
		{
			this.WellInd = WellInd;
		}
		
		protected override OctoTip.Lib.RobotJob BeforeRobotRun()
		{
			throw new NotImplementedException();
		}
		
		protected override void AfterRobotRun()
		{
			Dictionary<int, List<double>> MeasurementsResults = this.GetMeasurementsResults();
			_OD = MeasurementsResults[WellInd].Average();
		}
		
		public double OD
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
			return new List<Type>{typeof(SDEWait2ODState),typeof(SDEDiluteState),typeof(SDEDilute2NewPlateState)};
		}
		#endregion
		
	}
}
