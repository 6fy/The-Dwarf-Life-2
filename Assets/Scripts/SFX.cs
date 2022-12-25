using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFX : MonoBehaviour
{
    public AudioSource carCrash;
    public AudioSource repairCar;

    public void Play(SoundType sfx)
    {
        switch (sfx)
        {
            case SoundType.CarCrash:
                carCrash.Play();
                break;
            case SoundType.RepairCar:
                repairCar.Play();
                break;
        }
    }
}

public enum SoundType {
    CarCrash,
    RepairCar
}
