#region Usings 
using System;
using UnityEngine;
#endregion

namespace Skahal.Debugging
{
	/// <summary>
	/// Debug stuffs.
	/// </summary>
	public static class SHDebug
	{
		#region Properties
		/// <summary>
		/// Gets a value indicating whether is debug build.
		/// </summary>
		/// <value>
		/// <c>true</c> if is debug build; otherwise, <c>false</c>.
		/// </value>
		public static bool IsDebugBuild {	
			get {
				return Debug.isDebugBuild;
			}
		}
		#endregion
	}
}