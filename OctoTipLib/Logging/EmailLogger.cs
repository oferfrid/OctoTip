/*
 * Created by SharpDevelop.
 * User: oferfrid
 * Date: 17/04/2014
 * Time: 16:04
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Configuration;
using System.IO;
using System.Net;
using System.Net.Mail;
using OctoTip.Lib.Utils;

namespace OctoTip.Lib.Logging
{
	/// <summary>
	/// Description of EmailLogger.
	/// </summary>
	public class EmailLogger:Logger
	{
		public EmailLogger()
		{
			LoggerName = "Email Logger";
			LoggigLevel = (int)LoggingEntery.EnteryTypes.Error;
			ExtraData = "Enter Email";
		}
		
		private void SendEmail(string FromGmailEmail,
		                       string GmailPassword ,
		                       string ToEmail,
		                       string Subject,
		                       string Body)
		{
			
			System.Net.Mail.MailMessage msg = new System.Net.Mail.MailMessage();
			msg.To.Add(ToEmail);
			msg.Subject = Subject;
			msg.Body = Body;
			msg.IsBodyHtml = false;

			SmtpClient SMPclint = new SmtpClient();
			SMPclint.Host = "smtp.gmail.com";
			SMPclint.EnableSsl = true;
			NetworkCredential NetCry = new NetworkCredential();
			NetCry.UserName = FromGmailEmail;
			NetCry.Password = GmailPassword;
			SMPclint.UseDefaultCredentials = true;
			SMPclint.Credentials = NetCry;
			SMPclint.EnableSsl = true;
			// add the following two sentence.
			MailAddress fromMailAddr = new MailAddress(FromGmailEmail,"Automatic Email Sender");
			msg.From = fromMailAddr;
			SMPclint.Port = 587;
			SMPclint.Send(msg);
		}
		
		private void SendEmail(string Subject,
		                       string Body)
		{
			IniFile Ini = new IniFile(Path.Combine(Environment.CurrentDirectory, "GoogleDocs.ini"));
			
			string User = Ini.IniReadValue("UserLogin","User");
			string Password = Ini.IniReadValue("UserLogin","Password");
			
			SendEmail(User,Password,ExtraData,Subject,Body);
		}
		
		
		public override void Log(LoggingEntery LE)
		{
			string Subject = string.Format("{0}--{1}({2}):{3}",LE.EnteryType.ToString(),LE.Sender,LE.SubSender,LE.Title);
			string Body = LE.Message;
			SendEmail(Subject, Body);
				
		}
	}
}
