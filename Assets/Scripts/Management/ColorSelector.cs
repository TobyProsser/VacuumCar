using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorSelector : MonoBehaviour
{
    ColorWave curColorWave;

    public Material baseMat;
    public Material spinMat;
    public Material floorMat;


    void Awake()
    {
        curColorWave = SavedData.savedData.transform.GetComponent<ColorWavesHolder>().colorWaves[SavedData.savedData.colorWave];
        ApplyColors();
    }

    void ApplyColors()
    {
        baseMat.color = curColorWave.playerBaseColor;
        spinMat.color = curColorWave.playerSpinColor;
        floorMat.color = curColorWave.groundColor;
    }
}
