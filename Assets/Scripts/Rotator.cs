using UnityEngine;

public class Rotator : MonoBehaviour
{
    #region Private methods

    private void Update()
    {
        transform.Rotate(new Vector3(15, 30, 45) * Time.deltaTime);
    }

    #endregion
}