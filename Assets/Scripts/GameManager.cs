using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject menuSet;
    public GameObject gameSet;
    public GameObject baseBlue;
    public GameObject baseRed;

    public GameObject vic;
    public GameObject defeat;
    public GameObject controlSet;
    public GameObject reset;

    public AudioSource bgmaudio;
    public SoundManager sound;

    public int level;
    public bool isOver;
    public bool isLive;

    public List<bool> clears;
    public List<GameObject> diflist;

    void Awake()
    {
        for (int i = 0; i < 3; i++)
        {
            if (PlayerPrefs.HasKey($"clear{i}") && PlayerPrefs.GetInt($"clear{i}") == 1)
                diflist[i].SetActive(true);

            else
                PlayerPrefs.SetInt($"clear{i}", 0);
        }
        
    }

    void Update()
    {
        QuitGame();
    }

    public void SetLevel(int lv)
    {
        level = lv;
        menuSet.SetActive(false);
        gameSet.SetActive(true);

        baseBlue.SetActive(true);
        baseRed.SetActive(true);

        bgmaudio.Play();
    }

    public void GameOver(string basename)
    {
        isOver = true;
        sound.Stop();

        if (basename == "Red")
        {
            sound.SFX(SoundManager.Sound.Victory);

            vic.SetActive(true);

            if (PlayerPrefs.GetInt($"clear{level}") != 1)
                PlayerPrefs.SetInt($"clear{level}", 1);
        }

        else
        {
            defeat.SetActive(true);

            sound.SFX(SoundManager.Sound.Defeat);
        }

        foreach (GameObject go in baseBlue.GetComponent<Bases>().unitList)
        {
            Units gounit = go.GetComponent<Units>();
            gounit.isOver = true;
            gounit._state = Units.State.Stop;
            gounit.StopAllCoroutines();
            gounit.enabled = false;
        }


        foreach (GameObject go in baseRed.GetComponent<Bases>().unitList)
        {
            Units gounit = go.GetComponent<Units>();
            gounit.isOver = true;
            gounit._state = Units.State.Stop;
            gounit.StopAllCoroutines();
            gounit.enabled = false;
        }


        baseRed.GetComponent<Bases>().StopAllCoroutines();
        controlSet.SetActive(false);
        reset.SetActive(true);
    }

    public void ResetGame()
    {
        SceneManager.LoadScene(0);
    }

    public void QuitGame()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            Application.Quit();
    }
}
