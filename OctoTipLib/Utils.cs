/*
 * Created by SharpDevelop.
 * User: Tecan
 * Date: 18/10/2011
 * Time: 21:13
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;

namespace OctoTip.OctoTipLib
{

	public static class Utils
	{
		/// <summary>
		/// return the index of Liconic Possition [1 34]
		/// </summary>
		/// <param name="LP">LicPos</param>
		/// <returns>Liconic Possition [1 34]</returns>
		public static int LicPos2Ind(LicPos LP)
		{
			int ind = (LP.Cart -1)*17 + LP.Pos;
			return ind;
		}
		
		public static LicPos Ind2LicPos(int ind)
		{
			LicPos LP = new LicPos(((ind-1)%17)+1,Convert.ToInt32(Math.Floor(Convert.ToDouble(ind)/17.0)));
			
			return LP;
		}
		
	}
	public struct LicPos
	{
		public int Pos;
		public int Cart;
		public	LicPos(int Pos ,int Cart)
		{
			this.Pos = Pos;
			this.Cart = Cart;
		}
		
	}
}
