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
using System.Xml.Serialization;

namespace OctoTip.OctoTipLib
{
	/// <summary>
	/// Description of RobotJob.
	/// </summary>
	[Serializable]
	public class RobotJob : IComparable<RobotJob>
	{
		private enum ImpVarParams {Name=0, File=1, Type=2, DefaultValue=3, HasHeader=8};
		
			
		const string ImportVariableFunctionName = "ImportVariable";
		
		
		private double _Priority;
		
		//public FileInfo ScriptFile;
		
		public string _ScriptFilePath ;
		
		public List<RobotJobParameter> RobotJobParameters;
		
		
		#region RobotJob constructors

		public RobotJob(FileInfo ScriptFile):this(ScriptFile,0.5)
		{
			
		}
		
		public RobotJob()
		{
			
		}
		
		public RobotJob(string ScriptFilePath):this(new FileInfo(ScriptFilePath),0.5)
		{
			
		}
		
		public RobotJob(FileInfo ScriptFile,double Priority):this(ScriptFile.FullName,Priority)
		{
			
		}
		
		public RobotJob(string ScriptFilePath,double Priority)
		{
			this.ScriptFilePath = ScriptFilePath;
			this.Priority = Priority;
		}
		
		public RobotJob(FileInfo ScriptFile,List<RobotJobParameter> RobotJobParameters):this(ScriptFile,RobotJobParameters,0.5)
		{
			
		}
		
		public RobotJob(string ScriptFilePath,List<RobotJobParameter> RobotJobParameters)
		{
			this.ScriptFilePath = ScriptFilePath;
			this.RobotJobParameters = RobotJobParameters;
			this.Priority = Priority;
		}
		
		public RobotJob(FileInfo ScriptFile,List<RobotJobParameter> RobotJobParameters,double Priority):this(ScriptFile.FullName,RobotJobParameters,0.5)
		{
			
		}
		
		public RobotJob(string ScriptFilePath,List<RobotJobParameter> RobotJobParameters,double Priority):this(new FileInfo(ScriptFilePath),RobotJobParameters,Priority)
		{
			
		}
		
		
		#endregion
		
		#region RobotJob Properties
		
		public string ScriptName
		{
			get { return this.ScriptFile.Name; }
		}
		
		
		public string ScriptFilePath
		{
			get { return _ScriptFilePath; }
			set { _ScriptFilePath = value;}
		}

	
		public FileInfo ScriptFile
		{
			get { return new FileInfo(this.ScriptFilePath); }
		}
		
		
		public string RobotJobDisplayParameters
		{
			get {
				string ParamView = string.Empty ;
				if (!(RobotJobParameters==null))
				{
					foreach (RobotJobParameter RJP in RobotJobParameters)
					{
						if(RJP.Type == RobotJobParameterType.Number)
						{
							ParamView += RJP.Name + "(" + RJP.Type.ToString() + ")=" + RJP.doubleValue.ToString() + "\n";
						}
						else
						{
							ParamView += RJP.Name + "(" + RJP.Type.ToString() + ")=" + RJP.stringValue + "\n";
						}
					}
				}
				return ParamView;
				
				
			}
		}
		
		
		
		
		public double Priority
		{
			get { return this._Priority; }
			set {
				if (Priority<0 || Priority >= 1)
				{
					throw new Exception ("Priority is [0,1)");
				}
				
				this._Priority = value;
			}
		}
		#endregion
		
		public int CompareTo(RobotJob RJ)
		{
			return this.Priority.CompareTo(RJ.Priority);
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
