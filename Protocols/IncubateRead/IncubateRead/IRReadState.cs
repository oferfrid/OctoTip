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
		int Plate364PlatePositionIndex;
		FileInfo OutputFile;
		
		List<double>[] ReadResults = new List<double>[2];
			
		
		
		public IRReadState(int ReadPlateFirstInd,int LicInd,int Plate364PlatePositionIndex,string OutputFilePath):base()
		{
			this.ReadPlateFirstInd = ReadPlateFirstInd;
			this.LicInd = LicInd;
			this.Plate364PlatePositionIndex = Plate364PlatePositionIndex;
			this.OutputFile = new FileInfo(OutputFilePath);
			
			
			ReadResults[0] = new List<double>(10);
			ReadResults[1] = new List<double>(10);

		}
		
		protected override RobotJob BeforeRobotRun()
		{
			List<RobotJobParameter> RJP = new List<RobotJobParameter>(2);
			
			LicPos LP = Utils.Ind2LicPos(LicInd);
			
			RJP.Add(new RobotJobParameter("PlateCart",RobotJobParameter.ParameterType.Number,LP.Cart));
			RJP.Add(new RobotJobParameter("PlatePos",RobotJobParameter.ParameterType.Number,LP.Pos));
			
			RJP.Add(new RobotJobParameter("ReadPlateInd",RobotJobParameter.ParameterType.Number,Plate364PlatePositionIndex));
			RJP.Add(new RobotJobParameter("ReadPlateFirstInd",RobotJobParameter.ParameterType.Number,ReadPlateFirstInd));
			
			
			
			RobotJob RJ = new RobotJob(
				@"D:\OctoTip\Protocols\IncubateRead\Scripts\IRRead6.esc",RJP);
			
			return RJ;
		}
		
		
		
		protected override void AfterRobotRun()
		{
			
			//rename the results file
			FileInfo MyFileInfo = GetMeasurementsResultsFile();
			string NewFileName = "IR6" + @"_" +
				String.Format("{0:yyyyMMddHHmm}", DateTime.Now) + @".xml";
			try
			{
				MyFileInfo.MoveTo(MyFileInfo.Directory.FullName + @"\" + NewFileName);
			} catch (Exception ex) {
				throw(ex);
			}
			
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
						ReadResults[0].Add(Convert.ToDouble(node.Value));
					}
					if (ReadPlateFirstInd + 192 == WellInd)
					{
						node.MoveToParent();
						ReadResults[1].Add(Convert.ToDouble(node.Value));
					}
				}
				

			}
			
			using (StreamWriter sw = OutputFile.AppendText())
			{
				for (int i=0;i<ReadResults[0].Count;i++)
				{
					sw.WriteLine("{0:dd/MM/yyyy HH:mm:ss}\t{1}\t{2}" ,DateTime.Now,ReadResults[0][i],ReadResults[1][i]);
				}	
			}			
			
//
		}
		
		public double[] GetReadResult()
		{
			return new double[2]{ReadResults[1].Average(),	ReadResults[2].Average()};
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
