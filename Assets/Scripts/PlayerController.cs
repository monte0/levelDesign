using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed;
    public float jumpSpeed;
    public float gravity;

    public CharacterController charController;

    private Vector3 movement = Vector3.zero;

    void Start()
    {
        charController = GetComponent<CharacterController>();
    }

    private void Update()
    {
        if (charController.isGrounded)
        {
            movement = transform.forward * Input.GetAxis("Vertical") + transform.right * Input.GetAxis("Horizontal");
            movement = movement.normalized * speed;

            if (Input.GetButton("Jump"))
                movement.y = jumpSpeed;
        }

        movement.y -= gravity * Time.deltaTime;
    }

    void FixedUpdate()
    {
        moveCharacter(movement);
    }

    void moveCharacter(Vector3 direction)
    {
        charController.Move(movement * Time.deltaTime);
    }
}