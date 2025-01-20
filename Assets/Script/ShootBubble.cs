using System;
using UnityEngine;

public class ShootBubble : MonoBehaviour
{
    public PoolMunition poolMunition;
    public Transform shootPoint;
    public float shootForce = 10f;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Bubbleshoot();
        }
    }

    private void Bubbleshoot()
    {
        GameObject bubble = poolMunition.GetBubble();
        
        bubble.transform.position = shootPoint.position;
        
        Rigidbody rb = bubble.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.linearVelocity = Vector3.zero;
            
            rb.AddForce(shootPoint.forward * shootForce, ForceMode.Impulse);
        }
    }
}
