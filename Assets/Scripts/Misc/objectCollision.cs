using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class objectCollision : MonoBehaviour
{

    public AudioSource audio;

    private Rigidbody rb;
    private Vector3 vector;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        vector = rb.velocity;
    }

    private void OnCollisionEnter(Collision collision)
    {
        //used 'untagged' currently because that is easier than setting all surfaces to a new tag
        if (collision.gameObject.CompareTag("Moveable") || collision.gameObject.CompareTag("Untagged"))
        {
            audio.Play();
        }

    }
}
