using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{

    private int tour ;
    public int click ;
    private int[] carteJouees = new int [48];

    public CardStack[] players ;

    public List<Sprite> deckCard = new List<Sprite>() ;
    public GameObject[] playersObjects ;
   
    public CardStack deck;

   
    public GameObject currentCard;
    public GameObject blankCard;
   
    public RectTransform panelGameOver;
    public Text winnerText;

    public Canvas canvas ;
    public AudioListener audioListener ;
    public AudioSource music ;
  
    private int currentPlayer;
   
    private CardStackView view;
    private CardStackView[] views;
   
    private CardModel cardModel;
    private CardModel blankCardModel;

    private bool WasCardEffectApplied;
    private bool specialTour ;
   
    void Start()

    {
      

        winnerText.gameObject.SetActive(false);
        panelGameOver.gameObject.SetActive(false);
        tour = 0;
    	WasCardEffectApplied = false ;
        specialTour = false ;
        click = 0;
        

        currentPlayer = 0;

        view = deck.GetComponent<CardStackView>();
        cardModel = currentCard.GetComponent<CardModel>();

        blankCardModel = blankCard.GetComponent<CardModel>();

        views = new CardStackView[players.Length];

        for (int i = 0; i < players.Length; i++)
        {
            views[i] = playersObjects[i].GetComponent<CardStackView>();

        }
        StartGame();
        
    }

    void Restart() {
    	InfoSingleton.getInstance().setBool(true);
    	int winner = InfoSingleton.getInstance().getWinner();

        WasCardEffectApplied = false ;
        specialTour = false ;
 
        tour = 0;
        click = 0 ; 

       currentPlayer = winner;

        canvas.gameObject.SetActive(true);
        music.gameObject.SetActive(true);
        audioListener.gameObject.SetActive(true);


        if(winner == 0)
        	loseDuel(1);
        else
        	loseDuel(0);
    }



    void Update(){

        if(WasCardEffectApplied == false){
            if (currentPlayer == 0 && Input.GetMouseButtonDown(0))
            {
            	Click();
            }
       
        }        
        if(!(InfoSingleton.getInstance().getBool()))
        	Restart();     
    }

    void StartGame()
    {
        for (int i = 0; i < (48/players.Length); i++)
        {
            for (int j = 0; j < players.Length; j++)
            {
                players[j].Push(deck.Pop());   
            }
        }

    }


    IEnumerator Wait(){
    	yield return new WaitForSeconds(1);
    	Click();    
    }

    public void Click() {

        carteJouees[click] = players[currentPlayer].cards[0];

        deckCard.Add(currentCard.GetComponent<CardModel>().faces[carteJouees[click]]);

        players[currentPlayer].Pop();

        if (currentPlayer == 0) {
            view.DetermineCard(cardModel, carteJouees[click]);
            views[currentPlayer].cardCopies[carteJouees[click]].transform.position = new UnityEngine.Vector3(0, -1, 0);
            views[currentPlayer].cardCopies[carteJouees[click]].transform.localScale = new UnityEngine.Vector3(1, 1, 0);

        } 
        else {
        	view.DetermineCard(blankCardModel, carteJouees[click]);
            views[currentPlayer].cardCopies[carteJouees[click]].transform.position = new UnityEngine.Vector3(0, 1.5f, 0);
            views[currentPlayer].cardCopies[carteJouees[click]].transform.localScale = new UnityEngine.Vector3(1,1, 0);
        }
     
        if (currentPlayer == players.Length-1) {
            currentPlayer = 0 ;
            tour ++ ;
      	} 
        else 
        	currentPlayer++;

        click++ ;
        if(WasCardEffectApplied == false)
        {
            Tour();
            Gagnant(); 
            if (currentPlayer !=0 )
            {
                StartCoroutine(Wait());
            }
        }
        
                       
    }


     public void Tour() {

    	
    	WasCardEffectApplied = false ;
        if (cardModel.cardNumber == 6 || blankCardModel.cardNumber == 6){
            specialTour = true ;
        }


        //card 6 -> fleches int : tous les participants font le duel 

        else if (cardModel.cardNumber == 7 || blankCardModel.cardNumber == 7 ){
            WasCardEffectApplied = true ; 
            print("DUEL fleches int");
        }

        // Card 7 -> ext : posent 1 carte 

        else if (cardModel.cardNumber == 8 || blankCardModel.cardNumber == 8 ){
        	print(" fleches ext"); 
            for (int l = 0; l<players.Length; l++)
            {
            	if (WasCardEffectApplied == false)
                {
                	Click() ;
                }
            }        
        }

        if(specialTour){
            if(cardModel.cardColor == blankCardModel.cardColor) {
                
                print ("DUEL couleur");
                WasCardEffectApplied = true ;
            }
        }

        else if(cardModel.cardNumber == blankCardModel.cardNumber) {
	            print ("DUEL valeur");
	            WasCardEffectApplied = true ;
        }

        if (WasCardEffectApplied)
            Duel();
   
    }

    void Duel() {

            canvas.gameObject.SetActive(false);
            music.gameObject.SetActive(false);
            audioListener.gameObject.SetActive(false);
            InfoSingleton.getInstance().setNbPlayerDuel(2);
            SceneManager.LoadScene("Parcours", LoadSceneMode.Additive);

        
    }

      
        
    void loseDuel(int loser){

        for (int i=0; i<deckCard.Count; i++){

            int j = carteJouees[i];
            
            players[loser].Push(j);

            deckCard.RemoveAt(i);
        }
        print("Le joueur " + loser +  " obtient " + deckCard.Count + " cartes");
    }


    void Gagnant() {
        if(players[currentPlayer].CardStackCount() == 0){
            panelGameOver.gameObject.SetActive(true);
            if (currentPlayer == 0)
            {
              winnerText.text = "Vous avez gagné !!";  
            }
            else
            {
               winnerText.text = "Vous avez perdu !!"; 
            }
            StartCoroutine(fin());   
        }             
	}

    IEnumerator fin(){
        yield return new WaitForSeconds(3);
        SceneManager.LoadScene("Menu");    
    }

}



	

    

  



