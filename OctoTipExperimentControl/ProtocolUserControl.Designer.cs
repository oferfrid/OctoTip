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
			this.checkBoxStartPause = new System.Windows.Forms.CheckBox();
			this.EditParametersbutton = new System.Windows.Forms.Button();
			this.ProtocolStatesViewer = new Microsoft.Msagl.GraphViewerGdi.GViewer();
			this.textBoxStatus = new System.Windows.Forms.TextBox();
			this.SuspendLayout();
			// 
			// textBoxData
			// 
			this.textBoxData.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
									| System.Windows.Forms.AnchorStyles.Left) 
									| System.Windows.Forms.AnchorStyles.Right)));
			this.textBoxData.Location = new System.Drawing.Point(390, 47);
			this.textBoxData.Multiline = true;
			this.textBoxData.Name = "textBoxData";
			this.textBoxData.Size = new System.Drawing.Size(71, 45);
			this.textBoxData.TabIndex = 1;
			// 
			// buttonStop
			// 
			this.buttonStop.Anchor = System.Windows.Forms.AnchorStyles.Right;
			this.buttonStop.Enabled = false;
			this.buttonStop.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
			this.buttonStop.Image = ((System.Drawing.Image)(resources.GetObject("buttonStop.Image")));
			this.buttonStop.Location = new System.Drawing.Point(468, 28);
			this.buttonStop.Name = "buttonStop";
			this.buttonStop.Size = new System.Drawing.Size(55, 28);
			this.buttonStop.TabIndex = 2;
			this.buttonStop.Text = "Stop";
			this.buttonStop.UseVisualStyleBackColor = true;
			this.buttonStop.Click += new System.EventHandler(this.ButtonStopClick);
			// 
			// checkBoxStartPause
			// 
			this.checkBoxStartPause.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.checkBoxStartPause.Appearance = System.Windows.Forms.Appearance.Button;
			this.checkBoxStartPause.Enabled = false;
			this.checkBoxStartPause.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
			this.checkBoxStartPause.Image = ((System.Drawing.Image)(resources.GetObject("checkBoxStartPause.Image")));
			this.checkBoxStartPause.Location = new System.Drawing.Point(468, 60);
			this.checkBoxStartPause.Name = "checkBoxStartPause";
			this.checkBoxStartPause.Size = new System.Drawing.Size(55, 30);
			this.checkBoxStartPause.TabIndex = 3;
			this.checkBoxStartPause.Text = "Start";
			this.checkBoxStartPause.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this.checkBoxStartPause.UseVisualStyleBackColor = true;
			this.checkBoxStartPause.CheckedChanged += new System.EventHandler(this.CheckBoxStartPauseCheckedChanged);
			// 
			// EditParametersbutton
			// 
			this.EditParametersbutton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.EditParametersbutton.BackColor = System.Drawing.Color.Red;
			this.EditParametersbutton.Location = new System.Drawing.Point(468, 3);
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
			this.ProtocolStatesViewer.BackwardEnabled = false;
			this.ProtocolStatesViewer.BuildHitTree = true;
			this.ProtocolStatesViewer.CurrentLayoutMethod = Microsoft.Msagl.GraphViewerGdi.LayoutMethod.SugiyamaScheme;
			this.ProtocolStatesViewer.ForwardEnabled = false;
			this.ProtocolStatesViewer.Graph = null;
			this.ProtocolStatesViewer.LayoutAlgorithmSettingsButtonVisible = true;
			this.ProtocolStatesViewer.LayoutEditingEnabled = true;
			this.ProtocolStatesViewer.Location = new System.Drawing.Point(3, 3);
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
			this.ProtocolStatesViewer.Size = new System.Drawing.Size(381, 89);
			this.ProtocolStatesViewer.TabIndex = 5;
			this.ProtocolStatesViewer.ToolBarIsVisible = false;
			this.ProtocolStatesViewer.ZoomF = 1D;
			this.ProtocolStatesViewer.ZoomFraction = 0.5D;
			this.ProtocolStatesViewer.ZoomWindowThreshold = 0.05D;
			this.ProtocolStatesViewer.SelectionChanged += new System.EventHandler(this.ProtocolStatesViewerSelectionChanged);
			// 
			// textBoxStatus
			// 
			this.textBoxStatus.Location = new System.Drawing.Point(390, 3);
			this.textBoxStatus.Multiline = true;
			this.textBoxStatus.Name = "textBoxStatus";
			this.textBoxStatus.Size = new System.Drawing.Size(71, 38);
			this.textBoxStatus.TabIndex = 6;
			// 
			// ProtocolUserControl
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
			this.Controls.Add(this.textBoxStatus);
			this.Controls.Add(this.ProtocolStatesViewer);
			this.Controls.Add(this.EditParametersbutton);
			this.Controls.Add(this.checkBoxStartPause);
			this.Controls.Add(this.buttonStop);
			this.Controls.Add(this.textBoxData);
			this.Name = "ProtocolUserControl";
			this.Size = new System.Drawing.Size(530, 95);
			this.Load += new System.EventHandler(this.ProtocolUserControlLoad);
			this.ResumeLayout(false);
			this.PerformLayout();
		}
		private System.Windows.Forms.TextBox textBoxStatus;
		private Microsoft.Msagl.GraphViewerGdi.GViewer ProtocolStatesViewer;
		private System.Windows.Forms.Button EditParametersbutton;
		private System.Windows.Forms.CheckBox checkBoxStartPause;
		private System.Windows.Forms.Button buttonStop;
		private System.Windows.Forms.TextBox textBoxData;
	}
}
