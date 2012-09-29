/*
 * Created by SharpDevelop.
 * User: Tecan
 * Date: 20/09/2012
 * Time: 15:09
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;
using OctoTip.Lib;
using OctoTip.Lib.ExperimentsCore.Attributes;
using OctoTip.Lib.ExperimentsCore.Base;
using OctoTip.Lib.ExperimentsCore.Interfaces;

namespace CalibrateL1210Growth
{
	/// <summary>
	/// Description of CLGReadState.
	/// </summary>
	[State("Read","Read 6 plate OD")]
	public class CLGReadState:RunRobotState,IRestartableState
	{
		
		
		protected override OctoTip.Lib.RobotJob BeforeRobotRun()
		{
			RobotJob RJ = new RobotJob(@"D:\OctoTip\Protocols\CalibrateL1210Growth\Scripts\ReadOD.esc");
			return RJ;
		}
		
		protected override void AfterRobotRun()
		{
			
		}
		
		#region static
		public static new List<Type> NextStates()
		{
			return new List<Type>{typeof(CLGIncubateState)};
		}
		#endregion
	
		public void Restart()
		{
			this.Start();
		}
	}
}
