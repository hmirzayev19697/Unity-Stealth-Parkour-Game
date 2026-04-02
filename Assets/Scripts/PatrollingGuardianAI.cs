using UnityEngine;

public class PatrollingGuardianAI : MonoBehaviour
{
    public Transform pointA;
    public Transform pointB;
    public float speed = 3f;

    public Transform eyePoint;
    public float viewDistance = 8f;
    public float viewAngle = 90f;

    public PlayerController player;
    private Transform target;

    void Start()
    {
        target = pointB;
    }

    void Update()
    {
        Patrolling();
        DetectPlayer();
    }

    void Patrolling()
    {
        Vector3 direction = target.position - transform.position;

        if (direction.magnitude > 0.1f)
        {
            Vector3 moveDir = direction.normalized;
            transform.position += moveDir * speed * Time.deltaTime;
            Vector3 flatDir = new Vector3(moveDir.x, 0f, moveDir.z);

            // for smooth rotation
            if (flatDir != Vector3.zero)
            {
                Quaternion targetRotation = Quaternion.LookRotation(flatDir);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 3f * Time.deltaTime);
            }
        }
        else
        {
            target = (target == pointA) ? pointB : pointA;
        }
    }

    void DetectPlayer()
    {
        Vector3 toPlayer = player.transform.position - eyePoint.position;

        if (toPlayer.magnitude > viewDistance) return;

        float angle = Vector3.Angle(transform.forward, toPlayer);
        if (angle > viewAngle / 2f) return;

        RaycastHit hit;
        if (Physics.Raycast(eyePoint.position, toPlayer.normalized, out hit, viewDistance))
        {
            if (hit.collider.CompareTag("Player"))
            {
                player.RestartPlayer();
            }
        }
    }
}