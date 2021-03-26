using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    private GameObject canva, mus, audioLis;
    private GameObject[] playerSprite;
    private int nbjoueurs ,nbCartes, joueurCourant, nbfleche, winner;
    private List<int> jd;
    private Cards[] debut;
    public List<Cards>[] decks_joueur, decks_joué;
    public Sprite[] faces;
    public Sprite cardBack,none;
    private InitialiseCards init;
    private bool duel, color, joueurduel, cartespé, duelPendant,gagnant;


    void Start()
    {
        gagnant = false;
    	init = new InitialiseCards();
    	duelPendant = false;
    	nbfleche = 1;
    	nbCartes=36;
    	cartespé = false;
    	duel = false;
    	color = false;
    	joueurduel = false;
    	joueurCourant = 0;
    	jd = new List<int>();
        canva = GameObject.Find("Canvas");
        mus = GameObject.Find("Music");
        audioLis =  GameObject.Find("audioListener");
        nbjoueurs = 2 + InfoSingleton.getInstance().getNbBot();
        debut = init.createCard(nbCartes);
        findSprite(); 
        decks_joueur = new List<Cards>[nbjoueurs];
        decks_joué = new List<Cards>[nbjoueurs];
        initialise();
        repartirCard();
        makeBackSprite();
    }

    void findSprite()
    {
    	playerSprite = new GameObject[nbjoueurs];
    	for(int indice = 0; indice<nbjoueurs;indice++)
    	{
    		playerSprite[indice]=GameObject.Find("P"+(indice+1));
    	}
    }

    void initialise()
    {
    	for (int i=0;i<nbjoueurs;i++)
    	{
    		decks_joueur[i]= new List<Cards>();
    		decks_joué[i]= new List<Cards>();
    		canva.transform.GetChild(i+1).gameObject.SetActive(true);
    	}
    }

    
    void repartirCard()
    {
    	int indice = 0;
    	foreach(Cards carte in debut)
    	{
    		decks_joueur[indice].Add(carte);
    		if(indice == nbjoueurs-1)
    			indice=0;
    		else
    			indice++;
    	}
    }

    void makeBackSprite()
    {
    	for(int indice = 0; indice<nbjoueurs;indice++)
    	{
    		if (decks_joueur[indice].Count > 0)
    			playerSprite[indice].transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().sprite = cardBack;
    		else
    			playerSprite[indice].transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().sprite = none;
    		if (decks_joué[indice].Count == 0)
    			playerSprite[indice].transform.GetChild(1).gameObject.GetComponent<SpriteRenderer>().sprite = none;
    	}
    }
    void makeSprite()
    {
    	if (decks_joueur[joueurCourant].Count > 0)
    		playerSprite[joueurCourant].transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().sprite = cardBack;
    	else
    		playerSprite[joueurCourant].transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().sprite = none;

    	if (decks_joué[joueurCourant].Count > 0)
    	{
    		if(decks_joué[joueurCourant][(decks_joué[joueurCourant].Count-1)].getNumber() < 6)
    			playerSprite[joueurCourant].transform.GetChild(1).gameObject.GetComponent<SpriteRenderer>().sprite = faces[decks_joué[joueurCourant][(decks_joué[joueurCourant].Count-1)].getNumber()+(5*decks_joué[joueurCourant][(decks_joué[joueurCourant].Count-1)].getColor())];
    		else if(decks_joué[joueurCourant][(decks_joué[joueurCourant].Count-1)].getNumber() == 6)
    			playerSprite[joueurCourant].transform.GetChild(1).gameObject.GetComponent<SpriteRenderer>().sprite = faces[faces.Length-3];
    		else if(decks_joué[joueurCourant][(decks_joué[joueurCourant].Count-1)].getNumber() == 7)
    			playerSprite[joueurCourant].transform.GetChild(1).gameObject.GetComponent<SpriteRenderer>().sprite = faces[faces.Length-2];
    		else
    			playerSprite[joueurCourant].transform.GetChild(1).gameObject.GetComponent<SpriteRenderer>().sprite = faces[faces.Length-1];

    	}
    	else
    		playerSprite[joueurCourant].transform.GetChild(1).gameObject.GetComponent<SpriteRenderer>().sprite = none;
    }

    public void Click() 
    {
    	int index = Random.Range(0,decks_joueur[joueurCourant].Count);
    	decks_joué[joueurCourant].Add(decks_joueur[joueurCourant][index]);
    	decks_joueur[joueurCourant].RemoveAt(index);
    	//print("numéro "+decks_joué[joueurCourant][decks_joué[joueurCourant].Count-1].getNumber()+" color "+decks_joué[joueurCourant][decks_joué[joueurCourant].Count-1].getColor());
    	makeSprite();
    	checkGagnant();
    	//print("duel "+duel+" gagnant "+ gagnant);
        if(!duel && !gagnant)
        {
        	//print("checkDuel");
	        checkDuel();
        }
    }

    IEnumerator botPlay()
    {
    	yield return new WaitForSeconds(1);
    	//print("bot click");
    	Click();
    }

    void checkGagnant()
    {
    	if (decks_joueur[joueurCourant].Count == 0)
    	{
            gagnant = true;
    		canva.transform.GetChild(0).gameObject.SetActive(false);
            if (joueurCourant == 0)
            {
                canva.transform.GetChild(5).gameObject.SetActive(true);  
            }
            else
            {
                canva.transform.GetChild(6).gameObject.SetActive(true);  
            }
    	}
    }	
    void checkDuel()
    {
    	if (decks_joué[joueurCourant][(decks_joué[joueurCourant].Count-1)].getNumber() == 6 )
    	{
            color = true;
            //print("duel couleur");
        }
        else if(decks_joué[joueurCourant][(decks_joué[joueurCourant].Count-1)].getNumber() == 7)
        {
        	//print("duel tout le monde");
        	joueurduel = true;
        	duel = true;
        	color = false;
        	for(int i = 0;i<nbjoueurs;i++)
        	{
        		jd.Add(i);
        	}
        }
        else if(decks_joué[joueurCourant][(decks_joué[joueurCourant].Count-1)].getNumber() == 8 && !cartespé)
        {
        	//print("perte d'une carte");
        	cartespé = true;
        	for (int i = 0; i<nbjoueurs; i++)
            {
        		if (!duel && !duelPendant)
            	{
                	//print("click plus");
    				StartCoroutine(botPlay());//Click();
            	}
            }
            if(duelPendant)
            	duelPendant = !duelPendant;
            return;
        }

        if(color && !duel)
        {
        	for(int i=0;i<nbjoueurs-1;i++)
        	{
        		for(int j=i+1;j<nbjoueurs;j++)
        		{
        			if(decks_joué[i].Count >0 && decks_joué[j].Count > 0 && 
        				decks_joué[i][decks_joué[i].Count-1].getColor()!=8 && decks_joué[j][decks_joué[j].Count-1].getColor()!=8
        				&& decks_joué[i][decks_joué[i].Count-1].getColor() == decks_joué[j][decks_joué[j].Count-1].getColor())
        			{
        				duel = true;
        				if (!jd.Contains(i))
        					jd.Add(i);
        				if (!jd.Contains(j))
        					jd.Add(j);
        			}		
        		}
        	}
        }
        else if(!duel)
        {
        	for(int i=0;i<nbjoueurs-1;i++)
        	{
        		for(int j=i+1;j<nbjoueurs;j++)
        		{
        			if(decks_joué[i].Count >0 && decks_joué[j].Count > 0 && 
        				decks_joué[i][decks_joué[i].Count-1].getNumber()<6 && decks_joué[j][decks_joué[j].Count-1].getNumber()<6
        				&& decks_joué[i][decks_joué[i].Count-1].getNumber() == decks_joué[j][decks_joué[j].Count-1].getNumber())
        			{
        				duel = true;
        				if (!jd.Contains(i))
        					jd.Add(i);
        				if (!jd.Contains(j))
        					jd.Add(j);
        			}
        				
        		}
        	}
        }
        if(duel)
        {
        	//print("duel");
        	if(cartespé)
        		duelPendant = true;
        	StartCoroutine(letDuel());
        }
        else
        {
        	//print("non duel");
        	//print("joueurCourant : "+joueurCourant);
        	//print("duel "+duel+" gagnant "+ gagnant+" cartespé "+cartespé);
        	if (joueurCourant == nbjoueurs-1) 
	        {
	            joueurCourant = 0 ;
	      	} 
	        else 
	        	joueurCourant++;
	        if(nbfleche == nbjoueurs && cartespé)
	        {
	        	nbfleche = 1;
	        	cartespé = !cartespé;
	        }
	        else if(cartespé)
	        	nbfleche++;

	        
	        if(joueurCourant == 0 && !duel && !cartespé)
	        {
	        	canva.transform.GetChild(0).gameObject.SetActive(true);
	        }
	        
	        if(!duel && joueurCourant!=0 && !cartespé)
	        {
	        	StartCoroutine(botPlay());
	        }
        }
        
    }
    IEnumerator letDuel()
    {
    	yield return new WaitForSeconds(1);
    	
    	if(!joueurduel && !jd.Contains(0))
    	{
    		int index = Random.Range(0,jd.Count);
            winner = jd[index];
    		jd.RemoveAt(index);
    		loser();
    	}
    	else
    	{
    		canva.SetActive(false);
            mus.SetActive(false);
            audioLis.SetActive(false);
            InfoSingleton.getInstance().setNbPlayerDuel(jd.Count);
            SceneManager.LoadScene("Parcours", LoadSceneMode.Additive);
    	}
    }

    void loser()
    {
    	int lose=0;
    	for(int i=0;i<nbjoueurs;i++)
    	{
    		int nbc = decks_joué[i].Count;
    		for(int j=nbc-1;j>=0;j--)
    		{
    			decks_joueur[jd[lose]].Add(decks_joué[i][j]);
    			decks_joué[i].RemoveAt(j);
    			if(lose == jd.Count-1)
    				lose = 0;
    			else
    				lose++;
    		}
    	}
    	jd.Clear();
    	makeBackSprite();
    	duel = false;
    	joueurduel = false;
    	color = false;
    	cartespé = false;
    	joueurCourant = winner;
    	//print("repartition fini");
    	StartCoroutine(finalPrepa());
    }

    IEnumerator finalPrepa()
    {
    	yield return new WaitForSeconds(1);
    	//print("affichage canvas");
        if(winner == 0)
    	   canva.transform.GetChild(0).gameObject.SetActive(true);
        else
            StartCoroutine(botPlay());
    }

    void Update()
    {
        if(!InfoSingleton.getInstance().getBool())
        {
            InfoSingleton.getInstance().setBool(true);
            restart();
        }
    }

    void restart()
    {
        canva.SetActive(true);
        mus.SetActive(true);
        audioLis.SetActive(true);
        winner = jd[InfoSingleton.getInstance().getWinner()];
        jd.RemoveAt(InfoSingleton.getInstance().getWinner());
        loser();
    }

    
}
