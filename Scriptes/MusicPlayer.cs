using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MusicPlayer : MonoBehaviour
{
    AudioSource audiosource;
    [SerializeField] AudioClip[] songs;
    int currentSceneIndex;

    void Start()
    {
        audiosource = GetComponent<AudioSource>();
        currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        if (currentSceneIndex == 0)
        {
            audiosource.clip = songs[0];
            audiosource.Play();
        }
        else
        {
            audiosource.clip = songs[1];
            audiosource.Play();
            DontDestroyOnLoad(gameObject);
        }
    }
}
