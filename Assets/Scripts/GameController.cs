using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public GameObject Player;
    public Transform SpawnPoint;
    private Rigidbody rb;

    void Start()
    {
        rb = Player.GetComponent<Rigidbody>();
        
    }

    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Kill"))
        {
            Player.transform.position = SpawnPoint.transform.position;
        }

    }
}
