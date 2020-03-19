using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddGravity : MonoBehaviour
{
    [Range(0, 1000)] public float InitialForce;

    private void Start()
    {
        var randomDirection = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), Random.Range(-1f, 1f));

        GetComponent<Rigidbody>().AddForce(randomDirection * InitialForce);
    }

    //for collision with other objects
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Moveable"))
        {
            FindObjectOfType<AudioManager>().PlayAudio("objectCollision");
        }

    }
}
