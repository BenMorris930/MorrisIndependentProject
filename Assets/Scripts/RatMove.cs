using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RatMove : MonoBehaviour
{
    public float speed = 3.0f;
    private float count = 0;
    private float randomZ;
    PlayerController pcScript;
    // Start is called before the first frame update
    void Start()
    {
        randomZ = Random.Range(-1.0f, 1.0f);
        GameObject Player = GameObject.Find("Sphere");
        pcScript = Player.GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!pcScript.gameOver)
        {
            if (count < 2)
            {
                transform.Translate(new Vector3(randomZ, 0, Mathf.Sqrt(2)) * Time.deltaTime * speed);
                count += Time.deltaTime;
            }
            else transform.Translate(Vector3.forward * Time.deltaTime * speed);
        }
    }
}
