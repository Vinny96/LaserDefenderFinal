using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLaserDestroyer : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Destroy(collision.gameObject);
       // Debug.Log("An enemyLaser object has been destroyed.");
    }

}
