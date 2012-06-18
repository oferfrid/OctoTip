/*
 * Created by SharpDevelop.
 * User: Irit
 * Date: 18/12/11
 * Time: 11:23 AM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Xml;
using System.Xml.XPath;
using OctoTip.Lib;
using OctoTip.Lib.ExperimentsCore.Attributes;
using OctoTip.Lib.ExperimentsCore.Base;
 
namespace IncubateReadin384
{
	/// <summary>
	/// Description of IRReadState.
	/// </summary>
	[State("Read state","Reading in Infinite")]
	public class IRReadState:OctoTip.Lib.ExperimentsCore.Base.ReadState
	{
//		string Path;
		
		int LicInd;
		FileInfo ResultFileInfo;
		
		public IRReadState(int LicInd):base()
		{
			this.LicInd = LicInd;
		}
		
		protected override RobotJob BeforeRobotRun()
		{
			List<RobotJobParameter> RJP = new List<RobotJobParameter>(2);
			
			LicPos LP = Utils.Ind2LicPos(LicInd);
			
			RJP.Add(new RobotJobParameter("PlateCart",RobotJobParameter.ParameterType.Number,LP.Cart));
			RJP.Add(new RobotJobParameter("PlatePos",RobotJobParameter.ParameterType.Number,LP.Pos));
			RobotJob RJ = new RobotJob(
				@"D:\OctoTip\Protocols\IncubateReadin384\Scripts\IRRead384.esc",RJP);
			return RJ;
		}
		
		
		
		protected override void AfterRobotRun()
		{
			
			
			//rename the results file
			
			try
			{
				ResultFileInfo = GetMeasurementsResultsFile();
			}
			catch (Exception ex)
			{
				throw(new Exception("Unable to GetMeasurementsResultsFile!",ex));
			}
			
			
	}
	
	public FileInfo GetReadResultFileInfo()
	{
		return ResultFileInfo;
	}
	
	
	
	#region static
	public static new List<Type> NextStates()
	{
		return new List<Type>{typeof(IRIncubateState)};
	}
	#endregion
	
	
	
	
}
}
