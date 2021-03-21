using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
	GameObject canva, tmp, bot;
	GameObject[] spawns, players;
	List<int> num;
	int n,jmax,j;

    // Start is called before the first frame update
    void Start()
    {
        spawns = GameObject.FindGameObjectsWithTag("parcoursSpawn");
        players = GameObject.FindGameObjectsWithTag("Player");
        canva = GameObject.Find("Canvas");
        pickSpawn();
    }

    void pickSpawn()
    {
    	num = new List<int> {0,1,2,3};
    	foreach(GameObject joueur in players)
    	{
    		n = Random.Range(0, num.Count);
    		tmp = spawns[num[n]];
    		num.RemoveAt(n);
    		if (joueur.name == "Player1")
    			joueur.GetComponent<CharactereMotor>().choose = tmp;
    		joueur.GetComponent<Animation>().Play("idle");
    		transposePlayer(joueur,tmp);
    		if(joueur.name != "Player1")
    			joueur.GetComponent<UnityEngine.AI.NavMeshAgent>().enabled = true;
    	}
    	StartCoroutine(Wait_start());

    }

    void transposePlayer(GameObject j, GameObject t)
    {
    	j.transform.position = t.transform.position;
        if (t.name == "Spawn1")
            j.transform.eulerAngles = new Vector3(0.0f, 180f, 0.0f);
        else if(t.name == "Spawn2")
            j.transform.eulerAngles = new Vector3(0.0f, 270f, 0.0f);
        else if (t.name == "Spawn3")
            j.transform.eulerAngles = new Vector3(0.0f, 0f, 0.0f);
        else if (t.name == "Spawn4")
            j.transform.eulerAngles = new Vector3(0.0f, 90f, 0.0f);
    }

    IEnumerator Wait_start()
    {
    	canva.SetActive(true);
        yield return new WaitForSeconds(3);
        foreach(GameObject joueur in players)
    	{
    		if (joueur.name == "Player1")
    			joueur.GetComponent<CharactereMotor>().parcours = true;
    		else
    			joueur.GetComponent<BotMotor>().parcours = true;
    	}
        
    }

    IEnumerator Restart()
    {
    	yield return new WaitForSeconds(3);
    	foreach(GameObject joueur in players)
    	{
    		if (joueur.name == "Player1")
    		{
    			joueur.GetComponent<CharactereMotor>().parcours = false;
    			joueur.GetComponent<CharactereMotor>().winZone = false;
    		}
    		else
    		{
    			joueur.GetComponent<BotMotor>().parcours = false;
    			joueur.GetComponent<BotMotor>().winZone = false;
    		}
    			
    	}	
    	pickSpawn();
    		
    }

    // Update is called once per frame
    void Update()
    {
    	jmax = GameObject.Find("winarea").GetComponent<winZone>().nombreMax;
    	j = GameObject.Find("winarea").GetComponent<winZone>().nbj;
        if( jmax == players.Length || j == 1)
        {
        	GameObject.Find("winarea").GetComponent<winZone>().nombreMax = 0 ;
        	GameObject.Find("winarea").GetComponent<winZone>().nbj = 0 ;
        	StartCoroutine(Restart());
        }

        	

    }
}
