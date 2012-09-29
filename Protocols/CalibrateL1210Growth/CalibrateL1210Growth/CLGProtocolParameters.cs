/*
 * Created by SharpDevelop.
 * User: Tecan
 * Date: 20/09/2012
 * Time: 15:31
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using OctoTip.Lib.ExperimentsCore.Attributes;
using OctoTip.Lib.ExperimentsCore.Base;

namespace CalibrateL1210Growth
{
	/// <summary>
	/// Description of CLGProtocolParameters.
	/// </summary>
	public class CLGProtocolParameters:ProtocolParameters
	{
		public CLGProtocolParameters()
		{
		}	
		[ProtocolParameterAtribute("Num of samples","6",true)]
		public int NumOfSamples;
		[ProtocolParameterAtribute("Liconic Next Plate ind","1",true)]
		public int Liconic96Plate;
		[ProtocolParameterAtribute("Read Frequency (min)","15",true)]
		public double ReadFrequency;
		[ProtocolParameterAtribute("Num Of Reads Between Samples","32",true)]
		public int NumReadsBetweenSamples;
		[ProtocolParameterAtribute("Log file path",@"D:\OctoTip\Protocols\CalibrateL1210Growth\Output\")]
		public string OutputFilePath;
		
	}
}
