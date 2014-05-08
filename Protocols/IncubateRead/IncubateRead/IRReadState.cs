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
using OctoTip.Lib.Utils;

namespace IncubateRead
{
	/// <summary>
	/// Description of IRReadState.
	/// </summary>
	[State("Read state","Reading in Infinite")]
	public class IRReadState:OctoTip.Lib.ExperimentsCore.Base.ReadState
	{
//		string Path;
		
		int ReadPlateFirstInd;
		int LicInd;
		FileInfo OutputFile;
		string ScriptName;
		
		double[] ReadResults = new double[2];
			

		public IRReadState(int ReadPlateFirstInd,int LicInd,string OutputFilePath, int NWells, string MeasurmentType):base()
		{
			this.ReadPlateFirstInd = ReadPlateFirstInd;
			this.LicInd = LicInd;
			this.OutputFile = new FileInfo(OutputFilePath);
			this.ScriptName = @"D:\RobotScripts\Irit\IncubateRead\Scripts\IRRead" +MeasurmentType +NWells.ToString()+@".esc";
		}
		
		protected override RobotJob BeforeRobotRun()
		{
			List<RobotJobParameter> RJP = new List<RobotJobParameter>(2);
			
			LicPos LP = Utils.Ind2LicPos(LicInd);
			
			RJP.Add(new RobotJobParameter("PlateCart",RobotJobParameter.ParameterType.Number,LP.Cart));
			RJP.Add(new RobotJobParameter("PlatePos",RobotJobParameter.ParameterType.Number,LP.Pos));
			RJP.Add(new RobotJobParameter("ReadPlateFirstInd",RobotJobParameter.ParameterType.Number,ReadPlateFirstInd));
			
			RobotJob RJ = new RobotJob(ScriptName,RJP);
			
			return RJ;
		}
		
		
		
		protected override void AfterRobotRun()
		{
			
			
			//rename the results file
			
			try
			{
			FileInfo MyFileInfo = GetMeasurementsResultsFile();
			}
			 catch (Exception ex) 
			{
				throw(new Exception("Unable to GetMeasurementsResultsFile!",ex));
			}
			
			
			
//			
//			string NewFileName = "IR6" + @"_" +
//				String.Format("{0:yyyyMMddHHmm}", DateTime.Now) + @".xml";
//			try
//			{
//				myLogger.Add("in try " + MyFileInfo.FullName);
//				MyFileInfo.MoveTo(MyFileInfo.Directory.FullName + @"\" + NewFileName);
//			} catch (Exception ex) 
//			{
//				throw(new Exception("Unable to move file!",ex));
//			}
			
			//add output to result file
			XPathDocument  ResultsXPathDocument = this.GetXPathMeasurementsResults();
			
			
			XPathNavigator navigator = ResultsXPathDocument.CreateNavigator();

			
			XPathNodeIterator DataNodes = navigator.Select("MeasurementResultData/Section/Data"); //reads
			
			
			
			
			foreach (XPathNavigator DataNode in DataNodes)
			{
				
				foreach (XPathNavigator node in DataNode.SelectChildren("Well",""))
				{
					node.MoveToAttribute("Pos",string.Empty);
					
					int WellInd =  CalcIndFromPlatePos(node.Value);
					if (ReadPlateFirstInd == WellInd)
					{
						node.MoveToParent();
						Log(WellInd + "=" + node.Value);
						ReadResults[0]=Convert.ToDouble(node.Value);
					}
					if (ReadPlateFirstInd + 192 == WellInd)
					{
						node.MoveToParent();
						Log(WellInd + "=" + node.Value);
						ReadResults[1]=Convert.ToDouble(node.Value);
					}
				}
				

			}
			
			
			
			
			using (StreamWriter sw = OutputFile.AppendText())
			{
//				for (int i=0;i<ReadResults[0].Count;i++)
//				{
					sw.WriteLine("{0:dd/MM/yyyy HH:mm:ss}\t{1:0.0000}\t{2:0.0000}" ,DateTime.Now,ReadResults[0],ReadResults[1]);
//				}	
			}			
			
//
		}
		
		public double[] GetReadResult()
		{
			return ReadResults;
		}
		
		private int CalcIndFromPlatePos(string Pos)
		{
			//384
			int Row = char.Parse(Pos.Substring(0,1))-'A';
			int Col =  Convert.ToInt32(Pos.Substring(1,Pos.Length-1))-1;
			int ind = Row + Col*4*4+1;
			return ind;
		}
		
		
		#region static
		public static new List<Type> NextStates()
		{
			return new List<Type>{typeof(IRIncubateState)};
		}
		#endregion
		
		
		
		
	}
}
