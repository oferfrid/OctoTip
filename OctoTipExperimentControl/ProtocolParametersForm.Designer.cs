/*
 * Created by SharpDevelop.
 * User: oferfrid
 * Date: 02/10/2011
 * Time: 15:36
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
namespace OctoTip.OctoTipExperimentControl
{
	partial class ProtocolParametersForm
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
			this.textBox1 = new System.Windows.Forms.TextBox();
			this.ParametersPanel = new System.Windows.Forms.Panel();
			this.Updatebutton = new System.Windows.Forms.Button();
			this.Cancelbutton = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// textBox1
			// 
			this.textBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
									| System.Windows.Forms.AnchorStyles.Left) 
									| System.Windows.Forms.AnchorStyles.Right)));
			this.textBox1.Location = new System.Drawing.Point(12, 210);
			this.textBox1.Multiline = true;
			this.textBox1.Name = "textBox1";
			this.textBox1.Size = new System.Drawing.Size(199, 51);
			this.textBox1.TabIndex = 0;
			// 
			// ParametersPanel
			// 
			this.ParametersPanel.Location = new System.Drawing.Point(12, 12);
			this.ParametersPanel.Name = "ParametersPanel";
			this.ParametersPanel.Size = new System.Drawing.Size(313, 182);
			this.ParametersPanel.TabIndex = 1;
			// 
			// Updatebutton
			// 
			this.Updatebutton.Location = new System.Drawing.Point(287, 233);
			this.Updatebutton.Name = "Updatebutton";
			this.Updatebutton.Size = new System.Drawing.Size(55, 28);
			this.Updatebutton.TabIndex = 2;
			this.Updatebutton.Text = "Update";
			this.Updatebutton.UseVisualStyleBackColor = true;
			this.Updatebutton.Click += new System.EventHandler(this.UpdatebuttonClick);
			// 
			// Cancelbutton
			// 
			this.Cancelbutton.Location = new System.Drawing.Point(226, 233);
			this.Cancelbutton.Name = "Cancelbutton";
			this.Cancelbutton.Size = new System.Drawing.Size(55, 28);
			this.Cancelbutton.TabIndex = 3;
			this.Cancelbutton.Text = "Cancel";
			this.Cancelbutton.UseVisualStyleBackColor = true;
			this.Cancelbutton.Click += new System.EventHandler(this.CancelbuttonClick);
			// 
			// ProtocolParametersForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(354, 273);
			this.Controls.Add(this.Cancelbutton);
			this.Controls.Add(this.Updatebutton);
			this.Controls.Add(this.ParametersPanel);
			this.Controls.Add(this.textBox1);
			this.Name = "ProtocolParametersForm";
			this.Text = "ProtocolParametersForm";
			this.Load += new System.EventHandler(this.ProtocolParametersFormLoad);
			this.ResumeLayout(false);
			this.PerformLayout();
		}
		private System.Windows.Forms.Button Cancelbutton;
		private System.Windows.Forms.Button Updatebutton;
		private System.Windows.Forms.Panel ParametersPanel;
		private System.Windows.Forms.TextBox textBox1;
	}
}
