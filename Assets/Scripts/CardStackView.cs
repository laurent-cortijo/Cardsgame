using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CardStack))]
public class CardStackView : MonoBehaviour
{
    CardStack cardStack;
    Dictionary<int, GameObject> fetchedCards;
    int lastCount;

    public Vector3 start;
    public GameObject cardPrefab;
    public GameObject cardCopy ;
   
    public Dictionary<int, GameObject> cardCopies;
    public Dictionary<int, GameObject> deck ; 
    
    private void Awake()
    {
        fetchedCards = new Dictionary<int, GameObject>();
        cardCopies = new Dictionary<int, GameObject>();
        deck = new Dictionary<int, GameObject>();
        cardStack = GetComponent<CardStack>();
       
    }

    private void Start()
    {
        ShowCards();


        lastCount = cardStack.CardCount;

        cardStack.CardRemoved += CardStack_CardRemoved;
        cardStack.CardAdded += CardStack_CardAdded;
    }

    private void CardStack_CardAdded(object sender, CardEventArgs e)
    {
    
        
        AddCard(start, e.CardIndex);
    }

    private void CardStack_CardRemoved(object sender, CardEventArgs e)
    {
        if(fetchedCards.ContainsKey(e.CardIndex))
        {
            Destroy(fetchedCards[e.CardIndex]);
            fetchedCards.Remove(e.CardIndex);
        }
           
    }

    private void Update()
    {
        if(lastCount != cardStack.CardCount)
        {
            lastCount = cardStack.CardCount;
            //ShowCards();
        }
    }

    public void ShowCards()
    {
       // int cardCount = 0;

        foreach(int i in cardStack.GetCards())
        {
            
            AddCard(start, i);

        }
    }


// tableau du nombre de carte qu'il y a au total : 33 > marque cb de fois on veut que chaque carte apparaisse : 
    //while il reste des cartes : on continu la boucle 
    //prend valeur random dans le tableau et regarde l'index (si >0 crée sinon re-random )

    void AddCard(Vector3 position, int cardIndex)
    {
        
        GameObject cardCopy = (GameObject)Instantiate(cardPrefab);

        CardModel cardModel = cardCopy.GetComponent<CardModel>();
        cardModel.cardIndex = cardIndex;

        DetermineCard (cardModel, cardIndex);
        cardModel.ToggleFace(true);

        
       
        SpriteRenderer spriteRenderer = cardCopy.GetComponent<SpriteRenderer>();
        cardCopies.Add(cardIndex, cardCopy);
    }


     public void DetermineCard(CardModel cardModel, int cardIndex) {

        cardModel.cardIndex = cardIndex;

        int cardColorIndex = cardIndex/8 ;

        int cardID = cardIndex%8 ;


        if(cardID < 5)
            {
                cardModel.cardNumber = cardID +1 ;
            }
            else if(cardID == 5)
            {
                cardModel.cardNumber = 6;
            }
            else if (cardID == 6)
            {
                cardModel.cardNumber = 7;
            }
            else if (cardID == 7)
            {
                cardModel.cardNumber = 8 ;
            }

        if (cardColorIndex == 0)
        {
            cardModel.cardColor = "blue";
        }
        else if (cardColorIndex == 1)
        {
            cardModel.cardColor = "orange";
        }
        else if (cardColorIndex == 2)
        {
            cardModel.cardColor = "purple";
        }
        else if (cardColorIndex == 3)
        {
            cardModel.cardColor = "green";
        }else if (cardColorIndex == 4)
        {
            cardModel.cardColor = "red";
        }else if (cardColorIndex == 5)
        {
            cardModel.cardColor = "pink";
        }

        
    }
   
}
