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
			this.buttonClearLog = new System.Windows.Forms.Button();
			this.textBoxLog = new System.Windows.Forms.TextBox();
			this.statusStrip1 = new System.Windows.Forms.StatusStrip();
			this.toolStripStatusLabelProtocolCount = new System.Windows.Forms.ToolStripStatusLabel();
			this.ProtocoltoolStrip = new System.Windows.Forms.ToolStrip();
			this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
			this.Protocol1 = new System.Windows.Forms.ToolStripButton();
			this.menuStrip1 = new System.Windows.Forms.MenuStrip();
			this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.protocolsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.saveProtocolsAsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStrip1 = new System.Windows.Forms.ToolStrip();
			this.toolStripButtonRefreshProtocols = new System.Windows.Forms.ToolStripButton();
			this.toolStripContainer1.ContentPanel.SuspendLayout();
			this.toolStripContainer1.LeftToolStripPanel.SuspendLayout();
			this.toolStripContainer1.TopToolStripPanel.SuspendLayout();
			this.toolStripContainer1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
			this.splitContainer1.Panel1.SuspendLayout();
			this.splitContainer1.Panel2.SuspendLayout();
			this.splitContainer1.SuspendLayout();
			this.statusStrip1.SuspendLayout();
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
			this.toolStripContainer1.ContentPanel.Size = new System.Drawing.Size(834, 564);
			this.toolStripContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
			// 
			// toolStripContainer1.LeftToolStripPanel
			// 
			this.toolStripContainer1.LeftToolStripPanel.Controls.Add(this.ProtocoltoolStrip);
			this.toolStripContainer1.Location = new System.Drawing.Point(0, 0);
			this.toolStripContainer1.Name = "toolStripContainer1";
			this.toolStripContainer1.RightToolStripPanelVisible = false;
			this.toolStripContainer1.Size = new System.Drawing.Size(924, 613);
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
			// 
			// splitContainer1.Panel2
			// 
			this.splitContainer1.Panel2.Controls.Add(this.buttonClearLog);
			this.splitContainer1.Panel2.Controls.Add(this.textBoxLog);
			this.splitContainer1.Size = new System.Drawing.Size(834, 542);
			this.splitContainer1.SplitterDistance = 596;
			this.splitContainer1.TabIndex = 1;
			// 
			// Protocolpanel
			// 
			this.Protocolpanel.AutoScroll = true;
			this.Protocolpanel.Dock = System.Windows.Forms.DockStyle.Fill;
			this.Protocolpanel.Location = new System.Drawing.Point(0, 0);
			this.Protocolpanel.Name = "Protocolpanel";
			this.Protocolpanel.Size = new System.Drawing.Size(596, 542);
			this.Protocolpanel.TabIndex = 0;
			// 
			// buttonClearLog
			// 
			this.buttonClearLog.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonClearLog.Location = new System.Drawing.Point(2, 273);
			this.buttonClearLog.Name = "buttonClearLog";
			this.buttonClearLog.Size = new System.Drawing.Size(75, 23);
			this.buttonClearLog.TabIndex = 2;
			this.buttonClearLog.Text = "Clear Log";
			this.buttonClearLog.UseVisualStyleBackColor = true;
			this.buttonClearLog.Click += new System.EventHandler(this.ButtonClearLogClick);
			// 
			// textBoxLog
			// 
			this.textBoxLog.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.textBoxLog.Location = new System.Drawing.Point(3, 302);
			this.textBoxLog.Multiline = true;
			this.textBoxLog.Name = "textBoxLog";
			this.textBoxLog.Size = new System.Drawing.Size(228, 237);
			this.textBoxLog.TabIndex = 1;
			// 
			// statusStrip1
			// 
			this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
									this.toolStripStatusLabelProtocolCount});
			this.statusStrip1.Location = new System.Drawing.Point(0, 542);
			this.statusStrip1.Name = "statusStrip1";
			this.statusStrip1.Size = new System.Drawing.Size(834, 22);
			this.statusStrip1.TabIndex = 0;
			this.statusStrip1.Text = "statusStrip1";
			// 
			// toolStripStatusLabelProtocolCount
			// 
			this.toolStripStatusLabelProtocolCount.Name = "toolStripStatusLabelProtocolCount";
			this.toolStripStatusLabelProtocolCount.Size = new System.Drawing.Size(61, 17);
			this.toolStripStatusLabelProtocolCount.Text = "Protocols:0";
			// 
			// ProtocoltoolStrip
			// 
			this.ProtocoltoolStrip.Dock = System.Windows.Forms.DockStyle.None;
			this.ProtocoltoolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
									this.toolStripLabel1,
									this.Protocol1});
			this.ProtocoltoolStrip.Location = new System.Drawing.Point(0, 3);
			this.ProtocoltoolStrip.Name = "ProtocoltoolStrip";
			this.ProtocoltoolStrip.Size = new System.Drawing.Size(90, 50);
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
			// 
			// menuStrip1
			// 
			this.menuStrip1.Dock = System.Windows.Forms.DockStyle.None;
			this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
									this.fileToolStripMenuItem,
									this.protocolsToolStripMenuItem});
			this.menuStrip1.Location = new System.Drawing.Point(0, 0);
			this.menuStrip1.Name = "menuStrip1";
			this.menuStrip1.Size = new System.Drawing.Size(924, 24);
			this.menuStrip1.TabIndex = 1;
			this.menuStrip1.Text = "menuStrip1";
			// 
			// fileToolStripMenuItem
			// 
			this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
			this.fileToolStripMenuItem.Size = new System.Drawing.Size(35, 20);
			this.fileToolStripMenuItem.Text = "File";
			// 
			// protocolsToolStripMenuItem
			// 
			this.protocolsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
									this.saveProtocolsAsToolStripMenuItem,
									this.openToolStripMenuItem});
			this.protocolsToolStripMenuItem.Name = "protocolsToolStripMenuItem";
			this.protocolsToolStripMenuItem.Size = new System.Drawing.Size(63, 20);
			this.protocolsToolStripMenuItem.Text = "Protocols";
			// 
			// saveProtocolsAsToolStripMenuItem
			// 
			this.saveProtocolsAsToolStripMenuItem.Name = "saveProtocolsAsToolStripMenuItem";
			this.saveProtocolsAsToolStripMenuItem.Size = new System.Drawing.Size(125, 22);
			this.saveProtocolsAsToolStripMenuItem.Text = "Save As...";
			this.saveProtocolsAsToolStripMenuItem.Click += new System.EventHandler(this.SaveProtocolsAsToolStripMenuItemClick);
			// 
			// openToolStripMenuItem
			// 
			this.openToolStripMenuItem.Name = "openToolStripMenuItem";
			this.openToolStripMenuItem.Size = new System.Drawing.Size(125, 22);
			this.openToolStripMenuItem.Text = "Open";
			this.openToolStripMenuItem.Click += new System.EventHandler(this.OpenToolStripMenuItemClick);
			// 
			// toolStrip1
			// 
			this.toolStrip1.Dock = System.Windows.Forms.DockStyle.None;
			this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
									this.toolStripButtonRefreshProtocols});
			this.toolStrip1.Location = new System.Drawing.Point(3, 24);
			this.toolStrip1.Name = "toolStrip1";
			this.toolStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
			this.toolStrip1.Size = new System.Drawing.Size(35, 25);
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
			this.ClientSize = new System.Drawing.Size(924, 613);
			this.Controls.Add(this.toolStripContainer1);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
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
			this.splitContainer1.Panel2.ResumeLayout(false);
			this.splitContainer1.Panel2.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
			this.splitContainer1.ResumeLayout(false);
			this.statusStrip1.ResumeLayout(false);
			this.statusStrip1.PerformLayout();
			this.ProtocoltoolStrip.ResumeLayout(false);
			this.ProtocoltoolStrip.PerformLayout();
			this.menuStrip1.ResumeLayout(false);
			this.menuStrip1.PerformLayout();
			this.toolStrip1.ResumeLayout(false);
			this.toolStrip1.PerformLayout();
			this.ResumeLayout(false);
		}
		private System.Windows.Forms.Button buttonClearLog;
		private System.Windows.Forms.TextBox textBoxLog;
		private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem saveProtocolsAsToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem protocolsToolStripMenuItem;
		private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabelProtocolCount;
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
