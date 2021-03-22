using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class GameController : MonoBehaviour
{

    private int tour;

    public CardStack[] players ;

    public List<Sprite> deckCard = new List<Sprite>() ;
    public GameObject[] playersObjects ;
   
    public CardStack deck;

   
    public GameObject currentCard;
    public GameObject blankCard;
   
    public RectTransform panelGameOver;
    public Text winnerText;
  
    private int currentPlayer;
   
    private CardStackView view;
    private CardStackView[] views;
   
    private CardModel cardModel;
    private CardModel blankCardModel;

    private bool WasCardEffectApplied;
    private bool WasSpecialTour ;



    void Start()

    {
        //loseDuel();

        //winnerText.gameObject.SetActive(false);

        tour = 0;
    	WasCardEffectApplied = false ;
       
        WasSpecialTour = false;
        

        //panelGameOver.gameObject.SetActive(false);

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

    
    public void CardClick(){
    	Debug.Log("Clique");
    	int i = players[currentPlayer].cards[0];
       // view.ClickCard(currentPlayer);
            //deckUser.Add(players[currentPlayer].cards[0]);
        deckCard.Add(currentCard.GetComponent<CardModel>().faces[i]);



        players[currentPlayer].Pop();
        print("click " + i);

            
               
                //players[i].transform.Translate(1, 2, 0);
                        
              //  cardModel.ToggleFace(true); 


        if (currentPlayer == 1) {
            view.DetermineCard(cardModel, i);

            views[currentPlayer].cardCopies[i].transform.position = new UnityEngine.Vector3(0, -1, 0);
            views[currentPlayer].cardCopies[i].transform.localScale = new UnityEngine.Vector3(1, 1, 0);


                    
            

        } else 
            {view.DetermineCard(blankCardModel, i);
             

            views[currentPlayer].cardCopies[i].transform.position = new UnityEngine.Vector3(0, 1.5f, 0);
            views[currentPlayer].cardCopies[i].transform.localScale = new UnityEngine.Vector3(1,1, 0);

        }


            
               
        if (currentPlayer == players.Length-1) {

            currentPlayer = 0 ;
            tour ++ ;
            

        } else currentPlayer++;

        Tour();

        Gagnant();
                    
                            
    }

           
        	
    

  

     public void Tour() {

    	

    	WasSpecialTour = false ;
    	WasCardEffectApplied = false ;

        int carteCompare = players[currentPlayer].cards[0];

            if (cardModel.cardNumber == 5 || blankCardModel.cardNumber == 5)

                WasSpecialTour = true ;



            //card 6 -> fleches int : tous les participants font le duel 

            else if (cardModel.cardNumber == 6 || blankCardModel.cardNumber == 6 ){

                WasCardEffectApplied = true ; 
                print("DUEL fleches int");
            }

            // Card 7 -> ext : posent 1 carte 

            else if (cardModel.cardNumber == 7 || blankCardModel.cardNumber == 7 ){
                
                for (int k = 0 ; k<players.Length ; k++){

                print("DUEL fleches ext");
                

                }

            
            }

            if (WasSpecialTour) {
                    if(cardModel.cardColor == blankCardModel.cardColor) 
                        print ("DUEL couleur");
                    

            } else
                if(cardModel.cardNumber == blankCardModel.cardNumber) {
                print ("DUEL valeur");
                }
       
            }

      
        
    void loseDuel(int loser){

        for (int i=0; i<deckCard.Count; i++){
            print("Le joueur " + "n° perdant" + "obtiens" + deckCard.Count + " cartes");
            players[loser].Push(deck.Pop());
            deckCard.RemoveAt(i);

        }
    }


    void Gagnant() {

        for (int i = 0; i<players.Length ; i++){
            if(players[i].CardStackCount() == 0){
                panelGameOver.gameObject.SetActive(true);
                winnerText.text = "Le jouer  " + i + " a gagné !!";
                }
            }

        
	}

}



	

    

  



