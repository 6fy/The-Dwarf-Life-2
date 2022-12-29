using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GamePlay : MonoBehaviour
{
    public TextMeshProUGUI textMesh;

    public void changeText(string text)
    {
        this.textMesh.text = text;
    }

    public IEnumerator ChangeTextAfterSeconds(string text, float seconds)
    {
        yield return new WaitForSeconds(seconds);
        changeText(text);
    }
}
