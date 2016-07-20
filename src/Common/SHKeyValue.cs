#region Usings
using System;
using System.Collections.Generic;
#endregion

namespace Skahal.Common
{
	/// <summary>
	/// A key value for general purpose.
	/// </summary>
	[Serializable]
	public class SHKeyValue<TKey, TValue>
	{
		#region Properties
		/// <summary>
		/// Gets or sets the key.
		/// </summary>
		public TKey Key;
		
		/// <summary>
		/// Gets or sets the value.
		/// </summary>
		public TValue Value;
		#endregion

	}

	/// <summary>
	/// A key value for general purpose.
	/// </summary>
	[Serializable]
	public class SHKeyValue : SHKeyValue<string, string>
	{
	}
}