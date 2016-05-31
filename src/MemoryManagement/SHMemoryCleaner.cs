#region Usings
using System;
using Skahal.Logging;
using UnityEngine;
#endregion

namespace Skahal.MemoryManagement
{
	/// <summary>
	/// Memory cleaner.
	/// </summary>
	public static class SHMemoryCleaner
	{
		#region Methods
		/// <summary>
		/// Clear the memory.
		/// </summary>
		public static void Clear ()
		{
			SHLog.Debug ("SHMemoryCleaner: memory in use before cleaning: {0}", GC.GetTotalMemory (false));
			Resources.UnloadUnusedAssets ();
			SHLog.Debug ("SHMemoryCleaner: memory in use after cleaning: {0}", GC.GetTotalMemory (true));
		}
		#endregion
	}
}