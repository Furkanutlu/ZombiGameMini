using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 5f;
    public float rotationSpeed = 720f; // Dönüş hızı (derece/saniye)
    public Transform groundCheck;
    public float groundCheckRadius = 0.4f;
    public LayerMask groundLayer;

    [Header("Jump Settings")]
    public float jumpForce = 2f;
    public float gravity = -9.81f;

    private CharacterController controller;
    private Vector3 velocity;
    private bool isGrounded;
    private Animator animator;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();

        if (controller == null)
            Debug.LogError("CharacterController bileşeni eksik!");
        if (animator == null)
            Debug.LogError("Animator bileşeni eksik!");
    }

    void Update()
    {
        HandleMovement();
        HandleJump();
        ApplyGravity();
    }

    void HandleMovement()
    {
        // Giriş değerlerini al
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        Vector3 direction = new Vector3(horizontal, 0, vertical).normalized;

        // Hareket varsa
        if (direction.magnitude >= 0.1f)
        {
            // Yönü hesapla
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref rotationSpeed, 0.1f);
            transform.rotation = Quaternion.Euler(0, angle, 0);

            // Hareket vektörü
            Vector3 moveDirection = Quaternion.Euler(0, targetAngle, 0) * Vector3.forward;
            controller.Move(moveDirection * moveSpeed * Time.deltaTime);

            // Animasyon
            if (animator != null)
            {
                animator.SetFloat("Speed", 1); // Hareket animasyonu tetikle
            }
        }
        else
        {
            // Hareket yokken animasyonu sıfırla
            if (animator != null)
            {
                animator.SetFloat("Speed", 0);
            }
        }
    }

    void HandleJump()
    {
        // Zemin kontrolü
        isGrounded = Physics.CheckSphere(groundCheck.position, groundCheckRadius, groundLayer);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f; // Zeminle temas halindeyken düşüş hızını sıfırla
        }

        // Zıplama
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpForce * -2f * gravity);

            // Zıplama animasyonu
            if (animator != null)
            {
                animator.SetTrigger("Jump");
            }
        }
    }

    void ApplyGravity()
    {
        // Yerçekimi
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }

    private void OnDrawGizmosSelected()
    {
        // Zemin kontrolünü görselleştir
        if (groundCheck != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
        }
    }
}
