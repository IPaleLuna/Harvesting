using System;
using UnityEngine;

[Serializable]
public class CheckSphere
{
    [SerializeField]
    private float _radius;
    [SerializeField]
    private LayerMask _layerMask;

    public float radius => _radius;

    public bool Check(Vector3 pos, out Transform obj)
    {
        Collider2D collider = Physics2D.OverlapCircle(pos, _radius, _layerMask);
        
        if (collider) obj = collider.gameObject.transform;
        else obj = null;

        return collider != null;
    }

    public bool TryGetCenterOfObjects(Vector3 pos, out Vector3 center)
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(pos, _radius, _layerMask);

        bool isCollide = colliders.Length > 0;
        center = Vector3.zero;

        if(isCollide)
        {
            for(int i = 0; i < colliders.Length; i++)
                center += colliders[i].transform.position;

            center /= colliders.Length;
        }

        return isCollide;
    }
}
