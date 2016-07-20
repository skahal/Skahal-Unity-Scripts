using System;
using Skahal.Logging;
using UnityEngine;

namespace Skahal
{
	public interface ISHController<TModel>
	{
		#region Properties
		/// <summary>
		/// Gets or sets a value indicating whether this <see cref="Skahal.SHControllerBase"/> is cloneable.
		/// </summary>
		/// <value><c>true</c> if cloneable; otherwise, <c>false</c>.</value>
		bool Cloneable { get; set; }

		/// <summary>
		/// Gets or sets the model.
		/// </summary>
		/// <value>The model.</value>
		TModel Model { get; set; }

		/// <summary>
		/// Gets the game object.
		/// </summary>
		/// <value>The game object.</value>
		GameObject gameObject { get; }
		#endregion
	}
}
