using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public enum GameMode {
    idle,
    playing,
    levelEnd
}

public class MissionDemolition : MonoBehaviour
{
    static private MissionDemolition S; // a private Singleton

    [Header("Inscribed")]
    public Text uitLevel; // The UIText_Level Text
    public Text uitShots; // The UIText_Shots Text
    public Vector3 castlePos; // The place to put castles
    public GameObject[] castles; // An array of the castles

    [Header("Dynamic")]
    public int level; // The current level
    public int levelMax; // The number of levels
    public int shotsTaken;
    public GameObject castle; // The current castle
    public GameMode mode = GameMode.idle;
    public string showing = "Show Slingshot"; // FollowCam mode

    // Start is called before the first frame update
    void Start()
    {
        if (SceneManager.GetActiveScene().name != "_Scene_0") {
            SceneManager.LoadScene("_Scene_0", LoadSceneMode.Single);
        }
        
        S = this; 
        level = 0;
        shotsTaken = 0;
        levelMax = castles.Length;
        StartLevel();
        
    }

    void StartLevel() {
    
        if (castle != null) {
            Destroy(castle);
        }

   
        Projectile.DESTROY_PROJECTILES();

    
        if (level >= 0 && level < castles.Length) {
            // Instantiate the new castle
            castle = Instantiate<GameObject>(castles[level]);
            castle.transform.position = castlePos;
        } else {
            Debug.LogError("Level index out of bounds: " + level);
        }

    
        Goal.goalMet = false;

        UpdateGUI();

        mode = GameMode.playing;

        FollowCam.SWITCH_VIEW(FollowCam.eView.both);
}

    void UpdateGUI() {
        
        if (uitLevel != null && uitShots != null) {
            uitLevel.text = "Level: " + (level + 1) + " of " + levelMax;
            uitShots.text = "Shots Taken: " + shotsTaken;
        } else {
            Debug.LogError("UI Text components are not assigned!");
        }

        Debug.Log("Level: " + level);
        Debug.Log("Level Max: " + levelMax);
        Debug.Log("Shots Taken: " + shotsTaken);
}

    // Update is called once per frame
    void Update()
    {
        UpdateGUI();

        // Check for level end
        if((mode == GameMode.playing) && Goal.goalMet) {
            // Change mode to stop checking for level end
            mode = GameMode.levelEnd;
            // Zoom out to show both
            FollowCam.SWITCH_VIEW(FollowCam.eView.both);

            // Start the next level in 2 seconds
            Invoke("NextLevel", 2f);  
        }
    }

    void NextLevel() {
        level++;
        if(level == levelMax) {
            SceneManager.LoadScene("GameOver", LoadSceneMode.Single);
        } else {
            StartLevel();
        }
    }

    // Static method that allows code anywhere to increment shotsTaken
    static public void SHOT_FIRED() {
        S.shotsTaken++;
    }

    // Static method that allows code anywhere to get a reference to S.castle
    static public GameObject GET_CASTLE() {
        return S.castle;
    }

    public static int GetShotsTaken() {
        return S.shotsTaken;
    }
}
