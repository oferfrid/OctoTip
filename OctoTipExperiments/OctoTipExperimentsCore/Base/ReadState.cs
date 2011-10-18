/*
 * Created by SharpDevelop.
 * User: Tecan
 * Date: 16/10/2011
 * Time: 16:50
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Xml.XPath;

namespace OctoTip.OctoTipExperiments.Core.Base
{
	/// <summary>
	/// Description of ReadState.
	/// </summary>
	public abstract class ReadState:RunRobotState
	{
		
		public ReadState(Protocol RunningInProtocol):base(RunningInProtocol)
		{}			
		
		protected sealed override void AfterRobotRun()
		{
			DirectoryInfo DI = new DirectoryInfo(ConfigurationManager.AppSettings["ReaderFilesDirectory"]);
			
			FileSystemInfo[]  FSI = DI.GetFileSystemInfos("*.xml", SearchOption.TopDirectoryOnly);
			Dictionary<int, List<double>> MeasurementsResults;

			if(FSI.Length>0)
			{
				
				
				//Find the last File!
				FileSystemInfo LastFileInfo = FSI[0];
				foreach (FileSystemInfo FI in FSI)
				{
					if(FI.CreationTime>LastFileInfo.CreationTime)
					{
						LastFileInfo = FI;
					}
				}
				
				
			
				
				
				XPathDocument document = new XPathDocument(new FileInfo(LastFileInfo.FullName).Open(FileMode.Open));
				
				XPathNavigator navigator = document.CreateNavigator();


				XPathNodeIterator DataNodes = navigator.Select("MeasurementResultData/Section/Data"); //reads
				MeasurementsResults = new Dictionary<int, List<double>>(6);
				
				foreach (XPathNavigator DataNode in DataNodes)
				{
					
					foreach (XPathNavigator node in DataNode.SelectChildren("Well",""))
					{	
						node.MoveToAttribute("Pos",string.Empty);
						int WellInd =  CalcIndFromPlatePos(node.Value);
						if(!MeasurementsResults.ContainsKey(WellInd))
						{
							MeasurementsResults.Add(WellInd,new List<double>(5));
						}
						node.MoveToParent();
						MeasurementsResults[WellInd].Add(Convert.ToDouble(node.Value));
					}


				}

				
				
				
				
			}
			else
			{
				throw new Exception (string.Format("No Reder file found in {0}",DI));
			}
			
			AfterRobotRun(MeasurementsResults);
		}
		
		protected abstract void AfterRobotRun(Dictionary<int, List<double>> MeasurementsResults);
		
		 private int CalcIndFromPlatePos(string Pos)
		{
			int Row = char.Parse(Pos.Substring(0,1))-'A';
			int Col =  Convert.ToInt32(Pos.Substring(1,Pos.Length-1))-1;
			return Row + Col*2+1;
		}
		
	}
}
