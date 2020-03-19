using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class objectCollision : MonoBehaviour
{

    private AudioSource audio;

    [SerializeField]
    private AudioClip objectSound;

    

    //private void Start()
    //{
    //    audio = GetComponent<AudioSource>();
    //}

    //private void OnCollisionEnter(Collision collision)
    //{
    //    //used 'untagged' currently because that is easier than setting all surfaces to a new tag
    //    if (collision.gameObject.CompareTag("Moveable") || collision.gameObject.CompareTag("Untagged"))
    //    {
    //        audio.PlayOneShot(objectSound);
    //    }

    //}
}
