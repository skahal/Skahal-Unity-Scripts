#region Usings
using System;
#endregion

namespace Skahal.Timing
{
	/// <summary>
	/// A entry where a number of touches corresponds to a time scale.
	/// </summary>
	[Serializable]
	public class SHTouchesTimeScaleEntry
	{
		#region Properties
		/// <summary>
		/// Gets or sets the number of touches.
		/// </summary>
		public int Touches;
		
		/// <summary>
		/// Gets or sets the time scale.
		/// </summary>
		public float TimeScale;
		#endregion
	}
}