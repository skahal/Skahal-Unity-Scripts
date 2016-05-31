#region Usings
using UnityEngine;
using System.Collections;
using System.IO;
#endregion

namespace Skahal.IO
{
	/// <summary>
	/// Helpers for file stuffs.
	/// </summary>
	public static class SHFileHelper
	{
		/// <summary>
		/// Deletes the specified if exists.
		/// </summary>
		/// <returns>
		/// True if file exists, otherwise false.
		/// </returns>
		/// <param name='filePath'>
		/// If set to <c>true</c> file path.
		/// </param>
		public static bool DeleteIfExists (string filePath)
		{
			if (File.Exists (filePath)) {
				File.Delete (filePath);
				return true;
			}
			
			return false;
		}
	}
}