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

namespace SerialDilutionEvolution
{
	/// <summary>
	/// Description of Utils.
	/// </summary>
	public static class LocalUtils
	{
		const int WellsInDilution = 3;
		const int WellsInRow = 16;
		
		public static int GetNext384Index(string SharedResourcesFilePath)
		{
			string FilePath  =SharedResourcesFilePath +  @"384PlateIndex.txt";
			FileStream fileStream = new FileStream( FilePath, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.None);
			TextReader TRead = new StreamReader(fileStream);
			int Index = Convert.ToInt32(TRead.ReadLine());
			
			// Set the stream position to the beginning of the file.
			fileStream.Seek(0, SeekOrigin.Begin);
			TextWriter TWrite = new StreamWriter(fileStream);
			int NewIndex = Index + 1;
			TWrite.WriteLine(NewIndex.ToString());
			TWrite.Flush();
			fileStream.Close();
			return Index;
			
		}
		public static int GetNext384Pos(int Index)
		{
			int Pos = Convert.ToInt32((Math.Floor((double)(Index-1)/WellsInRow)*WellsInDilution*WellsInRow)+(Index-1)%(WellsInDilution*WellsInRow)+1);
			return Pos;
		}
		
		public static int GetNextFreezPos(string SharedResourcesFilePath,string Title)
		{
			string FilePath  = SharedResourcesFilePath + @"FreezPlate.csv";
			FileStream fileStream = new FileStream( FilePath, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.None);
			TextReader TRead = new StreamReader(fileStream);
			int Index = Convert.ToInt32(ReadLastline(fileStream).Split(new Char[] {','})[0]);


			fileStream.Seek(0, SeekOrigin.End);
			TextWriter TWrite = new StreamWriter(fileStream);
			int NewIndex = Index + 1;
			
			TWrite.WriteLine(NewIndex.ToString() + ","+ "Title");
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

				Console.WriteLine(ASCIIEncoding.ASCII.GetString(vagnretur));
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
