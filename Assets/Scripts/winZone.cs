using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class winZone : MonoBehaviour
{
    public int nombreMax = 0 , nbj = 0 ;

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            nombreMax++;
            if(col.gameObject.name == "Joueur1")
            {
                nbj++;
                col.GetComponent<CharactereMotor>().winZone = true;
                if (nombreMax == 1)
                {
                    col.GetComponent<CharactereMotor>().first = true;
                }
                else
                {
                    col.GetComponent<CharactereMotor>().first = false;
                }
            }

            else
            {
                col.GetComponent<BotMotor>().winZone = true;
                if (nombreMax == 1)
                {
                    col.GetComponent<BotMotor>().first = true;
                }
                else
                {
                    col.GetComponent<BotMotor>().first = false;
                }
                col.GetComponent<UnityEngine.AI.NavMeshAgent>().enabled = false;

            }
        }       
    }
   
}
