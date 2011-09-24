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
			this.statusStrip1 = new System.Windows.Forms.StatusStrip();
			this.menuStrip1 = new System.Windows.Forms.MenuStrip();
			this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.eToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.splitHorizontal = new System.Windows.Forms.SplitContainer();
			this.splitVertical = new System.Windows.Forms.SplitContainer();
			this.dataGridViewRobotJobsQueue = new System.Windows.Forms.DataGridView();
			this.menuStrip1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.splitHorizontal)).BeginInit();
			this.splitHorizontal.Panel1.SuspendLayout();
			this.splitHorizontal.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.splitVertical)).BeginInit();
			this.splitVertical.Panel1.SuspendLayout();
			this.splitVertical.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.dataGridViewRobotJobsQueue)).BeginInit();
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
									this.fileToolStripMenuItem});
			this.menuStrip1.Location = new System.Drawing.Point(0, 0);
			this.menuStrip1.Name = "menuStrip1";
			this.menuStrip1.Size = new System.Drawing.Size(713, 24);
			this.menuStrip1.TabIndex = 1;
			this.menuStrip1.Text = "menuStrip1";
			// 
			// fileToolStripMenuItem
			// 
			this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
									this.eToolStripMenuItem});
			this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
			this.fileToolStripMenuItem.Size = new System.Drawing.Size(35, 20);
			this.fileToolStripMenuItem.Text = "File";
			// 
			// eToolStripMenuItem
			// 
			this.eToolStripMenuItem.Name = "eToolStripMenuItem";
			this.eToolStripMenuItem.Size = new System.Drawing.Size(92, 22);
			this.eToolStripMenuItem.Text = "Exit";
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
			this.splitVertical.Size = new System.Drawing.Size(713, 386);
			this.splitVertical.SplitterDistance = 477;
			this.splitVertical.TabIndex = 0;
			// 
			// dataGridViewRobotJobsQueue
			// 
			this.dataGridViewRobotJobsQueue.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.dataGridViewRobotJobsQueue.Dock = System.Windows.Forms.DockStyle.Fill;
			this.dataGridViewRobotJobsQueue.Location = new System.Drawing.Point(0, 0);
			this.dataGridViewRobotJobsQueue.Name = "dataGridViewRobotJobsQueue";
			this.dataGridViewRobotJobsQueue.Size = new System.Drawing.Size(477, 386);
			this.dataGridViewRobotJobsQueue.TabIndex = 0;
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(713, 530);
			this.Controls.Add(this.splitHorizontal);
			this.Controls.Add(this.statusStrip1);
			this.Controls.Add(this.menuStrip1);
			this.Name = "MainForm";
			this.Text = "OctoTipManager";
			this.menuStrip1.ResumeLayout(false);
			this.menuStrip1.PerformLayout();
			this.splitHorizontal.Panel1.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.splitHorizontal)).EndInit();
			this.splitHorizontal.ResumeLayout(false);
			this.splitVertical.Panel1.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.splitVertical)).EndInit();
			this.splitVertical.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.dataGridViewRobotJobsQueue)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();
		}
		private System.Windows.Forms.DataGridView dataGridViewRobotJobsQueue;
		private System.Windows.Forms.SplitContainer splitVertical;
		private System.Windows.Forms.SplitContainer splitHorizontal;
		private System.Windows.Forms.ToolStripMenuItem eToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
		private System.Windows.Forms.MenuStrip menuStrip1;
		private System.Windows.Forms.StatusStrip statusStrip1;
		
		
	}
}
