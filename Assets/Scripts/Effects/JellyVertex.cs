// ADAPTED FROM: https://www.youtube.com/watch?v=UxLJ6XewTVs

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JellyVertex
{
    public int VertexIndex { get; set; }
    public Vector3 InitialVertexPosition { get; set; }
    public Vector3 CurrentVertexPosition { get; set; }
    public Vector3 CurrentVelocity { get; set; }

    public JellyVertex(int vertexIndex, Vector3 initialVertexPosition, Vector3 currentVertexPosition, Vector3 currentVelocity)
    {
        VertexIndex = vertexIndex;
        InitialVertexPosition = initialVertexPosition;
        CurrentVertexPosition = currentVertexPosition;
        CurrentVelocity = currentVelocity;
    }

    public Vector3 GetCurrentDisplacement()
    {
        return CurrentVertexPosition - InitialVertexPosition;
    }

    public void UpdateVelocity(float bounceSpeed)
    {
        CurrentVelocity -= GetCurrentDisplacement() * bounceSpeed * Time.deltaTime;
    }

    public void Settle(float stiffness)
    {
        CurrentVelocity *= 1f - stiffness * Time.deltaTime;
    }

    public void ApplyPressureToVertex(Transform transform, Vector3 position, float pressure)
    {
        Vector3 distanceVertexPoint = CurrentVertexPosition - transform.InverseTransformPoint(position);
        float adaptedPressure = pressure / (1f + distanceVertexPoint.sqrMagnitude);
        float velocity = adaptedPressure * Time.deltaTime;
        CurrentVelocity += distanceVertexPoint.normalized * velocity;
    }
}
