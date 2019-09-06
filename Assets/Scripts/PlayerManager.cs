using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerManager : MonoBehaviour
{
    public int lifeValue;
    public int playerScore;

    public GameObject Born;
    public bool GameOver;
    public bool isDead;

    public Text playerScoreText;
    public Text playerLifeText;
    public GameObject gameOverUI;

    private static PlayerManager instance;

    public static PlayerManager Instance { get => instance; set => instance = value; }

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isDead)
        {
            Respawn();
        }
        playerLifeText.text = lifeValue.ToString();
        playerScoreText.text = playerScore.ToString();
    }

    private void Respawn()
    {
        if (lifeValue <= 0)
        {
            gameOverUI.SetActive(true);
            Invoke("RetrunMainMenu", 3);
            return;
        }
        else
        {
            lifeValue--;
            GameObject go = Instantiate(Born, new Vector3(-2, -8, 0), Quaternion.identity);
            go.GetComponent<Born>().spawnPlayer = true;
            isDead = false;
        }
    }

    private void RetrunMainMenu()
    {
        SceneManager.LoadScene(0);
    }
}
