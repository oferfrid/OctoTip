/*
 * Created by SharpDevelop.
 * User: oferfrid
 * Date: 27/09/2011
 * Time: 08:21
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
namespace OctoTipExperimentControl
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
			this.textBox1 = new System.Windows.Forms.TextBox();
			this.checkBoxStartPause = new System.Windows.Forms.CheckBox();
			this.SuspendLayout();
			// 
			// textBox1
			// 
			this.textBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
			this.textBox1.Location = new System.Drawing.Point(11, 2);
			this.textBox1.Multiline = true;
			this.textBox1.Name = "textBox1";
			this.textBox1.Size = new System.Drawing.Size(232, 83);
			this.textBox1.TabIndex = 0;
			// 
			// checkBoxStartPause
			// 
			this.checkBoxStartPause.Appearance = System.Windows.Forms.Appearance.Button;
			this.checkBoxStartPause.Location = new System.Drawing.Point(348, 61);
			this.checkBoxStartPause.Name = "checkBoxStartPause";
			this.checkBoxStartPause.Size = new System.Drawing.Size(54, 24);
			this.checkBoxStartPause.TabIndex = 1;
			this.checkBoxStartPause.Text = "Start";
			this.checkBoxStartPause.UseVisualStyleBackColor = true;
			this.checkBoxStartPause.CheckedChanged += new System.EventHandler(this.CheckBoxStartPauseCheckedChanged);
			// 
			// ProtocolUserControl
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.AutoSize = true;
			this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.Controls.Add(this.checkBoxStartPause);
			this.Controls.Add(this.textBox1);
			this.Name = "ProtocolUserControl";
			this.Size = new System.Drawing.Size(405, 88);
			this.ResumeLayout(false);
			this.PerformLayout();
		}
		private System.Windows.Forms.CheckBox checkBoxStartPause;
		private System.Windows.Forms.TextBox textBox1;
	}
}
