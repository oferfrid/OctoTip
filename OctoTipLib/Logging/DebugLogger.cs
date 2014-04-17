/*
 * Created by SharpDevelop.
 * User: oferfrid
 * Date: 03/04/2014
 * Time: 16:51
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;

namespace OctoTip.Lib.Logging
{
	/// <summary>
	/// Description of DebugLogger.
	/// </summary>
	public class DebugLogger:Logger
	{
		public DebugLogger()
		{
			LoggerName = "Debug Logger";
			LoggigLevel = (int)LoggingEntery.EnteryTypes.Informational;
		}
		
		public override void Log(LoggingEntery LE)
		{
			System.Diagnostics.Debug.WriteLine(string.Empty);
			
			System.Diagnostics.Debug.WriteLine("-----Start Log---------");
			System.Diagnostics.Debug.WriteLine("EnteryType:" + LE.EnteryType.ToString());
			
			System.Diagnostics.Debug.WriteLine("Sender:" + LE.Sender +"(" + LE.SubSender + ")");
			System.Diagnostics.Debug.WriteLine("Title:" + LE.Title);
			System.Diagnostics.Debug.WriteLine("Message:" + LE.Message);
			System.Diagnostics.Debug.WriteLine("EnteryType:" + LE.EnteryType.ToString());
			System.Diagnostics.Debug.WriteLine("-----end Log---------");
			System.Diagnostics.Debug.WriteLine(string.Empty);
			
		}
	}
}
