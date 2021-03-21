using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class OptionalMenu : MonoBehaviour

{
    //private static OptionalMenu _instance;
    //public static OptionalMenu Instance { get; private set; }

    public float soundlvl;
    public int lvl;

    public static OptionalMenu Instance { get; private set; }

    private void Awake()
    {
        /*if (Instance == null)
            _instance = this;
        else if (Instance != this)
            Destroy(gameObject);*/
        if (Instance != null && Instance != this) {
            Destroy(gameObject);
            return;
        } else {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    public static int getDifficulty()
    {
        return Instance.lvl;
    }

    public static float getSound()
    {
        return Instance.soundlvl;
    }

    public static void changeAudio(float sliderValue)
    {
    	Instance.soundlvl = Mathf.Log10 (sliderValue)*20;
        //GameInfo.sound = soundlvl;
    }

    public static void setDifficulty() { return; }

    public static void setAudio() { return; }


    public static void changeDifficulties(int dif)
    {
    	Instance.lvl = dif;
        //GameInfo.difficultie = lvl;
    }
}
