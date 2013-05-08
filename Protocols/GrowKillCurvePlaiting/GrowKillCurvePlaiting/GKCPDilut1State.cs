/*
 * Created by SharpDevelop.
 * User: oferfrid
 * Date: 19/02/2013
 * Time: 15:16
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;
using OctoTip.Lib.ExperimentsCore.Attributes;

namespace GrowKillCurvePlaiting
{
	/// <summary>
	/// Description of GKCPDilut1.
	/// </summary>
	[State("Dilut1","Dilut 1")]
	public class GKCPDilut1State:GKCPDilut
	{
			public GKCPDilut1State(int LicInd,int NumberOfSamples,string SharedResourcesFilePath):base( LicInd, 2, NumberOfSamples, SharedResourcesFilePath)
			{}
			
			#region static
		public static new List<Type> NextStates()
		{
			return new List<Type>{typeof(GKCPReadBackroundState)};
		}
		#endregion
	}
}
