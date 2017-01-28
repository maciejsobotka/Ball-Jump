using UnityEngine;

public class BallController : MonoBehaviour
{
    #region Private fields

    private float m_JumpSpeed;
    private Rigidbody m_Rigidbody;
    private float m_Speed;

    #endregion
    #region Private methods

    // All rigidbody manipulated should be performed in FixedUpdate
    private void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");
        float moveJump = Input.GetAxis("Jump");

        var movement = new Vector3(moveHorizontal * m_Speed, moveJump * m_JumpSpeed, moveVertical * m_Speed);

        m_Rigidbody.AddForce(movement);
    }

    // Use this for initialization
    private void Start()
    {
        m_Rigidbody = GetComponent<Rigidbody>();
        m_Speed = 15;
        m_JumpSpeed = 20;
    }

    #endregion
}