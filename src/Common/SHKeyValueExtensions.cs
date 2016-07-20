using System.Collections.Generic;
using System.Linq;
using System;

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

		public static TValue Get<TKey, TValue> (this SHKeyValue<TKey, TValue>[] array, TKey key)
		{
			var item = array.FirstOrDefault (a => a.Key.Equals(key));

			if (item == null) {
				return default(TValue);
			}

			return item.Value;
		}
		#endregion
	}
}