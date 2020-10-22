using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject pauseCanvas;

    public Camera firstPersonCam;

    public Camera thirdPersonCam;

    public GameObject player;

    private PlayerController playerCont;

    public bool pause = false;

    private bool viewCam = false;

    public List<GameObject> obstacles;

    // Start is called before the first frame update
    void Start()
    {
        playerCont = player.GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        // Check if game needs to be paused
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.P))
        {
            Pause();
            pauseCanvas.SetActive(pause);
        }

        // Check if game over
        if (playerCont.dead)
        {
            SceneManager.LoadScene("FirstRun");
        }

        //Check if player has passed obstacle
        for (int i = 0; i < obstacles.Count; i++)
        {
            CheckObs(obstacles[i]);
        }
    }

    void CheckObs(GameObject obs)
    {
        if (obs.transform.position.z < player.transform.position.z)
        {
            obs.GetComponent<ObstacleScript>().SelfDestruct();
            obstacles.Remove(obs);
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
        playerCont.Save();
    }

    public void Load()
    {
        Debug.Log("Game Loaded");
        playerCont.Load();
    }

    public void SwitchCamera()
    {
        firstPersonCam.enabled = !viewCam;
        thirdPersonCam.enabled = viewCam;
        viewCam = !viewCam;
    }
}
