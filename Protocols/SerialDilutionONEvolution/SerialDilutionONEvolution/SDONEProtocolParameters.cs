/*
 * Created by SharpDevelop.
 * User: oferfrid
 * Date: 01/11/2012
 * Time: 16:22
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Linq;
using OctoTip.Lib.ExperimentsCore.Attributes;
using OctoTip.Lib.ExperimentsCore.Base;

namespace SerialDilutionONEvolution
{
	/// <summary>
	/// Description of SDONEProtocolParameters.
	/// </summary>
	public class SDONEProtocolParameters:ProtocolParameters
	{
		public SDONEProtocolParameters()
		{
		}
		[ProtocolParameterAtribute("Cycle","0",true)]
		public int Cycle;
		[ProtocolParameterAtribute("Liconic curent plate Index","1",true)]
		public int LicPlatePosition;
		[ProtocolParameterAtribute("Curent well","1",true)]
		public int CurentWell;
		[ProtocolParameterAtribute("Wells to freeze","1,4,6",true)]
		public int[] FreezeWells;
		[ProtocolParameterAtribute("Time till next dilution (Hours)","24",true)]
		public double Time4Dilution;
		[ProtocolParameterAtribute("Use reader?","false",true)]
		public bool UseReader;
		[ProtocolParameterAtribute("Time between reads (Hours)","1",true)]
		public double TimeBetweenReads;
		[ProtocolParameterAtribute("Shared Resources file path",@"D:\OctoTip\OctoTipPlus\SharedResources\")]
		public string SharedResourcesFilePath;

		
		
		public override bool IsValid()
		{
			
			if (FreezeWells.Length>0)
			{
				if (FreezeWells.Max()>24 || FreezeWells.Min()<1)
				{
					return false;
				}
			}
			if (LicPlatePosition>24||LicPlatePosition<1)
				{
					return false;
				}
			return true;
		}
		
		
		
		
		
		public override string GetErrorMessage()
		{
			string ErrorMessage= string.Empty;
			if (FreezeWells.Length>0)
			{
				if (FreezeWells.Max()>24 || FreezeWells.Min()<0)
				{
					ErrorMessage+= "FreezeWells should be smaller than 24 and larger than 0\n";
				}
			}
			if (LicPlatePosition>24||LicPlatePosition<1)
			{
				ErrorMessage+= "Liconic plate position should be smaller than 24 and larger than 0\n";;
			}
			return ErrorMessage;
		}
	}
}
