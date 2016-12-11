using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class UIManager : MonoBehaviour {

    public Text killCountText, timeText;

    public GameObject pausePanel;

    public GameObject deathPanel;





    System.Diagnostics.Stopwatch stopwatch = new System.Diagnostics.Stopwatch();

        
        // Do operations
      




    private void UpdateKillCountText() {

        killCountText.text = (int.Parse(killCountText.text) + 1).ToString();

    }


    private void ShowDeathScreen() {

        deathPanel.SetActive(true);
        stopwatch.Stop();
    }




    public void RestartGame() {

        SceneManager.LoadScene("MainLevel");

    }


    public void PauseGame() {
        if (!pausePanel.activeSelf) {
            pausePanel.SetActive(true);
            Time.timeScale = 0;
            stopwatch.Stop();
        }

        else {
            pausePanel.SetActive(false);
            Time.timeScale = 1;
            stopwatch.Start();
        }
    }

    public void QuitGame() {
        Application.Quit();
    }




    void OnDisable() {
        GameManager.Instance.EnemyWasKilled -= UpdateKillCountText;
        GameManager.Instance.PlayerDied -= ShowDeathScreen;
    }


	
	void Start () {
        stopwatch.Start();
        GameManager.Instance.PlayerDied += ShowDeathScreen;
        GameManager.Instance.EnemyWasKilled += UpdateKillCountText;

	}
	
	
	void Update () {

        timeText.text = stopwatch.Elapsed.ToString();


        if (Input.GetKeyDown(KeyCode.Escape))
            PauseGame();
    }
}
