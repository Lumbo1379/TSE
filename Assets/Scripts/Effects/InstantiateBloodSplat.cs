using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.HDPipeline;

public class InstantiateBloodSplat : MonoBehaviour
{
    public static float Offset { get; set; }

    public Sprite[] BloodSplatSprites;
    public Material[] BloodSplatMaterials;
    public GameObject SmallBloodSplat;
    public GameObject DecalProjector;

    private Material _bloodSplatMaterial;

    private void Start()
    {
        int randomNum = Random.Range(0, BloodSplatSprites.Length);
        _bloodSplatMaterial = BloodSplatMaterials[randomNum];
    }

    private void SpawnBloodSplat(Vector3 point)
    {
        FindObjectOfType<AudioManager>().PlayAudio("bloodSplat");
        
        var decalProjector = Instantiate(DecalProjector, point, DecalProjector.transform.rotation).GetComponent<DecalProjectorComponent>();
        decalProjector.material = _bloodSplatMaterial;

        Instantiate(SmallBloodSplat, transform.position, SmallBloodSplat.transform.rotation);
        Destroy(gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
        SpawnBloodSplat(collision.GetContact(0).point);
    }
}
