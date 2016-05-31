#region Usings
using UnityEngine;
#endregion

[AddComponentMenu("Skahal/Textures/SHRotateScript")]
public class RotateScript : MonoBehaviour
{
    #region Propriedades
    public Vector3 Rotation;
    #endregion

    #region M�todos
    /// <summary>
    /// Chamado a cada frame, se a inst�ncia do script est� habilitada.
    /// <remarks>
    /// Esse m�todo � normalmente utilizado para implementar qualquer tipo de comportamento do jogo.
    /// </remarks>
    /// </summary>
    private void Update()
    {        
        transform.Rotate(Rotation);
    }
    #endregion
}
