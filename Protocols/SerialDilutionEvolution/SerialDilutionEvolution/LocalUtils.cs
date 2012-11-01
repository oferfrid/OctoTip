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
	}
}
