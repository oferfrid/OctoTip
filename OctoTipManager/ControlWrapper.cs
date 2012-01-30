/*
 * Created by SharpDevelop.
 * User: Tecan
 * Date: 07/10/2011
 * Time: 00:18
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;

namespace OctoTip.Manager
{
	/// <summary>
	/// Description of ControlWrapper.
	/// </summary>
	public class ControlWrapper:System.Windows.Forms.DataGridView
	{
		public ControlWrapper():base()
		{
		}
		
		protected override void OnPaint(System.Windows.Forms.PaintEventArgs e)
		{
			try {
				base.OnPaint(e);
			} catch (Exception ex) {
				this.Invalidate();
			}
		}
	}
}
