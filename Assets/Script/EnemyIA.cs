using UnityEngine;

public class EnemyIA : MonoBehaviour
{
    public Transform target;
    [SerializeField] private float speed;
    
    private void Update()
    {
        if (target != null)
        {
            transform.position = Vector3.MoveTowards(transform.position, target.position, Time.deltaTime * speed);
            Vector3 dir = (target.position - transform.position).normalized;
            if(dir != Vector3.zero) transform.forward = dir;
        }
    }
}
