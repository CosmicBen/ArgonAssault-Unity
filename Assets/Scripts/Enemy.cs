using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private GameObject deathVfx = null;
    [SerializeField] private GameObject hitVfx = null;
    [SerializeField] private int pointsPerHit = 100;
    [SerializeField] private int hitPoints = 4;

    private ScoreBoard scoreBoard = null;
    private GameObject parentGameObject = null;

    private void Start()
    {
        scoreBoard = FindObjectOfType<ScoreBoard>();
        parentGameObject = GameObject.FindGameObjectWithTag("SpawnAtRuntime");

        Rigidbody myRigidbody = gameObject.AddComponent<Rigidbody>();
        myRigidbody.useGravity = false;
    }

    private void OnParticleCollision(GameObject other)
    {
        ProcessHit();

        if (hitPoints <= 0)
        {
            KillEnemy();
        }
    }

    private void KillEnemy()
    {
        GameObject vfx = Instantiate(deathVfx, transform.position, transform.rotation);
        vfx.transform.SetParent(parentGameObject.transform);

        scoreBoard.IncreaseScore(pointsPerHit);
        Destroy(gameObject);
    }

    private void ProcessHit()
    {
        GameObject vfx = Instantiate(hitVfx, transform.position, transform.rotation);
        vfx.transform.SetParent(parentGameObject.transform);

        hitPoints--;
    }
}
