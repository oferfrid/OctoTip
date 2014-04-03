/*
 * Created by SharpDevelop.
 * User: oferfrid
 * Date: 01/04/2014
 * Time: 18:29
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;

namespace OctoTip.OctoTipPlus.Logging
{
	/// <summary>
	/// Description of LoggingEntery.
	/// </summary>
	public class LoggingEntery
	{
		public enum EnteryTypes {Critical,Error,Warning,Informational};
	
		public string Sender;
		public string SubSender;
		public string Title;
		public string Messege;
		public EnteryTypes EnteryType;
		
		public LoggingEntery(string Sender,string SubSender,string Title, string Messege, EnteryTypes EnteryType)
		{
			this.Sender = Sender;
			this.SubSender = SubSender;
			this.Title = Title;
			this.Messege = Messege; 
			this.EnteryType = EnteryType;
		}
		
	}
		
}
