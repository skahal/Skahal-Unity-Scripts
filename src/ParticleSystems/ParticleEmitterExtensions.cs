#region Usings
using UnityEngine;
using System.Linq;
#endregion

namespace Skahal.ParticleSystems
{
	/// <summary>
	/// Extensions methods for ParticleEmitter.
	/// </summary>
	public static class SHParticleEmitterExtensions
	{
		/// <summary>
		/// Sets the emit property in all ParticleEmitters specified.
		/// </summary>
		/// <param name='emitters'>
		/// The ParticleEmitters.
		/// </param>
		/// <param name='emit'>
		/// The emit property value.
		/// </param>
		public static void SetEmit (this ParticleEmitter[] emitters, bool emit)
		{
			foreach (var e in emitters)
			{
				e.emit = emit;
			}
		}
		
		/// <summary>
		/// Sets the emit property recursively in all children GameObjects.
		/// </summary>
		/// <param name='go'>
		/// The parent GameObject
		/// </param>
		/// <param name='emit'>
		/// The emit property value.
		/// </param>
		public static void SetEmitRecursively (this GameObject go, bool emit)
		{
			go.GetComponentsInChildren<ParticleEmitter> ().SetEmit (emit);
		}
		
		/// <summary>
		/// Determines whether the GameObject (or any children GameObject) is emitting any particle.
		/// </summary>
		/// <returns>
		/// <c>true</c> if is emitting any particle; otherwise, <c>false</c>.
		/// </returns>
		/// <param name='go'>
		/// The parent GameObject.
		/// </param>
		public static bool IsEmittingAnyParticle(this GameObject go)
		{
			var emitters = go.GetComponentsInChildren<ParticleEmitter> ();
			
			return emitters.Count(e => e.emit) > 0;
		}
	}
}