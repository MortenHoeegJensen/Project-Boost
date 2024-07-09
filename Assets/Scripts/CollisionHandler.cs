using System.IO.Compression;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    private void OnCollisionEnter(Collision other)
    {

        switch (other.gameObject.tag)
        {
            case "Friendly":
                Debug.Log("This is friendly");
                break;

            //case "Fuel":
            //    Debug.Log("You picked up fuel");
            //    break;

            case "Finish":
                Debug.Log("Gz you made it");
                LoadNextLevel();
                break;

            default:
                Debug.Log("Oh no, something bad happens");

                ReloadLevel();

                break;

        }
    }

    void ReloadLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);

    }

    void LoadNextLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;
        if (nextSceneIndex == SceneManager.sceneCountInBuildSettings)
        {
            nextSceneIndex = 0;
        }
        SceneManager.LoadScene(nextSceneIndex);


        SceneManager.LoadScene(currentSceneIndex + 1);

    }
}
