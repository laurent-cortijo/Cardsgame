using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardClick : MonoBehaviour
{

	public GameController gameController ;

    // Start is called before the first frame update
    void Start()
    {

    	gameController = GetComponent<GameController>();
        
    }

    // Update is called once per frame
    void Update()
    {

        
    }

    public void OnClick() {

    	if(GUI.Button(new Rect(100, 165, 100, 45), "Button1" ))
    	{
    		//gameController.OnClick();
    	}
    }

    
}
