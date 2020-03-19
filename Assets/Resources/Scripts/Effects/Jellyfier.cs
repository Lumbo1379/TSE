// ADAPTED FROM: https://www.youtube.com/watch?v=UxLJ6XewTVs

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jellyfier : MonoBehaviour
{
    public float BounceSpeed;
    public float Pressure;
    public float Stiffness;

    private MeshFilter _meshFilter;
    private Mesh _mesh;
    private JellyVertex[] _jellyVertices;
    private Vector3[] _currentMeshVertices;
    private RaycastHit _hit;
    private Ray _clickRay;

    private void Start()
    {
        _meshFilter = GetComponent<MeshFilter>();
        _mesh = _meshFilter.mesh;

        _jellyVertices = new JellyVertex[_mesh.vertices.Length];
        _currentMeshVertices = new Vector3[_mesh.vertices.Length];

        GetVertices();

        ApplyPressureToPoint(transform.position);
    }

    private void Update()
    {
        UpdateVertices();
        UpdatePressurePoint(transform.position);
    }

    private void GetVertices()
    {
        for (int v = 0; v < _mesh.vertices.Length; v++)
        {
            _jellyVertices[v] = new JellyVertex(v, _mesh.vertices[v], _mesh.vertices[v], Vector3.zero);
            _currentMeshVertices[v] = _mesh.vertices[v];
        }
    }

    private void UpdateVertices()
    {
        for (int v = 0; v < _jellyVertices.Length; v++)
        {
            _jellyVertices[v].UpdateVelocity(BounceSpeed);
            _jellyVertices[v].Settle(Stiffness);

            _jellyVertices[v].CurrentVertexPosition += _jellyVertices[v].CurrentVelocity * Time.deltaTime;
            _currentMeshVertices[v] = _jellyVertices[v].CurrentVertexPosition;
        }

        _mesh.vertices = _currentMeshVertices;
        _mesh.RecalculateBounds();
        _mesh.RecalculateNormals();
        _mesh.RecalculateTangents();
    }

    private void ApplyPressureToPoint(Vector3 point)
    {
        foreach (var v in _jellyVertices)
            v.ApplyPressureToVertex(transform, point, Pressure);
    }

    private void UpdatePressurePoint(Vector3 point)
    {
        foreach (var v in _jellyVertices)
            v.ApplyPressureToVertex(transform, point, 0);
    }
}
