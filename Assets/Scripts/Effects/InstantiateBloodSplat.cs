using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantiateBloodSplat : MonoBehaviour
{
    public GameObject[] BloodSplats;
    public GameObject SmallBloodSplat;

    private GameObject _bloodSplat;

    private void Start()
    {
        _bloodSplat = BloodSplats[Random.Range(0, BloodSplats.Length)];
    }

    private void SpawnBloodSplat(Vector3 point)
    {
        Instantiate(_bloodSplat, point, _bloodSplat.transform.rotation);
        Instantiate(SmallBloodSplat, transform.position, SmallBloodSplat.transform.rotation);
        Destroy(gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
        SpawnBloodSplat(collision.GetContact(0).point);
    }
}
