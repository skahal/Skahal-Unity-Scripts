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
	public class SHKeyValue
	{
		#region Properties
		/// <summary>
		/// Gets or sets the key.
		/// </summary>
		public string Key;
		
		/// <summary>
		/// Gets or sets the value.
		/// </summary>
		public string Value;
		#endregion

	}
}