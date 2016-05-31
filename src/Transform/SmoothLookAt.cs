#region Usings
using UnityEngine;
#endregion

[AddComponentMenu("Skahal/Transform/SHSmoothLookAt")]
public class SmoothLookAt : MonoBehaviour
{
    #region Propriedades
    public Transform Target;
    public float Damping = 6.0f;
    public bool Smooth = true;
    #endregion

    #region Métodos
    /// <summary>
    /// Chamado a cada frame, se o script estiver habilitado.
    /// <remarks>
    /// É chamado após todos os métodos Update terem sido chamados. 
    /// É útil para ordernar a execução de scripts. Por exemplo uma câmera que segue um objeto
    /// deve ser sempre implementada no método LateUpdate, pois ela irá seguir a posição do objeto que foi
    /// alterada no Update de seu script.
    /// </remarks>
    /// </summary>
    private void LateUpdate()
    {
        if (Target)
        {
            if (Smooth)
            {
                // Look at and dampen the rotation
                Quaternion rotation = Quaternion.LookRotation(Target.position - transform.position);
                transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * Damping);
            }
            else
            {
                // Just lookat
                transform.LookAt(Target);
            }
        }
    }

    private void Start()
    {
        // Make the rigid body not change rotation
        if (GetComponent<Rigidbody>())
        {
            GetComponent<Rigidbody>().freezeRotation = true;
        }
    }
    #endregion
}