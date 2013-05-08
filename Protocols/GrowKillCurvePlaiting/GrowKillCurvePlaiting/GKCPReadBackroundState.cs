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
using OctoTip.Lib;
using OctoTip.Lib.ExperimentsCore.Attributes;
using OctoTip.Lib.ExperimentsCore.Base;
using OctoTip.Lib.ExperimentsCore.Interfaces;

namespace GrowKillCurvePlaiting
{
	/// <summary>
	/// Description of ReadBGState.
	/// </summary>
	[State("Read Backround","Read well Backround OD")]
	public class GKCPReadBackroundState:GKCPReadODState
	{

		public GKCPReadBackroundState (int LicPlateInd , int RowInd,int NumberOfExpSamples):base( LicPlateInd ,  RowInd, NumberOfExpSamples)
		{}
		
		
		public double[] BackroundOD
		{
			get { return OD; }
			
		}
		#region static
		public static new List<Type> NextStates()
		{
			return new List<Type>{typeof(GKCPWait2ODReadState)};
		}
		#endregion
		
	}
}
