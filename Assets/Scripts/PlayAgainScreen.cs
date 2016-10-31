using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class PlayAgainScreen : MonoBehaviour
{
    public Button playGame;
    public Button menuButton;
    public Text yourScore;
    public Text yourRank;

    // Use this for initialization
    void Start()
    {
        playGame = playGame.GetComponent<Button>();
        menuButton = menuButton.GetComponent<Button>();
        yourRank = yourRank.GetComponent<Text>();
        yourScore = yourScore.GetComponent<Text>();
        int temp;
        string rank;

        if (PlayerPrefs.HasKey("justScored"))
        {
            temp = PlayerPrefs.GetInt("justScored");
        }
        else temp = 0;

        if (PlayerPrefs.HasKey("justRanked"))
        {
            rank = PlayerPrefs.GetString("justRanked");
        }
        else rank = "N/A";

        if (rank == "N/A") yourRank.text = "Not enough for top three!";
        else
        {
            yourRank.text = "Ranking you " + rank + " amongst all players";
        }
        yourScore.text = "You scored " + temp + " points.";

    }

    public void StartLevel()
    {
        SceneManager.LoadScene("_scene_main");    
    }

    public void GoToMenu()
    {
        SceneManager.LoadScene("_scene_menu");
    }
}