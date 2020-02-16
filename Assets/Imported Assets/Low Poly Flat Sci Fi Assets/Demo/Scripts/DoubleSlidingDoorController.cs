using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum DoubleSlidingDoorStatus {
	Closed,
	Open,
	Animating
}

public class DoubleSlidingDoorController : MonoBehaviour {

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
	void Start () {
		leftDoorClosedPosition	= new Vector3 (0f, halfDoorLeftTransform.transform.localPosition.y, 0f);
		leftDoorOpenPosition	= new Vector3 (0f, halfDoorLeftTransform.transform.localPosition.y, slideDistance);

		rightDoorClosedPosition	= new Vector3 (0f, halfDoorRightTransform.transform.localPosition.y, 0f);
		rightDoorOpenPosition	= new Vector3 (0f, halfDoorRightTransform.transform.localPosition.y, -slideDistance);

		audioSource = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
		if (status != DoubleSlidingDoorStatus.Animating) {
			if (status == DoubleSlidingDoorStatus.Open) {
				if (objectsOnDoorArea == 0) {
					StartCoroutine ("CloseDoors");
				}
			}
		}
	}

	void OnTriggerEnter(Collider other) {
		
		if (status != DoubleSlidingDoorStatus.Animating) {
			if (status == DoubleSlidingDoorStatus.Closed) {
				StartCoroutine ("OpenDoors");
			}
		}

		if (other.GetComponent<Collider>().gameObject.layer == LayerMask.NameToLayer ("Characters")) {
			objectsOnDoorArea++;
		}
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
		
			halfDoorLeftTransform.localPosition = Vector3.Slerp(leftDoorClosedPosition, leftDoorOpenPosition, t);
			halfDoorRightTransform.localPosition = Vector3.Slerp(rightDoorClosedPosition, rightDoorOpenPosition, t);

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
