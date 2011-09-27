/*
 * Created by SharpDevelop.
 * User: oferfrid
 * Date: 25/09/2011
 * Time: 10:14
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
namespace OctoTip.OctoTipExperimentControl
{
	partial class MainForm
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
			this.toolStripContainer1 = new System.Windows.Forms.ToolStripContainer();
			this.splitContainer1 = new System.Windows.Forms.SplitContainer();
			this.Protocolpanel = new System.Windows.Forms.Panel();
			this.statusStrip1 = new System.Windows.Forms.StatusStrip();
			this.ProtocoltoolStrip = new System.Windows.Forms.ToolStrip();
			this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
			this.Protocol1 = new System.Windows.Forms.ToolStripButton();
			this.menuStrip1 = new System.Windows.Forms.MenuStrip();
			this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStrip1 = new System.Windows.Forms.ToolStrip();
			this.toolStripButtonRefreshProtocols = new System.Windows.Forms.ToolStripButton();
			this.toolStripContainer1.ContentPanel.SuspendLayout();
			this.toolStripContainer1.LeftToolStripPanel.SuspendLayout();
			this.toolStripContainer1.TopToolStripPanel.SuspendLayout();
			this.toolStripContainer1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
			this.splitContainer1.Panel1.SuspendLayout();
			this.splitContainer1.SuspendLayout();
			this.ProtocoltoolStrip.SuspendLayout();
			this.menuStrip1.SuspendLayout();
			this.toolStrip1.SuspendLayout();
			this.SuspendLayout();
			// 
			// toolStripContainer1
			// 
			this.toolStripContainer1.BottomToolStripPanelVisible = false;
			// 
			// toolStripContainer1.ContentPanel
			// 
			this.toolStripContainer1.ContentPanel.Controls.Add(this.splitContainer1);
			this.toolStripContainer1.ContentPanel.Controls.Add(this.statusStrip1);
			this.toolStripContainer1.ContentPanel.Size = new System.Drawing.Size(750, 548);
			this.toolStripContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
			// 
			// toolStripContainer1.LeftToolStripPanel
			// 
			this.toolStripContainer1.LeftToolStripPanel.Controls.Add(this.ProtocoltoolStrip);
			this.toolStripContainer1.Location = new System.Drawing.Point(0, 0);
			this.toolStripContainer1.Name = "toolStripContainer1";
			this.toolStripContainer1.RightToolStripPanelVisible = false;
			this.toolStripContainer1.Size = new System.Drawing.Size(840, 597);
			this.toolStripContainer1.TabIndex = 0;
			this.toolStripContainer1.Text = "toolStripContainer1";
			// 
			// toolStripContainer1.TopToolStripPanel
			// 
			this.toolStripContainer1.TopToolStripPanel.Controls.Add(this.menuStrip1);
			this.toolStripContainer1.TopToolStripPanel.Controls.Add(this.toolStrip1);
			// 
			// splitContainer1
			// 
			this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.splitContainer1.Location = new System.Drawing.Point(0, 0);
			this.splitContainer1.Name = "splitContainer1";
			// 
			// splitContainer1.Panel1
			// 
			this.splitContainer1.Panel1.Controls.Add(this.Protocolpanel);
			this.splitContainer1.Size = new System.Drawing.Size(750, 526);
			this.splitContainer1.SplitterDistance = 536;
			this.splitContainer1.TabIndex = 1;
			// 
			// Protocolpanel
			// 
			this.Protocolpanel.AutoScroll = true;
			this.Protocolpanel.Dock = System.Windows.Forms.DockStyle.Fill;
			this.Protocolpanel.Location = new System.Drawing.Point(0, 0);
			this.Protocolpanel.Name = "Protocolpanel";
			this.Protocolpanel.Size = new System.Drawing.Size(536, 526);
			this.Protocolpanel.TabIndex = 0;
			// 
			// statusStrip1
			// 
			this.statusStrip1.Location = new System.Drawing.Point(0, 526);
			this.statusStrip1.Name = "statusStrip1";
			this.statusStrip1.Size = new System.Drawing.Size(750, 22);
			this.statusStrip1.TabIndex = 0;
			this.statusStrip1.Text = "statusStrip1";
			// 
			// ProtocoltoolStrip
			// 
			this.ProtocoltoolStrip.Dock = System.Windows.Forms.DockStyle.None;
			this.ProtocoltoolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
									this.toolStripLabel1,
									this.Protocol1});
			this.ProtocoltoolStrip.Location = new System.Drawing.Point(0, 3);
			this.ProtocoltoolStrip.Name = "ProtocoltoolStrip";
			this.ProtocoltoolStrip.Size = new System.Drawing.Size(90, 48);
			this.ProtocoltoolStrip.TabIndex = 0;
			// 
			// toolStripLabel1
			// 
			this.toolStripLabel1.Name = "toolStripLabel1";
			this.toolStripLabel1.Size = new System.Drawing.Size(88, 13);
			this.toolStripLabel1.Text = "Avalble Protocols";
			// 
			// Protocol1
			// 
			this.Protocol1.Image = ((System.Drawing.Image)(resources.GetObject("Protocol1.Image")));
			this.Protocol1.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.Protocol1.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.Protocol1.Name = "Protocol1";
			this.Protocol1.Size = new System.Drawing.Size(88, 20);
			this.Protocol1.Text = "Protocol1";
			this.Protocol1.ToolTipText = "Protocol1gt";
			this.Protocol1.Click += new System.EventHandler(this.Protocol1Click);
			// 
			// menuStrip1
			// 
			this.menuStrip1.Dock = System.Windows.Forms.DockStyle.None;
			this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
									this.fileToolStripMenuItem});
			this.menuStrip1.Location = new System.Drawing.Point(0, 0);
			this.menuStrip1.Name = "menuStrip1";
			this.menuStrip1.Size = new System.Drawing.Size(840, 24);
			this.menuStrip1.TabIndex = 1;
			this.menuStrip1.Text = "menuStrip1";
			// 
			// fileToolStripMenuItem
			// 
			this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
			this.fileToolStripMenuItem.Size = new System.Drawing.Size(35, 20);
			this.fileToolStripMenuItem.Text = "File";
			// 
			// toolStrip1
			// 
			this.toolStrip1.Dock = System.Windows.Forms.DockStyle.None;
			this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
									this.toolStripButtonRefreshProtocols});
			this.toolStrip1.Location = new System.Drawing.Point(3, 24);
			this.toolStrip1.Name = "toolStrip1";
			this.toolStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
			this.toolStrip1.Size = new System.Drawing.Size(33, 25);
			this.toolStrip1.TabIndex = 0;
			// 
			// toolStripButtonRefreshProtocols
			// 
			this.toolStripButtonRefreshProtocols.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.toolStripButtonRefreshProtocols.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonRefreshProtocols.Image")));
			this.toolStripButtonRefreshProtocols.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.toolStripButtonRefreshProtocols.Name = "toolStripButtonRefreshProtocols";
			this.toolStripButtonRefreshProtocols.Size = new System.Drawing.Size(23, 22);
			this.toolStripButtonRefreshProtocols.Text = "Refresh Protocols";
			this.toolStripButtonRefreshProtocols.Click += new System.EventHandler(this.ToolStripButtonRefreshProtocolsClick);
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(840, 597);
			this.Controls.Add(this.toolStripContainer1);
			this.MainMenuStrip = this.menuStrip1;
			this.Name = "MainForm";
			this.Text = "MainForm";
			this.Load += new System.EventHandler(this.MainFormLoad);
			this.toolStripContainer1.ContentPanel.ResumeLayout(false);
			this.toolStripContainer1.ContentPanel.PerformLayout();
			this.toolStripContainer1.LeftToolStripPanel.ResumeLayout(false);
			this.toolStripContainer1.LeftToolStripPanel.PerformLayout();
			this.toolStripContainer1.TopToolStripPanel.ResumeLayout(false);
			this.toolStripContainer1.TopToolStripPanel.PerformLayout();
			this.toolStripContainer1.ResumeLayout(false);
			this.toolStripContainer1.PerformLayout();
			this.splitContainer1.Panel1.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
			this.splitContainer1.ResumeLayout(false);
			this.ProtocoltoolStrip.ResumeLayout(false);
			this.ProtocoltoolStrip.PerformLayout();
			this.menuStrip1.ResumeLayout(false);
			this.menuStrip1.PerformLayout();
			this.toolStrip1.ResumeLayout(false);
			this.toolStrip1.PerformLayout();
			this.ResumeLayout(false);
		}
		private System.Windows.Forms.Panel Protocolpanel;
		private System.Windows.Forms.SplitContainer splitContainer1;
		private System.Windows.Forms.ToolStripButton toolStripButtonRefreshProtocols;
		private System.Windows.Forms.ToolStrip ProtocoltoolStrip;
		private System.Windows.Forms.ToolStripButton Protocol1;
		private System.Windows.Forms.ToolStripLabel toolStripLabel1;
		private System.Windows.Forms.ToolStrip toolStrip1;
		private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
		private System.Windows.Forms.MenuStrip menuStrip1;
		private System.Windows.Forms.StatusStrip statusStrip1;
		private System.Windows.Forms.ToolStripContainer toolStripContainer1;
		
		
	}
}
