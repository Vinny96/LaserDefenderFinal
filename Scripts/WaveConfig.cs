using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Enemy Wave Config")]
public class WaveConfig : ScriptableObject
{
    // configuration parameters
    [SerializeField] GameObject enemyPrefab = null;
    [SerializeField] GameObject pathPrefab = null;
    [SerializeField] float waveSpeed = 0;
    [SerializeField] int numberOfEnemiesPerWave = 0;
    [SerializeField] float timeBetweenSpawns = 0;
    
    
    // variables
    
    // list of methods
    public GameObject getEnemyPrefab()
    {
        return enemyPrefab;
    }

    public List<Transform> getWayPoints()
    {
        var waveWayPoints = new List<Transform>();
        foreach(Transform child in pathPrefab.transform)
        {
            waveWayPoints.Add(child);
        }
        return waveWayPoints;
    }

    public float getWaveSpeed()
    {
        return waveSpeed;
    }

    public int getNumberOfEnemiesPerWave()
    {
        return numberOfEnemiesPerWave;
    }

    public float getTimeBetweenSpawns()
    {
        return timeBetweenSpawns;
    }
}

    // notes
    // 
