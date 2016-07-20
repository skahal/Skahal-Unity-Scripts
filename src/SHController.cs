using UnityEngine;
using System.Collections;
using System;
using Skahal.Logging;

namespace Skahal
{
	/// <summary>
	/// A controller base class with an associate model.
	/// </summary>
	public class SHController<TModel> : SHControllerBase, ISHController<TModel>
	{
		#region Fields
		private TModel m_model;
		#endregion

		#region Properties
		/// <summary>
		/// Gets or sets the model.
		/// </summary>
		/// <value>The model.</value>
		public TModel Model { 
			get {
				return m_model;
			}
			
			set {
				m_model = value;
				
				if (m_model != null) {
					OnModelAssigned();
				}
			}
		}
		#endregion
		
		#region Methods
		/// <summary>
		/// Called when the model is assigned.
		/// </summary>
		protected virtual void OnModelAssigned()
		{
		}
		#endregion
	}
}