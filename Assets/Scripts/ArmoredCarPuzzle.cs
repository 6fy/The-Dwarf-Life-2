using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmoredCarPuzzle : MonoBehaviour
{
    private SFX sfx;
    private GamePlay gamePlay;

    public bool puzzleBusy = false;
    public bool isPuzzleComplete = false;

    // Start is called before the first frame update
    void Start()
    {
        sfx = FindObjectOfType<SFX>();
        gamePlay = FindObjectOfType<GamePlay>();
    }

    public void StartPuzzle()
    {
        gamePlay.changeText("Objective: Find the turret and repair the armored car.");
        puzzleBusy = true;
    }

    public IEnumerator PuzzleCompleted(bool completed, float seconds)
    {
        yield return new WaitForSeconds(seconds);
        isPuzzleComplete = true;
    }
}
