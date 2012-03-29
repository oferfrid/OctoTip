/*
 * Created by SharpDevelop.
 * User: oferfrid
 * Date: 02/10/2011
 * Time: 10:26
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
namespace OctoTip.OctoTipExperimentControl
{
	partial class ProtocolUserControl
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ProtocolUserControl));
			this.buttonStop = new System.Windows.Forms.Button();
			this.EditParametersbutton = new System.Windows.Forms.Button();
			this.ProtocolStatesViewer = new Microsoft.Msagl.GraphViewerGdi.GViewer();
			this.panel1 = new System.Windows.Forms.Panel();
			this.buttonStart = new System.Windows.Forms.Button();
			this.buttonPause = new System.Windows.Forms.Button();
			this.textBoxProtocolData = new System.Windows.Forms.TextBox();
			this.textBoxStateData = new System.Windows.Forms.TextBox();
			this.labelProtocolName = new System.Windows.Forms.Label();
			this.Closebutton = new System.Windows.Forms.Button();
			this.labelProtocolType = new System.Windows.Forms.Label();
			this.Minimizebutton = new System.Windows.Forms.Button();
			this.ProtocolViewercontextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.runIsolatedToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.panel1.SuspendLayout();
			this.ProtocolViewercontextMenuStrip.SuspendLayout();
			this.SuspendLayout();
			// 
			// buttonStop
			// 
			this.buttonStop.Anchor = System.Windows.Forms.AnchorStyles.None;
			this.buttonStop.Enabled = false;
			this.buttonStop.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
			this.buttonStop.Image = ((System.Drawing.Image)(resources.GetObject("buttonStop.Image")));
			this.buttonStop.Location = new System.Drawing.Point(498, 248);
			this.buttonStop.Name = "buttonStop";
			this.buttonStop.Size = new System.Drawing.Size(53, 28);
			this.buttonStop.TabIndex = 2;
			this.buttonStop.Text = "Stop";
			this.buttonStop.UseVisualStyleBackColor = true;
			this.buttonStop.Click += new System.EventHandler(this.ButtonStopClick);
			// 
			// EditParametersbutton
			// 
			this.EditParametersbutton.Anchor = System.Windows.Forms.AnchorStyles.None;
			this.EditParametersbutton.BackColor = System.Drawing.Color.Red;
			this.EditParametersbutton.Location = new System.Drawing.Point(437, 248);
			this.EditParametersbutton.Name = "EditParametersbutton";
			this.EditParametersbutton.Size = new System.Drawing.Size(55, 28);
			this.EditParametersbutton.TabIndex = 4;
			this.EditParametersbutton.Text = "Edit Parameters";
			this.EditParametersbutton.UseVisualStyleBackColor = false;
			this.EditParametersbutton.Click += new System.EventHandler(this.EditParametersbuttonClick);
			// 
			// ProtocolStatesViewer
			// 
			this.ProtocolStatesViewer.AsyncLayout = false;
			this.ProtocolStatesViewer.AutoScroll = true;
			this.ProtocolStatesViewer.AutoSize = true;
			this.ProtocolStatesViewer.BackColor = System.Drawing.SystemColors.Control;
			this.ProtocolStatesViewer.BackwardEnabled = false;
			this.ProtocolStatesViewer.BuildHitTree = true;
			this.ProtocolStatesViewer.CurrentLayoutMethod = Microsoft.Msagl.GraphViewerGdi.LayoutMethod.SugiyamaScheme;
			this.ProtocolStatesViewer.Dock = System.Windows.Forms.DockStyle.Fill;
			this.ProtocolStatesViewer.ForwardEnabled = false;
			this.ProtocolStatesViewer.Graph = null;
			this.ProtocolStatesViewer.LayoutAlgorithmSettingsButtonVisible = true;
			this.ProtocolStatesViewer.LayoutEditingEnabled = true;
			this.ProtocolStatesViewer.Location = new System.Drawing.Point(0, 0);
			this.ProtocolStatesViewer.MouseHitDistance = 0.05D;
			this.ProtocolStatesViewer.Name = "ProtocolStatesViewer";
			this.ProtocolStatesViewer.NavigationVisible = true;
			this.ProtocolStatesViewer.NeedToCalculateLayout = true;
			this.ProtocolStatesViewer.PanButtonPressed = false;
			this.ProtocolStatesViewer.SaveAsImageEnabled = true;
			this.ProtocolStatesViewer.SaveAsMsaglEnabled = true;
			this.ProtocolStatesViewer.SaveButtonVisible = true;
			this.ProtocolStatesViewer.SaveGraphButtonVisible = true;
			this.ProtocolStatesViewer.SaveInVectorFormatEnabled = true;
			this.ProtocolStatesViewer.Size = new System.Drawing.Size(428, 280);
			this.ProtocolStatesViewer.TabIndex = 5;
			this.ProtocolStatesViewer.ToolBarIsVisible = true;
			this.ProtocolStatesViewer.ZoomF = 1D;
			this.ProtocolStatesViewer.ZoomFraction = 0.5D;
			this.ProtocolStatesViewer.ZoomWindowThreshold = 0.05D;
			this.ProtocolStatesViewer.SelectionChanged += new System.EventHandler(this.ProtocolStatesViewerSelectionChanged);
			// 
			// panel1
			// 
			this.panel1.Controls.Add(this.ProtocolStatesViewer);
			this.panel1.Location = new System.Drawing.Point(3, 30);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(428, 280);
			this.panel1.TabIndex = 7;
			// 
			// buttonStart
			// 
			this.buttonStart.Anchor = System.Windows.Forms.AnchorStyles.None;
			this.buttonStart.Enabled = false;
			this.buttonStart.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
			this.buttonStart.Image = ((System.Drawing.Image)(resources.GetObject("buttonStart.Image")));
			this.buttonStart.Location = new System.Drawing.Point(437, 282);
			this.buttonStart.Name = "buttonStart";
			this.buttonStart.Size = new System.Drawing.Size(55, 28);
			this.buttonStart.TabIndex = 8;
			this.buttonStart.Text = "Start";
			this.buttonStart.UseVisualStyleBackColor = true;
			this.buttonStart.Click += new System.EventHandler(this.ButtonStartClick);
			// 
			// buttonPause
			// 
			this.buttonPause.Anchor = System.Windows.Forms.AnchorStyles.None;
			this.buttonPause.Enabled = false;
			this.buttonPause.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
			this.buttonPause.Image = ((System.Drawing.Image)(resources.GetObject("buttonPause.Image")));
			this.buttonPause.Location = new System.Drawing.Point(498, 282);
			this.buttonPause.Name = "buttonPause";
			this.buttonPause.Size = new System.Drawing.Size(53, 28);
			this.buttonPause.TabIndex = 8;
			this.buttonPause.Text = "Pause";
			this.buttonPause.UseVisualStyleBackColor = true;
			this.buttonPause.Click += new System.EventHandler(this.ButtonPauseClick);
			// 
			// textBoxProtocolData
			// 
			this.textBoxProtocolData.Anchor = System.Windows.Forms.AnchorStyles.None;
			this.textBoxProtocolData.Location = new System.Drawing.Point(437, 30);
			this.textBoxProtocolData.Multiline = true;
			this.textBoxProtocolData.Name = "textBoxProtocolData";
			this.textBoxProtocolData.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.textBoxProtocolData.Size = new System.Drawing.Size(114, 149);
			this.textBoxProtocolData.TabIndex = 1;
			this.textBoxProtocolData.DoubleClick += new System.EventHandler(this.TextBoxProtocolDataDoubleClick);
			// 
			// textBoxStateData
			// 
			this.textBoxStateData.Anchor = System.Windows.Forms.AnchorStyles.None;
			this.textBoxStateData.Location = new System.Drawing.Point(437, 185);
			this.textBoxStateData.Multiline = true;
			this.textBoxStateData.Name = "textBoxStateData";
			this.textBoxStateData.Size = new System.Drawing.Size(114, 57);
			this.textBoxStateData.TabIndex = 1;
			// 
			// labelProtocolName
			// 
			this.labelProtocolName.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
			this.labelProtocolName.Location = new System.Drawing.Point(215, 3);
			this.labelProtocolName.Name = "labelProtocolName";
			this.labelProtocolName.Size = new System.Drawing.Size(287, 23);
			this.labelProtocolName.TabIndex = 9;
			// 
			// Closebutton
			// 
			this.Closebutton.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
			this.Closebutton.Location = new System.Drawing.Point(529, 3);
			this.Closebutton.Name = "Closebutton";
			this.Closebutton.Size = new System.Drawing.Size(21, 21);
			this.Closebutton.TabIndex = 10;
			this.Closebutton.Text = "X";
			this.Closebutton.UseVisualStyleBackColor = true;
			this.Closebutton.Click += new System.EventHandler(this.ClosebuttonClick);
			// 
			// labelProtocolType
			// 
			this.labelProtocolType.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
			this.labelProtocolType.Location = new System.Drawing.Point(3, 3);
			this.labelProtocolType.Name = "labelProtocolType";
			this.labelProtocolType.Size = new System.Drawing.Size(206, 23);
			this.labelProtocolType.TabIndex = 11;
			// 
			// Minimizebutton
			// 
			this.Minimizebutton.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
			this.Minimizebutton.Location = new System.Drawing.Point(502, 3);
			this.Minimizebutton.Name = "Minimizebutton";
			this.Minimizebutton.Size = new System.Drawing.Size(21, 21);
			this.Minimizebutton.TabIndex = 10;
			this.Minimizebutton.Text = "_";
			this.Minimizebutton.UseVisualStyleBackColor = true;
			this.Minimizebutton.Click += new System.EventHandler(this.MinimizebuttonClick);
			// 
			// ProtocolViewercontextMenuStrip
			// 
			this.ProtocolViewercontextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
									this.runIsolatedToolStripMenuItem});
			this.ProtocolViewercontextMenuStrip.Name = "ProtocolViewercontextMenuStrip";
			this.ProtocolViewercontextMenuStrip.Size = new System.Drawing.Size(136, 26);
			// 
			// runIsolatedToolStripMenuItem
			// 
			this.runIsolatedToolStripMenuItem.Name = "runIsolatedToolStripMenuItem";
			this.runIsolatedToolStripMenuItem.Size = new System.Drawing.Size(168, 22);
			this.runIsolatedToolStripMenuItem.Text = "Run Isolated";
			// 
			// ProtocolUserControl
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
			this.BackColor = System.Drawing.Color.Silver;
			this.Controls.Add(this.labelProtocolType);
			this.Controls.Add(this.Minimizebutton);
			this.Controls.Add(this.Closebutton);
			this.Controls.Add(this.labelProtocolName);
			this.Controls.Add(this.buttonPause);
			this.Controls.Add(this.buttonStart);
			this.Controls.Add(this.panel1);
			this.Controls.Add(this.EditParametersbutton);
			this.Controls.Add(this.buttonStop);
			this.Controls.Add(this.textBoxStateData);
			this.Controls.Add(this.textBoxProtocolData);
			this.Name = "ProtocolUserControl";
			this.Size = new System.Drawing.Size(554, 314);
			this.Load += new System.EventHandler(this.ProtocolUserControlLoad);
			this.panel1.ResumeLayout(false);
			this.panel1.PerformLayout();
			this.ProtocolViewercontextMenuStrip.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();
		}
		private System.Windows.Forms.ToolStripMenuItem runIsolatedToolStripMenuItem;
		private System.Windows.Forms.ContextMenuStrip ProtocolViewercontextMenuStrip;
		private System.Windows.Forms.Button Minimizebutton;
		private System.Windows.Forms.Label labelProtocolType;
		private System.Windows.Forms.Button Closebutton;
		private System.Windows.Forms.Label labelProtocolName;
		private System.Windows.Forms.TextBox textBoxProtocolData;
		private System.Windows.Forms.TextBox textBoxStateData;
		private System.Windows.Forms.Button buttonPause;
		private System.Windows.Forms.Button buttonStart;
		private System.Windows.Forms.Panel panel1;
		private Microsoft.Msagl.GraphViewerGdi.GViewer ProtocolStatesViewer;
		private System.Windows.Forms.Button EditParametersbutton;
		private System.Windows.Forms.Button buttonStop;
	}
}
