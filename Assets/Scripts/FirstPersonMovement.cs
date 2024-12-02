using UnityEngine;

public class FirstPersonMovement : MonoBehaviour
{
    public float speed = 5f; // Normal h�z
    public float sprintSpeed = 10f; // Ko�ma h�z�
    public float gravity = -9.81f; // Yer�ekimi
    public float jumpHeight = 2f; // Z�plama y�ksekli�i

    private Vector3 velocity; // Yer�ekimi ve hareket i�in h�z
    private bool isGrounded; // Yerde mi kontrol�

    public CharacterController controller;
    public Transform groundCheck; // Yere temas kontrol� i�in obje
    public float groundDistance = 0.4f; // Yere yak�nl�k mesafesi
    public LayerMask groundMask; // Yere temas edilecek katman

    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        // Yere temas kontrol�
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f; // Sabitlenmi� yer�ekimi
        }

        // Hareket
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 move = transform.right * moveHorizontal + transform.forward * moveVertical;

        // Shift tu�uyla ko�ma
        if (Input.GetKey(KeyCode.LeftShift))
        {
            controller.Move(move * sprintSpeed * Time.deltaTime);
        }
        else
        {
            controller.Move(move * speed * Time.deltaTime);
        }

        // Z�plama
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        // Yer�ekimi
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }
}
