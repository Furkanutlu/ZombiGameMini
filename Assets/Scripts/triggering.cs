using System;
using UnityEngine;

public class ZombieAI : MonoBehaviour
{
    private Transform player; // Oyuncu pozisyonu
    private Animator animator;
    public float runRange = 15f; // Kovalamaca mesafesi
    public float attackRange = 1.5f; // Saldırı mesafesi
    public int damage = 25; // Zombinin verdiği hasar miktarı
    public float damageCooldown = 1.0f; // Hasar verme aralığı (saniye)
    private float nextDamageTime = 0f; // Sonraki hasar zamanı
    private bool isAttacking = false;

    [Header("Agro Radius")]
    public SphereCollider agroRadiusCollider;
    public float agroRadius = 20f;

    void Start()
    {
        animator = GetComponent<Animator>();
        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
        if (playerObject != null)
        {
            player = playerObject.transform;
        }

        // Agro Radius için Collider ayarla
        if (agroRadiusCollider == null)
        {
            agroRadiusCollider = gameObject.AddComponent<SphereCollider>();
            agroRadiusCollider.isTrigger = true;
            agroRadiusCollider.radius = agroRadius;
        }
    }

    void Update()
    {
        if (player == null) return;

        float distanceToPlayer = Vector3.Distance(player.position, transform.position);

        if (distanceToPlayer <= attackRange)
        {
            if (!isAttacking && Time.time >= nextDamageTime)
            {
                AttackPlayer();
            }
        }
        else if (distanceToPlayer <= runRange)
        {
            isAttacking = false;
            ChasePlayer();
        }
        else
        {
            isAttacking = false;
            animator.SetBool("isRunning", false);
            animator.SetBool("isWalking", false);
            animator.SetBool("Z_Walk", true);
        }
    }

    void ChasePlayer()
    {
        // Oyuncuyu kovala
        animator.SetBool("isRunning", true);
        animator.SetBool("isWalking", false);
        animator.SetBool("Z_Walk", false);

        Vector3 targetPosition = new Vector3(player.position.x, transform.position.y, player.position.z);
        transform.LookAt(targetPosition);
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, Time.deltaTime * 3f);
    }

    void AttackPlayer()
    {
        // Oyuncuya saldır
        isAttacking = true;
        animator.SetBool("isRunning", false);
        animator.SetTrigger("Z_Attack");
        nextDamageTime = Time.time + damageCooldown; // Saldırı sonrası bekleme süresi
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player") && isAttacking && Time.time >= nextDamageTime)
        {
            PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damage); // Belirlenen miktarda hasar uygula
                nextDamageTime = Time.time + damageCooldown; // Hasar verme bekleme süresi
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player algılandı, kovalamaya başla!");
        }
    }

    public void EndAttack()
    {
        isAttacking = false;
    }
}
