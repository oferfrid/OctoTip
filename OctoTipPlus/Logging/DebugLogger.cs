/*
 * Created by SharpDevelop.
 * User: oferfrid
 * Date: 03/04/2014
 * Time: 16:51
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;

namespace OctoTip.OctoTipPlus.Logging
{
	/// <summary>
	/// Description of DebugLogger.
	/// </summary>
	public class DebugLogger:Logger
	{
		public DebugLogger()
		{
			LoggerName = "Debug Logger";
		}
		
		public override void Log(LoggingEntery LE)
		{
			System.Diagnostics.Debug.WriteLine("Looger Error");
			System.Diagnostics.Debug.WriteLine("--------------");
			System.Diagnostics.Debug.WriteLine("Sender:" + LE.Sender);
			System.Diagnostics.Debug.WriteLine("Title:" + LE.Title);
			System.Diagnostics.Debug.WriteLine("Messege:" + LE.Messege);
			System.Diagnostics.Debug.WriteLine("EnteryType:" + LE.EnteryType.ToString());
			System.Diagnostics.Debug.WriteLine("--------------");
			
		}
	}
}
