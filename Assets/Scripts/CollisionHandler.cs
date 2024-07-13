using System.IO.Compression;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] float delayForRestartLevel = 1f;

    [SerializeField] float delayForNextLevel = 1f;

    [SerializeField] AudioSource audioSource;

    [SerializeField] AudioClip crashSound;

    [SerializeField] AudioClip finishSound;

    [SerializeField] ParticleSystem crashParticles;

    [SerializeField] ParticleSystem finishParticles;

    bool isTransitioning = false;
    bool collisionDisable = false;

    private void Update()
    {
        RespondToDebugKeys();
    }

    private void OnCollisionEnter(Collision other)
    {

        if (isTransitioning || collisionDisable) { return; }

        switch (other.gameObject.tag)
        {
            case "Friendly":
                Debug.Log("This is friendly");
                break;

            case "Finish":
                Debug.Log("Gz you made it");

                StartFinishSequence();
                //LoadNextLevel();
                break;

            default:
                Debug.Log("Oh no, something bad happens");

                StartCrashSequence();

                break;
        }
    }

    void StartFinishSequence()
    {
        audioSource.Stop();
        isTransitioning = true;
        audioSource.PlayOneShot(finishSound);
        finishParticles.Play();

        GetComponent<Movement>().enabled = false;
        Invoke("LoadNextLevel", delayForNextLevel);
    }

    void StartCrashSequence()
    {
        audioSource.Stop();
        isTransitioning = true;
        audioSource.PlayOneShot(crashSound);
        crashParticles.Play();
        GetComponent<Movement>().enabled = false;
        Invoke("ReloadLevel", delayForRestartLevel);

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
    }

    void RespondToDebugKeys()
    {
        if (Input.GetKey(KeyCode.L))
        {
            LoadNextLevel();
        }
        else if (Input.GetKey(KeyCode.C))
        {
            Debug.Log("C has been pushed");
            collisionDisable = !collisionDisable; // Toggle collision
        }
    }

    void RemoveCollision()
    {

    }
}
