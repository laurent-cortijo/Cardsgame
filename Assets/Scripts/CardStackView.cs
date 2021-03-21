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
    public float cardOffset;
    public bool isHumanPlayerHand = false;
    public bool reverseLayerOrder = false;

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
        float co = cardOffset * cardStack.CardCount;
        Vector3 temp = start + new Vector3(co, 0f);
        AddCard(temp, e.CardIndex, cardStack.CardCount);
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
        int cardCount = 0;

        foreach(int i in cardStack.GetCards())
        {
            float co = cardOffset * cardCount;

            Vector3 temp = start;
            AddCard(temp, i, cardCount);

            cardCount++;
        }
    }

    public void ArrangeCards(int currentPlayer)
    {
        int cardCount = 0;

        foreach (int i in cardStack.GetCards())
        {
            float co = cardOffset * cardCount;

            Vector3 temp = new Vector3(0f, 0f);
            if(currentPlayer == 0)
            {
                temp = start + new Vector3(0f, 0f);
            }
            else
            {
                if (currentPlayer == 1)
                {
                    temp = start + new Vector3(0f, 1f);
                    cardCopies[i].transform.eulerAngles = new Vector3(0, 0, 0);
                }

                
            }

            cardCopies[i].transform.position = temp;

            cardCount++;
        }
    }

    void AddCard(Vector3 position, int cardIndex, int positionalIndex)
    {
        if(fetchedCards.ContainsKey(cardIndex))
        {
            return;
        }
        GameObject cardCopy = (GameObject)Instantiate(cardPrefab);
        cardCopy.transform.position = position;
        cardCopies.Add(cardIndex, cardCopy);

        CardModel cardModel = cardCopy.GetComponent<CardModel>();
        cardModel.cardIndex = cardIndex;
        DetermineCurrentCard(cardModel, cardIndex);
        //cardModel.ToggleFace(isHumanPlayerHand);

        SpriteRenderer spriteRenderer = cardCopy.GetComponent<SpriteRenderer>();
        if(reverseLayerOrder)
        {
            spriteRenderer.sortingOrder = 48 - positionalIndex;
        }
        else
        {
            spriteRenderer.sortingOrder = positionalIndex;
        }

        fetchedCards.Add(cardIndex, cardCopy);
    }



    public void DetermineCurrentCard(CardModel cardModel, int cardIndex)
    {
        // 0-4 normal cards (0-4)
        
        // 5 fleches couleurs
        // 6 fleches int
        // 7 fleches ext 

        
        int cardColorIndex = cardIndex / 8;
        int cardID = cardIndex/8 ;

       /* if (cardID == 5)
        {
            cardModel.cardNumber = 0;
        }
        else
        {*/
            if(cardID < 5)
            {
                cardModel.cardNumber = cardID + 1;
            }
            else if(cardID == 5)
            {
                cardModel.cardNumber = 5;
            }
            else if (cardID == 6)
            {
                cardModel.cardNumber = 6;
            }
            else if (cardID == 7)
            {
                cardModel.cardNumber = 7;
            }
       // }

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
        else if (cardColorIndex == 6)
        {
            cardModel.cardColor = "default";

           
        }
   
    }
}
