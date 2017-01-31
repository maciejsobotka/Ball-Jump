using UnityEngine;
using UnityEngine.UI;

public class BallController : MonoBehaviour
{
    #region Constants

    private const float JUMP_SPEED = 20;
    private const float SPEED = 15;

    #endregion
    #region Public fields

    public Camera Cam;
    public Text EndText;
    public Text ScoreText;

    #endregion
    #region Private fields

    private int m_JewelsCount;
    private int m_NumOfJewels;
    private Rigidbody m_Rigidbody;

    #endregion
    #region Private methods

    // All rigidbody manipulated should be performed in FixedUpdate
    private void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");
        float moveJump = Input.GetAxis("Jump");

        //transform.rotation = Cam.transform.rotation;
        var movement = new Vector3(moveHorizontal * SPEED, moveJump * JUMP_SPEED, moveVertical * SPEED);
        m_Rigidbody.AddForce(Cam.transform.rotation * movement);
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
        ScoreText.text = "Score: " + m_JewelsCount;
        if (m_JewelsCount == m_NumOfJewels)
        {
            EndText.text = "Mission Passed!";
        }
    }

    // Use this for initialization
    private void Start()
    {
        m_Rigidbody = GetComponent<Rigidbody>();
        EndText.text = "";
        m_NumOfJewels = GameObject.FindGameObjectsWithTag("Jewel").Length;
        m_JewelsCount = 0;
        SetCountText();
    }

    #endregion
}