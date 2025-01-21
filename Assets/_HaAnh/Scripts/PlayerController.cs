using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 5f;      // Tốc độ di chuyển
    public float gravity = -9.81f;   // Lực trọng trường

    [Header("Ground Detection")]
    public UnityEngine.Transform groundCheck;    // Điểm kiểm tra va chạm đất
    public float groundDistance = 0.4f; // Bán kính kiểm tra va chạm
    public LayerMask groundMask;     // Lớp đất để kiểm tra

    private CharacterController controller; // CharacterController cho Player
    private Vector3 velocity;        // Tốc độ di chuyển
    private bool isGrounded;         // Kiểm tra xem đang chạm đất hay không

    private void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    private void Update()
    {
        // Kiểm tra chạm đất
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f; // Giữ Player trên mặt đất
        }

        // Di chuyển ngang
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        Vector3 move = transform.right * vertical - transform.forward * horizontal;

        controller.Move(move * moveSpeed * Time.deltaTime);


        // Áp dụng trọng lực
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }
}