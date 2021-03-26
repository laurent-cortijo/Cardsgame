using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class OptionalMenu : MonoBehaviour
{
    public void changeAudio(float sliderValue)
    {
        InfoSingleton.getInstance().setAudio(sliderValue);
    }

    public void changeDifficulties(int dif)
    {
        InfoSingleton.getInstance().setDifficulties(dif);
    }

    public void changeNBBOT(int nb)
    {
    	InfoSingleton.getInstance().setNbBot(nb);
    }
}
