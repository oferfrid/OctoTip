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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ProtocolUserControl));
			this.textBoxStatus = new System.Windows.Forms.TextBox();
			this.textBoxData = new System.Windows.Forms.TextBox();
			this.buttonStop = new System.Windows.Forms.Button();
			this.checkBoxStartPause = new System.Windows.Forms.CheckBox();
			this.EditParametersbutton = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// textBoxStatus
			// 
			this.textBoxStatus.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
									| System.Windows.Forms.AnchorStyles.Left)));
			this.textBoxStatus.Location = new System.Drawing.Point(3, 3);
			this.textBoxStatus.Multiline = true;
			this.textBoxStatus.Name = "textBoxStatus";
			this.textBoxStatus.Size = new System.Drawing.Size(92, 94);
			this.textBoxStatus.TabIndex = 0;
			// 
			// textBoxData
			// 
			this.textBoxData.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
									| System.Windows.Forms.AnchorStyles.Left) 
									| System.Windows.Forms.AnchorStyles.Right)));
			this.textBoxData.Location = new System.Drawing.Point(101, 3);
			this.textBoxData.Multiline = true;
			this.textBoxData.Name = "textBoxData";
			this.textBoxData.Size = new System.Drawing.Size(162, 94);
			this.textBoxData.TabIndex = 1;
			// 
			// buttonStop
			// 
			this.buttonStop.Anchor = System.Windows.Forms.AnchorStyles.Right;
			this.buttonStop.Enabled = false;
			this.buttonStop.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
			this.buttonStop.Image = ((System.Drawing.Image)(resources.GetObject("buttonStop.Image")));
			this.buttonStop.Location = new System.Drawing.Point(270, 29);
			this.buttonStop.Name = "buttonStop";
			this.buttonStop.Size = new System.Drawing.Size(55, 30);
			this.buttonStop.TabIndex = 2;
			this.buttonStop.Text = "Stop";
			this.buttonStop.UseVisualStyleBackColor = true;
			this.buttonStop.Click += new System.EventHandler(this.ButtonStopClick);
			// 
			// checkBoxStartPause
			// 
			this.checkBoxStartPause.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.checkBoxStartPause.Appearance = System.Windows.Forms.Appearance.Button;
			this.checkBoxStartPause.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
			this.checkBoxStartPause.Image = ((System.Drawing.Image)(resources.GetObject("checkBoxStartPause.Image")));
			this.checkBoxStartPause.Location = new System.Drawing.Point(270, 65);
			this.checkBoxStartPause.Name = "checkBoxStartPause";
			this.checkBoxStartPause.Size = new System.Drawing.Size(55, 30);
			this.checkBoxStartPause.TabIndex = 3;
			this.checkBoxStartPause.Text = "Start";
			this.checkBoxStartPause.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this.checkBoxStartPause.UseVisualStyleBackColor = true;
			this.checkBoxStartPause.CheckedChanged += new System.EventHandler(this.CheckBoxStartPauseCheckedChanged);
			// 
			// EditParametersbutton
			// 
			this.EditParametersbutton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.EditParametersbutton.Location = new System.Drawing.Point(270, 3);
			this.EditParametersbutton.Name = "EditParametersbutton";
			this.EditParametersbutton.Size = new System.Drawing.Size(55, 20);
			this.EditParametersbutton.TabIndex = 4;
			this.EditParametersbutton.Text = "Edit Parameters";
			this.EditParametersbutton.UseVisualStyleBackColor = true;
			this.EditParametersbutton.Click += new System.EventHandler(this.EditParametersbuttonClick);
			// 
			// ProtocolUserControl
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
			this.AutoSize = true;
			this.Controls.Add(this.EditParametersbutton);
			this.Controls.Add(this.checkBoxStartPause);
			this.Controls.Add(this.buttonStop);
			this.Controls.Add(this.textBoxData);
			this.Controls.Add(this.textBoxStatus);
			this.Name = "ProtocolUserControl";
			this.Size = new System.Drawing.Size(332, 100);
			this.ResumeLayout(false);
			this.PerformLayout();
		}
		private System.Windows.Forms.Button EditParametersbutton;
		private System.Windows.Forms.CheckBox checkBoxStartPause;
		private System.Windows.Forms.Button buttonStop;
		private System.Windows.Forms.TextBox textBoxStatus;
		private System.Windows.Forms.TextBox textBoxData;
	}
}
