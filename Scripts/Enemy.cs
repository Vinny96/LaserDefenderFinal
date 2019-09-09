using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Enemy : MonoBehaviour
{
    // configuration parameters
    [SerializeField] float health = 0;
    [SerializeField] float shotTimeMinimum = 0;
    [SerializeField] float shotTimeMaximum = 0; // maximum should always be 1 less than the timeBetweenShots.
    [SerializeField] float randomShotFactor = 0; // will be used to add to the time betweenMinAndMax variable
    [SerializeField] float timeBetweenShots = 0; // this is the time that will be subtracted from factorToSubtract. The value will be provided by the user in the unity inspector. 
    [SerializeField] GameObject enemyLaserPrefab = null;
    [SerializeField] float enemyVelocityY = 0;


    // variables
    float timeBetweenMinAndMax; // this will use random.range to get a float between the time minimum and time maximum variable. 
    float factorToSubtract; // we are going to add timeBetweenMinAndMax and randomShotFactor. 
    float timeBetweenShotsHolder;


    // beta variables
    //[SerializeField] GameStatus gameStatusHandle = null;
    GameStatus gameStatusHandle;
    [SerializeField] DamageDealer damageDealer = null;

    // visual effects and audio effects
    [SerializeField] GameObject particleEffects = null;
    [SerializeField] AudioClip clipToPlay = null;

    // beta variables
    [SerializeField] GameObject experimentalParticleEffects = null;
    
    
    // Start is called before the first frame update
    void Start()
    {

        timeBetweenMinAndMax = Random.Range(shotTimeMinimum, shotTimeMaximum);
        factorToSubtract = timeBetweenMinAndMax + randomShotFactor;
        timeBetweenShotsHolder = timeBetweenShots; // we are creating this variable so it can hold the original value of timeBetweenShots. 

        gameStatusHandle = FindObjectOfType<GameStatus>();
    
    }

    // Update is called once per frame
    void Update()
    {
        countDownAndShoot();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(collision.gameObject);
        health -= damageDealer.getDamage();
        if (health <= 0)
        {
            destroyEnemy();
            gameStatusHandle.IncreaseAndGetScore();
            playDestroyAudio(); // testing to see if this works. This works 
        }
    }


    private void destroyEnemy()
    {
        Destroy(gameObject);
        //instantiateParticleEffects(); // testing this method. 
        instantiateExperimentalParticleEffects();
        Debug.Log("An enemy gameObject has been destroyed.");
    }
       
    private void countDownAndShoot()
    {
        if (timeBetweenShots > 0) // this used to be != 0
        {
            timeBetweenShots -= factorToSubtract;
           // Debug.Log("The timeBetweenShots is below.");
           // Debug.Log(timeBetweenShots);
        }
        else // the else block is now getting executed.
        {
            //Debug.Log("timeBetweenShots has now went below zero."); 
            makeEnemyShoot();

        }
    }

    private void makeEnemyShoot()
    {
        GameObject enemyLaserr = Instantiate(enemyLaserPrefab, transform.position, Quaternion.identity) as GameObject;
        enemyLaserr.GetComponent<Rigidbody2D>().velocity = new Vector2(0, enemyVelocityY);    // inserting velocity code
        //Debug.Log("An enemy is shooting.");
        timeBetweenShots = timeBetweenShotsHolder; // we are assinging the variable timeBetweenShotsHolder which holds the original timeBetweenShots value, and we are assigning it to the timeBetweenShots variable. 
    }


    // list of beta methods
    private void instantiateParticleEffects()
    {
        GameObject blockSparkles = Instantiate(particleEffects, transform.position, Quaternion.identity) as GameObject;
        Debug.Log("The blockSparkles is being instantiated.");
        Destroy(blockSparkles, 1.0f);
    }

    private void instantiateExperimentalParticleEffects()
    {
        GameObject experimentalParticleHandle = Instantiate(experimentalParticleEffects, transform.position, Quaternion.identity) as GameObject;
        Destroy(experimentalParticleHandle, 1.0f);
    }

    private void playDestroyAudio()
    {
        AudioSource.PlayClipAtPoint(clipToPlay, Camera.main.transform.position);
    }

}

// notes
/*
 * this script is meant to deal with the enemy's laser.
 * it is best to keep the instantiation to this script. 
 *  
 *  
 * what we want to do in the countDownAndShoot method is have a timer essentially that will subtract until it hits zero. After it hits zero we want to instantiate the enemyLaserPrefab and then
 * reset the timer. 
 * 
 * 
 * 
 *  
 *  As of currently the enemy script in only attached to the first enemyPrefab. For testing purposes lets attach it to every enemy prefab and see what happens. 
 * 
 * 
 * enemy is not firing any lasers at all. It is not even instantiating but the waves are working as intended. 
 * FIXED What we did was change the condition of the if block in the countDownAndShoot method to >0 instead of != to zero because there is a huge chance that when the calculations are happening
 * the number may not even hit zero and just immediately go below zero. By changing the condition to less than zero we are covering for this possibliity as well. 
 * 
 * But now after timeBetweenShots has fell below zero  the lasers are simply instantiating. After we instantiate the laser it is continously instantiating and we need to reset the 
 * timeBetweenShots. THIS HAS BEEN DONE. 
 * 
 * We created a variable that is called timeBetweenShotsHolder. What this variable holds is the original value of timeBetweenShots. Within the makeEnemyShoot method after the enemyLaser has instantiated
 * we are assinging timeBetweenShotsHolder to the variable timeBetweenShots. This will allow time between the enemyLaser instantiations. 
 * 
 * The enemies are now firiing their lasers. However one thing that needs to be fixed is that some of the enemies are firiing their lasers outside the gameScreen. And player lasers and enemyLasers
 * are bouncing off each other. 
 * 
 * Seems like 240 seconds is an ideal time between shots. 
 * 
 * 
 * We are getting an error which is saying that an object reference is not set to a specific instance of the object. The error is specifying line 57 on the enemy script. To counter this what I am going to try
 * is to add a serialize field to our damageDealer which will allow us to hook up the specific damage dealer object we want. 
 * 
 * The above solution fixed the problem. One thing I am going to try is removing the damage dealer componenet from the player laser object as we do not need it attached there anymore. We have a damage dealer
 * object that is going to be applying the damage per enemyObject. I have removed it and the code works. 
 * 
 * 
 * I noticed that when the player lasers were making contact with the enemy they were not being destroyed. So I added code that should address that problem. found in 1). But this now causes a bug in which 
 * the enemy objects are not firing. Collisions are acting wonky as well. The moment I take out the code found in 1) everything else is fine but the player laser is not destroying upong colliding with the enemy gameObject. 
 * 
 * The above issue has been somewhat somewhat resolved. The issues witht the lasers are fine. But colissions between the enemyObject and player object do not seem to exist. Should this be fixed?
 * I think we should keep the game like this so collisions between enemy objects and player objects are not present. 
 * 
 * Next thing we need to fix is getting the score to update after each enemy has been destroyed. 
 * 
 * 
 * 
 * 
 * */
