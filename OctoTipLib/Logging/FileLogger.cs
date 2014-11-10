/*
 * Created by SharpDevelop.
 * User: oferfrid
 * Date: 22/04/2014
 * Time: 12:30
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.IO;

namespace OctoTip.Lib.Logging
{
	/// <summary>
	/// Description of FileLogger.
	/// </summary>
	public class FileLogger:Logger
	{
		public FileLogger()
		{
			LoggerName = "File Logger";
			LoggigLevel = (int)LoggingEntery.EnteryTypes.Informational;
		}
		
		public override void Log(LoggingEntery LE)
		{
			FileInfo FI = new FileInfo(string.Format("{0}_{1}.log",LE.Sender,LE.SubSender));
			TextWriter TR = FI.AppendText();
			TR.WriteLine(string.Format("{0}\t{1}\t{2}",DateTime.Now,LE.EnteryType.ToString(),LE.Title));
			TR.WriteLine(string.Format("{0}",LE.Message));
			TR.Close();
		}
	}
}
