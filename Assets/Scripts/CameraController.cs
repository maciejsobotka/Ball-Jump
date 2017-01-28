using UnityEngine;

public class CameraController : MonoBehaviour
{
    #region Private fields

    private GameObject m_Ball;

    private Vector3 m_Offset;

    #endregion
    #region Private methods

    // Runs after fame
    private void LateUpdate()
    {
        transform.position = m_Ball.transform.position + m_Offset;
    }

    private void Start()
    {
        m_Ball = GameObject.Find("Ball");
        m_Offset = transform.position - m_Ball.transform.position;
    }

    #endregion
}