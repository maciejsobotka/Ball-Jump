using System.Linq;
using UnityEngine;

public class BallController : MonoBehaviour
{
    #region Constants

    private const float JUMP_SPEED = 20;
    private const float SPEED = 15;

    #endregion
    #region Public fields

    public Camera Cam;

    #endregion
    #region Private fields

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
            GetComponents<AudioSource>()[1].Play();
            Destroy(other.gameObject);
        }
    }

    // Use this for initialization
    private void Start()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, 1);
        // Ignore Ground and Ceiling in count
        if (colliders.Length > 2)
        {
            float maxScaleY = colliders.Aggregate<Collider, float>(0, (current, someCollider) => Mathf.Max(current, someCollider.transform.localScale.y));
            var position = new Vector3(0, maxScaleY + 0.5f, 0);
            transform.position = position;
        }
        m_Rigidbody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            GetComponents<AudioSource>()[0].Play();
        }
    }

    #endregion
}