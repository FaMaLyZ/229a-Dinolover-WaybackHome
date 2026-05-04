using UnityEngine;
using System.Collections;
using UnityEngine.PlayerLoop;
using Unity.VisualScripting;

public class DinosuarJump : MonoBehaviour
{
    
    [Header("Jump Settings")]
    public float jumpHeight = 4f;       // ความสูงสูงสุดของ arc
    public float airTime = 1.2f;        // เวลาที่ลอยอยู่กลางอากาศ (วินาที)
    public float detectionRange = 8f;   // ระยะที่เริ่มกระโดดใส่ player
    public float jumpCooldown = 2f;     // หน่วงเวลาระหว่างแต่ละครั้ง
 
    [Header("State")]
    [SerializeField] private bool isJumping = false;
 
    private Rigidbody2D rb;
    private Transform playerPos;
    private float cooldownCounter = 0f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        playerPos = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    private void Update() 
    {
        if (cooldownCounter > 0f)
        {
            cooldownCounter -= Time.deltaTime;
        }

        if(!isJumping && cooldownCounter <= 0f)
        {
            float dist = Vector2.Distance(transform.position, playerPos.position);    
            if (dist <= detectionRange)
            {
                StartCoroutine(JumpAtPlayer());
            }
        }
        
    }

    IEnumerator JumpAtPlayer()
    {
        isJumping = true;;
        cooldownCounter = jumpCooldown;
        float gravity = Mathf.Abs(Physics2D.gravity.y * rb.gravityScale);
        float vy = Mathf.Sqrt(2f * gravity * jumpHeight);

        float dx = playerPos.position.x - transform.position.x;
        float vx = dx / airTime;

        rb.linearVelocity = new Vector2(vx, vy);

        yield return new WaitForSeconds(airTime + 0.2f);
        isJumping = false;
    }
}
