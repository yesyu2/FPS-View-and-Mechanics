using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public GameObject death_effect;
    private bool player_alive = true;
    public void Death()
    {
        if(player_alive)
        {
            player_alive = false; 
            //Particle effect 
            Instantiate(death_effect, transform.position, Quaternion.identity);
        }
    }
}
