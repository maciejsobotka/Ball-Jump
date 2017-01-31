using UnityEngine;

public class CameraController : MonoBehaviour
{
    #region Constants

    private const float CURRENT_X_SENSITIVITY = 20.0f;
    private const float DISTANCE = 10.0f;
    private const float Y_ANGLE_MAX = 45.0f;
    private const float Y_ANGLE_MIN = 10.0f;

    #endregion
    #region Public fields

    public Transform LookAt;

    #endregion
    #region Private fields

    private float m_CurrentX;
    private float m_CurrentY;

    #endregion
    #region Private methods

    // Runs after fame
    private void LateUpdate()
    {
        var direction = new Vector3(0, 0, -DISTANCE);
        Quaternion rotation = Quaternion.Euler(m_CurrentY, m_CurrentX * CURRENT_X_SENSITIVITY, 0);

        transform.position = LookAt.position + rotation * direction;
        transform.LookAt(LookAt.position);
    }

    private void Update()
    {
        m_CurrentX += Input.GetAxis("Mouse X");
        m_CurrentY += Input.GetAxis("Mouse Y");

        m_CurrentY = Mathf.Clamp(m_CurrentY, Y_ANGLE_MIN, Y_ANGLE_MAX);
    }

    #endregion
}