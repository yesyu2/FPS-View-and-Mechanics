using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drone : MonoBehaviour
{
    private Transform player;

    private float follow_distance = 15f;
    private float speed = 9f;

    private float cooldown = 1f;
    public GameObject mesh;
    public GameObject bullet;
    public AudioClip death_sound;
    public GameObject death_effect;

    //Drone Health
    public float health = 100f;
    private void Awake() 
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Update() 
    {
        FollowPlayer();
        shot();
        Death();
    }

    //Drone Follow Player
    private void FollowPlayer()
    {
        //Look to Player
        transform.LookAt(player.position);
        transform.rotation *= Quaternion.Euler(new Vector3(-90,0,0));

        //Move to Player 
        if(Vector3.Distance(transform.position, player.position) > follow_distance)
        {
        transform.Translate(transform.forward * Time.deltaTime * speed * -1);
        }
        else
        {
            transform.RotateAround(player.position, transform.forward, Time.deltaTime * speed * Random.Range(0.2f , 3f));
        }
    }

    //Shot && 
    private void shot()
    {
        if(cooldown > 0)
        {
            cooldown -= Time.deltaTime;
        }
        else
        {
            cooldown = 1f;
           
            //Shot
            mesh.GetComponent<Animator>().SetTrigger("Shot");
            
            Instantiate(bullet, transform.position, transform.rotation * Quaternion.Euler(new Vector3(-90, 0 , 0)));

        }
        
    }
    //Death
    private void Death()
    {
        if(health <= 0)
        {
            //Spawn Particle
            Instantiate(death_effect, transform.position, Quaternion.identity);
            //Destroy GameObject
            GameObject.FindGameObjectWithTag("Player").GetComponent<AudioSource>().PlayOneShot(death_sound);
            Destroy(this.gameObject);
        }
    }
}
