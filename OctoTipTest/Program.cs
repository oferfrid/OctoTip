/*
 * Created by SharpDevelop.
 * User: oferfrid
 * Date: 21/09/2011
 * Time: 10:59
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Reflection;
using System.Threading;
using System.Xml.Serialization;
using System.Xml;
using System.IO;
using System.Xml.XPath;
using OctoTip.OctoTipExperiments;
using OctoTip.OctoTipLib;
using OctoTip.OctoTipExperiments.Core.Base;

namespace OctoTip.OctoTipTest
{
	
	class Program
	{
//		static void  HandleChangeStatusEvent(object sender, RobotWrapperEventArgs e)
//		{
//			Console.WriteLine("received this message: {0}", e.ScriptTerminationStatus.ToString());
//		}
//
		public static void Main(string[] args)
		{
			
			//XmlSerializer writer =	new XmlSerializer(typeof(WaitState));
			
			
			
			
			//FileInfo[] files = DI.GetFiles("*.xml", SearchOption.TopDirectoryOnly);
			
						
			Console.ReadKey();
		}
		
		
		static private int CalcIndFromPlatePos(string Pos)
		{
			int Row = char.Parse(Pos.Substring(0,1))-'A';
			int Col =  Convert.ToInt32(Pos.Substring(1,Pos.Length-1))-1;
			return Row + Col*2+1;
		}
		
	}
}


