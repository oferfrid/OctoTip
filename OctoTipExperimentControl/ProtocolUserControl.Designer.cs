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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ProtocolUserControl));
			this.textBoxData = new System.Windows.Forms.TextBox();
			this.buttonStop = new System.Windows.Forms.Button();
			this.EditParametersbutton = new System.Windows.Forms.Button();
			this.ProtocolStatesViewer = new Microsoft.Msagl.GraphViewerGdi.GViewer();
			this.textBoxStatus = new System.Windows.Forms.TextBox();
			this.panel1 = new System.Windows.Forms.Panel();
			this.buttonStart = new System.Windows.Forms.Button();
			this.buttonPause = new System.Windows.Forms.Button();
			this.panel1.SuspendLayout();
			this.SuspendLayout();
			// 
			// textBoxData
			// 
			this.textBoxData.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
									| System.Windows.Forms.AnchorStyles.Left) 
									| System.Windows.Forms.AnchorStyles.Right)));
			this.textBoxData.Location = new System.Drawing.Point(402, 109);
			this.textBoxData.Multiline = true;
			this.textBoxData.Name = "textBoxData";
			this.textBoxData.Size = new System.Drawing.Size(124, 144);
			this.textBoxData.TabIndex = 1;
			// 
			// buttonStop
			// 
			this.buttonStop.Anchor = System.Windows.Forms.AnchorStyles.Right;
			this.buttonStop.Enabled = false;
			this.buttonStop.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
			this.buttonStop.Image = ((System.Drawing.Image)(resources.GetObject("buttonStop.Image")));
			this.buttonStop.Location = new System.Drawing.Point(532, 141);
			this.buttonStop.Name = "buttonStop";
			this.buttonStop.Size = new System.Drawing.Size(55, 28);
			this.buttonStop.TabIndex = 2;
			this.buttonStop.Text = "Stop";
			this.buttonStop.UseVisualStyleBackColor = true;
			this.buttonStop.Click += new System.EventHandler(this.ButtonStopClick);
			// 
			// EditParametersbutton
			// 
			this.EditParametersbutton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.EditParametersbutton.BackColor = System.Drawing.Color.Red;
			this.EditParametersbutton.Location = new System.Drawing.Point(533, 3);
			this.EditParametersbutton.Name = "EditParametersbutton";
			this.EditParametersbutton.Size = new System.Drawing.Size(55, 20);
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
			this.ProtocolStatesViewer.Size = new System.Drawing.Size(393, 247);
			this.ProtocolStatesViewer.TabIndex = 5;
			this.ProtocolStatesViewer.ToolBarIsVisible = true;
			this.ProtocolStatesViewer.ZoomF = 1D;
			this.ProtocolStatesViewer.ZoomFraction = 0.5D;
			this.ProtocolStatesViewer.ZoomWindowThreshold = 0.05D;
			this.ProtocolStatesViewer.SelectionChanged += new System.EventHandler(this.ProtocolStatesViewerSelectionChanged);
			// 
			// textBoxStatus
			// 
			this.textBoxStatus.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
									| System.Windows.Forms.AnchorStyles.Left) 
									| System.Windows.Forms.AnchorStyles.Right)));
			this.textBoxStatus.Location = new System.Drawing.Point(402, 4);
			this.textBoxStatus.Multiline = true;
			this.textBoxStatus.Name = "textBoxStatus";
			this.textBoxStatus.Size = new System.Drawing.Size(125, 100);
			this.textBoxStatus.TabIndex = 6;
			// 
			// panel1
			// 
			this.panel1.Controls.Add(this.ProtocolStatesViewer);
			this.panel1.Location = new System.Drawing.Point(3, 4);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(393, 247);
			this.panel1.TabIndex = 7;
			// 
			// buttonStart
			// 
			this.buttonStart.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
			this.buttonStart.Image = ((System.Drawing.Image)(resources.GetObject("buttonStart.Image")));
			this.buttonStart.Location = new System.Drawing.Point(532, 175);
			this.buttonStart.Name = "buttonStart";
			this.buttonStart.Size = new System.Drawing.Size(55, 28);
			this.buttonStart.TabIndex = 8;
			this.buttonStart.Text = "Start";
			this.buttonStart.UseVisualStyleBackColor = true;
			this.buttonStart.Click += new System.EventHandler(this.ButtonStartClick);
			// 
			// buttonPause
			// 
			this.buttonPause.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
			this.buttonPause.Image = ((System.Drawing.Image)(resources.GetObject("buttonPause.Image")));
			this.buttonPause.Location = new System.Drawing.Point(532, 209);
			this.buttonPause.Name = "buttonPause";
			this.buttonPause.Size = new System.Drawing.Size(55, 28);
			this.buttonPause.TabIndex = 8;
			this.buttonPause.Text = "Pause";
			this.buttonPause.UseVisualStyleBackColor = true;
			this.buttonPause.Click += new System.EventHandler(this.ButtonPauseClick);
			// 
			// ProtocolUserControl
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
			this.BackColor = System.Drawing.Color.Silver;
			this.Controls.Add(this.buttonPause);
			this.Controls.Add(this.buttonStart);
			this.Controls.Add(this.panel1);
			this.Controls.Add(this.textBoxStatus);
			this.Controls.Add(this.EditParametersbutton);
			this.Controls.Add(this.buttonStop);
			this.Controls.Add(this.textBoxData);
			this.Name = "ProtocolUserControl";
			this.Size = new System.Drawing.Size(595, 256);
			this.Load += new System.EventHandler(this.ProtocolUserControlLoad);
			this.panel1.ResumeLayout(false);
			this.panel1.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();
		}
		private System.Windows.Forms.Button buttonPause;
		private System.Windows.Forms.Button buttonStart;
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.TextBox textBoxStatus;
		private Microsoft.Msagl.GraphViewerGdi.GViewer ProtocolStatesViewer;
		private System.Windows.Forms.Button EditParametersbutton;
		private System.Windows.Forms.Button buttonStop;
		private System.Windows.Forms.TextBox textBoxData;
	}
}
