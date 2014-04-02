/*
 * Created by SharpDevelop.
 * User: oferfrid
 * Date: 02/04/2014
 * Time: 10:17
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Diagnostics;

namespace OctoTip.OctoTipPlus.Logging
{
	/// <summary>
	/// Description of EventLogLogger.
	/// </summary>
	public class EventLogLogger
	{
		private string SourceName;
		
		public EventLogLogger(string LogName)
		{
			SourceName = System.AppDomain.CurrentDomain.FriendlyName;
			if(!EventLog.SourceExists(SourceName))
			{
				EventLog.CreateEventSource(SourceName, "Application");
				
			}

		}
		
		public void Log(string dsgfa)
		{
			// Create an EventLog instance and assign its source.
			EventLog myLog = new EventLog();
			myLog.Source = SourceName;

			// Write an informational entry to the event log.
			myLog.WriteEntry("Writing to event log.");
		}
	}
}