using UnityEngine;

public class FirstPersonMovement : MonoBehaviour
{
    public float speed = 5f; // Normal hýz
    public float sprintSpeed = 10f; // Koþma hýzý
    public float gravity = -9.81f; // Yerçekimi
    public float jumpHeight = 2f; // Zýplama yüksekliði

    private Vector3 velocity; // Yerçekimi ve hareket için hýz
    private bool isGrounded; // Yerde mi kontrolü

    public CharacterController controller;
    public Transform groundCheck; // Yere temas kontrolü için obje
    public float groundDistance = 0.4f; // Yere yakýnlýk mesafesi
    public LayerMask groundMask; // Yere temas edilecek katman

    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        // Yere temas kontrolü
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f; // Sabitlenmiþ yerçekimi
        }

        // Hareket
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 move = transform.right * moveHorizontal + transform.forward * moveVertical;

        // Shift tuþuyla koþma
        if (Input.GetKey(KeyCode.LeftShift))
        {
            controller.Move(move * sprintSpeed * Time.deltaTime);
        }
        else
        {
            controller.Move(move * speed * Time.deltaTime);
        }

        // Zýplama
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        // Yerçekimi
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }
}
