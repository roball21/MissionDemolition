using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOverScreen : MonoBehaviour
{
    public Button playAgainButton;
    public GameObject trophyImage;
    private int shotThreshold = 4;
    // Start is called before the first frame update
    void Start()
    {
        if (playAgainButton == null) {
            Debug.LogError("PlayAgain button is not assigned in the Inspector.");
        } else {
            playAgainButton.onClick.AddListener(PlayAgain);
            Debug.Log("PlayAgain button listener added.");
        }

        if (trophyImage == null) {
            Debug.LogError("TrophyImage is not assigned in the Inspector.");
        } else {
            Debug.Log("TrophyImage is assigned.");
            DisplayTrophy();
        }
        playAgainButton.gameObject.SetActive(true);
        playAgainButton.onClick.AddListener(PlayAgain);
        trophyImage.gameObject.SetActive(false);
        DisplayTrophy();
    }

    void DisplayTrophy() {
        try {
        int shotsTaken = MissionDemolition.GetShotsTaken();
        Debug.Log("Shots taken: " + shotsTaken);

        if (shotsTaken <= shotThreshold) {  // Correct the condition check
            trophyImage.SetActive(true);  // Show trophy if condition is met
            Debug.Log("Trophy displayed.");
        } else {
            trophyImage.SetActive(false);
            Debug.Log("Trophy hidden.");
        }
    } catch (System.Exception e) {
        Debug.LogError("Error while displaying trophy: " + e.Message);
    }
    }
    

    // Update is called once per frame
    public void PlayAgain() {
        SceneManager.LoadScene("_Scene_0", LoadSceneMode.Single);
        trophyImage.gameObject.SetActive(false);
    }
}
