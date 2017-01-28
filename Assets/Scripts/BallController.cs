using UnityEngine;
using UnityEngine.UI;

public class BallController : MonoBehaviour
{
    #region Private fields

    private int m_JewelsCount;

    private float m_JumpSpeed;
    private Rigidbody m_Rigidbody;
    private Text m_ScoreText;
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

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Jewel"))
        {
            other.gameObject.SetActive(false);
            m_JewelsCount++;
            SetCountText();
        }
    }

    private void SetCountText()
    {
        m_ScoreText.text = "Score: " + m_JewelsCount;
    }

    // Use this for initialization
    private void Start()
    {
        m_Rigidbody = GetComponent<Rigidbody>();
        m_Speed = 15;
        m_JumpSpeed = 20;
        m_JewelsCount = 0;
        m_ScoreText = GetComponentInParent<Canvas>().GetComponent<Text>();
        SetCountText();
    }

    #endregion
}