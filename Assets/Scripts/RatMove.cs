using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RatMove : MonoBehaviour
{
    public float speed = 3.0f;
    private float count = 0;
    private float randomZ;
    // Start is called before the first frame update
    void Start()
    {
        randomZ = Random.Range(-1.0f, 1.0f);
    }

    // Update is called once per frame
    void Update()
    {
        
        if (count < 2)
        {
            transform.Translate(new Vector3(-Mathf.Sqrt(2), 0, randomZ) * Time.deltaTime * speed);
            count += Time.deltaTime;
        }
        else transform.Translate(Vector3.left * Time.deltaTime * speed);
    }
}
