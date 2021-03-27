using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class badArea : MonoBehaviour
{
    private GameObject[] spawns;
    private GameObject respawn;

    void findSpawn()
    {
        int indice = 0 ;
        float dist, min= float.MaxValue;
        for (int i = 0; i < spawns.Length; i++)
        {
            dist = Mathf.Abs(Vector3.Distance(transform.position, spawns[i].transform.position));
            if (dist < min)
            {
                indice = i;
                min = Vector3.Distance(transform.position, spawns[i].transform.position);
            }
        }

        respawn = spawns[indice];
    }

    void Start()
    {
        spawns = GameObject.FindGameObjectsWithTag("parcoursSpawn");
        findSpawn();
    }


    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.name =="Joueur1")
        {
            col.transform.position = respawn.transform.position;
            col.GetComponent<CharactereMotor>().fall = true;
        }
    }
}
