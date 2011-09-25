/*
 * Created by SharpDevelop.
 * User: oferfrid
 * Date: 21/09/2011
 * Time: 11:00
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
namespace OctoTip.OctoTipManager
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
			this.statusStrip1 = new System.Windows.Forms.StatusStrip();
			this.menuStrip1 = new System.Windows.Forms.MenuStrip();
			this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.eToolStripMenuItemExit = new System.Windows.Forms.ToolStripMenuItem();
			this.queueToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.saveAsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.loadToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.splitHorizontal = new System.Windows.Forms.SplitContainer();
			this.splitVertical = new System.Windows.Forms.SplitContainer();
			this.dataGridViewRobotJobsQueue = new System.Windows.Forms.DataGridView();
			this.checkBoxServerState = new System.Windows.Forms.CheckBox();
			this.ClearLogButton = new System.Windows.Forms.Button();
			this.txtLog = new System.Windows.Forms.TextBox();
			this.toolStrip1 = new System.Windows.Forms.ToolStrip();
			this.toolStripButtonRefreshQueue = new System.Windows.Forms.ToolStripButton();
			this.toolStripButtonRemoveJob = new System.Windows.Forms.ToolStripButton();
			this.menuStrip1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.splitHorizontal)).BeginInit();
			this.splitHorizontal.Panel1.SuspendLayout();
			this.splitHorizontal.Panel2.SuspendLayout();
			this.splitHorizontal.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.splitVertical)).BeginInit();
			this.splitVertical.Panel1.SuspendLayout();
			this.splitVertical.Panel2.SuspendLayout();
			this.splitVertical.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.dataGridViewRobotJobsQueue)).BeginInit();
			this.toolStrip1.SuspendLayout();
			this.SuspendLayout();
			// 
			// statusStrip1
			// 
			this.statusStrip1.Location = new System.Drawing.Point(0, 508);
			this.statusStrip1.Name = "statusStrip1";
			this.statusStrip1.Size = new System.Drawing.Size(713, 22);
			this.statusStrip1.TabIndex = 0;
			this.statusStrip1.Text = "statusStrip1";
			// 
			// menuStrip1
			// 
			this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
									this.fileToolStripMenuItem,
									this.queueToolStripMenuItem,
									this.helpToolStripMenuItem});
			this.menuStrip1.Location = new System.Drawing.Point(0, 0);
			this.menuStrip1.Name = "menuStrip1";
			this.menuStrip1.Size = new System.Drawing.Size(713, 24);
			this.menuStrip1.TabIndex = 1;
			this.menuStrip1.Text = "menuStrip1";
			// 
			// fileToolStripMenuItem
			// 
			this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
									this.eToolStripMenuItemExit});
			this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
			this.fileToolStripMenuItem.Size = new System.Drawing.Size(35, 20);
			this.fileToolStripMenuItem.Text = "File";
			// 
			// eToolStripMenuItemExit
			// 
			this.eToolStripMenuItemExit.Name = "eToolStripMenuItemExit";
			this.eToolStripMenuItemExit.Size = new System.Drawing.Size(92, 22);
			this.eToolStripMenuItemExit.Text = "Exit";
			this.eToolStripMenuItemExit.Click += new System.EventHandler(this.EToolStripMenuItemExitClick);
			// 
			// queueToolStripMenuItem
			// 
			this.queueToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
									this.saveAsToolStripMenuItem,
									this.loadToolStripMenuItem});
			this.queueToolStripMenuItem.Name = "queueToolStripMenuItem";
			this.queueToolStripMenuItem.Size = new System.Drawing.Size(51, 20);
			this.queueToolStripMenuItem.Text = "Queue";
			// 
			// saveAsToolStripMenuItem
			// 
			this.saveAsToolStripMenuItem.Name = "saveAsToolStripMenuItem";
			this.saveAsToolStripMenuItem.Size = new System.Drawing.Size(112, 22);
			this.saveAsToolStripMenuItem.Text = "Save as";
			this.saveAsToolStripMenuItem.Click += new System.EventHandler(this.SaveAsToolStripMenuItemClick);
			// 
			// loadToolStripMenuItem
			// 
			this.loadToolStripMenuItem.Name = "loadToolStripMenuItem";
			this.loadToolStripMenuItem.Size = new System.Drawing.Size(112, 22);
			this.loadToolStripMenuItem.Text = "Load";
			this.loadToolStripMenuItem.Click += new System.EventHandler(this.LoadToolStripMenuItemClick);
			// 
			// helpToolStripMenuItem
			// 
			this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
									this.aboutToolStripMenuItem});
			this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
			this.helpToolStripMenuItem.Size = new System.Drawing.Size(40, 20);
			this.helpToolStripMenuItem.Text = "Help";
			// 
			// aboutToolStripMenuItem
			// 
			this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
			this.aboutToolStripMenuItem.Size = new System.Drawing.Size(103, 22);
			this.aboutToolStripMenuItem.Text = "About";
			// 
			// splitHorizontal
			// 
			this.splitHorizontal.Dock = System.Windows.Forms.DockStyle.Fill;
			this.splitHorizontal.Location = new System.Drawing.Point(0, 24);
			this.splitHorizontal.Name = "splitHorizontal";
			this.splitHorizontal.Orientation = System.Windows.Forms.Orientation.Horizontal;
			// 
			// splitHorizontal.Panel1
			// 
			this.splitHorizontal.Panel1.Controls.Add(this.splitVertical);
			// 
			// splitHorizontal.Panel2
			// 
			this.splitHorizontal.Panel2.Controls.Add(this.ClearLogButton);
			this.splitHorizontal.Panel2.Controls.Add(this.txtLog);
			this.splitHorizontal.Size = new System.Drawing.Size(713, 484);
			this.splitHorizontal.SplitterDistance = 386;
			this.splitHorizontal.TabIndex = 2;
			// 
			// splitVertical
			// 
			this.splitVertical.Dock = System.Windows.Forms.DockStyle.Fill;
			this.splitVertical.Location = new System.Drawing.Point(0, 0);
			this.splitVertical.Name = "splitVertical";
			// 
			// splitVertical.Panel1
			// 
			this.splitVertical.Panel1.Controls.Add(this.dataGridViewRobotJobsQueue);
			// 
			// splitVertical.Panel2
			// 
			this.splitVertical.Panel2.Controls.Add(this.checkBoxServerState);
			this.splitVertical.Size = new System.Drawing.Size(713, 386);
			this.splitVertical.SplitterDistance = 477;
			this.splitVertical.TabIndex = 0;
			// 
			// dataGridViewRobotJobsQueue
			// 
			this.dataGridViewRobotJobsQueue.AllowUserToAddRows = false;
			this.dataGridViewRobotJobsQueue.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
			this.dataGridViewRobotJobsQueue.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.dataGridViewRobotJobsQueue.Location = new System.Drawing.Point(3, 28);
			this.dataGridViewRobotJobsQueue.Name = "dataGridViewRobotJobsQueue";
			this.dataGridViewRobotJobsQueue.ReadOnly = true;
			this.dataGridViewRobotJobsQueue.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
			this.dataGridViewRobotJobsQueue.Size = new System.Drawing.Size(471, 355);
			this.dataGridViewRobotJobsQueue.TabIndex = 0;
			// 
			// checkBoxServerState
			// 
			this.checkBoxServerState.Appearance = System.Windows.Forms.Appearance.Button;
			this.checkBoxServerState.Location = new System.Drawing.Point(4, 359);
			this.checkBoxServerState.Name = "checkBoxServerState";
			this.checkBoxServerState.Size = new System.Drawing.Size(76, 24);
			this.checkBoxServerState.TabIndex = 0;
			this.checkBoxServerState.Text = "Start Server";
			this.checkBoxServerState.UseVisualStyleBackColor = true;
			this.checkBoxServerState.CheckedChanged += new System.EventHandler(this.CheckBoxServerStateCheckedChanged);
			// 
			// ClearLogButton
			// 
			this.ClearLogButton.Location = new System.Drawing.Point(613, 4);
			this.ClearLogButton.Name = "ClearLogButton";
			this.ClearLogButton.Size = new System.Drawing.Size(97, 23);
			this.ClearLogButton.TabIndex = 1;
			this.ClearLogButton.Text = "Clear Log";
			this.ClearLogButton.UseVisualStyleBackColor = true;
			this.ClearLogButton.Click += new System.EventHandler(this.ClearLogButtonClick);
			// 
			// txtLog
			// 
			this.txtLog.Location = new System.Drawing.Point(3, 3);
			this.txtLog.Multiline = true;
			this.txtLog.Name = "txtLog";
			this.txtLog.Size = new System.Drawing.Size(604, 88);
			this.txtLog.TabIndex = 0;
			// 
			// toolStrip1
			// 
			this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
									this.toolStripButtonRefreshQueue,
									this.toolStripButtonRemoveJob});
			this.toolStrip1.Location = new System.Drawing.Point(0, 24);
			this.toolStrip1.Name = "toolStrip1";
			this.toolStrip1.Size = new System.Drawing.Size(713, 25);
			this.toolStrip1.TabIndex = 3;
			this.toolStrip1.Text = "toolStrip1";
			// 
			// toolStripButtonRefreshQueue
			// 
			this.toolStripButtonRefreshQueue.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.toolStripButtonRefreshQueue.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonRefreshQueue.Image")));
			this.toolStripButtonRefreshQueue.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.toolStripButtonRefreshQueue.Name = "toolStripButtonRefreshQueue";
			this.toolStripButtonRefreshQueue.Size = new System.Drawing.Size(23, 22);
			this.toolStripButtonRefreshQueue.Text = "Refresh Queue";
			this.toolStripButtonRefreshQueue.Click += new System.EventHandler(this.ToolStripButtonRefreshQueueClick);
			// 
			// toolStripButtonRemoveJob
			// 
			this.toolStripButtonRemoveJob.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.toolStripButtonRemoveJob.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonRemoveJob.Image")));
			this.toolStripButtonRemoveJob.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.toolStripButtonRemoveJob.Name = "toolStripButtonRemoveJob";
			this.toolStripButtonRemoveJob.Size = new System.Drawing.Size(23, 22);
			this.toolStripButtonRemoveJob.Text = "Remove Job";
			this.toolStripButtonRemoveJob.Click += new System.EventHandler(this.ToolStripButtonRemoveJobClick);
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(713, 530);
			this.Controls.Add(this.toolStrip1);
			this.Controls.Add(this.splitHorizontal);
			this.Controls.Add(this.statusStrip1);
			this.Controls.Add(this.menuStrip1);
			this.Name = "MainForm";
			this.Text = "OctoTip Manager";
			this.Load += new System.EventHandler(this.MainFormLoad);
			this.menuStrip1.ResumeLayout(false);
			this.menuStrip1.PerformLayout();
			this.splitHorizontal.Panel1.ResumeLayout(false);
			this.splitHorizontal.Panel2.ResumeLayout(false);
			this.splitHorizontal.Panel2.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.splitHorizontal)).EndInit();
			this.splitHorizontal.ResumeLayout(false);
			this.splitVertical.Panel1.ResumeLayout(false);
			this.splitVertical.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.splitVertical)).EndInit();
			this.splitVertical.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.dataGridViewRobotJobsQueue)).EndInit();
			this.toolStrip1.ResumeLayout(false);
			this.toolStrip1.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();
		}
		private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem loadToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem saveAsToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem queueToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem eToolStripMenuItemExit;
		private System.Windows.Forms.ToolStripButton toolStripButtonRemoveJob;
		private System.Windows.Forms.ToolStripButton toolStripButtonRefreshQueue;
		private System.Windows.Forms.ToolStrip toolStrip1;
		private System.Windows.Forms.TextBox txtLog;
		private System.Windows.Forms.Button ClearLogButton;
		private System.Windows.Forms.CheckBox checkBoxServerState;
		private System.Windows.Forms.DataGridView dataGridViewRobotJobsQueue;
		private System.Windows.Forms.SplitContainer splitVertical;
		private System.Windows.Forms.SplitContainer splitHorizontal;
		private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
		private System.Windows.Forms.MenuStrip menuStrip1;
		private System.Windows.Forms.StatusStrip statusStrip1;
		
		
	}
}
