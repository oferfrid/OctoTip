/*
 * Created by SharpDevelop.
 * User: oferfrid
 * Date: 25/09/2011
 * Time: 10:12
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Threading;
using System.Windows.Forms;

namespace OctoTip.OctoTipExperimentControl
{
	/// <summary>
	/// Class with program entry point.
	/// </summary>
	internal sealed class Program
	{
		/// <summary>
		/// Program entry point.
		/// </summary>
		[STAThread]
		private static void Main(string[] args)
		{
			//Application.SetUnhandledExceptionMode(UnhandledExceptionMode.Automatic);
			//Application.ThreadException += new ThreadExceptionEventHandler(new ThreadExceptionHandler().ApplicationThreadException);
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			Application.Run(new MainForm());
		}
		
		
		   /// <summary>

        /// Handles any thread exceptions

        /// </summary>

        public class ThreadExceptionHandler

        {

            public void ApplicationThreadException(object sender, ThreadExceptionEventArgs e)

            {

                MessageBox.Show(e.Exception.Message, "An exception occurred:", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }

        }
	}
}
