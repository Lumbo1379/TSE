using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractE : MonoBehaviour
{
    [Range(0, 100)] public float InteractRange;
    public Camera Camera;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
            ShootRay();
    }

    private void ShootRay()
    {
        RaycastHit hit;
        Ray ray = Camera.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit))
        {
            if (hit.transform.CompareTag("Interactable") || hit.transform.CompareTag("Reflectable"))
                hit.transform.gameObject.GetComponent<IInteractable>().Interact();
        }
    }
}
