using System;
using UnityEngine;

public class Bubble : MonoBehaviour
{
    public Action<GameObject> OnBubbleTouched;

    private void OnTriggerEnter(Collider other)
    {
        NotyfieCollision();
    }

    private void NotyfieCollision()
    {
        OnBubbleTouched?.Invoke(gameObject);
    }
}
