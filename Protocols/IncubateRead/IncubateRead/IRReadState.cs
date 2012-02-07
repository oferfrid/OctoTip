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
using System.Xml;
using System.Xml.XPath;
using OctoTip.Lib;
using OctoTip.Lib.ExperimentsCore.Attributes;
using OctoTip.Lib.ExperimentsCore.Base;

namespace IncubateRead
{
	/// <summary>
	/// Description of IRReadState.
	/// </summary>
	[State("Read state","Reading in Infinite")]
	public class IRReadState:OctoTip.Lib.ExperimentsCore.Base.ReadState
	{
//		string Path;
		
		new IRProtocol RunningInProtocol
		{
			get
			{return (IRProtocol) base.RunningInProtocol;}
		}
	
		
		public IRReadState(IRProtocol RunningInIRProtocol):base((Protocol)RunningInIRProtocol)
		{
//			Path = 	RunningInIRProtocol.ProtocolParameters.Path;
		}
		
		protected override RobotJob BeforeRobotRun()
		{
			List<RobotJobParameter> RJP = new List<RobotJobParameter>(2);
			
			LicPos LP = Utils.Ind2LicPos(RunningInProtocol.ProtocolParameters.LicInd);
			
			RJP.Add(new RobotJobParameter("PlateCart",RobotJobParameter.ParameterType.Number,LP.Cart));
			RJP.Add(new RobotJobParameter("PlatePos",RobotJobParameter.ParameterType.Number,LP.Pos));
			        
			RobotJob RJ = new RobotJob(
				@"D:\RobotScripts\Irit\IncubateRead\IRRead"+RunningInProtocol.ProtocolParameters.PlateType.ToString()+".esc"
				,RJP);
			
			return RJ;
		}
		
		protected override void AfterRobotRun()
		{
			FileInfo MyFileInfo = GetMeasurementsResultsFile();
			string NewFileName = "IR" + RunningInProtocol.ProtocolParameters.PlateType.ToString() + @"_" +
				                 RunningInProtocol.ProtocolParameters.LicInd.ToString() + @"_" +
 				                 String.Format("{0:yyyyMMddHHmm}", DateTime.Now) + @".xml";
			try 
			{
				MyFileInfo.MoveTo(MyFileInfo.Directory.FullName + @"\" + NewFileName);
			} catch (Exception ex) {
				throw(ex);
			}
//			
		}
		
		
		#region static
		public static new List<Type> NextStates()
		{
			return new List<Type>{typeof(IRIncubateState)};
		}
		#endregion
		
		
		
	
	}
}
