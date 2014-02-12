/*
 * Created by SharpDevelop.
 * User: Tecan
 * Date: 11/02/2014
 * Time: 10:28
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;
using OctoTip.Lib;
using OctoTip.Lib.ExperimentsCore.Attributes;
using OctoTip.Lib.ExperimentsCore.Base;
using OctoTip.Lib.ExperimentsCore.Interfaces;

namespace MDK99
{
	/// <summary>
	/// Description of MDKIncubateState.
	/// </summary>
	/// 
	[State("Incubation","Incubation of plate")]
	public class MDKIncubateState:WaitState
	{
		public MDKIncubateState(double Hours2Wait):base(DateTime.Now.AddHours(Hours2Wait))
		{
		}
		
		protected override void OnWaitStart()
		{
			//throw new NotImplementedException();
		}
		
		protected override void OnWaitEnd()
		{
			//throw new NotImplementedException();
		}
		
		#region static
		public static new List<Type> NextStates()
		{
			return new List<Type>{typeof(MDKInoculateState),typeof(MDKDeactivateState)};
		}
		#endregion
	}
}
