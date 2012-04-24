/*
 * Created by SharpDevelop.
 * User: oferfrid
 * Date: 21/09/2011
 * Time: 11:00
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
namespace OctoTip.Manager
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
			this.errorToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.splitHorizontal = new System.Windows.Forms.SplitContainer();
			this.splitVertical = new System.Windows.Forms.SplitContainer();
			this.dataGridViewRobotJobsQueue = new OctoTip.Manager.ControlWrapper();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.RunningJobStatus = new System.Windows.Forms.Label();
			this.RunningJobName = new System.Windows.Forms.Label();
			this.textBoxRuningJobStatus = new System.Windows.Forms.TextBox();
			this.buttonStart = new System.Windows.Forms.Button();
			this.buttonPause = new System.Windows.Forms.Button();
			this.buttonStop = new System.Windows.Forms.Button();
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
			this.groupBox1.SuspendLayout();
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
			this.saveAsToolStripMenuItem.Size = new System.Drawing.Size(125, 22);
			this.saveAsToolStripMenuItem.Text = "Save As...";
			this.saveAsToolStripMenuItem.Click += new System.EventHandler(this.SaveAsToolStripMenuItemClick);
			// 
			// loadToolStripMenuItem
			// 
			this.loadToolStripMenuItem.Name = "loadToolStripMenuItem";
			this.loadToolStripMenuItem.Size = new System.Drawing.Size(125, 22);
			this.loadToolStripMenuItem.Text = "Open...";
			this.loadToolStripMenuItem.Click += new System.EventHandler(this.LoadToolStripMenuItemClick);
			// 
			// helpToolStripMenuItem
			// 
			this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
									this.aboutToolStripMenuItem,
									this.errorToolStripMenuItem});
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
			// errorToolStripMenuItem
			// 
			this.errorToolStripMenuItem.Name = "errorToolStripMenuItem";
			this.errorToolStripMenuItem.Size = new System.Drawing.Size(103, 22);
			this.errorToolStripMenuItem.Text = "Error";
			this.errorToolStripMenuItem.Click += new System.EventHandler(this.ErrorToolStripMenuItemClick);
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
			this.splitHorizontal.SplitterDistance = 304;
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
			this.splitVertical.Panel2.Controls.Add(this.groupBox1);
			this.splitVertical.Panel2.Controls.Add(this.textBoxRuningJobStatus);
			this.splitVertical.Panel2.Controls.Add(this.buttonStart);
			this.splitVertical.Panel2.Controls.Add(this.buttonPause);
			this.splitVertical.Panel2.Controls.Add(this.buttonStop);
			this.splitVertical.Panel2.Controls.Add(this.checkBoxServerState);
			this.splitVertical.Size = new System.Drawing.Size(713, 304);
			this.splitVertical.SplitterDistance = 529;
			this.splitVertical.TabIndex = 0;
			// 
			// dataGridViewRobotJobsQueue
			// 
			this.dataGridViewRobotJobsQueue.AllowUserToAddRows = false;
			this.dataGridViewRobotJobsQueue.AllowUserToDeleteRows = false;
			this.dataGridViewRobotJobsQueue.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
									| System.Windows.Forms.AnchorStyles.Left) 
									| System.Windows.Forms.AnchorStyles.Right)));
			this.dataGridViewRobotJobsQueue.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.dataGridViewRobotJobsQueue.Location = new System.Drawing.Point(12, 28);
			this.dataGridViewRobotJobsQueue.Name = "dataGridViewRobotJobsQueue";
			this.dataGridViewRobotJobsQueue.ReadOnly = true;
			this.dataGridViewRobotJobsQueue.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
			this.dataGridViewRobotJobsQueue.Size = new System.Drawing.Size(514, 268);
			this.dataGridViewRobotJobsQueue.TabIndex = 0;
			this.dataGridViewRobotJobsQueue.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(this.DataGridViewRobotJobsQueueDataError);
			// 
			// groupBox1
			// 
			this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
									| System.Windows.Forms.AnchorStyles.Right)));
			this.groupBox1.Controls.Add(this.RunningJobStatus);
			this.groupBox1.Controls.Add(this.RunningJobName);
			this.groupBox1.Location = new System.Drawing.Point(6, 105);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(168, 74);
			this.groupBox1.TabIndex = 6;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Runing Job";
			// 
			// RunningJobStatus
			// 
			this.RunningJobStatus.Location = new System.Drawing.Point(6, 39);
			this.RunningJobStatus.Name = "RunningJobStatus";
			this.RunningJobStatus.Size = new System.Drawing.Size(100, 23);
			this.RunningJobStatus.TabIndex = 2;
			// 
			// RunningJobName
			// 
			this.RunningJobName.Location = new System.Drawing.Point(6, 16);
			this.RunningJobName.Name = "RunningJobName";
			this.RunningJobName.Size = new System.Drawing.Size(100, 23);
			this.RunningJobName.TabIndex = 2;
			// 
			// textBoxRuningJobStatus
			// 
			this.textBoxRuningJobStatus.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
									| System.Windows.Forms.AnchorStyles.Left) 
									| System.Windows.Forms.AnchorStyles.Right)));
			this.textBoxRuningJobStatus.Location = new System.Drawing.Point(3, 185);
			this.textBoxRuningJobStatus.Multiline = true;
			this.textBoxRuningJobStatus.Name = "textBoxRuningJobStatus";
			this.textBoxRuningJobStatus.Size = new System.Drawing.Size(171, 111);
			this.textBoxRuningJobStatus.TabIndex = 5;
			// 
			// buttonStart
			// 
			this.buttonStart.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonStart.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
			this.buttonStart.Image = ((System.Drawing.Image)(resources.GetObject("buttonStart.Image")));
			this.buttonStart.Location = new System.Drawing.Point(6, 68);
			this.buttonStart.Name = "buttonStart";
			this.buttonStart.Size = new System.Drawing.Size(53, 30);
			this.buttonStart.TabIndex = 4;
			this.buttonStart.Text = "Start";
			this.buttonStart.UseVisualStyleBackColor = true;
			this.buttonStart.Click += new System.EventHandler(this.ButtonStartClick);
			// 
			// buttonPause
			// 
			this.buttonPause.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonPause.Enabled = false;
			this.buttonPause.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
			this.buttonPause.Image = ((System.Drawing.Image)(resources.GetObject("buttonPause.Image")));
			this.buttonPause.Location = new System.Drawing.Point(65, 68);
			this.buttonPause.Name = "buttonPause";
			this.buttonPause.Size = new System.Drawing.Size(53, 30);
			this.buttonPause.TabIndex = 3;
			this.buttonPause.Text = "Pause";
			this.buttonPause.UseVisualStyleBackColor = true;
			this.buttonPause.Click += new System.EventHandler(this.ButtonPauseClick);
			// 
			// buttonStop
			// 
			this.buttonStop.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonStop.Enabled = false;
			this.buttonStop.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
			this.buttonStop.Image = ((System.Drawing.Image)(resources.GetObject("buttonStop.Image")));
			this.buttonStop.Location = new System.Drawing.Point(124, 68);
			this.buttonStop.Name = "buttonStop";
			this.buttonStop.Size = new System.Drawing.Size(53, 30);
			this.buttonStop.TabIndex = 2;
			this.buttonStop.Text = "Stop";
			this.buttonStop.UseVisualStyleBackColor = true;
			this.buttonStop.Click += new System.EventHandler(this.ButtonStopClick);
			// 
			// checkBoxServerState
			// 
			this.checkBoxServerState.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.checkBoxServerState.Appearance = System.Windows.Forms.Appearance.Button;
			this.checkBoxServerState.CheckAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this.checkBoxServerState.Location = new System.Drawing.Point(6, 28);
			this.checkBoxServerState.Name = "checkBoxServerState";
			this.checkBoxServerState.Size = new System.Drawing.Size(171, 34);
			this.checkBoxServerState.TabIndex = 0;
			this.checkBoxServerState.Text = "Start Listener";
			this.checkBoxServerState.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this.checkBoxServerState.UseVisualStyleBackColor = true;
			this.checkBoxServerState.CheckedChanged += new System.EventHandler(this.CheckBoxServerStateCheckedChanged);
			// 
			// ClearLogButton
			// 
			this.ClearLogButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
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
			this.txtLog.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
									| System.Windows.Forms.AnchorStyles.Left) 
									| System.Windows.Forms.AnchorStyles.Right)));
			this.txtLog.Location = new System.Drawing.Point(3, 4);
			this.txtLog.Multiline = true;
			this.txtLog.Name = "txtLog";
			this.txtLog.ScrollBars = System.Windows.Forms.ScrollBars.Both;
			this.txtLog.Size = new System.Drawing.Size(604, 169);
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
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "MainForm";
			this.Text = "OctoTip-Manager";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainFormFormClosing);
			this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MainFormFormClosed);
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
			this.splitVertical.Panel2.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.splitVertical)).EndInit();
			this.splitVertical.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.dataGridViewRobotJobsQueue)).EndInit();
			this.groupBox1.ResumeLayout(false);
			this.toolStrip1.ResumeLayout(false);
			this.toolStrip1.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();
		}
		private System.Windows.Forms.Label RunningJobName;
		private System.Windows.Forms.Label RunningJobStatus;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.ToolStripMenuItem errorToolStripMenuItem;
		private System.Windows.Forms.TextBox textBoxRuningJobStatus;
		private System.Windows.Forms.Button buttonPause;
		private System.Windows.Forms.Button buttonStart;
		private OctoTip.Manager.ControlWrapper dataGridViewRobotJobsQueue;
		private System.Windows.Forms.Button buttonStop;
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
		private System.Windows.Forms.SplitContainer splitVertical;
		private System.Windows.Forms.SplitContainer splitHorizontal;
		private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
		private System.Windows.Forms.MenuStrip menuStrip1;
		private System.Windows.Forms.StatusStrip statusStrip1;
		
		
	}
}
