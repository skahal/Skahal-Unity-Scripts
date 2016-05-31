#region Usings
using System.Collections.Generic;
#endregion

namespace Skahal.Common
{
	/// <summary>
	/// SHKeyValue's extensions.
	/// </summary>
	public static class SHKeyValueExtensions
	{
			
		#region Public Methods
		/// <summary>
		/// Converts the array of SHKeyValue to a Dictionary<string, string>.
		/// </summary>
		/// <returns>
		/// The dictionary.
		/// </returns>
		/// <param name='keysValues'>
		/// Keys values.
		/// </param>
		public static Dictionary<string, string> ToDictionary (this SHKeyValue[] keysValues)
		{
			var dictionary = new Dictionary<string, string> ();
				
			foreach (var kv in keysValues)
			{
				dictionary.Add (kv.Key, kv.Value);	
			}
				
			return dictionary;
		}
		#endregion
	}
}