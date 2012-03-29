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

namespace OctoTip.Lib.ExperimentsCore.Base
{
	/// <summary>
	/// Description of ReadState.
	/// </summary>
	public abstract class ReadState:RunRobotState
	{
		
		
		public ReadState():base()
		{}
		
		
		
		protected XPathDocument GetXPathMeasurementsResults()
		{
		return new XPathDocument(GetMeasurementsResultsFile().Open(FileMode.Open));
			
		}
		protected Dictionary<int, List<double>> GetMeasurementsResults()
		{
			
			
			
				
			XPathNavigator navigator = GetXPathMeasurementsResults().CreateNavigator();

			Dictionary<int, List<double>> MeasurementsResults;
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
				
				return MeasurementsResults;
		}
		
		protected FileInfo GetMeasurementsResultsFile()
		{
						const string ImportVariableFunctionName = @"FACTS(""ReaderNETwork"",""ReaderNETwork_Measure""";
			
			string[] ScriptLines = RunRobotJob.Script.Split('\n');
			
			string  Path = string.Empty;
			
			
			for (int l=0; l<ScriptLines.Length;l++)
			{
				
				string Line = ScriptLines[l];
				if (Line.Length > ImportVariableFunctionName.Length  && Line.Substring(0,ImportVariableFunctionName.Length).Equals(ImportVariableFunctionName))
				{
					//FACTS("ReaderNETwork","ReaderNETwork_Measure","D:\RobotScripts\KillingCurve\<YYYY-MM-DD HH-mm-ss>.xml|<TecanFile xmlns:xsi&equal;&quote;http://www.w3.org/2001/XMLSchema-instance&quote; xsi:schemaLocation&equal;&quote;tecan.at.schema.documents Main.xsd&quote; fileformat&equal;&quote;Tecan.At.Measurement&quote; fileversion&equal;&quote;2.0&quote; xmlns&equal;&quote;tecan.at.schema.documents&quote;><FileInfo type&equal;&quote;&quote; instrument&equal;&quote;infinite 200Pro&quote; version&equal;&quote;&quote; createdFrom&equal;&quote;Tecan&quote; createdAt&equal;&quote;2012-01-19T12:33:45.5615Z&quote; createdWith&equal;&quote;Tecan.At.XFluor.ReaderEditor.XFluorReaderEditor&quote; description&equal;&quote;&quote; /><TecanMeasurement id&equal;&quote;1&quote; class&equal;&quote;Measurement&quote;><MeasurementManualCycle id&equal;&quote;2&quote; number&equal;&quote;1&quote; type&equal;&quote;Standard&quote;><CyclePlate id&equal;&quote;3&quote; file&equal;&quote;Thermo_Immulon96ft&quote; plateWithCover&equal;&quote;True&quote;><PlateRange id&equal;&quote;0&quote; range&equal;&quote;A1&tilde;H12&quote; auto&equal;&quote;true&quote;><MeasurementAbsorbance id&equal;&quote;5&quote; mode&equal;&quote;Normal&quote; type&equal;&quote;&quote; name&equal;&quote;ABS&quote; longname&equal;&quote;&quote; description&equal;&quote;&quote;><MeasurementKinetic id&equal;&quote;4&quote; loops&equal;&quote;4&quote; timeSpan&equal;&quote;PT0S&quote; maxDeviation&equal;&quote;PT0S&quote; duration&equal;&quote;PT1M&quote; useDuration&equal;&quote;false&quote;><Well id&equal;&quote;6&quote; auto&equal;&quote;true&quote;><MeasurementReading id&equal;&quote;7&quote; name&equal;&quote;&quote; refID&equal;&quote;4&quote; refName&equal;&quote;KINETIC.RUN.CYCLE&quote; beamDiameter&equal;&quote;500&quote; beamGridType&equal;&quote;Single&quote; beamGridSize&equal;&quote;1&quote; beamEdgeDistance&equal;&quote;auto&quote;><ReadingLabel id&equal;&quote;8&quote; name&equal;&quote;OD&quote; scanType&equal;&quote;ScanFixed&quote; refID&equal;&quote;0&quote;><ReadingSettings number&equal;&quote;25&quote; rate&equal;&quote;25000&quote; /><ReadingTime integrationTime&equal;&quote;0&quote; lagTime&equal;&quote;0&quote; readDelay&equal;&quote;0&quote; flash&equal;&quote;0&quote; dark&equal;&quote;0&quote; /><ReadingFilter id&equal;&quote;0&quote; type&equal;&quote;Ex&quote; wavelength&equal;&quote;6300&quote; bandwidth&equal;&quote;100&quote; attenuation&equal;&quote;0&quote; usage&equal;&quote;ABS&quote; /></ReadingLabel></MeasurementReading></Well><CustomData id&equal;&quote;9&quote; /></MeasurementKinetic></MeasurementAbsorbance></PlateRange></CyclePlate></MeasurementManualCycle><MeasurementInfo id&equal;&quote;0&quote; description&equal;&quote;&quote;><ScriptTemplateSettings id&equal;&quote;0&quote;><ScriptTemplateGeneralSettings id&equal;&quote;0&quote; Title&equal;&quote;&quote; Group&equal;&quote;&quote; Info&equal;&quote;&quote; Image&equal;&quote;&quote; /><ScriptTemplateDescriptionSettings id&equal;&quote;0&quote; Internal&equal;&quote;&quote; External&equal;&quote;&quote; IsExternal&equal;&quote;False&quote; /></ScriptTemplateSettings></MeasurementInfo></TecanMeasurement></TecanFile>","0","");
					int FilePathEnd	 = Line.IndexOf('|') - 1;
					
					string FilePath =  Line.Substring(ImportVariableFunctionName.Length+2, FilePathEnd - ImportVariableFunctionName.Length - 1 );
					Path = FilePath.Substring(0, FilePath.LastIndexOf(@"\"));
					
					break;
				}
			}
			
						
			DirectoryInfo DI = new DirectoryInfo(Path) ;
			
			
			
			FileSystemInfo[]  FSI = DI.GetFileSystemInfos("*.xml", SearchOption.TopDirectoryOnly);
	
			FileSystemInfo LastFileInfo;
			
			if(FSI.Length==0)
			{
				throw new FileNotFoundException (string.Format("No Reader XML file found in {0}",Path));
			}
			else
			{
				//Find the last File!
				LastFileInfo = FSI[0];
				foreach (FileSystemInfo FI in FSI)
				{
					if(FI.CreationTime>LastFileInfo.CreationTime)
					{
						LastFileInfo = FI;
					}
				}
				
				return new FileInfo(LastFileInfo.FullName);
			}
		}
		

	
		private int CalcIndFromPlatePos(string Pos)
		{
			int Row = char.Parse(Pos.Substring(0,1))-'A';
			int Col =  Convert.ToInt32(Pos.Substring(1,Pos.Length-1))-1;
			return Row + Col*4*4+1;
		}
		
	}
}
