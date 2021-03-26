using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class nbCartes : MonoBehaviour
{
    void Update()
    {
    	if(gameObject.name == "name1")
    	{
    		gameObject.GetComponent<UnityEngine.UI.Text>().text = ""+GameObject.Find("Game Controller").GetComponent<GameController>().decks_joueur[0].Count;
    	}
    	else if(gameObject.name == "name2")
    	{
    		gameObject.GetComponent<UnityEngine.UI.Text>().text = ""+GameObject.Find("Game Controller").GetComponent<GameController>().decks_joueur[1].Count;
    	}
    	else if(gameObject.name == "name3")
    	{
    		gameObject.GetComponent<UnityEngine.UI.Text>().text = ""+GameObject.Find("Game Controller").GetComponent<GameController>().decks_joueur[2].Count;
    	}
    	else
    	{
    		gameObject.GetComponent<UnityEngine.UI.Text>().text = ""+GameObject.Find("Game Controller").GetComponent<GameController>().decks_joueur[3].Count;
    	}
        
    }
}
