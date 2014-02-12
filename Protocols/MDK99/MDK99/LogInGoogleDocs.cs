/*
 * Created by SharpDevelop.
 * User: oferfrid
 * Date: 29/11/2012
 * Time: 10:48
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using Google.GData.Client;
using Google.GData.Extensions;
using Google.GData.Spreadsheets;



namespace MDK99
{
	/// <summary>
	/// Description of LogInGoogleDocs.
	/// </summary>	
	public class LogInGoogleDocs
	{
		string LogName;
		string  SharedResourcesFilePath;
		const string DateHeader = "date";
		const string MessegeHeader  = "messege";
		
		public LogInGoogleDocs(string LogName,string SharedResourcesFilePath)
		{
			this.LogName = LogName;
			this.SharedResourcesFilePath = SharedResourcesFilePath;
		}
		
		public void Log(string Messege)
		{
			
			IniFile Ini = new IniFile(SharedResourcesFilePath + "GoogleDocs.ini");
			
			string User = Ini.IniReadValue("UserLogin","User");
			string Password = Ini.IniReadValue("UserLogin","Password");
			
			
			SpreadsheetsService myService = new SpreadsheetsService("MySpreadsheetIntegration-v1");
			myService.setUserCredentials(User,Password);
			
			SpreadsheetQuery Squery = new SpreadsheetQuery();
			string Title =Ini.IniReadValue("Spreadsheet","Name");
			Squery.Title = Title;
			Squery.Exact = true;
			SpreadsheetFeed Sfeed = myService.Query(Squery);

			if(Sfeed.Entries.Count == 0)
			{
				
				throw new Exception("GoogleDocs log file " + Title +"Was Not found");
			}
			else
			{
				
				SpreadsheetEntry spreadsheet = (SpreadsheetEntry)Sfeed.Entries[0];
				
				WorksheetEntry ProtocolWorksheetEntry=null;
				
				
				AtomLink link = spreadsheet.Links.FindService(GDataSpreadsheetsNameTable.WorksheetRel, null);

				WorksheetQuery Wquery = new WorksheetQuery(link.HRef.ToString());
				WorksheetFeed Wfeed = myService.Query(Wquery);

				foreach (WorksheetEntry worksheet in Wfeed.Entries)
				{
					if (worksheet.Title.Text==LogName)
					{
						ProtocolWorksheetEntry = worksheet;
					}
				}
				
				
				if (ProtocolWorksheetEntry==null)
				{
					// cteate new worksheet
					WorksheetEntry worksheet = new WorksheetEntry();
					worksheet.Title.Text = LogName;
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
					cellEntry= new CellEntry (1, 2, MessegeHeader);
					cellFeed.Insert(cellEntry);
					
					
					
					
				}
				
				
				// Define the URL to request the list feed of the worksheet.
				AtomLink listFeedLink = ProtocolWorksheetEntry.Links.FindService(GDataSpreadsheetsNameTable.ListRel, null);

				// Fetch the list feed of the worksheet.
				ListQuery listQuery = new ListQuery(listFeedLink.HRef.ToString());
				ListFeed listFeed = myService.Query(listQuery);

				// Create a local representation of the new row.
				ListEntry row = new ListEntry();
				row.Elements.Add(new ListEntry.Custom() { LocalName = DateHeader, Value = DateTime.Now.ToString() });
				row.Elements.Add(new ListEntry.Custom() { LocalName = MessegeHeader, Value = Messege });


				// Send the new row to the API for insertion.
				myService.Insert(listFeed, row);
				
				
				
				
			}
			
		}
	}
}
