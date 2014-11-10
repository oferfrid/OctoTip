/*
 * Created by SharpDevelop.
 * User: oferfrid
 * Date: 02/04/2014
 * Time: 12:45
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;

namespace OctoTip.Lib.Logging
{
	/// <summary>
	/// Description of Logger.
	/// </summary>
	public abstract class Logger:ILogger
	{
		
		public string LoggerName { get; set; }
		
		public LoggingEntery.EnteryTypes LogEnteryTypeLevel { get; set; }
		
		
		
		public abstract void Log(LoggingEntery LE);
		
		
		public Logger ThisLogger
		{
			get
			{
				return this;
			}
		}
		
		
		public int LoggigLevel {
			get ;
			set ;
		}
		
		public string ExtraData {
			get ;
			set;
		}
		
		public virtual bool IsChecked {
			get {
				return false;
			}
		}
	}
}

