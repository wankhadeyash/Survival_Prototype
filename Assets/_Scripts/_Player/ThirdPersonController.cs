using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonController : MonoBehaviour
{
    public Camera m_Cam;
    public float moveSpeed = 5.0f; // Speed at which the character moves
    public float jumpForce = 4.0f; // Speed at which the character jumps
    public float gravity = -12f; // Gravity applied to the character
    float m_VelocityY;

    private Vector3 moveDirection = Vector3.zero; // Movement direction of the character
    CharacterController controller;
    bool isGrounded;
    public LayerMask groundMask;
    public Transform groundCheck;
    public float groundDistance = 0.1f;
    private Animator animator; // Reference to the Animator component

    float m_TurnSmoothVelocity;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        Vector3 direction = new Vector3(Input.GetAxisRaw("Horizontal"), 0,  Input.GetAxisRaw("Vertical")).normalized;

        if (direction.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + m_Cam.transform.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref m_TurnSmoothVelocity, 0.1f);
            transform.rotation = Quaternion.Euler(0, angle, 0);


           // Vector3 moveDir = Quaternion.Euler(0, targetAngle, 0) * Vector3.forward;
        }
        m_VelocityY += Time.deltaTime * gravity;
        if (isGrounded)
        {
            m_VelocityY = 0;
            if (Input.GetKeyDown(KeyCode.Space))
                m_VelocityY = Mathf.Sqrt(jumpForce * -2f * gravity);
        }
        Vector3 velocity = transform.forward * moveSpeed * direction.magnitude + Vector3.up * m_VelocityY;

        controller.Move(velocity * Time.deltaTime);
       
    }
}



