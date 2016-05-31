using UnityEngine;
using Skahal.Tweening;

[AddComponentMenu("Skahal/Transform/SHScaleScript")]
public class ScaleScript : MonoBehaviour
{
	#region Propriedades
	public Vector3 Scale = new Vector3(5, 5, 5);
	public float TimeToScale = 5f;
	public iTween.EaseType EaseType = iTween.EaseType.linear;
    #endregion

    #region Métodos
    void Start()
    {
    	iTweenHelper.ScaleTo(gameObject, 
			iT.ScaleTo.scale, Scale, 
			iT.ScaleTo.time, TimeToScale,
			iT.ScaleTo.easetype, EaseType,
			iT.ScaleTo.looptype, iTween.LoopType.pingPong);
	}
    #endregion
}

