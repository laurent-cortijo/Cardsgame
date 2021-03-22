using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class audioController : MonoBehaviour
{

    void Update()
    {
      gameObject.GetComponent<AudioSource>().volume = InfoSingleton.getInstance().getAudio();  
    }
}
