/*
 * Created by SharpDevelop.
 * User: oferfrid
 * Date: 02/04/2014
 * Time: 12:32
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;

namespace OctoTip.Lib.Logging
{
	/// <summary>
	/// Description of ILogger.
	/// </summary>
	public interface ILogger
	{
		string LoggerName
		{
			get;
		}
		
		int LoggigLevel
		{
			get;
			set;
		}
		
		string ExtraData
		{
			get;
			set;
		}
		void Log(LoggingEntery LE);
	}
}
