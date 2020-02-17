using UnityEngine;

public class TurretNRV : MonoBehaviour
{
    private Transform player;
    [SerializeField]
    private float maxFocusRange = 40f;
    [SerializeField]
    private float strength = 20;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void LateUpdate()
    {
        float distance = Vector3.Distance(player.position, transform.position);
        if (distance < maxFocusRange)
        {
            Quaternion targetRotation = Quaternion.LookRotation(player.position - transform.position);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Mathf.Min(strength * Time.deltaTime, 1));
        }
    }
}
