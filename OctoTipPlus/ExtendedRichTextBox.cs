/*
 * Created by SharpDevelop.
 * User: oferfrid
 * Date: 22/04/2014
 * Time: 15:36
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Drawing;
using System.Windows.Forms;

namespace OctoTip.OctoTipPlus
{
	/// <summary>
	/// Description of RichTextBox.
	/// </summary>
	public class ExtendedRichTextBox:RichTextBox 
	{
		public void AppendText(string text, Color color)
		{
			this.SelectionStart = this.TextLength;
			this.SelectionLength = 0;

			this.SelectionColor = color;
			this.AppendText(text);
			this.SelectionColor = this.ForeColor;
			this.ScrollToCaret();
		}
	}
}
