/*
 * Created by SharpDevelop.
 * User: oferfrid
 * Date: 21/09/2011
 * Time: 11:28
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.IO;
using System.Collections.Generic;

namespace OctoTip.OctoTipLib
{
	/// <summary>
	/// Description of RobotJob.
	/// </summary>
	public class RobotJob
	{
		private enum ImpVarParams {Name=0, File=1, Type=2, DefaultValue=3, HasHeader=8};
		
		FileInfo ScriptFile;
		List<RobotJobParameter> RobotJobParameters;
		const string ImportVariableFunctionName = "ImportVariable";
		
		public RobotJob(FileInfo ScriptFile)
		{
			this.ScriptFile = ScriptFile;
		}
		
		public RobotJob(string ScriptFilePath)
		{
			
			this.ScriptFile = new FileInfo(ScriptFilePath);
		}
		
		public RobotJob(string ScriptFilePath,List<RobotJobParameter> RobotJobParameters)
		{
			
			this.ScriptFile = new FileInfo(ScriptFilePath);
			this.RobotJobParameters = RobotJobParameters;
		}
		
		public RobotJob(FileInfo ScriptFile,List<RobotJobParameter> RobotJobParameters)
		{
			this.ScriptFile = ScriptFile;
			this.RobotJobParameters = RobotJobParameters;
		}
		
		public void TestJob()
		{
			List<RobotJobParameter> ScriptParameters = ParseScriptParameters();
		}
		
		private List<RobotJobParameter> ParseScriptParameters()
		{
			
			
			List<RobotJobParameter> ScriptParameters = new List<RobotJobParameter>();
			
			StreamReader sr = new StreamReader(ScriptFile.OpenRead());
			while (!sr.EndOfStream)
			{
				
				string Line = sr.ReadLine();
				
				
				
				
				if (Line.Length > ImportVariableFunctionName.Length  && Line.Substring(0,ImportVariableFunctionName.Length).Equals(ImportVariableFunctionName))
				{
					
					//ImportVariable(Var1#Var2,"C:\Documents and Settings\Tecan\Desktop\testVar.csv",0#1,"0.000000#0",0,1,0,1,1);
					string[] parameters =  Line.Substring(ImportVariableFunctionName.Length+1, Line.Length - ImportVariableFunctionName.Length+1 - 4).Split(',');
					string[] Names = parameters[(int)ImpVarParams.Name].Split('#');
					string[] Types = parameters[(int)ImpVarParams.Type].Split('#');
					
					for (int i=0;i< Names.Length;i++)
					{
						
						RobotJobParameter RP = new RobotJobParameter(Names[i], (RobotJobParameterType)Convert.ToInt32(Types[i]));
						ScriptParameters.Add(RP);
					}
				}
				
				
			}
			
			return ScriptParameters;
			
			
			
		}
		
		
		
		
	}
	
	
	public struct RobotJobParameter
	{
		public string Name;
		public RobotJobParameterType Type;
		public string stringValue;
		public double? doubleValue;
		
		public RobotJobParameter(string Name, RobotJobParameterType Type)
		{
			this.Name = Name;
			this.Type = Type;
			this.stringValue = string.Empty;
			this.doubleValue = null;
		}
		public RobotJobParameter(string Name, RobotJobParameterType Type,string Value)
		{
			this.Name = Name;
			this.Type = Type;
			this.stringValue = Value;
			this.doubleValue = null;
		}
		public RobotJobParameter(string Name, RobotJobParameterType Type,int Value)
		{
			this.Name = Name;
			this.Type = Type;
			this.doubleValue = Value;
			this.stringValue = string.Empty;
		}
		
		
	}
	
	public enum RobotJobParameterType
	{String=1,Number=0};
	
}
