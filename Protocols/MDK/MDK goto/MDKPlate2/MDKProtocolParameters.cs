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

namespace MDKPlate2
{
	/// <summary>
	/// Description of SDONEProtocolParameters.
	/// </summary>
	public class MDKProtocolParameters:ProtocolParameters
	{
		public MDKProtocolParameters()
		{
		}
		
		[ProtocolParameterAtribute("Current liconic plate index","34",true)]
		public int LicPlatePosition;
		[ProtocolParameterAtribute("Bacteria eppendorf index","1",true)]
		public int GermIndex;
		[ProtocolParameterAtribute("Beta-Lac eppendorf index","2",true)]
		public int BLacIndex;
		[ProtocolParameterAtribute(@"Antibiotic concentration in truogh (ug/ml)","500",true)]
		public double TroughConcentration;
		[ProtocolParameterAtribute(@"Minimum antibiotic concentration (ug/ml)","100",true)]
		public double MinConcentration;
		[ProtocolParameterAtribute(@"Maximum antibiotic concentration (ug/ml)","400",true)]
		public double MaxConcentration;
		[ProtocolParameterAtribute("Minimum time in antibiotic (hours)","1",true)]
		public double MinTime;
		[ProtocolParameterAtribute("Maximum time in antibiotic (hours)","40",true)]
		public double MaxTime;
		[ProtocolParameterAtribute("Final incubation time (hours)","12",true)]
		public double FinIncTime;
		[ProtocolParameterAtribute("Current inoculation cycle","0",true)]
		public int InoculateCycle;
		[ProtocolParameterAtribute("Log file path",@"D:\OctoTip\Protocols\MDK\MDKPlate2\Output\")]
		public string OutputFilePath;
		[ProtocolParameterAtribute("Shared Resources file path",@"D:\OctoTip\Protocols\MDK\MDKPlate2\SharedResources\")]
		public string SharedResourcesFilePath;
		
		
		public override bool IsValid()
		{
			if(LicPlatePosition<1||LicPlatePosition>34)
			{
				return false;
			}
			
			if(GermIndex<1||GermIndex>24)
			{
				return false;
			}
			
			if(BLacIndex<1||BLacIndex>24)
			{
				return false;
			}
			
			if((MinConcentration/TroughConcentration)<0.05||MinConcentration>MaxConcentration)
			{
				return false;
			}
			
			if((MaxConcentration/TroughConcentration)>0.9)
			{
				return false;
			}
			
			if(MinTime>MaxTime)
			{
				return false;
			}
			
			if(InoculateCycle<0)
			{
				return false;
			}
			
			return true;
		}
		
		public override string GetErrorMessege()
		{
			string ErrorMsg = "";
			
			if(LicPlatePosition<1||LicPlatePosition>34)
			{
				ErrorMsg += string.Format("Plate position {0} not in range (1-34)/n",LicPlatePosition);
			}
			
			if(GermIndex<1||GermIndex>24)
			{
				ErrorMsg += string.Format("Eppendorf position {0} not in range (1-24)/n",GermIndex);
			}
			
			if(BLacIndex<1||BLacIndex>24)
			{
				ErrorMsg += string.Format("Eppendorf position {0} not in range (1-24)/n",BLacIndex);
			}
			
			if((MinConcentration/TroughConcentration)<0.05)
			{
				ErrorMsg += string.Format("Minimum concentration {0} too small (must be >5% of trough concentration)\n",MinConcentration);
			}
			
			if(MinConcentration>MaxConcentration)
			{
				ErrorMsg += string.Format("Minimum concentration {0} greater than Maximum concentration {1}\n",MinConcentration,MaxConcentration);
			}
			
			if((MaxConcentration/TroughConcentration)>0.9)
			{
				ErrorMsg += string.Format("Maximum concentration {0} too large (must be <90% of trough concentration)\n",MaxConcentration);
			}
			
			if(MinTime>MaxTime)
			{
				ErrorMsg += string.Format("Minimum incubation time {0} greater than maximum time {1}",MinTime,MaxTime);
			}
			
			if(InoculateCycle<0)
			{
				ErrorMsg += string.Format("Inoculation cycle {0} not in range (0-8)/n",InoculateCycle);
			}
			
			return ErrorMsg;
		}
	}
}
