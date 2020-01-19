using System.Collections;
using System.Collections.Generic;
using DecalSystem;
using UnityEngine;

public class InstantiateBloodSplat : MonoBehaviour
{
    public static float Offset { get; set; }

    public Sprite[] BloodSplatSprites;
    public Material[] BloodSplatMaterials;
    public GameObject SmallBloodSplat;
    public GameObject DecalProjector;
    public LayerMask DecalLayerMask;

    private Sprite _bloodSplatSprite;
    private Material _bloodSplatMaterial;

    private void Start()
    {
        int randomNum = Random.Range(0, BloodSplatSprites.Length);
        _bloodSplatSprite = BloodSplatSprites[randomNum]; // Make sure in same order, corresponding to each other
        _bloodSplatMaterial = BloodSplatMaterials[randomNum];
    }

    private void SpawnBloodSplat(Vector3 point)
    {
        var decalProjector = Instantiate(DecalProjector, point, DecalProjector.transform.rotation).GetComponent<Decal>();
        decalProjector.Sprite = _bloodSplatSprite;
        decalProjector.Material = _bloodSplatMaterial;
        decalProjector.LayerMask = DecalLayerMask;
        decalProjector.Offset = 0.005f + Offset;
        Offset += 0.00001f;
        decalProjector.BuildAndSetDirty();

        Instantiate(SmallBloodSplat, transform.position, SmallBloodSplat.transform.rotation);
        Destroy(gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
        SpawnBloodSplat(collision.GetContact(0).point);
    }
}
