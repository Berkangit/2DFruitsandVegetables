using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    //müzik eklenecek  ,diffulcty deðiþtirtilebilir , reklam ekle 
   public List<GameObject> allitems;
   [SerializeField]private float spawnRate = 1.0f;
   public TextMeshProUGUI scoreText, challangeText , timerText;
    public bool blowfruits;
   private int score;
    private float currentTime;
    private float startingTime = 60;
    public GameObject gamePanel;
    public GameObject gameOverPanel;
    public GameObject menuPanel;
    public bool isGameOn;
    public TextMeshProUGUI highScoreText;
    // public menupanelScript menupanelsc;
    public Animator menuPanelAnimator;
    private string highscore = "High Score : ";
    public soundManager Sound;
    public Camera mainCam;

    //private float decreaseSpawnRate = 0.2f;




    private void Start()
    {
        
        currentTime = startingTime;
        score = 0;
        blowfruits = true;
        challangeText.text = "BLOW THE FRUITS";
        //ChangeTheChallange();
        isGameOn = true;
        StartCoroutine(SpawnTarget());
        StartCoroutine(ChangeTheChallange());

        UpdateScore(0);     
        
        
        //menupanelsc = GameObject.Find("menuPanel").GetComponent<menupanelScript>();

        highScoreText.text = highscore + PlayerPrefs.GetInt("HighScore", 0).ToString();


    }
    private void Update()
    {
        currentTime -= 1 * Time.deltaTime;
        timerText.text = currentTime.ToString("0");


        if (currentTime <= 0)
        {
            currentTime = 0;
            GameOver();
        }
       // else if (currentTime / 5 == 0)
            

          
    }



    IEnumerator SpawnTarget()
    {
        while(isGameOn)
        {
        yield return new WaitForSeconds(spawnRate);
        int index = Random.Range(0, allitems.Count);
        Instantiate(allitems[index]);
        }
    }


   public void UpdateScore(int scoreToAdd)
    {
      
        score += scoreToAdd;
        scoreText.text = score.ToString();

        if(score > PlayerPrefs.GetInt("HighScore",0))
        {
            PlayerPrefs.SetInt("HighScore", score);
            highScoreText.text = highscore + score.ToString();

        }
        
    }



    IEnumerator ChangeTheChallange()
    {
        while(isGameOn)
        {
            int randomindex = Random.Range(6, 15);
            yield return new WaitForSeconds(randomindex);
            int change = Random.Range(0, 2);
           
            
            if (change == 0)
            {
                challangeText.text = "BLOW THE FRUITS"; 
                blowfruits = true;
            }

            else
            {
               
                challangeText.text = "BLOW THE VEGETABLES";
                blowfruits = false;

            }
            Sound.IncreasePitch();


            // Debug.Log(challangeText.text);

        }

    }


    public void GameOver()
    {
        isGameOn = false;
        menuPanelAnimator.SetBool("isGameOn", false);
        if (!isGameOn)
        {
            gamePanel.gameObject.SetActive(false);
            gameOverPanel.gameObject.SetActive(true);


            mainCam.gameObject.GetComponent<AudioSource>().enabled = false;
            


        }
    }

    //public void ResetHighScore()
    //{
    //    PlayerPrefs.DeleteKey("HighScore");
    //}

    public void RestartGame()
    {
       
        SceneManager.LoadScene("GameScene");
        



    }

    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Quit");
        
    }
    
}
