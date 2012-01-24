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
using OctoTip.OctoTipExperiments.Core.Attributes;
using OctoTip.OctoTipExperiments.Core.Base;
using OctoTip.OctoTipLib;

namespace IncubateRead
{
	/// <summary>
	/// Description of IRReadState.
	/// </summary>
	[State("Read state","Reading in Infinite")]
	public class IRReadState:OctoTip.OctoTipExperiments.Core.Base.ReadState
	{
		string Path;
		
		new IRProtocol RunningInProtocol
		{
			get
			{return (IRProtocol) base.RunningInProtocol;}
		}
	
		
		public IRReadState(IRProtocol RunningInIRProtocol):base((Protocol)RunningInIRProtocol)
		{
			Path = 	RunningInIRProtocol.ProtocolParameters.Path;
		}
		
		protected override OctoTip.OctoTipLib.RobotJob BeforeRobotRun()
		{
			List<RobotJobParameter> RJP = new List<RobotJobParameter>(2);
			
			LicPos LP = Utils.Ind2LicPos(RunningInProtocol.ProtocolParameters.LicInd);
			
			RJP.Add(new RobotJobParameter("PlateCart",RobotJobParameter.ParameterType.Number,LP.Cart));
			RJP.Add(new RobotJobParameter("PlatePos",RobotJobParameter.ParameterType.Number,LP.Pos));
			        
			RobotJob RJ = new RobotJob(@"D:\RobotScripts\Irit\IncubateRead\IRRead.esc",RJP);
			
			return RJ;
		}
		
		protected override void AfterRobotRun()
		{
//			XPathDocument MeasurementsResults = GetXPathMeasurementsResults();
//			XPathNavigator navigator    = MeasurementsResults.CreateNavigator();			
//			XPathNodeIterator DataNodes = XPathNavigator.Select("MeasurementResultData/Section/Data"); //reads
//			MeasurementsResults = new Dictionary<int, List<double>>(6);
//			
//			foreach (XPathNavigator DataNode in DataNodes)
//			{
//				
//				foreach (XPathNavigator node in DataNode.SelectChildren("Well",""))
//				{	
//					
//					node.MoveToAttribute("Pos",string.Empty);
//					int WellInd =  CalcIndFromPlatePos(node.Value);
//					if(!MeasurementsResults.ContainsKey(WellInd))
//					{
//						MeasurementsResults.Add(WellInd,new List<double>(5));
//					}
//					node.MoveToParent();
//					MeasurementsResults[WellInd].Add(Convert.ToDouble(node.Value));
//					
//				}
//			}
		}
		
		
		#region static
		public static new List<Type> NextStates()
		{
			return new List<Type>{typeof(IRIncubateState)};
		}
		#endregion
		
		
		
	
	}
}
