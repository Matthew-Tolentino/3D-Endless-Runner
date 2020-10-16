using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject pauseCanvas;

    public Camera firstPersonCam;

    public Camera thirdPersonCam;

    private PlayerController player;

    public bool pause = false;

    private bool viewCam = false;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        // Check if game needs to be paused
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.P))
        {
            Pause();
            pauseCanvas.SetActive(!pause);
        }

        // Check if game over
        if (player.dead)
        {
            SceneManager.LoadScene("FirstRun");
        }
    }

    void Pause()
    {
        switch (pause)
        {
            case false:
                pauseGame();
                //showPauseMenu();
                break;
            case true:
                resumeGame();
                //hidePauseMenu();
                break;
        }
        pause = !pause;
    }

    void pauseGame()
    {
        Time.timeScale = 0;
    }

    void resumeGame()
    {
        Time.timeScale = 1;
    }

    public void Save()
    {
        Debug.Log("Game Saved!");
        player.Save();
    }

    public void Load()
    {
        Debug.Log("Game Loaded");
        player.Load();
    }

    public void SwitchCamera()
    {
        firstPersonCam.enabled = !viewCam;
        thirdPersonCam.enabled = viewCam;
        viewCam = !viewCam;
    }
}
