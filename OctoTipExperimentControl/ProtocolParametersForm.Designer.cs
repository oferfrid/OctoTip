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
			this.ParametersPanel = new System.Windows.Forms.Panel();
			this.Updatebutton = new System.Windows.Forms.Button();
			this.Cancelbutton = new System.Windows.Forms.Button();
			this.Errorlabel = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// ParametersPanel
			// 
			this.ParametersPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
									| System.Windows.Forms.AnchorStyles.Left) 
									| System.Windows.Forms.AnchorStyles.Right)));
			this.ParametersPanel.AutoScroll = true;
			this.ParametersPanel.Location = new System.Drawing.Point(4, 2);
			this.ParametersPanel.Name = "ParametersPanel";
			this.ParametersPanel.Size = new System.Drawing.Size(360, 359);
			this.ParametersPanel.TabIndex = 1;
			// 
			// Updatebutton
			// 
			this.Updatebutton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.Updatebutton.Location = new System.Drawing.Point(297, 409);
			this.Updatebutton.Name = "Updatebutton";
			this.Updatebutton.Size = new System.Drawing.Size(55, 28);
			this.Updatebutton.TabIndex = 2;
			this.Updatebutton.Text = "Update";
			this.Updatebutton.UseVisualStyleBackColor = true;
			this.Updatebutton.Click += new System.EventHandler(this.UpdatebuttonClick);
			// 
			// Cancelbutton
			// 
			this.Cancelbutton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.Cancelbutton.Location = new System.Drawing.Point(236, 409);
			this.Cancelbutton.Name = "Cancelbutton";
			this.Cancelbutton.Size = new System.Drawing.Size(55, 28);
			this.Cancelbutton.TabIndex = 3;
			this.Cancelbutton.Text = "Cancel";
			this.Cancelbutton.UseVisualStyleBackColor = true;
			this.Cancelbutton.Click += new System.EventHandler(this.CancelbuttonClick);
			// 
			// Errorlabel
			// 
			this.Errorlabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.Errorlabel.BackColor = System.Drawing.SystemColors.Control;
			this.Errorlabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
			this.Errorlabel.ForeColor = System.Drawing.Color.Red;
			this.Errorlabel.Location = new System.Drawing.Point(4, 364);
			this.Errorlabel.Name = "Errorlabel";
			this.Errorlabel.Size = new System.Drawing.Size(226, 76);
			this.Errorlabel.TabIndex = 4;
			// 
			// ProtocolParametersForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(364, 449);
			this.Controls.Add(this.Cancelbutton);
			this.Controls.Add(this.Errorlabel);
			this.Controls.Add(this.Updatebutton);
			this.Controls.Add(this.ParametersPanel);
			this.Name = "ProtocolParametersForm";
			this.Text = "ProtocolParametersForm";
			this.Load += new System.EventHandler(this.ProtocolParametersFormLoad);
			this.ResumeLayout(false);
		}
		private System.Windows.Forms.Label Errorlabel;
		private System.Windows.Forms.Button Cancelbutton;
		private System.Windows.Forms.Button Updatebutton;
		private System.Windows.Forms.Panel ParametersPanel;
	}
}
