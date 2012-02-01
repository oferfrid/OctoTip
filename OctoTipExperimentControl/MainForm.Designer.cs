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
			this.NotificationgroupBox = new System.Windows.Forms.GroupBox();
			this.TestNotificationbutton = new System.Windows.Forms.Button();
			this.NotificationcheckBox = new System.Windows.Forms.CheckBox();
			this.NotificationPhonetextBox = new System.Windows.Forms.TextBox();
			this.NotificationEmailtextBox = new System.Windows.Forms.TextBox();
			this.Phonelabel = new System.Windows.Forms.Label();
			this.Emaillabel = new System.Windows.Forms.Label();
			this.buttonClearLog = new System.Windows.Forms.Button();
			this.textBoxLog = new System.Windows.Forms.TextBox();
			this.statusStrip1 = new System.Windows.Forms.StatusStrip();
			this.toolStripStatusLabelAllProtocolCount = new System.Windows.Forms.ToolStripStatusLabel();
			this.toolStripStatusLabelProtocolCount = new System.Windows.Forms.ToolStripStatusLabel();
			this.ProtocoltoolStrip = new System.Windows.Forms.ToolStrip();
			this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
			this.Protocol1 = new System.Windows.Forms.ToolStripButton();
			this.menuStrip1 = new System.Windows.Forms.MenuStrip();
			this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.protocolsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.saveProtocolsAsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStrip1 = new System.Windows.Forms.ToolStrip();
			this.toolStripButtonRefreshProtocols = new System.Windows.Forms.ToolStripButton();
			this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.errorToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripContainer1.ContentPanel.SuspendLayout();
			this.toolStripContainer1.LeftToolStripPanel.SuspendLayout();
			this.toolStripContainer1.TopToolStripPanel.SuspendLayout();
			this.toolStripContainer1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
			this.splitContainer1.Panel1.SuspendLayout();
			this.splitContainer1.Panel2.SuspendLayout();
			this.splitContainer1.SuspendLayout();
			this.NotificationgroupBox.SuspendLayout();
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
			this.splitContainer1.Panel2.Controls.Add(this.NotificationgroupBox);
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
			// NotificationgroupBox
			// 
			this.NotificationgroupBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
									| System.Windows.Forms.AnchorStyles.Right)));
			this.NotificationgroupBox.Controls.Add(this.TestNotificationbutton);
			this.NotificationgroupBox.Controls.Add(this.NotificationcheckBox);
			this.NotificationgroupBox.Controls.Add(this.NotificationPhonetextBox);
			this.NotificationgroupBox.Controls.Add(this.NotificationEmailtextBox);
			this.NotificationgroupBox.Controls.Add(this.Phonelabel);
			this.NotificationgroupBox.Controls.Add(this.Emaillabel);
			this.NotificationgroupBox.Location = new System.Drawing.Point(4, 3);
			this.NotificationgroupBox.Name = "NotificationgroupBox";
			this.NotificationgroupBox.Size = new System.Drawing.Size(227, 103);
			this.NotificationgroupBox.TabIndex = 3;
			this.NotificationgroupBox.TabStop = false;
			this.NotificationgroupBox.Text = "Notifications";
			// 
			// TestNotificationbutton
			// 
			this.TestNotificationbutton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.TestNotificationbutton.Location = new System.Drawing.Point(154, 72);
			this.TestNotificationbutton.Name = "TestNotificationbutton";
			this.TestNotificationbutton.Size = new System.Drawing.Size(63, 23);
			this.TestNotificationbutton.TabIndex = 3;
			this.TestNotificationbutton.Text = "Test";
			this.TestNotificationbutton.UseVisualStyleBackColor = true;
			this.TestNotificationbutton.Click += new System.EventHandler(this.TestNotificationbuttonClick);
			// 
			// NotificationcheckBox
			// 
			this.NotificationcheckBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.NotificationcheckBox.Location = new System.Drawing.Point(7, 71);
			this.NotificationcheckBox.Name = "NotificationcheckBox";
			this.NotificationcheckBox.Size = new System.Drawing.Size(141, 24);
			this.NotificationcheckBox.TabIndex = 2;
			this.NotificationcheckBox.Text = "Notification enabled";
			this.NotificationcheckBox.UseVisualStyleBackColor = true;
			// 
			// NotificationPhonetextBox
			// 
			this.NotificationPhonetextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
									| System.Windows.Forms.AnchorStyles.Right)));
			this.NotificationPhonetextBox.Location = new System.Drawing.Point(56, 45);
			this.NotificationPhonetextBox.Name = "NotificationPhonetextBox";
			this.NotificationPhonetextBox.Size = new System.Drawing.Size(162, 20);
			this.NotificationPhonetextBox.TabIndex = 1;
			// 
			// NotificationEmailtextBox
			// 
			this.NotificationEmailtextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
									| System.Windows.Forms.AnchorStyles.Right)));
			this.NotificationEmailtextBox.Location = new System.Drawing.Point(56, 19);
			this.NotificationEmailtextBox.Name = "NotificationEmailtextBox";
			this.NotificationEmailtextBox.Size = new System.Drawing.Size(162, 20);
			this.NotificationEmailtextBox.TabIndex = 1;
			// 
			// Phonelabel
			// 
			this.Phonelabel.Location = new System.Drawing.Point(7, 48);
			this.Phonelabel.Name = "Phonelabel";
			this.Phonelabel.Size = new System.Drawing.Size(43, 20);
			this.Phonelabel.TabIndex = 0;
			this.Phonelabel.Text = "Phone";
			// 
			// Emaillabel
			// 
			this.Emaillabel.Location = new System.Drawing.Point(7, 20);
			this.Emaillabel.Name = "Emaillabel";
			this.Emaillabel.Size = new System.Drawing.Size(43, 20);
			this.Emaillabel.TabIndex = 0;
			this.Emaillabel.Text = "Email";
			// 
			// buttonClearLog
			// 
			this.buttonClearLog.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.buttonClearLog.Location = new System.Drawing.Point(3, 273);
			this.buttonClearLog.Name = "buttonClearLog";
			this.buttonClearLog.Size = new System.Drawing.Size(75, 23);
			this.buttonClearLog.TabIndex = 2;
			this.buttonClearLog.Text = "Clear Log";
			this.buttonClearLog.UseVisualStyleBackColor = true;
			this.buttonClearLog.Click += new System.EventHandler(this.ButtonClearLogClick);
			// 
			// textBoxLog
			// 
			this.textBoxLog.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
									| System.Windows.Forms.AnchorStyles.Right)));
			this.textBoxLog.Location = new System.Drawing.Point(3, 302);
			this.textBoxLog.Multiline = true;
			this.textBoxLog.Name = "textBoxLog";
			this.textBoxLog.Size = new System.Drawing.Size(228, 237);
			this.textBoxLog.TabIndex = 1;
			// 
			// statusStrip1
			// 
			this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
									this.toolStripStatusLabelAllProtocolCount,
									this.toolStripStatusLabelProtocolCount});
			this.statusStrip1.Location = new System.Drawing.Point(0, 542);
			this.statusStrip1.Name = "statusStrip1";
			this.statusStrip1.Size = new System.Drawing.Size(834, 22);
			this.statusStrip1.TabIndex = 0;
			this.statusStrip1.Text = "statusStrip1";
			// 
			// toolStripStatusLabelAllProtocolCount
			// 
			this.toolStripStatusLabelAllProtocolCount.Name = "toolStripStatusLabelAllProtocolCount";
			this.toolStripStatusLabelAllProtocolCount.Size = new System.Drawing.Size(75, 17);
			this.toolStripStatusLabelAllProtocolCount.Text = "All Protocols:0";
			// 
			// toolStripStatusLabelProtocolCount
			// 
			this.toolStripStatusLabelProtocolCount.Name = "toolStripStatusLabelProtocolCount";
			this.toolStripStatusLabelProtocolCount.Size = new System.Drawing.Size(94, 17);
			this.toolStripStatusLabelProtocolCount.Text = "Active Protocols:0";
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
									this.protocolsToolStripMenuItem,
									this.helpToolStripMenuItem});
			this.menuStrip1.Location = new System.Drawing.Point(0, 0);
			this.menuStrip1.Name = "menuStrip1";
			this.menuStrip1.Size = new System.Drawing.Size(924, 24);
			this.menuStrip1.TabIndex = 1;
			this.menuStrip1.Text = "menuStrip1";
			// 
			// fileToolStripMenuItem
			// 
			this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
									this.exitToolStripMenuItem});
			this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
			this.fileToolStripMenuItem.Size = new System.Drawing.Size(35, 20);
			this.fileToolStripMenuItem.Text = "File";
			// 
			// exitToolStripMenuItem
			// 
			this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
			this.exitToolStripMenuItem.Size = new System.Drawing.Size(92, 22);
			this.exitToolStripMenuItem.Text = "Exit";
			this.exitToolStripMenuItem.Click += new System.EventHandler(this.ExitToolStripMenuItemClick);
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
			this.saveProtocolsAsToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
			this.saveProtocolsAsToolStripMenuItem.Text = "Save As...";
			this.saveProtocolsAsToolStripMenuItem.Click += new System.EventHandler(this.SaveProtocolsAsToolStripMenuItemClick);
			// 
			// openToolStripMenuItem
			// 
			this.openToolStripMenuItem.Name = "openToolStripMenuItem";
			this.openToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
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
			// helpToolStripMenuItem
			// 
			this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
									this.errorToolStripMenuItem});
			this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
			this.helpToolStripMenuItem.Size = new System.Drawing.Size(40, 20);
			this.helpToolStripMenuItem.Text = "Help";
			// 
			// errorToolStripMenuItem
			// 
			this.errorToolStripMenuItem.Name = "errorToolStripMenuItem";
			this.errorToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
			this.errorToolStripMenuItem.Text = "Error";
			this.errorToolStripMenuItem.Click += new System.EventHandler(this.ErrorToolStripMenuItemClick);
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
			this.Text = "OctoTip-Experiment Manager";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainFormFormClosing);
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
			this.NotificationgroupBox.ResumeLayout(false);
			this.NotificationgroupBox.PerformLayout();
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
		private System.Windows.Forms.ToolStripMenuItem errorToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
		private System.Windows.Forms.Button TestNotificationbutton;
		private System.Windows.Forms.CheckBox NotificationcheckBox;
		private System.Windows.Forms.Label Emaillabel;
		private System.Windows.Forms.Label Phonelabel;
		private System.Windows.Forms.TextBox NotificationEmailtextBox;
		private System.Windows.Forms.TextBox NotificationPhonetextBox;
		private System.Windows.Forms.GroupBox NotificationgroupBox;
		private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
		private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabelAllProtocolCount;
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
