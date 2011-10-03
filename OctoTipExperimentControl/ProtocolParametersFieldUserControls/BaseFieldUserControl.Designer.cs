/*
 * Created by SharpDevelop.
 * User: oferfrid
 * Date: 03/10/2011
 * Time: 13:25
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
namespace OctoTip.OctoTipExperimentControl.ProtocolParametersFieldUserControls
{
	partial class BaseFieldUserControl
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
		protected void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			this.errorProvider = new System.Windows.Forms.ErrorProvider(this.components);
			this.ValueTextBox = new System.Windows.Forms.TextBox();
			this.ParamNameLabel = new System.Windows.Forms.Label();
			((System.ComponentModel.ISupportInitialize)(this.errorProvider)).BeginInit();
			this.SuspendLayout();
			// 
			// errorProvider
			// 
			this.errorProvider.ContainerControl = this;
			// 
			// ValueTextBox
			// 
			this.ValueTextBox.Location = new System.Drawing.Point(123, 0);
			this.ValueTextBox.Name = "ValueTextBox";
			this.ValueTextBox.Size = new System.Drawing.Size(145, 20);
			this.ValueTextBox.TabIndex = 0;
			// 
			// ParamNameLabel
			// 
			this.ParamNameLabel.Location = new System.Drawing.Point(13, 5);
			this.ParamNameLabel.Name = "ParamNameLabel";
			this.ParamNameLabel.Size = new System.Drawing.Size(104, 23);
			this.ParamNameLabel.TabIndex = 1;
			// 
			// BaseFieldUserControl
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.ParamNameLabel);
			this.Controls.Add(this.ValueTextBox);
			this.Name = "BaseFieldUserControl";
			this.Size = new System.Drawing.Size(321, 27);
			((System.ComponentModel.ISupportInitialize)(this.errorProvider)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();
		}
		protected System.Windows.Forms.TextBox ValueTextBox;
		protected System.Windows.Forms.Label ParamNameLabel;
		protected System.Windows.Forms.ErrorProvider errorProvider;
	}
}
