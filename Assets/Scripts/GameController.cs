using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public GameObject[] hazards;
    public Vector3 spawnValues;
    public int hazardCount;
    public float spawnWait;
    public float waveWait;
    public float startWait;
    private int playerNumber;

    public GUIText[] scoreTexts;
    public GUIText restartText;
    public GUIText gameOverText;

    public GameObject masterText;

    private bool gameOver;
    private bool restart;
    public int[] scores;

    public enum PlayerPool{PLAYER1 = 0, PLAYER2 = 1};

    public bool GameOver1
    {
        get
        {
            return gameOver;
        }

        set
        {
            gameOver = value;
        }
    }

    public int PlayerNumber
    {
        get
        {
            return playerNumber;
        }

        set
        {
            playerNumber = value;
        }
    }

    private void Start()
    {
        this.DisplayScore();
        gameOver = false;
        restart = false;
        restartText.text = "";
        gameOverText.text = "";
        this.UpdateScore();
        StartCoroutine(SpawnWaves());
    }

    private void DisplayScore()
    {
        masterText.SetActive(true);

        for(int i = 0; i < scoreTexts.Length; i++)
        {
            if(i <= PlayerNumber - 1)
            {
                scoreTexts[i].gameObject.SetActive(true);
            }

        }
    }

    private void Update()
    {
        if(restart)
        {
            if(Input.GetKeyDown(KeyCode.R))
            {
                SceneManager.LoadScene("Main");
            }
        }
    }

    IEnumerator SpawnWaves()
    {
        yield return new WaitForSeconds(startWait);

        while(true)
        {
            for(int i = 0; i < hazardCount; i++)
            {
                GameObject hazard = hazards[Random.Range(0, hazards.Length)];
                Vector3 spawnPosition = new Vector3(Random.Range(-spawnValues.x, spawnValues.x), spawnValues.y, spawnValues.z);
                Quaternion spawnRotation = Quaternion.identity;

                Instantiate(hazard, spawnPosition, spawnRotation);
                yield return new WaitForSeconds(spawnWait);

            }

            yield return new WaitForSeconds(waveWait);

            if(gameOver)
            {
                restartText.text = "Press 'R' for restart";
                restart = true;
                break;
            }

        }
       

    }

    public void AddScore(int newScoreValue, PlayerPool pool)
    {
        int oldScoreValue = this.scores[(int)pool];
        int finalScore = oldScoreValue + newScoreValue;
        this.scores[(int)pool] = finalScore;
        this.UpdateScore();
    }

    void UpdateScore()
    {

        for(int i = 0;  i  < scores.Length; i++)
        {

            int scoreValue = this.scores[i];
            GUIText scoreText = this.scoreTexts[i];

            switch (i)
            {

                case (int)PlayerPool.PLAYER1:
                    scoreText.text = "Player 1 Score : " + scoreValue;
                    break;
                case (int)PlayerPool.PLAYER2:
                    scoreText.text = "Player 2 Score : " + scoreValue;
                    break;

            }

        }
        
    }

    public void GameOver()
    {
        gameOverText.text = "Game Over";
        gameOver = true;
    }

}
