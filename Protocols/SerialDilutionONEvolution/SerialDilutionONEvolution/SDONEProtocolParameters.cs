/*
 * Created by SharpDevelop.
 * User: oferfrid
 * Date: 01/11/2012
 * Time: 16:22
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
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
		[ProtocolParameterAtribute("Time Till Next Dilution (Hours)","24",true)]
		public double Time4Dilution;
		[ProtocolParameterAtribute("Log file path",@"D:\OctoTip\Protocols\SerialDilutionONEvolution\Output\")]
		public string OutputFilePath;
		[ProtocolParameterAtribute("Shared Resources file path",@"D:\OctoTip\Protocols\SerialDilutionONEvolution\SharedResources\")]
		public string SharedResourcesFilePath;
		
		
		public override bool IsValid()
		{
			//throw new NotImplementedException();
			return true;
		}
		
		
		public override string GetErrorMessage()
		{
			throw new NotImplementedException();
		}
	}
}
