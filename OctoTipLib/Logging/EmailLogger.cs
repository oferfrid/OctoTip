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
using System.Net;
using System.Net.Mail;

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
			string FromGmailEmail = ConfigurationManager.AppSettings["GmailSenderUser"];
			string FromEmailPassword = ConfigurationManager.AppSettings["GmailSenderPassword"];
			SendEmail(FromGmailEmail,FromEmailPassword,FromGmailEmail,Subject,Body);
		}
		
		
		public override void Log(LoggingEntery LE)
		{
			string Subject = string.Format("{0}--{1}({2}):{3}",LE.EnteryType.ToString(),LE.Sender,LE.SubSender,LE.Title);
			string Body = LE.Message;
			SendEmail(Subject, Body);
				
		}
	}
}
