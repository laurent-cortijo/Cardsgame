using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitialiseCards 
{
	private Cards[] deck_pioche;
    
    public Cards[] createCard(int nb)
    {
    	deck_pioche = new Cards[nb];
    	int num,co=0,special=6;
    	for (int indice=0;indice<nb;indice++)
    	{
    		if(indice<(nb-6))
    		{
    			num = indice%5;
    			if(indice%5 == 0 && indice !=0)
    			{
    				co++;
    			}
    			deck_pioche[indice] = new Cards(num,co); 
    		}
    		else
    		{
    			deck_pioche[indice] = new Cards(special,8);
    			if(special == 8)
    			{
    				special = 6;
    			}
    			else
    			{
    				special++;
    			}
    		}
    	}
    	return deck_pioche;
    }

    string getColor(int n)
    {
    	switch (n)
    	{
    		case 0 :
    			return "bleu";
    		case 1 : 
    			return "orange";
    		case 2 :
    			return "violet";
    		case 3 :
    			return "vert";
    		case 4 :
    			return "rouge";
    		default:
    			return "rose";
    	}
    }
}
