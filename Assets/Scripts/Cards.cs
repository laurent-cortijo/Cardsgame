using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cards
{
    private int number, color;

    public Cards(int number, int color)
    {
    	this.number = number;
    	this.color = color;
    }

    public int getNumber()
    {
    	return number;
    }

    public int getColor()
    {
    	return color;
    }

}
