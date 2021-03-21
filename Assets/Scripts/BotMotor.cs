using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BotMotor : MonoBehaviour
{
	public Transform Target;
	public NavMeshAgent agent;
	Animation animations;
	public int de =0;
	public bool winZone = false, first = false, fall = false, parcours = false;
	
	
    // Start is called before the first frame update
    void Start()
    {
    	animations = gameObject.GetComponent<Animation>();
        agent.speed = agent.speed + GameMaster.difficultie;
    }

    // Update is called once per frame
    void Update()
    {
        if(parcours)
        {
        	
            if (winZone)
            {
            	if(de == 1)
            	{
            		//agent.isStopped = true;
            		de--;
            	}
                if (first)
                    animations.Play("victory");
                else
                    animations.Play("die");
            }
            else
            {
            	if(de == 0)
            	{
            		agent.SetDestination(Target.position);
            		de++;
            	}
                animations.Play("run");
            }
        } 
    }
}
