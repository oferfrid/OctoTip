/*
 * Created by SharpDevelop.
 * User: oferfrid
 * Date: 01/11/2012
 * Time: 17:09
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.IO;
using System.Text;

namespace SerialDilutionONEvolution
{
	/// <summary>
	/// Description of Utils.
	/// </summary>
	public static class LocalUtils
	{
		const double WellsInDilution = 3.0;
		const double WellsInRow = 16.0;
		
	
		public static int GetNextFreezPos(string SharedResourcesFilePath,string Title)
		{
			string FilePath  = SharedResourcesFilePath + @"FreezPlate.csv";
			FileStream fileStream = new FileStream( FilePath, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.None);
			TextReader TRead = new StreamReader(fileStream);
			string sIndex = ReadLastline(fileStream).Split(new Char[] {','})[0];
			int Index;
			if (sIndex ==string.Empty)
			{
				Index = 0;
			}
			else
			{
				Index = Convert.ToInt32(sIndex);
			}
			

			fileStream.Seek(0, SeekOrigin.End);
			TextWriter TWrite = new StreamWriter(fileStream);
			int NewIndex = Index + 1;
			
			TWrite.WriteLine(NewIndex.ToString() + ","+ Title);
			TWrite.Flush();
			fileStream.Close();
			return NewIndex;
		}
		
		
		private static string ReadLastline(FileStream fs)
		{
			byte[] line;
			byte[] text = new byte[1];
			long position = 0;
			int count;

			fs.Seek(0, SeekOrigin.End);
			position = fs.Position;

			//do we have trailing rn?

			if (fs.Length > 1)
			{
				byte[] vagnretur = new byte[2];
				fs.Seek(-2, SeekOrigin.Current);
				fs.Read(vagnretur, 0, 2);
				if (ASCIIEncoding.ASCII.GetString(vagnretur).Equals("\r\n"))
				{
					//move it back
					fs.Seek(-2, SeekOrigin.Current);
					position = fs.Position;
				}
				else
				{
					fs.Seek(0, SeekOrigin.End);
					TextWriter TWrite = new StreamWriter(fs);
					TWrite.WriteLine();
					TWrite.Flush();
				}
			}

			while (fs.Position > 0)
			{
				text.Initialize();
				//read one char
				fs.Read(text, 0, 1);
				string asciiText = ASCIIEncoding.ASCII.GetString(text);

				//moveback to the charachter before
				fs.Seek(-2, SeekOrigin.Current);

				if (asciiText.Equals("\n"))
				{
					fs.Read(text, 0, 1);
					asciiText = ASCIIEncoding.ASCII.GetString(text);
					if (asciiText.Equals("\r"))
					{
						fs.Seek(1, SeekOrigin.Current);
						break;
					}
				}
			}

			count = int.Parse((position - fs.Position).ToString());
			line = new byte[count];
			fs.Read(line, 0, count);
			fs.Seek(-count, SeekOrigin.Current);

			return ASCIIEncoding.ASCII.GetString(line);
		}

		
		
	}
}
