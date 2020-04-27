using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DogRun : MonoBehaviour
{
    PlayerController pcScript;
    // Start is called before the first frame update
    void Start()
    {
        GameObject Player = GameObject.Find("Sphere");
        pcScript = Player.GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        if(!pcScript.gameOver) transform.Rotate(0, 60 * Time.deltaTime, 0);
    }
}
