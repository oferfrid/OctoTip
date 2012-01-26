/*
 * Created by SharpDevelop.
 * User: oferfrid
 * Date: 26/01/2012
 * Time: 14:37
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
namespace OctoTip.OctoTipExperimentControl.ProtocolParametersFieldUserControls
{
	partial class ProtocolParametersFieldUserControl
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
			this.components = new System.ComponentModel.Container();
			this.Fieldlabel = new System.Windows.Forms.Label();
			this.ControlPanel = new System.Windows.Forms.Panel();
			this.ControlErrorProvider = new System.Windows.Forms.ErrorProvider(this.components);
			((System.ComponentModel.ISupportInitialize)(this.ControlErrorProvider)).BeginInit();
			this.SuspendLayout();
			// 
			// Fieldlabel
			// 
			this.Fieldlabel.Location = new System.Drawing.Point(3, 2);
			this.Fieldlabel.Name = "Fieldlabel";
			this.Fieldlabel.Size = new System.Drawing.Size(176, 23);
			this.Fieldlabel.TabIndex = 0;
			this.Fieldlabel.Text = "Field label";
			// 
			// ControlPanel
			// 
			this.ControlPanel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
									| System.Windows.Forms.AnchorStyles.Right)));
			this.ControlPanel.AutoSize = true;
			this.ControlPanel.Location = new System.Drawing.Point(204, 4);
			this.ControlPanel.Name = "ControlPanel";
			this.ControlPanel.Size = new System.Drawing.Size(215, 18);
			this.ControlPanel.TabIndex = 1;
			// 
			// ControlErrorProvider
			// 
			this.ControlErrorProvider.ContainerControl = this;
			// 
			// ProtocolParametersFieldUserControl
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.ControlPanel);
			this.Controls.Add(this.Fieldlabel);
			this.Name = "ProtocolParametersFieldUserControl";
			this.Size = new System.Drawing.Size(457, 25);
			((System.ComponentModel.ISupportInitialize)(this.ControlErrorProvider)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();
		}
		private System.Windows.Forms.ErrorProvider ControlErrorProvider;
		private System.Windows.Forms.Panel ControlPanel;
		private System.Windows.Forms.Label Fieldlabel;
	}
}
