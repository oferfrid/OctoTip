/*
 * Created by SharpDevelop.
 * User: oferfrid
 * Date: 01/04/2014
 * Time: 11:18
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
namespace OctoTip.OctoTipPlus
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
			this.components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
			this.MainStatusStrip = new System.Windows.Forms.StatusStrip();
			this.ProtocolsCountToolStripStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
			this.RuningProtocolsToolStripStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
			this.MainSplitContainer = new System.Windows.Forms.SplitContainer();
			this.MainTabControl = new System.Windows.Forms.TabControl();
			this.ProtocolTab = new System.Windows.Forms.TabPage();
			this.ProtocolPanel = new System.Windows.Forms.Panel();
			this.ProtocolsToolStrip = new System.Windows.Forms.ToolStrip();
			this.toolStripAvailableProtocolsLabel = new System.Windows.Forms.ToolStripLabel();
			this.ToolStripButtonRefreshProtocol = new System.Windows.Forms.ToolStripButton();
			this.ProtocolsToolStripSeparator = new System.Windows.Forms.ToolStripSeparator();
			this.RobotQTab = new System.Windows.Forms.TabPage();
			this.dataGridViewRobotJobsQueue = new OctoTip.Manager.ControlWrapper();
			this.RobotQToolStrip = new System.Windows.Forms.ToolStrip();
			this.RefreshToolStripButton = new System.Windows.Forms.ToolStripButton();
			this.ActivateJobToolStripButton = new System.Windows.Forms.ToolStripButton();
			this.DeactivateJobToolStripButton = new System.Windows.Forms.ToolStripButton();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.ErrorExtendedRichTextBox = new OctoTip.OctoTipPlus.ExtendedRichTextBox();
			this.LastErrorLabel = new System.Windows.Forms.Label();
			this.CreateErrorButton = new System.Windows.Forms.Button();
			this.ActiveLoggersCheckedListBox = new System.Windows.Forms.CheckedListBox();
			this.LoggersContextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.label2 = new System.Windows.Forms.Label();
			this.RobotGroupBox = new System.Windows.Forms.GroupBox();
			this.RobotStatuslabel = new System.Windows.Forms.Label();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.RunningJobStatus = new System.Windows.Forms.Label();
			this.RunningJobName = new System.Windows.Forms.Label();
			this.buttonRobotStart = new System.Windows.Forms.Button();
			this.buttonRobotPause = new System.Windows.Forms.Button();
			this.buttonRobotStop = new System.Windows.Forms.Button();
			this.MainMenuStrip = new System.Windows.Forms.MenuStrip();
			this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
			this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripMenuItem();
			this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.MainStatusStrip.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.MainSplitContainer)).BeginInit();
			this.MainSplitContainer.Panel1.SuspendLayout();
			this.MainSplitContainer.Panel2.SuspendLayout();
			this.MainSplitContainer.SuspendLayout();
			this.MainTabControl.SuspendLayout();
			this.ProtocolTab.SuspendLayout();
			this.ProtocolsToolStrip.SuspendLayout();
			this.RobotQTab.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.dataGridViewRobotJobsQueue)).BeginInit();
			this.RobotQToolStrip.SuspendLayout();
			this.groupBox2.SuspendLayout();
			this.RobotGroupBox.SuspendLayout();
			this.groupBox1.SuspendLayout();
			this.MainMenuStrip.SuspendLayout();
			this.SuspendLayout();
			// 
			// MainStatusStrip
			// 
			this.MainStatusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
									this.ProtocolsCountToolStripStatusLabel,
									this.RuningProtocolsToolStripStatusLabel});
			this.MainStatusStrip.Location = new System.Drawing.Point(0, 540);
			this.MainStatusStrip.Name = "MainStatusStrip";
			this.MainStatusStrip.Size = new System.Drawing.Size(967, 22);
			this.MainStatusStrip.TabIndex = 1;
			this.MainStatusStrip.Text = "MainStatusStrip";
			// 
			// ProtocolsCountToolStripStatusLabel
			// 
			this.ProtocolsCountToolStripStatusLabel.Name = "ProtocolsCountToolStripStatusLabel";
			this.ProtocolsCountToolStripStatusLabel.Size = new System.Drawing.Size(105, 17);
			this.ProtocolsCountToolStripStatusLabel.Text = "Active Protocols: 0";
			// 
			// RuningProtocolsToolStripStatusLabel
			// 
			this.RuningProtocolsToolStripStatusLabel.Name = "RuningProtocolsToolStripStatusLabel";
			this.RuningProtocolsToolStripStatusLabel.Size = new System.Drawing.Size(110, 17);
			this.RuningProtocolsToolStripStatusLabel.Text = "Runing Protocols: 0";
			// 
			// MainSplitContainer
			// 
			this.MainSplitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
			this.MainSplitContainer.Location = new System.Drawing.Point(0, 24);
			this.MainSplitContainer.Name = "MainSplitContainer";
			// 
			// MainSplitContainer.Panel1
			// 
			this.MainSplitContainer.Panel1.Controls.Add(this.MainTabControl);
			// 
			// MainSplitContainer.Panel2
			// 
			this.MainSplitContainer.Panel2.Controls.Add(this.groupBox2);
			this.MainSplitContainer.Panel2.Controls.Add(this.RobotGroupBox);
			this.MainSplitContainer.Size = new System.Drawing.Size(967, 516);
			this.MainSplitContainer.SplitterDistance = 753;
			this.MainSplitContainer.TabIndex = 2;
			// 
			// MainTabControl
			// 
			this.MainTabControl.Controls.Add(this.ProtocolTab);
			this.MainTabControl.Controls.Add(this.RobotQTab);
			this.MainTabControl.Dock = System.Windows.Forms.DockStyle.Fill;
			this.MainTabControl.Location = new System.Drawing.Point(0, 0);
			this.MainTabControl.Name = "MainTabControl";
			this.MainTabControl.SelectedIndex = 0;
			this.MainTabControl.Size = new System.Drawing.Size(753, 516);
			this.MainTabControl.TabIndex = 0;
			// 
			// ProtocolTab
			// 
			this.ProtocolTab.Controls.Add(this.ProtocolPanel);
			this.ProtocolTab.Controls.Add(this.ProtocolsToolStrip);
			this.ProtocolTab.Location = new System.Drawing.Point(4, 22);
			this.ProtocolTab.Name = "ProtocolTab";
			this.ProtocolTab.Padding = new System.Windows.Forms.Padding(3);
			this.ProtocolTab.Size = new System.Drawing.Size(745, 490);
			this.ProtocolTab.TabIndex = 0;
			this.ProtocolTab.Text = "Protocols";
			this.ProtocolTab.UseVisualStyleBackColor = true;
			// 
			// ProtocolPanel
			// 
			this.ProtocolPanel.AutoScroll = true;
			this.ProtocolPanel.Dock = System.Windows.Forms.DockStyle.Fill;
			this.ProtocolPanel.Location = new System.Drawing.Point(67, 3);
			this.ProtocolPanel.Name = "ProtocolPanel";
			this.ProtocolPanel.Size = new System.Drawing.Size(675, 484);
			this.ProtocolPanel.TabIndex = 1;
			// 
			// ProtocolsToolStrip
			// 
			this.ProtocolsToolStrip.Dock = System.Windows.Forms.DockStyle.Left;
			this.ProtocolsToolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
									this.toolStripAvailableProtocolsLabel,
									this.ToolStripButtonRefreshProtocol,
									this.ProtocolsToolStripSeparator});
			this.ProtocolsToolStrip.Location = new System.Drawing.Point(3, 3);
			this.ProtocolsToolStrip.Name = "ProtocolsToolStrip";
			this.ProtocolsToolStrip.Size = new System.Drawing.Size(64, 484);
			this.ProtocolsToolStrip.TabIndex = 0;
			this.ProtocolsToolStrip.Text = "ProtocolsToolStrip";
			// 
			// toolStripAvailableProtocolsLabel
			// 
			this.toolStripAvailableProtocolsLabel.Name = "toolStripAvailableProtocolsLabel";
			this.toolStripAvailableProtocolsLabel.Size = new System.Drawing.Size(61, 15);
			this.toolStripAvailableProtocolsLabel.Text = " Protocols:";
			// 
			// ToolStripButtonRefreshProtocol
			// 
			this.ToolStripButtonRefreshProtocol.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.ToolStripButtonRefreshProtocol.Image = ((System.Drawing.Image)(resources.GetObject("ToolStripButtonRefreshProtocol.Image")));
			this.ToolStripButtonRefreshProtocol.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.ToolStripButtonRefreshProtocol.Name = "ToolStripButtonRefreshProtocol";
			this.ToolStripButtonRefreshProtocol.Size = new System.Drawing.Size(61, 20);
			this.ToolStripButtonRefreshProtocol.Text = "Refresh protocols list";
			this.ToolStripButtonRefreshProtocol.Click += new System.EventHandler(this.ToolStripButtonRefreshProtocolClick);
			// 
			// ProtocolsToolStripSeparator
			// 
			this.ProtocolsToolStripSeparator.Name = "ProtocolsToolStripSeparator";
			this.ProtocolsToolStripSeparator.Size = new System.Drawing.Size(61, 6);
			// 
			// RobotQTab
			// 
			this.RobotQTab.Controls.Add(this.dataGridViewRobotJobsQueue);
			this.RobotQTab.Controls.Add(this.RobotQToolStrip);
			this.RobotQTab.Location = new System.Drawing.Point(4, 22);
			this.RobotQTab.Name = "RobotQTab";
			this.RobotQTab.Padding = new System.Windows.Forms.Padding(3);
			this.RobotQTab.Size = new System.Drawing.Size(745, 490);
			this.RobotQTab.TabIndex = 1;
			this.RobotQTab.Text = "Robot-Q";
			this.RobotQTab.UseVisualStyleBackColor = true;
			// 
			// dataGridViewRobotJobsQueue
			// 
			this.dataGridViewRobotJobsQueue.AllowUserToAddRows = false;
			this.dataGridViewRobotJobsQueue.AllowUserToDeleteRows = false;
			this.dataGridViewRobotJobsQueue.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.dataGridViewRobotJobsQueue.Dock = System.Windows.Forms.DockStyle.Fill;
			this.dataGridViewRobotJobsQueue.Location = new System.Drawing.Point(3, 28);
			this.dataGridViewRobotJobsQueue.Name = "dataGridViewRobotJobsQueue";
			this.dataGridViewRobotJobsQueue.ReadOnly = true;
			this.dataGridViewRobotJobsQueue.Size = new System.Drawing.Size(739, 459);
			this.dataGridViewRobotJobsQueue.TabIndex = 1;
			// 
			// RobotQToolStrip
			// 
			this.RobotQToolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
									this.RefreshToolStripButton,
									this.ActivateJobToolStripButton,
									this.DeactivateJobToolStripButton});
			this.RobotQToolStrip.Location = new System.Drawing.Point(3, 3);
			this.RobotQToolStrip.Name = "RobotQToolStrip";
			this.RobotQToolStrip.Size = new System.Drawing.Size(739, 25);
			this.RobotQToolStrip.TabIndex = 0;
			this.RobotQToolStrip.Text = "RobotQToolStrip";
			// 
			// RefreshToolStripButton
			// 
			this.RefreshToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.RefreshToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("RefreshToolStripButton.Image")));
			this.RefreshToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.RefreshToolStripButton.Name = "RefreshToolStripButton";
			this.RefreshToolStripButton.Size = new System.Drawing.Size(23, 22);
			this.RefreshToolStripButton.Text = "Refresh";
			this.RefreshToolStripButton.Click += new System.EventHandler(this.RefreshToolStripButtonClick);
			// 
			// ActivateJobToolStripButton
			// 
			this.ActivateJobToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.ActivateJobToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("ActivateJobToolStripButton.Image")));
			this.ActivateJobToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.ActivateJobToolStripButton.Name = "ActivateJobToolStripButton";
			this.ActivateJobToolStripButton.Size = new System.Drawing.Size(23, 22);
			this.ActivateJobToolStripButton.Text = "Activate Job";
			this.ActivateJobToolStripButton.Click += new System.EventHandler(this.ActivateJobToolStripButtonClick);
			// 
			// DeactivateJobToolStripButton
			// 
			this.DeactivateJobToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.DeactivateJobToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("DeactivateJobToolStripButton.Image")));
			this.DeactivateJobToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.DeactivateJobToolStripButton.Name = "DeactivateJobToolStripButton";
			this.DeactivateJobToolStripButton.Size = new System.Drawing.Size(23, 22);
			this.DeactivateJobToolStripButton.Text = "Deactivate Job";
			this.DeactivateJobToolStripButton.Click += new System.EventHandler(this.DeactivateJobToolStripButtonClick);
			// 
			// groupBox2
			// 
			this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
									| System.Windows.Forms.AnchorStyles.Right)));
			this.groupBox2.Controls.Add(this.ErrorExtendedRichTextBox);
			this.groupBox2.Controls.Add(this.LastErrorLabel);
			this.groupBox2.Controls.Add(this.CreateErrorButton);
			this.groupBox2.Controls.Add(this.ActiveLoggersCheckedListBox);
			this.groupBox2.Controls.Add(this.label2);
			this.groupBox2.Location = new System.Drawing.Point(3, 201);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(203, 311);
			this.groupBox2.TabIndex = 1;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "Logging and Errors";
			// 
			// ErrorExtendedRichTextBox
			// 
			this.ErrorExtendedRichTextBox.Location = new System.Drawing.Point(7, 169);
			this.ErrorExtendedRichTextBox.Name = "ErrorExtendedRichTextBox";
			this.ErrorExtendedRichTextBox.ReadOnly = true;
			this.ErrorExtendedRichTextBox.Size = new System.Drawing.Size(190, 136);
			this.ErrorExtendedRichTextBox.TabIndex = 4;
			this.ErrorExtendedRichTextBox.Text = "";
			this.ErrorExtendedRichTextBox.DoubleClick += new System.EventHandler(this.ErrorExtendedRichTextBoxDoubleClick);
			// 
			// LastErrorLabel
			// 
			this.LastErrorLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
			this.LastErrorLabel.Location = new System.Drawing.Point(6, 141);
			this.LastErrorLabel.Name = "LastErrorLabel";
			this.LastErrorLabel.Size = new System.Drawing.Size(140, 24);
			this.LastErrorLabel.TabIndex = 3;
			this.LastErrorLabel.Text = "Last Error";
			// 
			// CreateErrorButton
			// 
			this.CreateErrorButton.Location = new System.Drawing.Point(152, 135);
			this.CreateErrorButton.Name = "CreateErrorButton";
			this.CreateErrorButton.Size = new System.Drawing.Size(45, 24);
			this.CreateErrorButton.TabIndex = 1;
			this.CreateErrorButton.Text = "Test";
			this.CreateErrorButton.UseVisualStyleBackColor = true;
			this.CreateErrorButton.Click += new System.EventHandler(this.CreateErrorButtonClick);
			// 
			// ActiveLoggersCheckedListBox
			// 
			this.ActiveLoggersCheckedListBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
									| System.Windows.Forms.AnchorStyles.Right)));
			this.ActiveLoggersCheckedListBox.ContextMenuStrip = this.LoggersContextMenuStrip;
			this.ActiveLoggersCheckedListBox.FormattingEnabled = true;
			this.ActiveLoggersCheckedListBox.Location = new System.Drawing.Point(6, 35);
			this.ActiveLoggersCheckedListBox.Name = "ActiveLoggersCheckedListBox";
			this.ActiveLoggersCheckedListBox.Size = new System.Drawing.Size(191, 94);
			this.ActiveLoggersCheckedListBox.TabIndex = 0;
			this.ActiveLoggersCheckedListBox.MouseDown += new System.Windows.Forms.MouseEventHandler(this.ActiveLoggersCheckedListBoxMouseDown);
			// 
			// LoggersContextMenuStrip
			// 
			this.LoggersContextMenuStrip.Name = "LoggersContextMenuStrip";
			this.LoggersContextMenuStrip.Size = new System.Drawing.Size(61, 4);
			this.LoggersContextMenuStrip.Opening += new System.ComponentModel.CancelEventHandler(this.LoggersContextMenuStripOpening);
			// 
			// label2
			// 
			this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
			this.label2.Location = new System.Drawing.Point(6, 20);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(181, 23);
			this.label2.TabIndex = 2;
			this.label2.Text = "Avalble Loggers:";
			// 
			// RobotGroupBox
			// 
			this.RobotGroupBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
									| System.Windows.Forms.AnchorStyles.Right)));
			this.RobotGroupBox.Controls.Add(this.RobotStatuslabel);
			this.RobotGroupBox.Controls.Add(this.groupBox1);
			this.RobotGroupBox.Controls.Add(this.buttonRobotStart);
			this.RobotGroupBox.Controls.Add(this.buttonRobotPause);
			this.RobotGroupBox.Controls.Add(this.buttonRobotStop);
			this.RobotGroupBox.Location = new System.Drawing.Point(3, 3);
			this.RobotGroupBox.Name = "RobotGroupBox";
			this.RobotGroupBox.Size = new System.Drawing.Size(203, 192);
			this.RobotGroupBox.TabIndex = 0;
			this.RobotGroupBox.TabStop = false;
			this.RobotGroupBox.Text = "Robot Control";
			// 
			// RobotStatuslabel
			// 
			this.RobotStatuslabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
			this.RobotStatuslabel.ForeColor = System.Drawing.Color.Red;
			this.RobotStatuslabel.Location = new System.Drawing.Point(12, 138);
			this.RobotStatuslabel.Name = "RobotStatuslabel";
			this.RobotStatuslabel.Size = new System.Drawing.Size(173, 45);
			this.RobotStatuslabel.TabIndex = 11;
			this.RobotStatuslabel.Text = "Stoped";
			// 
			// groupBox1
			// 
			this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
									| System.Windows.Forms.AnchorStyles.Right)));
			this.groupBox1.Controls.Add(this.RunningJobStatus);
			this.groupBox1.Controls.Add(this.RunningJobName);
			this.groupBox1.Location = new System.Drawing.Point(6, 55);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(191, 76);
			this.groupBox1.TabIndex = 10;
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
			// buttonRobotStart
			// 
			this.buttonRobotStart.Anchor = System.Windows.Forms.AnchorStyles.Top;
			this.buttonRobotStart.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
			this.buttonRobotStart.Image = ((System.Drawing.Image)(resources.GetObject("buttonRobotStart.Image")));
			this.buttonRobotStart.Location = new System.Drawing.Point(26, 19);
			this.buttonRobotStart.Name = "buttonRobotStart";
			this.buttonRobotStart.Size = new System.Drawing.Size(53, 30);
			this.buttonRobotStart.TabIndex = 9;
			this.buttonRobotStart.Text = "Start";
			this.buttonRobotStart.UseVisualStyleBackColor = true;
			this.buttonRobotStart.Click += new System.EventHandler(this.ButtonRobotStartClick);
			// 
			// buttonRobotPause
			// 
			this.buttonRobotPause.Anchor = System.Windows.Forms.AnchorStyles.Top;
			this.buttonRobotPause.Enabled = false;
			this.buttonRobotPause.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
			this.buttonRobotPause.Image = ((System.Drawing.Image)(resources.GetObject("buttonRobotPause.Image")));
			this.buttonRobotPause.Location = new System.Drawing.Point(79, 19);
			this.buttonRobotPause.Name = "buttonRobotPause";
			this.buttonRobotPause.Size = new System.Drawing.Size(53, 30);
			this.buttonRobotPause.TabIndex = 8;
			this.buttonRobotPause.Text = "Pause";
			this.buttonRobotPause.UseVisualStyleBackColor = true;
			// 
			// buttonRobotStop
			// 
			this.buttonRobotStop.Anchor = System.Windows.Forms.AnchorStyles.Top;
			this.buttonRobotStop.Enabled = false;
			this.buttonRobotStop.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
			this.buttonRobotStop.Image = ((System.Drawing.Image)(resources.GetObject("buttonRobotStop.Image")));
			this.buttonRobotStop.Location = new System.Drawing.Point(132, 19);
			this.buttonRobotStop.Name = "buttonRobotStop";
			this.buttonRobotStop.Size = new System.Drawing.Size(53, 30);
			this.buttonRobotStop.TabIndex = 7;
			this.buttonRobotStop.Text = "Stop";
			this.buttonRobotStop.UseVisualStyleBackColor = true;
			// 
			// MainMenuStrip
			// 
			this.MainMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
									this.toolStripMenuItem1,
									this.toolStripMenuItem2,
									this.toolStripMenuItem3});
			this.MainMenuStrip.Location = new System.Drawing.Point(0, 0);
			this.MainMenuStrip.Name = "MainMenuStrip";
			this.MainMenuStrip.Size = new System.Drawing.Size(967, 24);
			this.MainMenuStrip.TabIndex = 3;
			this.MainMenuStrip.Text = "MainMenuStrip";
			// 
			// toolStripMenuItem1
			// 
			this.toolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
									this.exitToolStripMenuItem});
			this.toolStripMenuItem1.Name = "toolStripMenuItem1";
			this.toolStripMenuItem1.Size = new System.Drawing.Size(37, 20);
			this.toolStripMenuItem1.Text = "File";
			// 
			// exitToolStripMenuItem
			// 
			this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
			this.exitToolStripMenuItem.Size = new System.Drawing.Size(92, 22);
			this.exitToolStripMenuItem.Text = "Exit";
			// 
			// toolStripMenuItem2
			// 
			this.toolStripMenuItem2.Name = "toolStripMenuItem2";
			this.toolStripMenuItem2.Size = new System.Drawing.Size(39, 20);
			this.toolStripMenuItem2.Text = "Edit";
			// 
			// toolStripMenuItem3
			// 
			this.toolStripMenuItem3.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
									this.aboutToolStripMenuItem});
			this.toolStripMenuItem3.Name = "toolStripMenuItem3";
			this.toolStripMenuItem3.Size = new System.Drawing.Size(44, 20);
			this.toolStripMenuItem3.Text = "Help";
			// 
			// aboutToolStripMenuItem
			// 
			this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
			this.aboutToolStripMenuItem.Size = new System.Drawing.Size(107, 22);
			this.aboutToolStripMenuItem.Text = "About";
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(967, 562);
			this.Controls.Add(this.MainSplitContainer);
			this.Controls.Add(this.MainStatusStrip);
			this.Controls.Add(this.MainMenuStrip);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "MainForm";
			this.Text = "OctoTip+";
			this.Load += new System.EventHandler(this.MainFormLoad);
			this.MainStatusStrip.ResumeLayout(false);
			this.MainStatusStrip.PerformLayout();
			this.MainSplitContainer.Panel1.ResumeLayout(false);
			this.MainSplitContainer.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.MainSplitContainer)).EndInit();
			this.MainSplitContainer.ResumeLayout(false);
			this.MainTabControl.ResumeLayout(false);
			this.ProtocolTab.ResumeLayout(false);
			this.ProtocolTab.PerformLayout();
			this.ProtocolsToolStrip.ResumeLayout(false);
			this.ProtocolsToolStrip.PerformLayout();
			this.RobotQTab.ResumeLayout(false);
			this.RobotQTab.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.dataGridViewRobotJobsQueue)).EndInit();
			this.RobotQToolStrip.ResumeLayout(false);
			this.RobotQToolStrip.PerformLayout();
			this.groupBox2.ResumeLayout(false);
			this.RobotGroupBox.ResumeLayout(false);
			this.groupBox1.ResumeLayout(false);
			this.MainMenuStrip.ResumeLayout(false);
			this.MainMenuStrip.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();
		}
		private OctoTip.OctoTipPlus.ExtendedRichTextBox ErrorExtendedRichTextBox;
		private System.Windows.Forms.Label LastErrorLabel;
		private System.Windows.Forms.ContextMenuStrip LoggersContextMenuStrip;
		private System.Windows.Forms.ToolStripStatusLabel RuningProtocolsToolStripStatusLabel;
		private System.Windows.Forms.ToolStripStatusLabel ProtocolsCountToolStripStatusLabel;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Button CreateErrorButton;
		private System.Windows.Forms.CheckedListBox ActiveLoggersCheckedListBox;
		private System.Windows.Forms.GroupBox groupBox2;
		private OctoTip.Manager.ControlWrapper dataGridViewRobotJobsQueue;
		private System.Windows.Forms.Button buttonRobotStop;
		private System.Windows.Forms.Button buttonRobotPause;
		private System.Windows.Forms.Button buttonRobotStart;
		private System.Windows.Forms.Label RunningJobName;
		private System.Windows.Forms.Label RunningJobStatus;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.Label RobotStatuslabel;
		private System.Windows.Forms.GroupBox RobotGroupBox;
		private System.Windows.Forms.ToolStripSeparator ProtocolsToolStripSeparator;
		private System.Windows.Forms.Panel ProtocolPanel;
		private System.Windows.Forms.ToolStripButton ToolStripButtonRefreshProtocol;
		private System.Windows.Forms.ToolStripButton DeactivateJobToolStripButton;
		private System.Windows.Forms.ToolStripButton ActivateJobToolStripButton;
		private System.Windows.Forms.ToolStripButton RefreshToolStripButton;
		private System.Windows.Forms.ToolStrip RobotQToolStrip;
		private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem3;
		private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem2;
		private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
		private System.Windows.Forms.MenuStrip MainMenuStrip;
		private System.Windows.Forms.ToolStripLabel toolStripAvailableProtocolsLabel;
		private System.Windows.Forms.ToolStrip ProtocolsToolStrip;
		private System.Windows.Forms.TabPage RobotQTab;
		private System.Windows.Forms.TabPage ProtocolTab;
		private System.Windows.Forms.TabControl MainTabControl;
		private System.Windows.Forms.SplitContainer MainSplitContainer;
		private System.Windows.Forms.StatusStrip MainStatusStrip;
		
	}
}
