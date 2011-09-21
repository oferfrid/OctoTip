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
		FileInfo ScriptFile;
		List<RobotJobParameter> RobotJobParameters;
		
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
		
		public void TestParameters()
		{
			
		}
		
		
	}
	
	
	public struct RobotJobParameter
	{
		public string Name;
		public RobotJobParameterType Type;
		public string stringValue;
		public double? doubleValue;
		
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
	{String,Number};
	
}
