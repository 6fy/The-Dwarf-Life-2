using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheelPuzzle : MonoBehaviour
{

    public GameObject lostWheel; // wheel the player has to find
    public GameObject hiddenWheel; // wheel in the car

    private SFX sfx;
    private GamePlay gamePlay;

    public bool isPuzzleComplete = false;

    private void Start()
    {
        sfx = FindObjectOfType<SFX>();
        sfx.Play(SoundType.CarCrash);

        gamePlay = FindObjectOfType<GamePlay>();
        
        lostWheel.SetActive(true);
        hiddenWheel.SetActive(false);

        gamePlay.changeText("Objective: Find and pickup the wheel the blue car lost in the crash!");
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject == lostWheel)
        {
            sfx.Play(SoundType.RepairCar);

            lostWheel.SetActive(false);
            hiddenWheel.SetActive(true);

            gamePlay.changeText("You have found the wheel!");
            StartCoroutine(gamePlay.ChangeTextAfterSeconds("Objective: Find the green armored vehicle, and hit it!", 5));
            StartCoroutine(PuzzleCompleted(true, 5));
        }
    }

    public IEnumerator PuzzleCompleted(bool completed, float seconds)
    {
        yield return new WaitForSeconds(seconds);
        isPuzzleComplete = true;
    }
}