using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Perso1 : MonoBehaviour
{
    public GameObject card;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("c") && card.activeSelf)
        {
            card.SetActive(false);
        }

        else if (Input.GetKeyDown("c") && !card.activeSelf)
        {
            card.SetActive(true);
        }
    }
}
