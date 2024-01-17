using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    RaycastHit hit;
    public LayerMask obstacle , player_layer;
    public GameObject death_effect;
    public float laser_multiplier;

    void Update()
    {   
        //Line Renderer 
        if(Physics.Raycast(transform.position, transform.forward, out hit, Mathf.Infinity, obstacle))
        {
            GetComponent<LineRenderer>().enabled = true;
            
            GetComponent<LineRenderer>().SetPosition(0, transform.position);
            GetComponent<LineRenderer>().SetPosition(1, hit.point);

            GetComponent<LineRenderer>().startWidth = 0.035f * laser_multiplier + Mathf.Sin(Time.time) / 75;
        }
        else
        {
            GetComponent<LineRenderer>().enabled = false;
        }
        //Hit Laser Player
        if(Physics.Raycast(transform.position, transform.forward, out hit, Mathf.Infinity, player_layer))
        {
            hit.transform.gameObject.GetComponent<PlayerManager>().Death();
        }
        
    }
}
