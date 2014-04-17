/*
 * Created by SharpDevelop.
 * User: oferfrid
 * Date: 03/04/2014
 * Time: 17:46
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.IO;
using Google.GData.Client;
using Google.GData.Spreadsheets;
using OctoTip.Lib.Utils;

namespace OctoTip.Lib.Logging
{
	/// <summary>
	/// Description of GoogleSpreadsheetLogger.
	/// </summary>
	public class GoogleSpreadsheetLogger:Logger
	{
		
		const string DateHeader = "date";
		const string MessageHeader  = "Message";
		
		
		public GoogleSpreadsheetLogger()
		{
			
			LoggerName = "Google Spreadsheet";
			LoggigLevel = (int)LoggingEntery.EnteryTypes.Informational;
		}
		
		public override void Log(LoggingEntery LE)
		{
			IniFile Ini = new IniFile(Path.Combine(Environment.CurrentDirectory, "GoogleDocs.ini"));
			
			string User = Ini.IniReadValue("UserLogin","User");
			string Password = Ini.IniReadValue("UserLogin","Password");
			
			
			SpreadsheetsService myService = new SpreadsheetsService("MySpreadsheetIntegration-v1");
			myService.setUserCredentials(User,Password);
			
			SpreadsheetQuery Squery = new SpreadsheetQuery();
			string Sender = LE.Sender;
			Squery.Title = Sender;
			Squery.Exact = true;
			SpreadsheetFeed Sfeed = myService.Query(Squery);

			
			
			if(Sfeed.Entries.Count == 0)
			{
				
				Google.GData.Documents.DocumentsService service = new Google.GData.Documents.DocumentsService("MyDocumentsListIntegration-v1");
				service.setUserCredentials(User,Password);
				// Instantiate a DocumentEntry object to be inserted.
				Google.GData.Documents.DocumentEntry entry = new Google.GData.Documents.DocumentEntry();

				// Set the document title
				entry.Title.Text = LE.Sender;

				// Add the document category
				entry.Categories.Add(Google.GData.Documents.DocumentEntry.SPREADSHEET_CATEGORY);

				// Make a request to the API and create the document.
				Google.GData.Documents.DocumentEntry newEntry = service.Insert(
					Google.GData.Documents.DocumentsListQuery.documentsBaseUri, entry);
				
				Squery = new SpreadsheetQuery();
				Squery.Title = Sender;
				Squery.Exact = true;
				Sfeed = myService.Query(Squery);

			}
			
			
			SpreadsheetEntry spreadsheet = (SpreadsheetEntry)Sfeed.Entries[0];
			
			WorksheetEntry ProtocolWorksheetEntry=null;
			
			
			AtomLink link = spreadsheet.Links.FindService(GDataSpreadsheetsNameTable.WorksheetRel, null);

			WorksheetQuery Wquery = new WorksheetQuery(link.HRef.ToString());
			WorksheetFeed Wfeed = myService.Query(Wquery);

			foreach (WorksheetEntry worksheet in Wfeed.Entries)
			{
				if (worksheet.Title.Text==LE.SubSender)
				{
					ProtocolWorksheetEntry = worksheet;
				}
			}
			
			
			if (ProtocolWorksheetEntry==null)
			{
				// cteate new worksheet
				WorksheetEntry worksheet = new WorksheetEntry();
				worksheet.Title.Text = LE.SubSender;
				worksheet.Cols = 3;
				worksheet.Rows = 5;

				// Send the local representation of the worksheet to the API for
				// creation.  The URL to use here is the worksheet feed URL of our
				// spreadsheet.
				WorksheetFeed wsFeed = spreadsheet.Worksheets;
				ProtocolWorksheetEntry = myService.Insert(wsFeed, worksheet);
				
				
				
				CellFeed cellFeed= ProtocolWorksheetEntry.QueryCellFeed();
				
				CellEntry cellEntry= new CellEntry (1, 1,DateHeader);
				cellFeed.Insert(cellEntry);
				cellEntry= new CellEntry (1, 2, MessageHeader);
				cellFeed.Insert(cellEntry);
				
				
				
				
			}
			
			
			// Define the URL to request the list feed of the worksheet.
			AtomLink listFeedLink = ProtocolWorksheetEntry.Links.FindService(GDataSpreadsheetsNameTable.ListRel, null);

			// Fetch the list feed of the worksheet.
			ListQuery listQuery = new ListQuery(listFeedLink.HRef.ToString());
			ListFeed listFeed = myService.Query(listQuery);

			string Message = string.Format("{0}\n{1}",LE.Title,LE.Message);
			
			// Create a local representation of the new row.
			ListEntry row = new ListEntry();
			row.Elements.Add(new ListEntry.Custom() { LocalName = DateHeader, Value = DateTime.Now.ToString() });
			row.Elements.Add(new ListEntry.Custom() { LocalName = MessageHeader, Value = Message });


			// Send the new row to the API for insertion.
			myService.Insert(listFeed, row);
			
			
			
		}
	}
}


