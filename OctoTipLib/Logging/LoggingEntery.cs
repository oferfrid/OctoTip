/*
 * Created by SharpDevelop.
 * User: oferfrid
 * Date: 01/04/2014
 * Time: 18:29
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;

namespace OctoTip.Lib.Logging
{
	/// <summary>
	/// Description of LoggingEntery.
	/// </summary>
	public class LoggingEntery
	{
		public enum EnteryTypes {Critical=0,Error=1,Warning=2,Informational=3};
	
		public string Sender;
		public string SubSender;
		public string Title;
		public string Message;
		public EnteryTypes EnteryType;
		
		public LoggingEntery(string Sender,string SubSender,string Title, string Message, EnteryTypes EnteryType)
		{
			this.Sender = Sender;
			this.SubSender = SubSender;
			this.Title = Title;
			this.Message = Message; 
			this.EnteryType = EnteryType;
		}
		public LoggingEntery(string Sender,string SubSender,string Title, EnteryTypes EnteryType)
		{
			this.Sender = Sender;
			this.SubSender = SubSender;
			this.Title = Title;
			this.Message = string.Empty; 
			this.EnteryType = EnteryType;
		}
		
	}
		
}
