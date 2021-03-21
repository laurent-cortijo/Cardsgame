using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardModel : MonoBehaviour
{
   public SpriteRenderer spriteRenderer;

    public Sprite[] faces;
    public Sprite cardBack;

    public int cardIndex;

    public int cardNumber;
    public string cardColor;

    public bool showFace = false ;

    public void ToggleFace(bool showFace)
    {
        if(showFace)
        {
            spriteRenderer.sprite = faces[cardIndex];
           
        }
        

    }

    void Awake()
    {

       spriteRenderer = GetComponent<SpriteRenderer>();
        
    }
}
