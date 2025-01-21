using System;
using UnityEngine;

public class ShootBubble : MonoBehaviour
{
    public PoolMunition poolMunition;
    public Transform shootPoint;
    public Transform Canon;
    public float shootForce = 10f;
    
    private Camera mainCamera;

    private void Awake()
    {
        mainCamera = Camera.main;
    }

    private void Update()
    {
        MoveCanon();
        if (Input.GetMouseButtonDown(0))
        {
            Bubbleshoot();
        }
    }

    private void MoveCanon()
    {
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit,Mathf.Infinity))
        {
            Vector3 targetDir = hit.point;
            Vector3 dir = (targetDir - Canon.position).normalized;
            
            Quaternion targetRotation = Quaternion.LookRotation(dir);
            Canon.rotation = Quaternion.Euler(targetRotation.eulerAngles.x, targetRotation.eulerAngles.y, 0);
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
