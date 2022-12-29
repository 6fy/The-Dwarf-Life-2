using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 10.0f;
    public float turnSpeed = 25.0f;
    public float jumpForce = 5.0f;

    public float mouseTurnSpeed = 5.0f;

    public Camera mainCamera;
    private GamePlay gamePlay;

    private Rigidbody playerCollider;

    private bool isGrounded = false;
    private int jumpsLeft = 0;

    private WheelPuzzle wheelPuzzle;
    private ArmoredCarPuzzle armoredCarPuzzle;

    // Start is called before the first frame update
    void Start()
    {
        playerCollider = GetComponent<Rigidbody>();
        ChangeSense(PlayerPrefs.GetFloat("Sense", 5.0f));

        wheelPuzzle = FindObjectOfType<WheelPuzzle>();
        armoredCarPuzzle = FindObjectOfType<ArmoredCarPuzzle>();
        gamePlay = FindObjectOfType<GamePlay>();

        Cursor.lockState = CursorLockMode.Locked;

        mainCamera = Camera.main;
        UpdateCamera();        
    }

    public void ChangeSense(float sense)
    {
        mouseTurnSpeed = sense;
    }

    // Update is called once per frame
    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        if (Input.GetKey(KeyCode.Tab))
        {
            Cursor.lockState = Cursor.lockState == CursorLockMode.Locked ? CursorLockMode.None : CursorLockMode.Locked;
        }

        // handle mouse turning
        HandleMouseTurning();
        Zoom();

        // handle player movement
        PlayerMovement(horizontalInput, verticalInput);

        // make sure player can jump and move at the same time
        HandleJump();

        // place the camera on the player
        UpdateCamera();
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
            jumpsLeft = 2;
        }

        if (collision.gameObject.CompareTag("Wall"))
        {
            transform.position = new Vector3(4, 5, 18);
        }

        if (wheelPuzzle.isPuzzleComplete && collision.gameObject.CompareTag("ArmoredCar"))
        {
            armoredCarPuzzle.StartPuzzle();
        }

        if (armoredCarPuzzle.puzzleBusy && collision.gameObject.CompareTag("Turret"))
        {
            gamePlay.changeText("");
            Debug.Log("Level Completed");
        }
    }

    private void UpdateCamera()
    {
        mainCamera.transform.position = transform.position;
        // mainCamera.transform.rotation = new Quaternion(transform.rotation.x, transform.rotation.y, transform.rotation.z, transform.rotation.w);
    }

    private void PlayerMovement(float horizontalInput, float verticalInput)
    {
        Transform oldtransform = transform;

        transform.Rotate(Vector3.up, Time.deltaTime * turnSpeed * horizontalInput);

        // move forward and backward
        if (Input.GetKey(KeyCode.W))
        {
            transform.Translate(Vector3.forward * speed * Time.deltaTime);
            return;
        }

        if (Input.GetKey(KeyCode.S))
        {
            transform.Translate(Vector3.back * speed * Time.deltaTime);
            return;
        }

        // move left and right
        if (Input.GetKey(KeyCode.A))
        {
            transform.Translate(horizontalInput * (turnSpeed / 2) * Time.deltaTime, 0, 0);
            return;
        }

        if (Input.GetKey(KeyCode.D))
        {
            transform.Translate(horizontalInput * (turnSpeed / 2) * Time.deltaTime, 0, 0);
            return;
        }
    }

    private void HandleMouseTurning()
    {
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");

        transform.Rotate(Vector3.up, mouseX * turnSpeed * (mouseTurnSpeed * 10) * Time.deltaTime);
    }

    private void Zoom()
    {
        // zoom in and out
        if (Input.GetAxis("Mouse ScrollWheel") > 0)
        {
            mainCamera.fieldOfView -= 2;
        }

        if (Input.GetAxis("Mouse ScrollWheel") < 0)
        {
            mainCamera.fieldOfView += 2;
        }

        // clamp the camera so it doesn't bug out
        mainCamera.fieldOfView = Mathf.Clamp(mainCamera.fieldOfView, 10, 60);
    }

    private void HandleJump()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (!isGrounded && jumpsLeft <= 0) {
                return;
            }

            playerCollider.AddForce(Vector3.up * (jumpForce * (jumpsLeft * 0.7f)), ForceMode.Impulse);

            jumpsLeft--;
            isGrounded = false;
        }
    }
}
