#region Usings
using UnityEngine;
#endregion

[AddComponentMenu("Skahal/Transform/SHSmoothFollow")]
public class SmoothFollow : MonoBehaviour
{
    #region Propriedades
    public Transform Target;
    public float Distance = 10;
    public float Height = 5;
    public float HeightDamping = 2;
    public float RotationDamping = 3;
    #endregion

    #region M�todos
    /// <summary>
    /// Chamado a cada frame, se o script estiver habilitado.
    /// <remarks>
    /// � chamado ap�s todos os m�todos Update terem sido chamados. 
    /// � �til para ordernar a execu��o de scripts. Por exemplo uma c�mera que segue um objeto
    /// deve ser sempre implementada no m�todo LateUpdate, pois ela ir� seguir a posi��o do objeto que foi
    /// alterada no Update de seu script.
    /// </remarks>
    /// </summary>
    public void LateUpdate()
    {
        if (!Target)
        {
            return;
        }

        // Calculate the current rotation angles
        float wantedRotationAngle = Target.eulerAngles.y;
        float wantedHeight = Target.position.y + Height;

        float currentRotationAngle = transform.eulerAngles.y;
        float currentHeight = transform.position.y;

        // Damp the rotation around the y-axis
        currentRotationAngle = Mathf.LerpAngle(currentRotationAngle, wantedRotationAngle, RotationDamping * Time.deltaTime);

        // Damp the Height
        currentHeight = Mathf.Lerp(currentHeight, wantedHeight, HeightDamping * Time.deltaTime);

        // Convert the angle into a rotation
        Quaternion currentRotation = Quaternion.Euler(0, currentRotationAngle, 0);

        // Set the position of the camera on the x-z plane to:
        // distance meters behind the target
        transform.position = Target.position;
        transform.position -= currentRotation * Vector3.forward * Distance;

        // Set the Height of the camera
        TransformHelper.SetPositionY(transform, currentHeight);

        // Always look at the target
        transform.LookAt(Target);
    }
    #endregion
}
