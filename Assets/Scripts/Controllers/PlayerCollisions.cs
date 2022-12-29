using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerCollisions : MonoBehaviour
{
    private PlayerController playerController;
    private WheelPuzzle wheelPuzzle;
    private ArmoredCarPuzzle armoredCarPuzzle;
    private GamePlay gamePlay;

    private void Start()
    {
        playerController = GetComponent<PlayerController>();
        wheelPuzzle = FindObjectOfType<WheelPuzzle>();
        armoredCarPuzzle = FindObjectOfType<ArmoredCarPuzzle>();
        gamePlay = FindObjectOfType<GamePlay>();
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            playerController.isGrounded = true;
            playerController.jumpsLeft = 2;
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
            armoredCarPuzzle.PuzzleCompleted(true, 0.5f);
            
            Destroy(collision.gameObject);
            SceneManager.LoadScene("FinalCutScene");
        }
    }
}
