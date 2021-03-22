using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfoSingleton : MonoBehaviour
{
	[SerializeField]
	private int difficulties = 0, nb_player_duel, winner;
	[SerializeField]
	private float audiolvl = 1;
    private bool parcour = true;
    static InfoSingleton instance;

    public static InfoSingleton getInstance()
    {
    	return instance;
    }
    void Start()
    {
    	if(instance != null)
    	{
    		Destroy(this.gameObject);
    		return;
    	}	
    	instance = this;
        GameObject.DontDestroyOnLoad(this.gameObject);
    }


    public void setDifficulties(int di)
    {
    	difficulties = di;
    }

    public int getDifficulties()
    {
    	return difficulties;
    }

    public void setAudio(float au)
    {
    	audiolvl = au;
    }

    public float getAudio()
    {
    	return audiolvl;
    }

    public void setNbPlayerDuel(int nb)
    {
    	nb_player_duel = nb;
    }

    public int getNbPlayerDuel()
    {
    	return nb_player_duel;
    }

    public void setWinner(int win)
    {
    	winner = win;
    }

    public int getWinner()
    {
    	return winner;
    }

    public void setBool(bool bo)
    {
        parcour = bo;
    }

    public bool getBool()
    {
        return parcour;
    }
}
