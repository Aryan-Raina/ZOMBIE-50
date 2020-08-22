using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
using System;

public class StartMenu : MonoBehaviour
{
    public AudioManager audioManager;
    public AudioSource startTheme;
    public void LoadGame()
    {
        SceneManager.LoadScene("PlayScene");
    }

    public void LoadStartMenu()
    {
        SceneManager.LoadScene("StartMenu");
    }

    public void QuitGame()
    {
        Debug.Log("Quit!");
        Application.Quit();
    }

    public void SetBackgroundVolume(float sliderValue)
    {
        Sound s = Array.Find(audioManager.sounds, sound => sound.name == "Theme");
        s.volume = sliderValue;
        startTheme.volume = sliderValue;
    }
}
