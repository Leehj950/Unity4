using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePad : MonoBehaviour
{
    public int Speed;

    Rigidbody rigidbody;


    // Update is called once per frame
    void Update()
    {
        rigidbody = GetComponent<Rigidbody>();    
    }

    void Move(float direction)
    {
        rigidbody.velocity *= direction;
    }

}
