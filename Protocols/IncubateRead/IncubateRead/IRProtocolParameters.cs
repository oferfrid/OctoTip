/*
 * Created by SharpDevelop.
 * User: Irit
 * Date: 18/12/11
 * Time: 11:37 AM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using OctoTip.Lib.ExperimentsCore.Attributes;
using OctoTip.Lib.ExperimentsCore.Base;

namespace IncubateRead
{
	/// <summary>
	/// Description of IRProtocolParameters.
	/// </summary>
	public class IRProtocolParameters:ProtocolParameters
	{
		[ProtocolParameterAtribute("Index in Liconic","17",true)]
		public int LicInd;
		[ProtocolParameterAtribute("Total Growth time (hr)","24",true)]
		public double TotalTime;
		[ProtocolParameterAtribute("Frequency of reading (min)","2",true)]
		public double ReadFrequency;
		[ProtocolParameterAtribute("Type of plate (e.g. 6,12,24,96...)","24",true)]
		public int TypeOfPlate;
		[ProtocolParameterAtribute("Type of measurments {OD,YFP,Cherry}","OD",true)]
		public string TypeOfMeasurment;
		[ProtocolParameterAtribute("Start Round","1",true)]
		public int StartRound;
		[ProtocolParameterAtribute("Results file path and name",@"D:\OctoTip\Protocols\IncubateRead\Output\Out.csv")]
		public string OutputFile;
		
		public override bool IsValid()
		{
			bool PValid = true;
			if (!(TypeOfPlate==6 || TypeOfPlate==12 || TypeOfPlate==24 || TypeOfPlate==96))
			{ 
				PValid = false;
			}
			if (!(TypeOfMeasurment.ToUpper()=="OD" || TypeOfMeasurment.ToUpper()=="YFP" || TypeOfMeasurment.ToUpper()=="CHERRY"))
			{
				PValid = false;
			}
			return PValid;
		}
		
		public override string GetErrorMessage()
		{
			string ErrMsg = string.Empty;
			if (!(TypeOfPlate==6 || TypeOfPlate==12 || TypeOfPlate==24 || TypeOfPlate==96))
			{ 
				ErrMsg = "Type of plate can be only: {6,12,24,96,384}";
			}
			if (!(TypeOfMeasurment.ToUpper()=="OD" || TypeOfMeasurment.ToUpper()=="YFP" || TypeOfMeasurment.ToUpper()=="CHERRY"))
			{
				ErrMsg = ErrMsg + "\nType of measurment can be only: {OD,YFP,CHERRY}";
			}
			return ErrMsg;
		}
	}
}
