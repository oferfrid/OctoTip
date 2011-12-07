/*
 * Created by SharpDevelop.
 * User: Tecan
 * Date: 01/11/2011
 * Time: 19:35
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
namespace OctoTip.OctoTipExperimentControl
{
	partial class ProtocolLogForm
	{
		/// <summary>
		/// Designer variable used to keep track of non-visual components.
		/// </summary>
		private System.ComponentModel.IContainer components = null;
		
		/// <summary>
		/// Disposes resources used by the form.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing) {
				if (components != null) {
					components.Dispose();
				}
			}
			base.Dispose(disposing);
		}
		
		/// <summary>
		/// This method is required for Windows Forms designer support.
		/// Do not change the method contents inside the source code editor. The Forms designer might
		/// not be able to load this method if it was changed manually.
		/// </summary>
		private void InitializeComponent()
		{
			this.textBoxProtocolLog = new System.Windows.Forms.TextBox();
			this.SuspendLayout();
			// 
			// textBoxProtocolLog
			// 
			this.textBoxProtocolLog.Location = new System.Drawing.Point(12, 12);
			this.textBoxProtocolLog.Multiline = true;
			this.textBoxProtocolLog.Name = "textBoxProtocolLog";
			this.textBoxProtocolLog.ScrollBars = System.Windows.Forms.ScrollBars.Both;
			this.textBoxProtocolLog.Size = new System.Drawing.Size(582, 352);
			this.textBoxProtocolLog.TabIndex = 0;
			// 
			// ProtocolLogForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(606, 376);
			this.Controls.Add(this.textBoxProtocolLog);
			this.Name = "ProtocolLogForm";
			this.Text = "ProtocolLog";
			this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.ProtocolLogFormFormClosed);
			this.Load += new System.EventHandler(this.ProtocolLogLoad);
			this.ResumeLayout(false);
			this.PerformLayout();
		}
		private System.Windows.Forms.TextBox textBoxProtocolLog;
	}
}
