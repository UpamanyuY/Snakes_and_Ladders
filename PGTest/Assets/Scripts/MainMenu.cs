using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public static MainMenu instance = null;
    int P1Score = 0;
    int P2Score = 0;
    int P3Score = 0;
    int P4Score = 0;

    GameObject Winner;
    GameObject Loser1;
    GameObject Loser2;
    GameObject Loser3;

    public Text WinnerText;
    public Text Loser1Text;
    public Text Loser2Text;
    public Text Loser3Text;
       
    public Image EndResult;

    private float delayInSeconds = 5.0f;


    private void Awake()
    {
        SetUpSingleton();
    }

    private void SetUpSingleton()
    {
        int numberGameSessions = FindObjectsOfType<MainMenu>().Length;
        
        if (numberGameSessions > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    public GameObject GetWinner()
    {
        return Winner;
    }

    public GameObject GetLoser1()
    {
        return Loser1;
    }
    public GameObject GetLoser2()
    {
        return Loser2;
    }
    public GameObject GetLoser3()
    {
        return Loser3;
    }

    public void SetWinner(GameObject Win)
    {
        Winner = Win;
    }

    public void SetLoser1(GameObject Lose)
    {
        Loser1 = Lose;
    }
    public void SetLoser2(GameObject Lose)
    {
        Loser2 = Lose;
    }
    public void SetLoser3(GameObject Lose)
    {
        Loser3 = Lose;
    }

    public void SetScorces(int P1score, int P2score, int P3score, int P4score)
    {
        P1Score = P1score;
        P2Score = P2score;
        P3Score = P3score;
        P4Score = P4score;
    }

    private void UIUpdate()
    {
        if (Winner.tag == "P1")
        {
            WinnerText.text = "Player 1 Wins!";
            Loser1Text.text = "Player 2 Loses by " + (P1Score - P2Score) + " tiles";
            Loser2Text.text = "Player 3 Loses by " + (P1Score - P3Score) + " tiles";
            Loser3Text.text = "Player 4 Loses by " + (P1Score - P4Score) + " tiles";
        }
        else if (Winner.tag == "P2")
        {
            WinnerText.text = "Player 2 Wins!";
            Loser1Text.text = "Player 1 Loses by " + (P2Score - P1Score) + " tiles";
            Loser2Text.text = "Player 3 Loses by " + (P2Score - P3Score) + " tiles";
            Loser3Text.text = "Player 4 Loses by " + (P2Score - P4Score) + " tiles";

        }
        else if (Winner.tag == "P3")
        {
            WinnerText.text = "Player 3 Wins!";
            Loser1Text.text = "Player 1 Loses by " + (P3Score - P1Score) + " tiles";
            Loser2Text.text = "Player 2 Loses by " + (P3Score - P2Score) + " tiles";
            Loser3Text.text = "Player 3 Loses by " + (P3Score - P4Score) + " tiles";

        }
        else if (Winner.tag == "P4")
        {
            WinnerText.text = "Player 4 Wins!";
            Loser1Text.text = "Player 1 Loses by " + (P4Score - P1Score) + " tiles";
            Loser2Text.text = "Player 2 Loses by " + (P4Score - P2Score) + " tiles";
            Loser3Text.text = "Player 3 Loses by " + (P4Score - P3Score) + " tiles";

        }
    }

    public void ResetGame()
    {
        Destroy(gameObject);
    }

    public void PlayGame()
    {
        Destroy(gameObject);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);   
    }

    public void ReturnToMainMenu()
    {
        Destroy(gameObject);
        SceneManager.LoadScene(0);
    }

    IEnumerator WaitAndLoad()
    {
        yield return new WaitForSeconds(delayInSeconds);
        ReturnToMainMenu();
    }


    public void EndGame(GameObject Win, GameObject Lose1, GameObject Lose2, GameObject Lose3)
    {
        Winner = Win;
        Loser1 = Lose1;
        Loser2 = Lose2;
        Loser3 = Lose3;
        EndResult.enabled = true;
        WinnerText.enabled = true;
        Loser1Text.enabled = true;
        Loser2Text.enabled = true;
        Loser3Text.enabled = true;

        UIUpdate();
        //coroutine for 5 seconds 
        StartCoroutine(WaitAndLoad());
    }

    public void StartGame()
    {
        EndResult.enabled = false;
        WinnerText.enabled = false;
        Loser1Text.enabled = false;
        Loser2Text.enabled = false;
        Loser3Text.enabled = false;
    }

    public void QuitGame()
    {
        Application.Quit();

        Debug.Log("Quit");
    }

}
