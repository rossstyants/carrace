using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public enum StateEnum
    {
        ChoosingCar,
        WaitingToStart,
        Racing,
        GameOver
    }

    public StateEnum State;

    public static GameManager instance;

    public bool gameStarted;
    public GameObject platformSpawner;                     //reference to the platform spawner
    public GameObject gamePlayUI;                          // reference to the game play ui gameobject
    public GameObject menuUIChooseCar;                              // reference to the Menu gameobject
    public GameObject menuUIWaitingToStart;                              // reference to the Menu gameobject
    public GameObject gameOverPanel;                       // reference to the gameOver gameobject
    public CameraFollow cameraFollow;                       // reference to the gameOver gameobject
    public CarsRoulette carsRoulette;
    public GameObject carRouletteScene, racingScene;
    public SimpleFade transition;

    public Text scoreText;
    public Text highScoreText;
    public Text gameoverScoreText;
    public Text gameOverScore;
    public Text gameOverCash;

    AudioSource audioSource;
    public AudioClip[] gameMusic;
    
    int score = 0;                                        // when store the score value
    int highScore;                                        // when store the haighScore value
    public Text cashText;
    public static int points;
    // Start is called before the first frame update
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        audioSource = GetComponent<AudioSource>();                    //get audiosource component
            
    }
    private void Start()
    {
        points = PlayerPrefs.GetInt("Score", 0);                           // get current score

        if (PlayerPrefs.HasKey("Score"))                                     // check if the game stock last score
        {
            PlayerPrefs.GetInt("Score");                                        // get the last score 
            cashText.text = PlayerPrefs.GetInt("Score").ToString();           
        }

        platformSpawner.SetActive(false);
        highScore = PlayerPrefs.GetInt("HighScore");                // get the highScore
        highScoreText.text = "ABest Score:" + highScore;             //prinat the highscore
        cashText.text = ("Cash:" + points.ToString());              //print the cash 
        //gameOverScore.text= "Best Score:" + highScore;              // print the score in the game over panel

        State = StateEnum.ChoosingCar;
        cameraFollow.enabled = false;
        gamePlayUI.SetActive(false);
        gameOverPanel.SetActive(false);
        menuUIWaitingToStart.SetActive(false);                          // desactive Menu panel
        menuUIChooseCar.SetActive(true);
        carRouletteScene.SetActive(true);
        racingScene.SetActive(false);
        transition.Hide();

    }

    // Update is called once per frame
    void Update()
    {
        bool tapped = (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space));

        switch (State)
        {
            case StateEnum.ChoosingCar:

                if (tapped)
                {
                    carsRoulette.ChooseCurrentCar();
                    State = StateEnum.WaitingToStart;
                    cameraFollow.enabled = true;
                    menuUIChooseCar.SetActive(false);                          // desactive Menu panel
                    menuUIWaitingToStart.SetActive(true);
                    carRouletteScene.SetActive(false);
                    racingScene.SetActive(true);

                }

                break;
            case StateEnum.WaitingToStart:

                if (tapped)
                {
                    State = StateEnum.Racing;
                    StartBTN();
                }

                break;
            case StateEnum.GameOver:

                if (tapped)
                {
                    transition.Show(RestartApp);                    
                }

                break;
        }

        cashText.text = ("Cash:" + points.ToString());

    }

    private void RestartApp(int i)
    {
        ReloadGame();
    }

    public void StartBTN()                      //Button start 
    {
        if (!gameStarted)                        // check if the game started bool is equal false call the function gamestart
        {
            GameStart();     
        }

    }
    public void GameStart()
    {
        gameStarted = true;
        platformSpawner.SetActive(true);                  //active the platforeSpawner
        menuUIWaitingToStart.SetActive(false);                          // desactive Menu panel
        gamePlayUI.SetActive(true);                       //active game play Ui panel

        audioSource.clip = gameMusic[1];              
        audioSource.Play();                             // play the sound 1
        StartCoroutine("UpdateScore");                  // call function to Update the score
    }

    public void GameOver()
    {
        platformSpawner.SetActive(false);                 // desactive platformSpawner
        StopCoroutine("UpdateScore");                     // stop the function Update the score
        SaveHighScore();

        gameoverScoreText.text = score.ToString("D5");

        Invoke("ShowGameOverPanel", 1f);                         // call function to reload level

        if (points > PlayerPrefs.GetInt("Score"))             // check if points less than score value
        {
            PlayerPrefs.SetInt("Score", points);            // set game score value  to Points
        }
        cashText.text = ("Cash:" + points.ToString());
       // gameOverScore.text= "Best Score:" + highScore;
        gameOverCash.text= ("Cash:" + points.ToString());
        State = StateEnum.GameOver;
    }
    public void ShowGameOverPanel()
    {
        //SceneManager.LoadScene("Menu");
        gameOverPanel.SetActive(true);              //active game over panel
        gamePlayUI.SetActive(false);                //desactive gamePlay Ui
        
    }
    public void ReloadGame()
    {
        SceneManager.LoadScene("Game");      
    }

    IEnumerator UpdateScore()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f);        //wait 1 secend and increase the score
            score+=100;
            //points++;

            scoreText.text = score.ToString();
            //print(score);
        }
    }

    public void IncrementScore()
    {
        score += 500;                                     //increase score by 5 points
        points += 3;                                    // increase cash by 3 points
        scoreText.text = score.ToString();
        audioSource.PlayOneShot(gameMusic[2]);          //play coin sound
    }

    void SaveHighScore()
    {
        if (PlayerPrefs.HasKey("HighScore"))
        {
            // already have highScore
            if(score > PlayerPrefs.GetInt("HighScore"))        //check if the score greater than highscore
            {
                PlayerPrefs.SetInt("HighScore", score);        // save the new highScore
            }

        }
        else
        {
            //Playing for first time
            PlayerPrefs.SetInt("HighScore", score);       // get the score and save for highscore

        }

        
    }
    public void ExitGame()
    {
        Application.Quit();
    }
    public void Shop()                     //Button Shop
    {
        SceneManager.LoadScene("Shop");
    }

}
