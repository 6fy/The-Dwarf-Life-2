using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 10.0f;
    public float turnSpeed = 25.0f;
    public float jumpForce = 5.0f;

    public float mouseTurnSpeed = 5;

    public Transform groundCheck;
    public Camera mainCamera;

    private Rigidbody playerCollider;

    // Start is called before the first frame update
    void Start()
    {
        playerCollider = GetComponent<Rigidbody>();
        mainCamera = Camera.main;
        UpdateCamera();        
    }

    // Update is called once per frame
    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        // handle mouse turning
        HandleMouseTurning();

        // handle player movement
        PlayerMovement(horizontalInput, verticalInput);

        // make sure player can jump and move at the same time
        HandleJump();

        // place the camera on the player
        UpdateCamera();
    }

    private void UpdateCamera()
    {
        mainCamera.transform.position = transform.position;
        mainCamera.transform.rotation = transform.rotation;
    }

    private void PlayerMovement(float horizontalInput, float verticalInput)
    {
        Transform oldtransform = transform;

        transform.Translate(Vector3.forward * Time.deltaTime * speed * verticalInput);
        transform.Rotate(Vector3.up, Time.deltaTime * turnSpeed * horizontalInput);

        if (Input.GetKey(KeyCode.A))
        {
            transform.Rotate(Vector3.up, Time.deltaTime * turnSpeed * 4 * horizontalInput);
            return;
        }

        if (Input.GetKey(KeyCode.D))
        {
            transform.Rotate(Vector3.up, Time.deltaTime * turnSpeed * 4 * horizontalInput);
            return;
        }
    }

    private void HandleMouseTurning()
    {
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");

        transform.Rotate(Vector3.up, mouseX * turnSpeed * (mouseTurnSpeed * 10) * Time.deltaTime);
    }

    private void HandleJump()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            playerCollider.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }
}
