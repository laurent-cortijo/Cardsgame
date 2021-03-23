using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class parcourController : MonoBehaviour
{
    GameObject canva, tmp, bot;
	GameObject[] spawns, players, playParcours;
	List<int> num;
	int n,jmax,j, nbplayers;

    // Start is called before the first frame update
    void Start()
    {
        spawns = GameObject.FindGameObjectsWithTag("parcoursSpawn");
        players = GameObject.FindGameObjectsWithTag("Player");
        canva = GameObject.Find("Canvas");
        nbplayers = InfoSingleton.getInstance().getNbPlayerDuel();
        takePlayer();
    }

    void takePlayer()
    {
        playParcours = new GameObject[nbplayers];
        for (int indice = 0; indice<nbplayers;indice++)
        {
            playParcours[indice] = players[indice];
        }

        pickSpawn();
    }

    void pickSpawn()
    {
    	num = new List<int> {0,1,2,3};
    	foreach(GameObject joueur in playParcours)
    	{
    		n = Random.Range(0, num.Count);
    		tmp = spawns[num[n]];
    		num.RemoveAt(n);
    		joueur.GetComponent<Animation>().Play("idle");
    		transposePlayer(joueur,tmp);
    		if(joueur.name != "Joueur1")
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
        foreach(GameObject joueur in playParcours)
    	{
    		if (joueur.name == "Joueur1")
    			joueur.GetComponent<CharactereMotor>().parcours = true;
    		else
    			joueur.GetComponent<BotMotor>().parcours = true;
    	}
        
    }

    IEnumerator Restart()
    {
    	yield return new WaitForSeconds(5);
        InfoSingleton.getInstance().setBool(false);
        SceneManager.UnloadSceneAsync("Parcours");

        /*
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
    	pickSpawn();*/
    		
    }

    // Update is called once per frame
    void findWinner()
    {
        int indice = 0; 
        foreach(GameObject joueur in playParcours)
        {
            if (joueur.name == "Joueur1")
            {
                if(joueur.GetComponent<CharactereMotor>().first)
                {
                    InfoSingleton.getInstance().setWinner(indice);
                    return;
                }
            }  
            else
            {
              if(joueur.GetComponent<BotMotor>().first)
                {
                    InfoSingleton.getInstance().setWinner(indice);
                    return;
                }  
            } 
            indice++;
        }
    }
    void Update()
    {
    	jmax = GameObject.Find("winarea").GetComponent<winZone>().nombreMax;
    	j = GameObject.Find("winarea").GetComponent<winZone>().nbj;
        if( jmax == players.Length || j == 1)
        {
        	GameObject.Find("winarea").GetComponent<winZone>().nombreMax = 0 ;
        	GameObject.Find("winarea").GetComponent<winZone>().nbj = 0 ;
            findWinner();
            StartCoroutine(Restart());
        }

        	

    }
}
