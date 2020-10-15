using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject canvas;

    private PlayerController player;

    public bool pause = false;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.P))
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
            canvas.SetActive(!pause);
            pause = !pause;
        }
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

    /*
    void showPauseMenu()
    {
        canvas.SetActive(!pause);
    }

    void hidePauseMenu()
    {
        canvas.SetActive(!pause);
    }
    */
}
