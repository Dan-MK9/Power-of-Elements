using UnityEngine;
using UnityEngine.Rendering;

public class Player : MonoBehaviour
{
    public float velocidade = 5f;

    CharacterController controller;

    Vector3 forward;
    Vector3 strafe;
    Vector3 vertical;

    private float forwardSpeed = 5f;
    private float strafeSpeed = 5f;

    float gravity;
    float jumpSpeed;
    float maxJumpHeight = 2f;
    float timeToMaxHeight = 0.5f;

    public bool podeMover = true;

    void Start()
    {
        controller = GetComponent<CharacterController>();    

        gravity = (-2 * maxJumpHeight) / (timeToMaxHeight * timeToMaxHeight);
        jumpSpeed = (2 *  maxJumpHeight) / timeToMaxHeight;
    }

    void Update()
    {
        if (!podeMover) return;

        float forwardInput = Input.GetAxisRaw("Vertical");
        float strafeInput = Input.GetAxisRaw("Horizontal");

        forward = forwardInput * velocidade * transform.forward;
        strafe = strafeInput * velocidade * transform.right;

        vertical += gravity * Time.deltaTime * Vector3.up;

        if (controller.isGrounded)
        {
            vertical = Vector3.down;
        }

        if (Input.GetKeyDown(KeyCode.Space) && controller.isGrounded)
        {
            vertical = jumpSpeed * Vector3.up;
        }

        Vector3 finalVelocity = forward + strafe + vertical;

        controller.Move(finalVelocity * Time.deltaTime);
    }
}
