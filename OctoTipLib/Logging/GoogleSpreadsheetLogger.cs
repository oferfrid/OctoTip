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
using Newtonsoft.Json;
//using Google.Apis.Drive.v2;

namespace OctoTip.Lib.Logging
{
	/// <summary>
	/// Description of GoogleSpreadsheetLogger.
	/// </summary>
	public class GoogleSpreadsheetLogger:Logger
	{
		
		const string DateHeader = "date";
		const string MessageHeader  = "message";
		const string ConnectSection = "Connection";
		
		
		
		public  override bool IsChecked
		{
			get
			{
				return true;
			}
		}
		
			
		
		public GoogleSpreadsheetLogger()
		{
			
			LoggerName = "Google Spreadsheet";
			LoggigLevel = (int)LoggingEntery.EnteryTypes.Informational;
		}
		
		public override void Log(LoggingEntery LE)
		{
			IniFile Ini = new IniFile(Path.Combine(Environment.CurrentDirectory, "GoogleDocs.ini"));
			
			// nir start
			      ////////////////////////////////////////////////////////////////////////////
      // STEP 1: Configure how to perform OAuth 2.0
      ////////////////////////////////////////////////////////////////////////////

      // TODO: Update the following information with that obtained from
      // https://code.google.com/apis/console. After registering
      // your application, these will be provided for you.

      //string CLIENT_ID = "339569043085-6k0io9kdubi7a3g3jes4m76t614fkccr.apps.googleusercontent.com";

      // This is the OAuth 2.0 Client Secret retrieved
      // above.  Be sure to store this value securely.  Leaking this
      // value would enable others to act on behalf of your application!
      //string CLIENT_SECRET = "wWC4Wcb12RbQg4YuGWJtkh4j";

      // Space separated list of scopes for which to request access.
      //string SCOPE = "https://spreadsheets.google.com/feeds https://docs.google.com/feeds";

      // This is the Redirect URI for installed applications.
      // If you are building a web application, you have to set your
      // Redirect URI at https://code.google.com/apis/console.
      //tring REDIRECT_URI = "urn:ietf:wg:oauth:2.0:oob";

      ////////////////////////////////////////////////////////////////////////////
      // STEP 2: Set up the OAuth 2.0 object
      ////////////////////////////////////////////////////////////////////////////

      // OAuth2Parameters holds all the parameters related to OAuth 2.0.
      OAuth2Parameters parameters = new OAuth2Parameters();
      
      // Set your OAuth 2.0 Client Id (which you can register at
      // https://code.google.com/apis/console).
      parameters.ClientId = Ini.IniReadValue(ConnectSection,"ClientID");

      // Set your OAuth 2.0 Client Secret, which can be obtained at
      // https://code.google.com/apis/console.
      parameters.ClientSecret = Ini.IniReadValue(ConnectSection,"ClientSecret");

      // Set your Redirect URI, which can be registered at
      // https://code.google.com/apis/console.
      parameters.RedirectUri = Ini.IniReadValue(ConnectSection,"RedirectURI");
      
      // Set your refresh token
      parameters.RefreshToken = Ini.IniReadValue(ConnectSection,"RefreshToken");
      parameters.AccessToken = Ini.IniReadValue(ConnectSection,"LastAccessToken");

      // Set the scope for this particular service.
      parameters.Scope = Ini.IniReadValue(ConnectSection,"Scope");

      // Get the authorization url.  The user of your application must visit
      // this url in order to authorize with Google.  If you are building a
      // browser-based application, you can redirect the user to the authorization
      // url.
       //string authorizationUrl = OAuthUtil.CreateOAuth2AuthorizationUrl(parameters);
      //Console.WriteLine(authorizationUrl);
      //Console.WriteLine("Please visit the URL above to authorize your OAuth "
      //  + "request token.  Once that is complete, type in your access code to "
      //  + "continue...");

      ////////////////////////////////////////////////////////////////////////////
      // STEP 4: Get the Access Token
      ////////////////////////////////////////////////////////////////////////////

      // Once the user authorizes with Google, the request token can be exchanged
      // for a long-lived access token.  If you are building a browser-based
      // application, you should parse the incoming request token from the url and
      // set it in OAuthParameters before calling GetAccessToken().
      OAuthUtil.RefreshAccessToken(parameters);//parameters.AccessToken;
      Ini.IniWriteValue(ConnectSection,"LastAccessToken",parameters.AccessToken);
      // Console.WriteLine("OAuth Access Token: " + accessToken);

      ////////////////////////////////////////////////////////////////////////////
      // STEP 5: Make an OAuth authorized request to Google
      ////////////////////////////////////////////////////////////////////////////

      // Initialize the variables needed to make the request
      GOAuth2RequestFactory requestFactory =
          new GOAuth2RequestFactory(null, "OctoTipPlus", parameters);
      SpreadsheetsService myService = new SpreadsheetsService("OctoTipPlus");
      myService.RequestFactory = requestFactory;
			// nir end
			
			string User = Ini.IniReadValue("UserLogin","User");
			string Password = Ini.IniReadValue("UserLogin","Password");
			
		//	SpreadsheetsService myService = new SpreadsheetsService("MySpreadsheetIntegration-v1");
			//myService.setUserCredentials(User,Password);
			
			SpreadsheetQuery Squery = new SpreadsheetQuery();
			string Sender = LE.Sender;
			Squery.Title = Sender;
			Squery.Exact = true;
			SpreadsheetFeed Sfeed;
			try
			{
				Sfeed = myService.Query(Squery);
			}
			catch (Google.GData.Client.InvalidCredentialsException e)
			{
				throw(new Exception(string.Format("Credentials error in google acount for user:{0}",User),e));
			}
			
			
			if(Sfeed.Entries.Count == 0)
			{
				//DriveService service1 = new DriveService();
				
	         	//service.SetAuthenticationToken(parameters.AccessToken);//.setUserCredentials(User,Password);
				//Google.GData.Client.GOAuth2RequestFactory requestf =  new Google.GData.Client.GOAuth2RequestFactory(null, "OctoTipPlus",parameters);
				OAuthUtil.RefreshAccessToken(parameters);//parameters.AccessToken;
      			Ini.IniWriteValue(ConnectSection,"LastAccessToken",parameters.AccessToken);
				Google.GData.Documents.DocumentsService service = new Google.GData.Documents.DocumentsService("OctoTipPlus");
				GOAuth2RequestFactory requestFactory2 =
          				new GOAuth2RequestFactory(null, "OctoTipPlus", parameters);
				service.RequestFactory = requestFactory2;
				//service.RequestFactory=requestf;
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


