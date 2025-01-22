using UnityEngine;

public class ShootBubble : MonoBehaviour
{
    public PoolMunition poolMunition;
    public Transform shootPoint;
    public Transform Canon;
    public float shootForce = 10f;
    
    private Camera mainCamera;
    
    public int missileCount = 1;
    [SerializeField] float spreadAngle = 30f;
    [SerializeField] float missileSpeed = 10f;
    
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
        float angleStep = spreadAngle / (missileCount - 1); 
        float startAngle = -spreadAngle / 2 ;
        if (missileCount > 1)
        {
            for (int i = 0; i < missileCount; i++)
            {
                GameObject bubble = poolMunition.GetBubble();

                bubble.transform.position = shootPoint.position;

                Rigidbody rb = bubble.GetComponent<Rigidbody>();
                if (rb != null)
                {
                    float angle = startAngle + angleStep * i;
                    Vector3 dir = Quaternion.Euler(0, angle, 0) * shootPoint.forward;

                    bubble.transform.position = transform.position;
                    rb.AddForce(dir * shootForce, ForceMode.Impulse);
                }
            }
        }
        else
        {
            GameObject bubble = poolMunition.GetBubble();
            bubble.transform.position = shootPoint.position;
            Rigidbody rb = bubble.GetComponent<Rigidbody>();
            
            rb.AddForce(shootPoint.forward * shootForce, ForceMode.Impulse);
        }
    }
}

