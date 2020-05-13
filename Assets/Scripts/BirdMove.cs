using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdMove : MonoBehaviour
{
    PlayerController pcScript;
    public float speed = 4;
    float count = 0;
    // Start is called before the first frame update
    void Start()
    {
        GameObject Player = GameObject.Find("Sphere");
        pcScript = Player.GetComponent<PlayerController>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!pcScript.gameOver)
        {
            transform.Translate(Vector3.forward * speed * Time.deltaTime);
            if (count < 1)
            {
                transform.Translate(Vector3.up * 0.5f * Time.deltaTime);
                count += Time.deltaTime;
            }
            else
            {
                transform.Translate(Vector3.down * 0.5f * Time.deltaTime);
                count += Time.deltaTime;
            }
        }
        if (count >= 2) count = 0;
        Debug.Log(count);
        if (transform.position.z > 6) Destroy(gameObject);
    }
}
