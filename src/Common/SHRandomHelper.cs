#region Usings
using UnityEngine;
using System.Linq;
#endregion

namespace Skahal.Common
{
	/// <summary>
	/// Utility to generate random subjects.
	/// </summary>
	public static class SHRandomHelper
	{
	    #region Methods	    
	    /// <summary>
	    /// Gera um Vector3 aleatório.
	    /// </summary>
	    /// <param name="min">Valor mínimo para x, y, e z.</param>
	    /// <param name="max">Valor máximo para x, y, e z.</param>
	    /// <returns>O Vector3.</returns>
	    public static Vector3 NextVector3(float min, float max)
	    {
	        return new Vector3(
	            Random.Range(min, max),
	            Random.Range(min, max),
	            Random.Range(min, max));
	    }
	
	    /// <summary>
	    /// Gera um Vector3 aleatório.
	    /// </summary>
	    /// <param name="minX">Valor mínimo para x.</param>
	    /// <param name="maxX">Valor máxiomo para x.</param>
	    /// <param name="minY">Valor mínimo para y.</param>
	    /// <param name="maxY">Valor máxiomo para y.</param>
	    /// <param name="minZ">Valor mínimo para z.</param>
	    /// <param name="maxZ">Valor máxiomo para z.</param>
	    /// <returns></returns>
	    public static Vector3 NextVector3(float minX, float maxX, float minY, float maxY, float minZ, float maxZ)
	    {
	        return new Vector3(
	            Random.Range(minX, maxX),
	            Random.Range(minY, maxY),
	            Random.Range(minZ, maxZ));
	    }
      
        /// <summary>
        /// Generates a random boolean.
        /// </summary>
        /// <returns>The boolean value.</returns>
        public static bool NextBool()
		{
			return Random.Range(0, 2) == 0;
		}

	    /// <summary>
	    /// Gera um Color aleatório.
	    /// </summary>
	    /// <returns>O Color.</returns>
	    public static Color NextColor()
	    {
	        return new Color(Random.value, Random.value, Random.value);
	    }

        /// <summary>
        /// Generates a random enum value
        /// </summary>
        /// <typeparam name="TEnum">The type of the enum.</typeparam>
        /// <returns>The enum value.</returns>
        /// <exception cref="System.ArgumentException">TEnum must be an enumerated type</exception>
        public static TEnum NextEnum<TEnum>()
            where TEnum: struct, System.IConvertible
        {
            var enumType = typeof(TEnum);

            if (!enumType.IsEnum)
            {
                throw new System.ArgumentException("TEnum must be an enumerated type");
            }

            var values = System.Enum.GetValues(enumType).Cast<TEnum>().ToArray();
            
            return values[Random.Range(0, values.Length)];
        }
	    #endregion
	}
}