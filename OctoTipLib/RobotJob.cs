/*
 * Created by SharpDevelop.
 * User: oferfrid
 * Date: 21/09/2011
 * Time: 11:28
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.IO;
using System.Xml.Serialization;

namespace OctoTip.Lib
{
	/// <summary>
	/// Description of RobotJob.
	/// </summary>
	[Serializable]
	public class RobotJob : IComparable<RobotJob>,INotifyPropertyChanged
	{
		
		
		private enum ImpVarParams {Name=0, File=1, Type=2, DefaultValue=3, HasHeader=8};
		
		const string ImportVariableFunctionName = "ImportVariable";
		
		private Guid _UniqueID;
		private string _ScriptFilePath = string.Empty;
		private string _ScriptName ;
		
		private RobotJob.Status _JobStatus = RobotJob.Status.Created;
		public string ParametersFilePath = string.Empty;
		public bool PrametersFileHasHeader = true;
		public string Script;
		private double _Priority;
		public List<RobotJobParameter> RobotJobParameters;
		#region RobotJob constructors

		
		public RobotJob()
		{
			
		}
		
		public RobotJob(FileInfo ScriptFile):this(ScriptFile,0.5)
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
			InitScript(ScriptFilePath);
			this.Priority = Priority;
		}
		
		public RobotJob(FileInfo ScriptFile,List<RobotJobParameter> RobotJobParameters):this(ScriptFile,RobotJobParameters,0.5)
		{
			
		}
		
		public RobotJob(string ScriptFilePath,List<RobotJobParameter> RobotJobParameters):this(ScriptFilePath, RobotJobParameters,0.5)
		{
			

		}
		
		public RobotJob(FileInfo ScriptFile,List<RobotJobParameter> RobotJobParameters,double Priority):this(ScriptFile.FullName,RobotJobParameters,Priority)
		{
			
		}
		
		public RobotJob(string ScriptFilePath,List<RobotJobParameter> RobotJobParameters,double Priority)
		{
			InitScript(ScriptFilePath);
			this.RobotJobParameters = RobotJobParameters;
			this.Priority = Priority;
		}
		
		#endregion
		
		#region RobotJob Properties
		
		public RobotJob.Status JobStatus 
		{
			get{return _JobStatus ;}
			set
			{
				_JobStatus = value;
				NotifyPropertyChanged("JobStatus");
			}
		}
		
		public string ScriptFilePath
		{
			get { return _ScriptFilePath; }
			set { _ScriptFilePath = value;}
		}
		
		public Guid UniqueID
		{
			get { return _UniqueID; }
			set { _UniqueID = value;}
		}
		
		
		public string RobotJobDisplayParameters
		{
			get {
				string ParamView = string.Empty ;
				if (!(RobotJobParameters==null))
				{
					foreach (RobotJobParameter RJP in RobotJobParameters)
					{
						if(RJP.Type == RobotJobParameter.ParameterType.Number)
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
				NotifyPropertyChanged("Priority");
			}
		}
		
		public string ScriptName
		{
			get { return _ScriptName; }
			set {
				this._ScriptName = value;
			}
		}
		#endregion
		
		#region public Methods
		
		public int CompareTo(RobotJob RJ)
		{
			return this.Priority.CompareTo(RJ.Priority);
		}
		
		
		public void TestJobParameters()
		{
			List<RobotJobParameter> ScriptParameters = ParseScriptParameters();
			if (!this.HasSameParameters(ScriptParameters))
			{
				throw new Exception("Script Parameters and JobParameters don't match");
			}
			
		}
		
		public void CreateScript()
		{
			// Write the string to a file.
			string ScriptFileName = string.Format("{0}\\{1}",ConfigurationManager.AppSettings["DefultScriptFolder"],ScriptName);
			StreamWriter file = new StreamWriter(ScriptFileName);
			file.Write(Script);
			file.Close();
			_ScriptFilePath = ScriptFileName;
			WriteParameterFile();
		}
		
		public Guid GenerateUniqueID()
		{
			UniqueID = Guid.NewGuid();
			return UniqueID;
		}
		#endregion
		
		#region Private Methods
		
		private List<RobotJobParameter> ParseScriptParameters()
		{
			
			List<RobotJobParameter> ScriptParameters = new List<RobotJobParameter>();
			
			string[] ScriptLines = Script.Split('\n');
			
			for (int l=0; l<ScriptLines.Length;l++)
			{
				
				string Line = ScriptLines[l];
				
				if (Line.Length > ImportVariableFunctionName.Length  && Line.Substring(0,ImportVariableFunctionName.Length).Equals(ImportVariableFunctionName))
				{
					
					//ImportVariable(Var1#Var2,"C:\Documents and Settings\Tecan\Desktop\testVar.csv",0#1,"0.000000#0",0,1,0,1,1);
					string[] parameters =  Line.Substring(ImportVariableFunctionName.Length+1, Line.Length - ImportVariableFunctionName.Length+1 - 4).Split(',');
					string[] Names = parameters[(int)ImpVarParams.Name].Split('#');
					string[] Types = parameters[(int)ImpVarParams.Type].Split('#');
					
					for (int i=0;i< Names.Length;i++)
					{
						
						RobotJobParameter RP = new RobotJobParameter(Names[i], (RobotJobParameter.ParameterType)Convert.ToInt32(Types[i]));
						ScriptParameters.Add(RP);
					}
					
					break;
				}
			}
			
			return ScriptParameters;
		}
		
		private void ParseScriptFile()
		{
			string ScriptParameterFilePath = string.Empty;
			string[] ScriptLines = Script.Split('\n');
			
			for (int l=0; l<ScriptLines.Length;l++)
			{
				
				string Line = ScriptLines[l];
				
				if (Line.Length > ImportVariableFunctionName.Length  && Line.Substring(0,ImportVariableFunctionName.Length).Equals(ImportVariableFunctionName))
				{
					//ImportVariable(Var1#Var2,"C:\Documents and Settings\Tecan\Desktop\testVar.csv",0#1,"0.000000#0",0,1,0,1,1);
					string[] parameters =  Line.Substring(ImportVariableFunctionName.Length+1, Line.Length - ImportVariableFunctionName.Length+1 - 4).Split(',');
					ScriptParameterFilePath = parameters[(int)ImpVarParams.File].Trim(new char[]{'\"'});
					
					break;
				}
			}
			
			ParametersFilePath = ScriptParameterFilePath;
			PrametersFileHasHeader = true;
			
		}

		
		
		private void InitScript( string ScriptPath)
		{
			FileInfo FI = new FileInfo(ScriptPath);
			if (!FI.Exists)
			{
				throw new Exception("Script file "  + ScriptPath + " doesn't exsist");
			}
			
			ScriptName = FI.Name;
			
			StreamReader streamReader = new StreamReader(ScriptPath);
			Script = streamReader.ReadToEnd();
			streamReader.Close();
			
			ParseScriptFile();
			
		}
		
		
		private void WriteParameterFile()
		{
			if (ParametersFilePath!=string.Empty)
			{
				// Write the string to a file.
				
				//TODO:TestParameters
				if ((RobotJobParameters==null || RobotJobParameters.Count<0) && ParametersFilePath !=string.Empty)
				{
					throw new Exception(string.Format("Script {0} contains parameter file, but parameters was not supplied",ParametersFilePath));
				}
				else
				{
					if (RobotJobParameters.Count>0)
					{
						StreamWriter file = new StreamWriter(ParametersFilePath);
						
						//return ProtocolList.ConvertAll<IProtocol>(delegate(Type t) { return Activator.CreateInstance(t) as IProtocol; });
						
						string Header = string.Join(",",RobotJobParameters.ConvertAll<string>(delegate(RobotJobParameter RJP) { return RJP.Name ; }).ToArray());

						file.WriteLine(Header);
						
						string Value = string.Join(",",RobotJobParameters.ConvertAll<string>(delegate(RobotJobParameter RJP) {
						                                                                     	string V = string.Empty;
						                                                                     	switch (RJP.Type)
						                                                                     	{
						                                                                     		case RobotJobParameter.ParameterType.Number:
						                                                                     			V = Convert.ToString(RJP.doubleValue);
						                                                                     			break;
						                                                                     		case RobotJobParameter.ParameterType.String:
						                                                                     			V = string.Format("\"{0}\"",RJP.stringValue);
						                                                                     			break;
						                                                                     	}
						                                                                     	return V;
						                                                                     }).ToArray());
						file.WriteLine(Value);
						file.Close();

					}
				}
			}
		}
		#endregion
		
		
		public event PropertyChangedEventHandler PropertyChanged;
		
		private void NotifyPropertyChanged(String info)
		{
			if (PropertyChanged != null)
			{
				PropertyChanged(this, new PropertyChangedEventArgs(info));
			}
		}
		
		
		public enum Status
		{
			Created,
			Queued,
			Enqueued,
			Running,
			Paused,
			Finished,
			RuntimeError,
			Failed,
			TerminatedByUser
		};
		
		
		
		public  bool HasSameParameters( List<RobotJobParameter> RJP)
		{
			bool Equal = true;
			if(this.RobotJobParameters.Count == RJP.Count)
			{
				for(int i=0;i<this.RobotJobParameters.Count;i++)
				{
					if((this.RobotJobParameters[i].Name != RJP[i].Name)
					   || (this.RobotJobParameters[i].Type != RJP[i].Type))
					{
						Equal = false;
						break;
					}
				}
			}
			else
			{
				Equal = false;
			}
			return Equal;
			
		}
		
		
		
		
		
		
		
		
	}
	
	
	public struct RobotJobParameter
	{
		public string Name;
		public ParameterType Type;
		public string stringValue;
		public double? doubleValue;
		
		public RobotJobParameter(string Name, ParameterType Type)
		{
			this.Name = Name;
			this.Type = Type;
			this.stringValue = string.Empty;
			this.doubleValue = null;
		}
		public RobotJobParameter(string Name, ParameterType Type,string Value)
		{
			this.Name = Name;
			this.Type = Type;
			this.stringValue = Value;
			this.doubleValue = null;
		}
		public RobotJobParameter(string Name, ParameterType Type,double Value)
		{
			this.Name = Name;
			this.Type = Type;
			this.doubleValue = Value;
			this.stringValue = string.Empty;
		}

		public enum ParameterType
		{String=1,Number=0};
	}
}
