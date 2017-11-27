using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelectionManager : MonoBehaviour {

    public GameObject noticePanel;

	// Use this for initialization
	void Start () {
		
	}

	// Update is called once per frame
	void Update () {
		
	}

    public void LoadLevelSelection() {
        SceneManager.LoadScene("LevelSelectionScene");
    }

    public void LoadTutorial() {
        SceneManager.LoadScene("Tutorial");
    }

    public void LoadPractice() {
        if (PlayerPrefs.GetInt("CompletedTutorial") == 0) {
            if (!noticePanel.activeSelf) { 
                noticePanel.SetActive(true);
                return;
            }
        }

        SceneManager.LoadScene("PracticeMode");
    }

    public void LoadEndless() {
        if (PlayerPrefs.GetInt("CompletedTutorial") == 0) {
            if (!noticePanel.activeSelf) {
                noticePanel.SetActive(true);
                return;
            }
        }

        SceneManager.LoadScene("EndlessMode");
    }

    public void LoadPracticeLevel(int level) {
        SceneManager.LoadScene("PracticeLevel" + level);
    }
}
