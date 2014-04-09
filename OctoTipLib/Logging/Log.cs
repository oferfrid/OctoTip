/*
 * Created by SharpDevelop.
 * User: oferfrid
 * Date: 08/04/2014
 * Time: 16:45
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;

namespace OctoTip.Lib.Logging
{
	/// <summary>
	/// Description of Log.
	/// </summary>
	public sealed class Log
	{
		
		private static volatile Log instance;
		private static object syncRoot = new Object();
		
		
		public event EventHandler<LogEnteryCreatedEventArgs> LogEnteryCreated;
		
		
		public Log()
		{}
		
		
		public static void LogEntery(LoggingEntery LE)
		{
			
			EventHandler<LogEnteryCreatedEventArgs> handler = Instance.LogEnteryCreated;
			if (handler != null)
			{
				handler(Instance, new LogEnteryCreatedEventArgs(LE) );
			}
			
		}
		
		public static Log Instance
		{
			get
			{
				if (instance == null)
				{
					lock (syncRoot)
					{
						if (instance == null)
							instance = new Log();
					}
				}

				return instance;
			}
		}
	}
	
	
	public class LogEnteryCreatedEventArgs : EventArgs
	{
		
		private LoggingEntery _LE;
		
		
		public LogEnteryCreatedEventArgs(LoggingEntery LE)
		{
			this._LE  = LE;

		}

		public LoggingEntery LE
		{
			get 
			{
				return _LE; 
			}
		}
	}
}

