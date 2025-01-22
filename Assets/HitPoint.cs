using System;
using UnityEngine;

public class HitPoint : MonoBehaviour
{
    public Action<GameObject> OnHit;
    private void OnTriggerEnter(Collider other)
    {
        OnHit?.Invoke(other.gameObject);
    }
}
