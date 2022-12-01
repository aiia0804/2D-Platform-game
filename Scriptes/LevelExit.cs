using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelExit : MonoBehaviour
{
    [SerializeField] bool final = false;
    private void OnTriggerEnter2D(Collider2D player)
    {
        var rightConditions = player.GetComponent<Player>();
        if (rightConditions && !final)
        {
            player.GetComponent<Animator>().SetBool("Entering Vortex", true);
            StartCoroutine(LevelActivited());
        }
        else
        {
            FindObjectOfType<GameSession>().WinTheGame();
        }

    }

    IEnumerator LevelActivited()
    {
        yield return new WaitForSeconds(1);
        LoadNextScene();
    }

    private static void LoadNextScene()
    {
        var currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex + 1);
        Destroy(FindObjectOfType<ScenePersistance>().gameObject);
    }

}
