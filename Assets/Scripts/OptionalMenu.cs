using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class OptionalMenu : MonoBehaviour
{
    public float soundlvl ;
    public int lvl;

    public void changeAudio(float sliderValue)
    {
    	soundlvl = Mathf.Log10 (sliderValue)*20;
        GameInfo.sound = soundlvl;
    }

    public void changeDifficulties(int dif)
    {
    	lvl = dif;
        GameInfo.difficultie = lvl;
    }
}
