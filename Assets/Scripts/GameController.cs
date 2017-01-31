using System.Linq;
using UnityEngine;

public class GameController : MonoBehaviour
{
    #region Constants

    private const int NUM_OF_JEWELS = 20;
    private const int NUM_OF_TERRAIN = 10;

    #endregion
    #region Public fields

    public GameObject Jewel;
    public GameObject Terrain;

    #endregion
    #region Private methods

    private void CreateJewels()
    {
        for (var i = 0; i < NUM_OF_JEWELS; ++i)
        {
            var position = new Vector3(Random.Range(-19.0f, 19.0f), 0.8f, Random.Range(-19.0f, 19.0f));
            Collider[] colliders = Physics.OverlapBox(position, new Vector3(0.25f, 0.25f, 0.25f));
            if (colliders.Length > 0)
            {
                float maxScaleY = colliders.Aggregate<Collider, float>(0, (current, someCollider) => Mathf.Max(current, someCollider.transform.localScale.y));
                position.y = maxScaleY + 0.8f;
            }
            Instantiate(Jewel, position, Quaternion.identity);
        }
    }

    private void CreateTerrain()
    {
        for (var i = 0; i < NUM_OF_TERRAIN; ++i)
        {
            float xSale = Random.Range(3.0f, 8.0f);
            float ySale = Random.Range(3.0f, 8.0f);
            float zSale = Random.Range(3.0f, 8.0f);
            var position = new Vector3(Random.Range(-15.0f, 15.0f), ySale / 2, Random.Range(-15.0f, 15.0f));
            var scale = new Vector3(xSale, ySale, zSale);
            GameObject newTerrain = Instantiate(Terrain, position, Quaternion.identity);
            newTerrain.transform.localScale = scale;
        }
    }

    // Use this for initialization
    private void Start()
    {
        CreateTerrain();
        CreateJewels();
    }

    #endregion
}