using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    #region Constants

    private const int NUM_OF_JEWELS = 20;
    private const int NUM_OF_TERRAIN = 10;

    #endregion
    #region Public fields

    public Text InfoText;
    public GameObject Jewel;
    public Text ScoreText;
    public GameObject Terrain;
    public Text TimerText;

    #endregion
    #region Private fields

    private int m_JewelsCount;
    private float m_Timer;
    private bool m_IsGameFinished;

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

    private string FormatTime(float timer)
    {
        int minutes = Mathf.FloorToInt(timer / 60F);
        int seconds = Mathf.FloorToInt(timer - minutes * 60);
        int miliseconds = Mathf.FloorToInt((timer - minutes * 60 - seconds) * 100);
        return string.Format("{0:00}:{1:00}:{2:00}", minutes, seconds, miliseconds);
    }

    private void SetTexts()
    {
        if (m_JewelsCount != NUM_OF_JEWELS)
        {
            m_JewelsCount = NUM_OF_JEWELS - GameObject.FindGameObjectsWithTag("Jewel").Length;
            m_Timer += Time.deltaTime;
            TimerText.text = FormatTime(m_Timer);
            ScoreText.text = "Score: " + m_JewelsCount + " / " + NUM_OF_JEWELS;
        }
        else
        {
            InfoText.text = "Congratulations!";
            m_IsGameFinished = true;
        }
    }

    // Use this for initialization
    private void Start()
    {
        CreateTerrain();
        CreateJewels();
        Cursor.visible = false;
        m_IsGameFinished = false;
        InfoText.text = "";
        SetTexts();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (Mathf.FloorToInt(Time.timeScale) == 0)
            {
                Time.timeScale = 1;
                InfoText.text = "";
            }
            else
            {
                Time.timeScale = 0;
                InfoText.text = "Paused";
            }
        }
        if ((m_IsGameFinished || Mathf.FloorToInt(Time.timeScale) == 0) && Input.GetKeyDown(KeyCode.Return))
        {
            SceneManager.LoadScene("MainScene");
            m_IsGameFinished = false;
            Time.timeScale = 1;
            InfoText.text = "";
        }
        SetTexts();
    }

    #endregion
}