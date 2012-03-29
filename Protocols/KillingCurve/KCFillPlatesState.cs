/*
 * Created by SharpDevelop.
 * User: Irit
 * Date: 11/01/12
 * Time: 12:28 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;
using OctoTip.Lib;
using OctoTip.Lib.ExperimentsCore.Attributes;
using OctoTip.Lib.ExperimentsCore.Base;

namespace KillingCurve
{
	/// <summary>
	/// Description of KCFillPlatesState.
	/// </summary>
	[State("Fill Plates state","Fills 96 wells plate with 180ul per well")]
	public class KCFillPlatesState:RunRobotState
	{
		new KCProtocol RunningInProtocol
		{
			get
			{return (KCProtocol) base.RunningInProtocol;}
		}
		
		public KCFillPlatesState(KCProtocol RunningInIRProtocol):base((Protocol)RunningInIRProtocol)
		{
		}
		
		protected override RobotJob BeforeRobotRun()
		{
			throw new NotImplementedException();
		}
		
		protected override void AfterRobotRun()
		{
			throw new NotImplementedException();
		}
		
		#region static
		public static new List<Type> NextStates()
		{
			return new List<Type>{typeof(KCInoculateCultureState)};
		}
		#endregion
	}
}
