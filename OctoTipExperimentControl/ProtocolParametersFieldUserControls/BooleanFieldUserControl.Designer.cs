/*
 * Created by SharpDevelop.
 * User: Tecan
 * Date: 26/01/2012
 * Time: 12:12
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
namespace OctoTip.OctoTipExperimentControl.ProtocolParametersFieldUserControls
{
	partial class BooleanFieldUserControl
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
			this.ValueCheckBox = new System.Windows.Forms.CheckBox();
			this.SuspendLayout();
			// 
			// ValueCheckBox
			// 
			this.ValueCheckBox.Location = new System.Drawing.Point(47, 35);
			this.ValueCheckBox.Name = "ValueCheckBox";
			this.ValueCheckBox.Size = new System.Drawing.Size(104, 24);
			this.ValueCheckBox.TabIndex = 0;
			this.ValueCheckBox.Text = "checkBox1";
			this.ValueCheckBox.UseVisualStyleBackColor = true;
			// 
			// BooleanFieldUserControl
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.ValueCheckBox);
			this.Name = "BooleanFieldUserControl";
			this.Size = new System.Drawing.Size(305, 97);
			this.ResumeLayout(false);
		}
		private System.Windows.Forms.CheckBox ValueCheckBox;
	}
}
