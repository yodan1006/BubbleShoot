using System;
using UnityEngine;

public class Bubble : MonoBehaviour
{
    public Action<GameObject> OnBubbleTouched;

    private void OnCollisionEnter(Collision collision)
    {
        NotyfieCollision();
    }

    private void NotyfieCollision()
    {
        OnBubbleTouched?.Invoke(gameObject);
    }
}
