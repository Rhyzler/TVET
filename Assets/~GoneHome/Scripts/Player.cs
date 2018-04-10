using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public string message = "Be Gone THOT!";
    public Rigidbody rigid;

    float speed = 5;

    /*
     [] - Brackets
     {} - Braces
     () - Parentheses

    Hot Keys:
         - Clean Code: CRTL + K + D
         - Fold Code: CTRL + M + O
         - Un-Fold: CTRL + M + P
     */
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.W))
        {
            rigid.AddForce(Vector3.forward * speed);
        }
    }
}
