using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Sensitivity : MonoBehaviour
{
    private Slider slider;

    void Start()
    {
        slider = GetComponent<Slider>();

        if (PlayerPrefs.HasKey("Sense")) {
            slider.value = PlayerPrefs.GetFloat("Sense");
        }
    }

    public void ChangeSense()
    {
        float sense = slider.value;
        PlayerPrefs.SetFloat("Sense", sense);

        PlayerController player = FindObjectOfType<PlayerController>();
        player.ChangeSense(sense);
    }
}
