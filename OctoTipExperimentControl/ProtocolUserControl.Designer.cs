/*
 * Created by SharpDevelop.
 * User: oferfrid
 * Date: 02/10/2011
 * Time: 10:26
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
namespace OctoTip.OctoTipExperimentControl
{
	partial class ProtocolUserControl
	{
		/// <summary>
		/// Designer variable used to keep track of non-visual components.
		/// </summary>
		private System.ComponentModel.IContainer components = null;
		
		/// <summary>
		/// Disposes resources used by the control.
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
			this.textBoxStatus = new System.Windows.Forms.TextBox();
			this.textBoxData = new System.Windows.Forms.TextBox();
			this.buttonStop = new System.Windows.Forms.Button();
			this.checkBoxStartPause = new System.Windows.Forms.CheckBox();
			this.SuspendLayout();
			// 
			// textBoxStatus
			// 
			this.textBoxStatus.Location = new System.Drawing.Point(3, 3);
			this.textBoxStatus.Multiline = true;
			this.textBoxStatus.Name = "textBoxStatus";
			this.textBoxStatus.Size = new System.Drawing.Size(92, 93);
			this.textBoxStatus.TabIndex = 0;
			// 
			// textBoxData
			// 
			this.textBoxData.Location = new System.Drawing.Point(101, 3);
			this.textBoxData.Multiline = true;
			this.textBoxData.Name = "textBoxData";
			this.textBoxData.Size = new System.Drawing.Size(100, 93);
			this.textBoxData.TabIndex = 1;
			// 
			// buttonStop
			// 
			this.buttonStop.Location = new System.Drawing.Point(246, 46);
			this.buttonStop.Name = "buttonStop";
			this.buttonStop.Size = new System.Drawing.Size(52, 49);
			this.buttonStop.TabIndex = 2;
			this.buttonStop.Text = "buttonStop";
			this.buttonStop.UseVisualStyleBackColor = true;
			this.buttonStop.Click += new System.EventHandler(this.ButtonStopClick);
			// 
			// checkBoxStartPause
			// 
			this.checkBoxStartPause.Appearance = System.Windows.Forms.Appearance.Button;
			this.checkBoxStartPause.Location = new System.Drawing.Point(344, 46);
			this.checkBoxStartPause.Name = "checkBoxStartPause";
			this.checkBoxStartPause.Size = new System.Drawing.Size(66, 48);
			this.checkBoxStartPause.TabIndex = 3;
			this.checkBoxStartPause.Text = "checkBoxStartPause";
			this.checkBoxStartPause.UseVisualStyleBackColor = true;
			// 
			// ProtocolUserControl
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.checkBoxStartPause);
			this.Controls.Add(this.buttonStop);
			this.Controls.Add(this.textBoxData);
			this.Controls.Add(this.textBoxStatus);
			this.Name = "ProtocolUserControl";
			this.Size = new System.Drawing.Size(442, 99);
			this.ResumeLayout(false);
			this.PerformLayout();
		}
		private System.Windows.Forms.CheckBox checkBoxStartPause;
		private System.Windows.Forms.Button buttonStop;
		private System.Windows.Forms.TextBox textBoxStatus;
		private System.Windows.Forms.TextBox textBoxData;
	}
}
