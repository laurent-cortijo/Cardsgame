using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class audioController : MonoBehaviour
{
	void Start()
	{
		gameObject.GetComponent<AudioSource>().volume = InfoSingleton.getInstance().getAudio();
	}

    void Update()
    {
    	gameObject.GetComponent<AudioSource>().volume = InfoSingleton.getInstance().getAudio();  
    }
}
