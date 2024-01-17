using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    //Bullet
   public float speed = 30f;

   private float lifetime = 2.5f;

   //Enemy Hit Effect
   public GameObject hit_effect;
   public AudioClip drone_shot;

   //Enemy Bullet
   public bool enemy_bullet = false;
   public float bullet_radius = 0.5f;
   public LayerMask player_layer;
   

   private void Update()
   {

        //Bullet
        transform.Translate(Vector3.forward *-1 * Time.deltaTime* speed);

        lifetime -= Time.deltaTime;
        
        if(lifetime <= 0)
        {
            Destroy(this.gameObject);
        }

        //Enemy Bullet
        if(enemy_bullet)
        {
            if(Physics.CheckSphere(transform.position, bullet_radius, player_layer))
            {
                GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerManager>().Death();
            }
        }

   }

   //if hit enemy
    private void OnTriggerEnter(Collider other)
   {
        if(other.CompareTag("Enemy"))
        {
            GameObject drone = other.transform.parent.gameObject;
            drone.GetComponent<Drone>().health -= 25f;
            drone.GetComponent<AudioSource>().PlayOneShot(drone_shot);
        }

        //Hit
        Instantiate(hit_effect, transform.position, transform.rotation);
   }
}