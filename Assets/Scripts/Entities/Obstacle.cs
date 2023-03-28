using UnityEngine;

public class Obstacle
{
    public float Distance { get; set; }
    public float AngleToHit { get; set; }
    public Collider Collider { get; set; }
    public LayerMask LayerMask { get; set; }

    public Obstacle(Collider _collider, float _Distance, float _angleToHit, LayerMask layerMask)
    {
        Collider = _collider;
        Distance = _Distance;
        AngleToHit = _angleToHit;
        LayerMask = layerMask;
    }

    public override string ToString()
    {
        return $"Obstacle: 'angle: {AngleToHit}' | 'distance: {Distance}'";
    }
}

