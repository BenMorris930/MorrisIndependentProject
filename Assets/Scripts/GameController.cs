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

    void Start()
    {
        rb = Player.GetComponent<Rigidbody>();
        InvokeRepeating("RatSpawn", 1, 1);
        
    }

    void Update()
    {
        
    }

    void RatSpawn()
    {
        Instantiate(ratPrefab[i], ratPrefab[i].transform.position, ratPrefab[i].transform.rotation);
        i = (i + 1) % ratPrefab.Length;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Kill"))
        {
            Player.transform.position = SpawnPoint.transform.position;
        }

    }
}
