using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpPad : MonoBehaviour
{
    // Start is called before the first frame update
    private float jumpPower = 400;

    private void OnCollisionEnter(Collision collision)
    {
        collision.rigidbody.AddForce(Vector3.up * jumpPower, ForceMode.Impulse);
    }

}
