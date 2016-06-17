using UnityEngine;
using System.Collections;
using Skahal.Logging;

namespace Skahal
{
	/// <summary>
	/// A base classe to controller with strategy.
	/// </summary>
	public class SHControllerWithStrategy<TModel, TStrategy> : SHController<TModel>
	{
		#region Properties
		/// <summary>
		/// Gets or sets the strategy.
		/// </summary>
		/// <value>The strategy.</value>
		public TStrategy Strategy { get; set; } 
		#endregion
	}
}
