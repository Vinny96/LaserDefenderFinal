using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageDealer : MonoBehaviour
{
    // configuration parameters
    [SerializeField] int damage = 0;

    

    // methods
    public int getDamage()
    {
        return damage;
    }

    public void hit()
    {
        Destroy(gameObject);
        Debug.Log("A gameObject has been destroyed.");
    }

    // beta methods
   /* private void OnTriggerEnter2D(Collider2D collision)
    {
        Destroy(gameObject);
    }*/
    // the goal of this beta method is to destroy the laserPrefab the momeny it comes it experiences a collision. 
}

// notes
/* 
 * remmeber that this script is attached to the laser gameObject. 
 * the damage dealer is attached to the laser gameObject. Everytime the laser gameObject collides with another gameObject we want the laser gameObject to be destroyed. 
 * 
 * 
 * 
 * 
 * 
 * 
 * 
 * */
