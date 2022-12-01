using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenePersistance : MonoBehaviour
{
    int StartSceneIndex;

    //Keep the coins will not reset when in the samve level
    void Start()
    {
        int numberOfScenepereistance = FindObjectsOfType<ScenePersistance>().Length;
        print(numberOfScenepereistance);

        if (numberOfScenepereistance > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }






}
