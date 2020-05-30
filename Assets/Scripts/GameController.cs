using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public GameObject Player;
    public Transform SpawnPoint;
    private Rigidbody rb;
    public GameObject[] ratPrefab = new GameObject[3];
    public GameObject birdPrefab;
    public int i = 0;
    PlayerController pcScript;
    public int waveNumber = 1;
    private int enemyCount;
    public float randRange = 5f;
    private int numberEnemies;
    private GameObject birdSpawn;
    private AudioSource audioSource;

    void Start()
    {

        InvokeRepeating("RatSpawn", 1, 1);
        GameObject Player = GameObject.Find("Sphere");
        pcScript = Player.GetComponent<PlayerController>();
        birdSpawn = GameObject.Find("BirdSpawn");
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        enemyCount = FindObjectsOfType<BirdMove>().Length;
        if (enemyCount == 0)
        {
            Debug.Log(waveNumber);
            BirdSpawn(waveNumber);
            waveNumber++;
        }
        if (pcScript.gameOver) audioSource.Stop();
    }

    void RatSpawn()
    {
        if (!pcScript.gameOver)
        {
            Instantiate(ratPrefab[i], ratPrefab[i].transform.position, ratPrefab[i].transform.rotation);
            i = (i + 1) % ratPrefab.Length;
        }
    }

    private Vector3 RandomSpawn()
    {
        float spawnX = Random.Range(-randRange, randRange);
        float spawnY = Random.Range(-randRange, randRange);
        return new Vector3(spawnX, spawnY, 0) + birdSpawn.transform.position;

    }

    void BirdSpawn(int numberEnemies)
    {
        for (int en = 0; en < numberEnemies; en++)
        {
            Vector3 spawnPosition = RandomSpawn();
            Debug.Log(spawnPosition);
            Instantiate(birdPrefab, spawnPosition, birdPrefab.transform.rotation);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Kill"))
        {
            //Player.transform.position = SpawnPoint.transform.position;
            pcScript.gameOver = true;
        }

    }
}
