using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPathing : MonoBehaviour
{
    //  configuration parameters
    List<Transform> listOfWayPoints = null; // we no longer need a serialize field here as our waveconfig object will get all the waypoints programatically.
   // [SerializeField] float enemySpeed = 5f; we are now getting the speed programatically from our waveconfig object. 
     WaveConfig waveConfig = null; // the serialize field will be removed from the waveConfig object. 
    
    //  variables
    int listOfWayPointsIndex = 0;
  


    // beta variables
   // Coroutine enemyMovingHandle;

    // Start is called before the first frame update
    void Start()
    {
        listOfWayPoints = waveConfig.getWayPoints();
        transform.position = listOfWayPoints[0].transform.position; // when the game first runs the enemy's location is that of the first waypoint. 
        listOfWayPointsIndex++;
       // waveCounter++;
       // Debug.Log(listOfWayPointsIndex);  
    }

    // Update is called once per frame
    void Update()
    {
        moveEnemyOnPath();
    }

    // list of methods
    private void moveEnemyOnPath()
    {
        if (gameObject.transform.position != listOfWayPoints[listOfWayPoints.Count - 1].transform.position)
        {
            transform.position = Vector2.MoveTowards(transform.position, listOfWayPoints[listOfWayPointsIndex].transform.position, waveConfig.getWaveSpeed() * Time.deltaTime);
            if (transform.position == listOfWayPoints[listOfWayPointsIndex].transform.position)
            {
                listOfWayPointsIndex++;
              // Debug.Log(listOfWayPointsIndex);
            }
        }
        else
        {
            Destroy(gameObject);
           // Debug.Log("The enemy gameObject has been destroyed.");
        }

    }

    public void setWaveConfig (WaveConfig waveConfigToSet) // this method will allows us to set the waveConfig we want as we removed the serializeField from the waveConfig above
    {
        this.waveConfig = waveConfigToSet; // here what we are doing is which ever class calls this method, the waveConfig they are passing into the method wil be assigned as the variables waveConfig. 
    }
   
    // list of coroutines

}   
   
    
    
    // beta methods and coroutines
    /*private void allowEnemyToMoveOnPaths()
    {
        if (waveCounter < 2)
        {
            enemyMovingHandle = StartCoroutine(getEnemyMovingOnPath());
            // waveCounter++; // I think its because while the coroutine is still executing we are increasing the wave counter, and the wave counter is getting to two right away. yet the enemy object has yet to move. This is the issue!!
        }
        else
        {
            StopCoroutine(enemyMovingHandle);
            Debug.Log("The stop coroutine method is executing.");
        }
    }

    private void testingCoRoutine()
    {
        enemyMovingHandle = StartCoroutine(getEnemyMovingOnPath());
    }
    IEnumerator getEnemyMovingOnPath()
    {
        if (gameObject.transform.position != listOfWayPoints[listOfWayPoints.Count - 1].transform.position)
        {
            transform.position = Vector2.MoveTowards(transform.position, listOfWayPoints[listOfWayPointsIndex].transform.position, enemySpeed * Time.deltaTime);
            if (gameObject.transform.position == listOfWayPoints[listOfWayPointsIndex].transform.position)
            {
                // this block is not getting executed either.
                listOfWayPointsIndex++;

                Debug.Log(listOfWayPointsIndex);
                Debug.Log("The enemy object is moving.");
            }
        }
        else
        {
            waveCounter++;
            Debug.Log("The following number below is that of the wave counter");
            Debug.Log(waveCounter);
            Destroy(gameObject);

            Debug.Log("The enemy gameObject has been destroyed.");
            listOfWayPointsIndex = 0;
            Debug.Log(listOfWayPointsIndex);
            yield return new WaitForSeconds(timeBetweenSpawns);
        }
    }*/


    
    
    
   // notes
    // remember that what a coroutine is, it is basically a method that can halt execution and resume in the following frame assuming all conditions were met. When it resumes execution
    // it does not loop back to the begining of the coroutine and executes itself again. All it does is halt execution at the return line then resumes where it left off in the following frame
    // assumming the conditions have been met. 

    // the original plan was to have this execute in an infinite loop but this is not possible. The reason why this is not possible is we cannot get the gameObject to instantiate itself after it has been destroyed.
    // even if we make it a prefab and attach to itself the moment it gets destroyed the attached prefab gets destroyed too. So this simply is not possible. 


    // One thing that I have noticed is that within our method if we remove the Destroy(gameObject) or if we relocate it to within our method we get an infinite loop wih the debug.log
    // (the enemy gameObject has been destroyed) running constantly. The reason why this happens is because within our coroutine once we have exceeded the last waypoint index that condition
    // no longer evaluates to true. This means that the else block is going to continously implemented and because the if condition is always going to be false, the else block will cotinously run.
    // However the moment we put the destroy(gameobject) in the else block the gameObject no longer exists so the if else block cannot be implemented anymore. 

    // we created a waveconfig object with the serializeField in front of it as we want to be able to hook up the specific wave asset file to our enemy object in Unity. The reason why we want to do this
    // is because the wave asset files have data in it that is specific to each wave and has its own unique set of waypoints. 

    //  one pointer when it comes to creating new paths. Duplicate the path first then adjust the points to whatever you want. After you have adjusted the points, then prefab it and your good to go.
    // however if you want to edit the points you are going to have to delete the entire duplicated path and redo the process. 

    // Ther reason why the setWaveConfig method is being created in the enemyPathing is because it is in this class that we have the waveConfig variable that we are going to use. What the method does is whenever
    // we call it from the other class, the waveConfig object that was passed in as the argument to the parameter waveConfig to set, becomes the waveConfig that this class is referring too. This is possible because
    // in the method this line of code is present this.waveConfig = waveConfigToSet. What this does is whatever waveConfig that was passed into the method that waveConfig "becomes" the waveConfig for the class. 