using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class StartMenu : MonoBehaviour {

    public Canvas scoreMenu;
    public Button playGame;
    public Button scoreBoard;
    public Button quitButton;
    public Text firstPlace;
    public Text secondPlace;
    public Text thirdPlace;

    // Use this for initialization
    void Start () {
        scoreMenu = scoreMenu.GetComponent<Canvas>();
        playGame = playGame.GetComponent<Button>();
        scoreBoard = scoreBoard.GetComponent<Button>();
        quitButton = quitButton.GetComponent<Button>();
        firstPlace = firstPlace.GetComponent<Text>();
        secondPlace = secondPlace.GetComponent<Text>();
        thirdPlace = thirdPlace.GetComponent<Text>();
        if (!PlayerPrefs.HasKey("firstPlace"))
        {
            PlayerPrefs.SetFloat("firstPlace", 0);
            PlayerPrefs.SetFloat("secondPlace", 0);
            PlayerPrefs.SetFloat("thirdPlace", 0);
            PlayerPrefs.Save();
        }
        firstPlace.text = "First Place: " + PlayerPrefs.GetFloat("firstPlace");
        secondPlace.text = "Second Place: " + PlayerPrefs.GetFloat("secondPlace");
        thirdPlace.text = "Third Place: " + PlayerPrefs.GetFloat("thirdPlace");
        scoreMenu.enabled = false;
	}
	
	public void ScorePress()
    {
        scoreMenu.enabled = true;
        playGame.enabled = false;
        scoreBoard.enabled = false;
        quitButton.enabled = false;
    }

    public void ExitPress()
    {
        scoreMenu.enabled = false;
        playGame.enabled = true;
        scoreBoard.enabled = true;
        quitButton.enabled = true;
    }

    public void StartLevel()
    {
        SceneManager.LoadScene("_scene_main");
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
