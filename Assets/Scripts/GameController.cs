using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public GameObject Player;
    public Transform SpawnPoint;
    private Rigidbody rb;
    public GameObject[] ratPrefab = new GameObject[3];
    public int i = 0;
    PlayerController pcScript;

    void Start()
    {

        InvokeRepeating("RatSpawn", 1, 1);
        GameObject Player = GameObject.Find("Sphere");
        pcScript = Player.GetComponent<PlayerController>();
    }

    void Update()
    {
        
    }

    void RatSpawn()
    {
        if (!pcScript.gameOver)
        {
            Instantiate(ratPrefab[i], ratPrefab[i].transform.position, ratPrefab[i].transform.rotation);
            i = (i + 1) % ratPrefab.Length;
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
