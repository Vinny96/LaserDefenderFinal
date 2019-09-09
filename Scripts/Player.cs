using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Player : MonoBehaviour
{

    //  variables

    // player movement 
    float xPos;
    float yPos;
    [SerializeField] float moveSpeed = 0;

    float xMax;
    float yMax;
    float xMin;
    float yMin;

    // player's laser 
    [SerializeField] float laserVelcoityYValue = 10f;
    [SerializeField] GameObject playerLaser = null;
    [SerializeField] float timeBetweenShots = 1.0f;
    Coroutine fireHandle;
    [SerializeField] int playersHealth = 500; // each enemy laser will result in 25 damage to the players health. 
    [SerializeField] TextMeshProUGUI playerHealthDisplay = null;
    

    // other player variables
    [SerializeField] AudioClip clipToPlay = null;
    [SerializeField] GameObject particleEffects = null;

    // scenemanager variables
    SceneLoader sceneloaderHandle;
    Coroutine sceneCoroutine;


    // Start is called before the first frame update
    void Start()
    {
        sceneloaderHandle = FindObjectOfType<SceneLoader>();
        displayPlayersHealth();
    }

    // Update is called once per frame
    void Update()
    {
        movePlayer();
        playerFire();
    }

    // list of methods
    private void movePlayer()
    {
        Camera gameCamera = Camera.main;
        xMin = gameCamera.ViewportToWorldPoint(new Vector3(0.05f, 0, 0)).x;
        xMax = gameCamera.ViewportToWorldPoint(new Vector3(0.95f, 0, 0)).x;
        yMin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0.04f, 0)).y;
        yMax = gameCamera.ViewportToWorldPoint(new Vector3(0, 0.98f, 0)).y;

        xPos = transform.position.x + (Input.GetAxis("Horizontal") * moveSpeed * Time.deltaTime);
        yPos = transform.position.y + (Input.GetAxis("Vertical") * moveSpeed * Time.deltaTime);
        transform.position = new Vector2(Mathf.Clamp(xPos, xMin, xMax), Mathf.Clamp(yPos, yMin, yMax));
    }

    private void playerFire()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            fireHandle = StartCoroutine(fireContinously());
        }
        if(Input.GetButtonUp("Fire1"))
        {
            StopCoroutine(fireHandle);
        } 
    }

    // list of coroutines
    IEnumerator fireContinously()
    {
        while (true)
        {
            GameObject laserHandle = Instantiate(playerLaser, transform.position, Quaternion.identity) as GameObject;
            laserHandle.GetComponent<Rigidbody2D>().velocity = new Vector2(0, laserVelcoityYValue);
           // Debug.Log("This line is running after the velocity code.");
            yield return new WaitForSeconds(timeBetweenShots);
        }
    }

    // we need to create a method that will decrease the players health. 
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (playersHealth > 0)
        {
            Destroy(collision.gameObject); // this line is working.
            playPlayerHitAudio();
            decreasePlayersHealth();
            displayPlayersHealth();
        }
        else 
        {
            Destroy(gameObject);
            playerDestroyedEffects();
            playPlayerHitAudio();
            Destroy(collision.gameObject);
            sceneloaderHandle.gameOverScene();
        }

    }
    // list of beta methods
    public int getPlayersHealth()
    {
        return playersHealth;
    }

    public int decreasePlayersHealth()
    {
        Debug.Log(playersHealth);
        playersHealth -= 25; // change to 25 after
        Debug.Log(playersHealth);
        return playersHealth;
    }

    private void displayPlayersHealth()
    {
        playerHealthDisplay.text = playersHealth.ToString();
    }

    private void playPlayerHitAudio()
    {
        AudioSource.PlayClipAtPoint(clipToPlay, Camera.main.transform.position);
    }

    private void playerDestroyedEffects()
    {
        GameObject playerDestroyedParticles = Instantiate(particleEffects, transform.position, Quaternion.identity) as GameObject;
    }

    
} 
/*NOTES
 * None of the playerHealthDisplay.text code seems to be working. 
 * I think maybe it is a good idea if we create a class which will keep track of the score and possibly the players health as well. Class can be called gameTracker ??
 * 
 * Within our OnTriggerEnter2d method we called a method that belonged to gameStatus. What this does is it displays the new health in the textMeshProUGUI element found in GameStatus. 
 * 
 * 
 * right now the enemyLaser has the isTrigger checked right next to it. Im going to uncheck it and put the code in the onTrigger method into a onCollision method. DOES NOT WORK
 * 
 * The player laser code has been moved to the player code.
 * However there are still issues in getting the playerhealth to display in the playerhealth display. 
 * HEALTH DISPLAYING CODE NOW WORKS 
 * 
 * Issues with the coroutine to load gameoverScene
 * Nothing is working after the yield return new waitforseconds line but the method for loading the gameOver scene is working as I have tested this line above the yield line and it works. So there
 * is something going on witht the yield return line that is preventing any code that is after it from running. Code can run after the yield line this was tested in the fire method. 
 * FIXED
 * This now works remember it is always best to put the calling of the coroutine within a method. This was not being done before which was causing the problem. Remember to put the code of the coroutine within a coroutine itself and to 
 * do the calling of the coroutine within a method. 
 * 
 * 
 * 
 *  
     */


