using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum DoubleSlidingDoorStatus 
{
	Closed,
	Open,
	Animating
}

public class DoubleSlidingDoorController : MonoBehaviour 
{

    public bool Open { get; set; }

	private DoubleSlidingDoorStatus status = DoubleSlidingDoorStatus.Closed;

	[SerializeField]
	private Transform halfDoorLeftTransform;
	[SerializeField]
	public Transform halfDoorRightTransform;

	[SerializeField]
	private float slideDistance	= 0.88f;

	private Vector3 leftDoorClosedPosition;
	private Vector3 leftDoorOpenPosition;

	private Vector3 rightDoorClosedPosition;
	private Vector3 rightDoorOpenPosition;

	[SerializeField]
	private float speed = 1f;

	private int objectsOnDoorArea	= 0;


	//	Sound Fx
	[SerializeField]
	private AudioClip doorOpeningSoundClip;
	[SerializeField]
	public AudioClip doorClosingSoundClip;

	private AudioSource audioSource;


	// Use this for initialization
	void Start ()
    {
        Open = false;

		leftDoorClosedPosition	= new Vector3 (halfDoorLeftTransform.transform.localPosition.x, halfDoorLeftTransform.transform.localPosition.y, halfDoorLeftTransform.transform.localPosition.z);
		leftDoorOpenPosition	= new Vector3 (halfDoorLeftTransform.transform.localPosition.x, halfDoorLeftTransform.transform.localPosition.y, halfDoorLeftTransform.transform.localPosition.z + slideDistance);

		rightDoorClosedPosition	= new Vector3 (halfDoorRightTransform.transform.localPosition.x, halfDoorRightTransform.transform.localPosition.y, halfDoorRightTransform.transform.localPosition.z);
		rightDoorOpenPosition	= new Vector3 (halfDoorRightTransform.transform.localPosition.x, halfDoorRightTransform.transform.localPosition.y, halfDoorRightTransform.transform.localPosition.z - slideDistance);

		audioSource = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
        if (Open && status == DoubleSlidingDoorStatus.Closed)
            StartCoroutine("OpenDoors");
    }

    void OnTriggerStay(Collider other) {
		
	}

	void OnTriggerExit(Collider other) {
		//	Keep tracking of objects on the door
		if (other.GetComponent<Collider>().gameObject.layer == LayerMask.NameToLayer ("Characters")) {
			objectsOnDoorArea--;
		}
	}

	IEnumerator OpenDoors () {

		if (doorOpeningSoundClip != null) {
			audioSource.PlayOneShot (doorOpeningSoundClip, 0.7F);
		}

		status = DoubleSlidingDoorStatus.Animating;

		float t = 0f;

		while (t < 1f) {
			t += Time.deltaTime * speed;
		
			halfDoorLeftTransform.localPosition = Vector3.MoveTowards(halfDoorLeftTransform.transform.localPosition, leftDoorOpenPosition, speed * Time.deltaTime);
            halfDoorRightTransform.localPosition = Vector3.MoveTowards(halfDoorRightTransform.transform.localPosition, rightDoorOpenPosition, speed * Time.deltaTime);

            yield return null;
		}

		status = DoubleSlidingDoorStatus.Open;

	}

	IEnumerator CloseDoors () {

		if (doorClosingSoundClip != null) {
			audioSource.PlayOneShot(doorClosingSoundClip, 0.7F);
		}

		status = DoubleSlidingDoorStatus.Animating;

		float t = 0f;

		while (t < 1f) {
			t += Time.deltaTime * speed;

			halfDoorLeftTransform.localPosition = Vector3.Slerp(leftDoorOpenPosition, leftDoorClosedPosition, t);
			halfDoorRightTransform.localPosition = Vector3.Slerp(rightDoorOpenPosition, rightDoorClosedPosition, t);

			yield return null;
		}

		status = DoubleSlidingDoorStatus.Closed;

	}

	//	Forced door opening
	public bool DoOpenDoor () {

		if (status != DoubleSlidingDoorStatus.Animating) {
			if (status == DoubleSlidingDoorStatus.Closed) {
				StartCoroutine ("OpenDoors");
				return true;
			}
		}

		return false;
	}

	//	Forced door closing
	public bool DoCloseDoor () {

		if (status != DoubleSlidingDoorStatus.Animating) {
			if (status == DoubleSlidingDoorStatus.Open) {
				StartCoroutine ("CloseDoors");
				return true;
			}
		}

		return false;
	}
}
