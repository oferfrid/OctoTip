/*
 * Created by SharpDevelop.
 * User: oferfrid
 * Date: 22/04/2014
 * Time: 18:25
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Drawing;
using System.Windows.Forms;

namespace OctoTip.OctoTipPlus
{
	/// <summary>
	/// Description of ShowLogForm.
	/// </summary>
	public partial class ShowLogForm : Form
	{
		string LogText;
		public ShowLogForm(string LogText)
		{
			
			InitializeComponent();
			this.LogText = LogText ;
			
		}
		
		void CloseButtonClick(object sender, EventArgs e)
		{
			this.Close();
		}
		
		void ShowLogFormLoad(object sender, EventArgs e)
		{
			LogRichTextBox.Rtf = LogText;			
		}
	}
}
