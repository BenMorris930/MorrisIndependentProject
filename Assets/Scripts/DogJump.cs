using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DogJump : MonoBehaviour
{
    Rigidbody dogRB;
    Animator dogAnimator;
    public float jumpHeight = 5;
    PlayerController pcScript;
    
    // Start is called before the first frame update
    void Start()
    {
        dogRB = GetComponent<Rigidbody>();
        dogAnimator = GetComponent<Animator>();
        InvokeRepeating("Jump", 1, 2);
        GameObject Player = GameObject.Find("Sphere");
        pcScript = Player.GetComponent<PlayerController>();
    }

    void Jump()
    {
        if (!pcScript.gameOver)
        {
            dogRB.AddForce(Vector3.up * jumpHeight);
            dogAnimator.SetTrigger("jump");
        }

    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
