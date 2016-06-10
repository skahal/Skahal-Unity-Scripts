#region Usings
using UnityEngine;
#endregion

/// <summary>
/// Utilitário para Transform.
/// </summary>
public static class TransformHelper
{
    #region Métodos

    #region PositionX
    /// <summary>
    /// Define o x da propriedade position de todos os Transform.
    /// </summary>
    /// <param name="transform">Os Transform.</param>
    /// <param name="x">O valor para x.</param>
    public static void SetPositionX(Transform[] transforms, float x)
    {
        foreach (Transform t in transforms)
        {
            SetPositionX(t, x);
        }
    }

    /// <summary>
    /// Define o x da propriedade position do Transform.
    /// </summary>
    /// <param name="transform">O Transform.</param>
    /// <param name="x">O valor para x.</param>
    public static void SetPositionX (this Transform transform, float x)
	{
		Vector3 p = transform.position;
		p.x = x;
		transform.position = p;
	}
		
	/// <summary>
	/// Sets the position z.
	/// </summary>
	/// <param name="transform">Transform.</param>
	/// <param name="z">The z coordinate.</param>
	public static void SetPositionZ (this Transform transform, float z)
	{
		Vector3 p = transform.position;
		p.z = z;
		transform.position = p;
	}
	
	/// <summary>
	/// Sets the local position x.
	/// </summary>
	/// <param name='transform'>
	/// Transform.
	/// </param>
	/// <param name='x'>
	/// X.
	/// </param
	public static void SetLocalPositionX (this Transform transform, float x)
	{
		Vector3 p = transform.localPosition;
		p.x = x;
		transform.localPosition = p;
	}
    
    #endregion

    #region PositionY
    /// <summary>
    /// Define o x da propriedade position de todos os Transform.
    /// </summary>
    /// <param name="transform">Os Transform.</param>
    /// <param name="x">O valor para x.</param>
    public static void SetPositionY(Transform[] transforms, float y)
    {
        foreach (Transform t in transforms)
        {
            SetPositionY(t, y);
        }
    }

    /// <summary>
    /// Define o x da propriedade position do Transform.
    /// </summary>
    /// <param name="transform">O Transform.</param>
    /// <param name="x">O valor para x.</param>
    public static void SetPositionY(this Transform transform, float y)
    {
        Vector3 p = transform.position;
        p.y = y;
        transform.position = p;
    }
    #endregion
	
	#region RotationY
	/// <summary>
	/// Define o y da propriedade rotation de todos os Transform.
	/// </summary>
	/// <param name="transform">Os Transform.</param>
	/// <param name="x">O valor para y.</param>
	public static void SetRotationY(Transform[] transforms, float y)
	{
		foreach (Transform t in transforms)
		{
			SetRotationY(t, y);
		}
	}

	/// <summary>
	/// Define o y da propriedade rotation do Transform.
	/// </summary>
	/// <param name="transform">O Transform.</param>
	/// <param name="x">O valor para y.</param>
	public static void SetRotationY(this Transform transform, float y)
	{
		var r = transform.rotation;
		r.y = y;
		transform.rotation = r;
	}
	#endregion
	
	#region RotationZ
	/// <summary>
	/// Define o z da propriedade rotation de todos os Transform.
	/// </summary>
	/// <param name="transform">Os Transform.</param>
	/// <param name="x">O valor para z.</param>
	public static void SetRotationZ(Transform[] transforms, float z)
	{
		foreach (Transform t in transforms)
		{
			SetRotationZ(t, z);
		}
	}

	/// <summary>
	/// Define o yzda propriedade rotation do Transform.
	/// </summary>
	/// <param name="transform">O Transform.</param>
	/// <param name="x">O valor para z.</param>
	public static void SetRotationZ(this Transform transform, float z)
	{
		var r = transform.rotation;
		r.z = z;
		transform.rotation = r;
	}
	#endregion
    #endregion   
}

