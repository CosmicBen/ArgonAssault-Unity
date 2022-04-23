using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] private float loadDelay = 1.0f;
    [SerializeField] private ParticleSystem explosionVfx = null;
    [SerializeField] private AudioSource explosionSfx = null;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("CRASH: " + other.name);
        StartCoroutine(StartCrashSequence());
    }

    private IEnumerator StartCrashSequence()
    {
        GetComponent<PlayerControls>().enabled = false;
        explosionVfx.Play();
        explosionSfx.Play();
        GetComponent<MeshRenderer>().enabled = false;
        GetComponent<BoxCollider>().enabled = false;

        yield return new WaitForSeconds(loadDelay);

        ReloadLevel();
    }

    private void ReloadLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }
}
