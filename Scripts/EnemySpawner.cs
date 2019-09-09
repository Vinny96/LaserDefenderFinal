using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    // configuration parameters
    [SerializeField] List<WaveConfig> listOfWaveConfigs = null;
    [SerializeField] int waveConfigIndex = 0;


    // list of beta variables
    [SerializeField] bool IsLoopingOn = false;
    
    // Start is called before the first frame update
    IEnumerator Start() // this was originally of the type void but we are going to make it be a type Coroutine. It did not have a do while loop either. 
    {
        WaveConfig currentWave = listOfWaveConfigs[waveConfigIndex];
        do
        {
            yield return StartCoroutine(SpawnAllWaves());
        }
        while (IsLoopingOn);    
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // list of coroutines
    private IEnumerator instantiateEnemies(WaveConfig waveConfig)
    {
        for(int numberOfEnemies = 0; numberOfEnemies <= waveConfig.getNumberOfEnemiesPerWave(); numberOfEnemies++)
        {
           var newEnemy = Instantiate(waveConfig.getEnemyPrefab(), waveConfig.getWayPoints()[waveConfigIndex].transform.position, Quaternion.identity);
            //Debug.Log("An enemy gameObject has been instantiated.");
            newEnemy.GetComponent<EnemyPathing>().setWaveConfig(waveConfig); // here what we are doing is getting the component that is attached to our newEnemy object and we are getting the EnemyPathing script which is a component that is attached to our EnemyGame Object. 
            yield return new WaitForSeconds(waveConfig.getTimeBetweenSpawns());
        }
    }

    private IEnumerator SpawnAllWaves()
    {
        for(int numberOfWavesSpawned = waveConfigIndex; numberOfWavesSpawned <= listOfWaveConfigs.Count-1; numberOfWavesSpawned++) 
        {
            WaveConfig currentWave = listOfWaveConfigs[numberOfWavesSpawned];
            yield return StartCoroutine(instantiateEnemies(currentWave));
        }
    }


    // list of beta coroutines and methods
   

}

/*
 * Here is an explanatation of what we did so far. As a reminder this the enemySpawner script is assigned to the enemySpawner gameObject that we created. We created a list of WaveConfigs
 * as we want to be able to store all of our wave configurations. The end goal of this is to eventually have all the waveconfigurations spawn. 
 * We also created a waveConfig index variable has a starting point for our list. 
 * When we start the method we declare and initliaze our currentWave variable and we assign it the waveConfigIndex variable. By doing this it is assigned the first wave in our list. 
 * We also start our coroutine and we pass in the currentWave variable we just created as our parameter for this coroutine. 
 * 
 * A coroutine has been created to implement the instantiation of the enemy objects. The reason why a couroutine was chosen rather than regular method was we wanted to halt the execution of the coroutine
 * until a certain amount of time has passed then we resume execution. The coroutine will essentailly loop back to the begining and execute from there. This is possible because the coroutine is wrapped in a for loop so how this is going to work is at the yield line
 * after the waitforseconds has passed, it is then going to increment the numberOfEnemies, check the condition then instantiate the enemy again. This process just repeats itself until the condition in the for loop
 * no longer evaluates to true. 
 * 
 * UPDATES WITHIN COROUTINE
 * We assigned the instantiation of our enemy prefab to our var newEnemy object we have created. This object is of the type GameObject. What we then did was we did getComponent from our newEnemy object
 * and we got the EnemyPathing script from it. Keep in mind with the getComponent we can get any component that is attached to any of our gameObjects. The var newEnemy object is simply a gameObject and if we want
 * we can get the Player script, laser destroyer script etc. We can get any component and a script is a component that is attached to our gameObjects. We got the EnemyPathing component and we then called our
 * setWaveConfig method and passed in our waveConfig parameter from the instantiateEnemies coroutine as the parameter for the setWaveConfig method. What this is doing is whatever parameter we pass into our coroutine
 * is the same parameter that will get passed into our newEnemy.GetComponent code. 
 * 
 * What the setWaveConfig(waveConfig) method is specfically doing is whatever waveConfig object we pass into the methods paraemeter, that waveConfig now "becomes" the waveConfig for the enemyPathing
 * script. So what then happens is that the waveConfig that was passed in is the "wave" that the EnemyPathing script refers too, and it gets that specifics waveConfigs waypoints, speed, and the other variables that
 * have getter methods for them. That is the wave that the enemyobject within the enemypathing script will move along. 
 * 
 * 
 * SpawnAllWaves coroutine explanation
 * The goal of this coroutine is to spawn all enemy waves so it would be to spawn all wave configurations. We have a for loop in the wave and the variable numberOfWavesSpawned is assigned waveConfig
 * index. The reason for this is waveConfigsIndex is assigned the value of zero so we can essentially just reuse that same variable. We then have a local currentWave variable which is of type
 * waveConfig and this is assigned as of now the zeroth index in the listOfWaveConfigs. We are then halting execution of this coroutine until the instantiateEnemies coroutine has been executed. The 
 * reason for this is as we want all enemies within each waveConfiguration to instantiate before we move on to the next waveConfiguration. 
 * 
 * */