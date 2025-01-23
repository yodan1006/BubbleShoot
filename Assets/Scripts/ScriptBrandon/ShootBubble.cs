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

    public float clickspeed = 1f;
    private float lastClickTime;
    
    private void Awake()
    {
        mainCamera = Camera.main;
    }

    private void Update()
    {
        MoveCanon();
        if (Input.GetMouseButtonDown(0) && Time.time > lastClickTime + clickspeed )
        {
            Bubbleshoot();
            lastClickTime = Time.time;
        }
    }

    private void MoveCanon()
    {
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity))
        {
            Vector3 targetDir = hit.point - Canon.position; // Direction entre le canon et le point touché
            //targetDir.y = 0; // Si tu veux que le canon ne se déplace qu'horizontalement

            Debug.DrawLine(Canon.position, hit.point, Color.yellow);

            if (targetDir != Vector3.zero)
            {
                Quaternion targetRotation = Quaternion.LookRotation(targetDir);
                Canon.rotation = targetRotation;
            }
        }
    }

    private void Bubbleshoot()
    {
        float angleStep = spreadAngle / (missileCount - 1); 
        float startAngle = -spreadAngle / 2;

        if (missileCount > 1)
        {
            for (int i = 0; i < missileCount; i++)
            {
                GameObject bubble = poolMunition.GetBubble(); // Récupère une nouvelle bulle pour chaque tir
                Rigidbody rb = bubble.GetComponent<Rigidbody>();
                if (bubble != null)
                {
                    bubble.transform.position = shootPoint.position;

                    if (rb != null)
                    {
                        rb.linearVelocity = Vector3.zero; // Réinitialise la vitesse
                        float angle = startAngle + angleStep * i;
                        Vector3 dir = Quaternion.Euler(0, angle, 0) * shootPoint.forward;
                        rb.AddForce(dir * shootForce, ForceMode.Impulse);
                    }
                }
            }
        }
        else
        {
            GameObject bubble = poolMunition.GetBubble();
            Rigidbody rb = bubble.GetComponent<Rigidbody>();
            bubble.transform.position = shootPoint.position;
            rb.linearVelocity = Vector3.zero;
            Vector3 dir = Quaternion.Euler(0, 1, 0) * shootPoint.forward;
            rb.AddForce(dir * shootForce, ForceMode.Impulse);
        }
    }
}

