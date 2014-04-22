/*
 * Created by SharpDevelop.
 * User: oferfrid
 * Date: 22/04/2014
 * Time: 18:25
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
namespace OctoTip.OctoTipPlus
{
	partial class ShowLogForm
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ShowLogForm));
			this.LogRichTextBox = new System.Windows.Forms.RichTextBox();
			this.CloseButton = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// LogRichTextBox
			// 
			this.LogRichTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
									| System.Windows.Forms.AnchorStyles.Left) 
									| System.Windows.Forms.AnchorStyles.Right)));
			this.LogRichTextBox.Location = new System.Drawing.Point(12, 12);
			this.LogRichTextBox.Name = "LogRichTextBox";
			this.LogRichTextBox.Size = new System.Drawing.Size(722, 512);
			this.LogRichTextBox.TabIndex = 0;
			this.LogRichTextBox.Text = "";
			// 
			// CloseButton
			// 
			this.CloseButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.CloseButton.Location = new System.Drawing.Point(659, 528);
			this.CloseButton.Name = "CloseButton";
			this.CloseButton.Size = new System.Drawing.Size(75, 23);
			this.CloseButton.TabIndex = 1;
			this.CloseButton.Text = "Close";
			this.CloseButton.UseVisualStyleBackColor = true;
			this.CloseButton.Click += new System.EventHandler(this.CloseButtonClick);
			// 
			// ShowLogForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(746, 563);
			this.Controls.Add(this.CloseButton);
			this.Controls.Add(this.LogRichTextBox);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "ShowLogForm";
			this.Text = "Show Log";
			this.Load += new System.EventHandler(this.ShowLogFormLoad);
			this.ResumeLayout(false);
		}
		private System.Windows.Forms.Button CloseButton;
		private System.Windows.Forms.RichTextBox LogRichTextBox;
	}
}
