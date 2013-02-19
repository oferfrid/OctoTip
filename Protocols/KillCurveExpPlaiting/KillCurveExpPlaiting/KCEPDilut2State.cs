/*
 * Created by SharpDevelop.
 * User: oferfrid
 * Date: 19/02/2013
 * Time: 15:17
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;
using OctoTip.Lib.ExperimentsCore.Attributes;

namespace KillCurveExpPlaiting
{
	/// <summary>
	/// Description of KCEPDilut2.
	/// </summary>
	[State("Dilut Exponential","Dilut Exponential")]
	public class KCEPDilut2State:KCEPDilut
	{
		public KCEPDilut2State(int LicInd,int NumberOfExpSamples,string SharedResourcesFilePath):base( LicInd, 3, NumberOfExpSamples, SharedResourcesFilePath)
			{}
		
		#region static
		public static new List<Type> NextStates()
		{
			return new List<Type>{typeof(KCEPReadBackroundState)};
		}
		#endregion
	}
}
