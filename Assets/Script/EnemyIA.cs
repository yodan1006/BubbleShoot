using System;
using UnityEngine;

public class EnemyIA : MonoBehaviour
{
    public Transform target;
    [SerializeField] private float speed;
    
    public Action<GameObject> OnTouched;
    
    private void Update()
    {
        if (target != null)
        {
            transform.position = Vector3.MoveTowards(transform.position, target.position, Time.deltaTime * speed);
            Vector3 dir = (target.position - transform.position).normalized;
            if(dir != Vector3.zero) transform.forward = dir;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        NotyfieCollision();
    }

    private void NotyfieCollision()
    {
        OnTouched?.Invoke(gameObject);
    }
}
