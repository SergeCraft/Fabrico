using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class FactorySpawner
{

    #region Fields

    private GameObject _factoryPrefab;

    private List<Vector3> _spawnedPositions;

    #endregion

    #region Properties 
    
    public FactorySpawnerSettings Settings {get; private set;}
               
    #endregion

    #region Constructors

    public FactorySpawner(FactorySpawnerSettings settings)
    {
        Settings = settings;
        _factoryPrefab = Resources.Load("Prefabs/Factory") as GameObject;
        _spawnedPositions = new List<Vector3>();

    }
        
    #endregion

    #region Public methods
    
    /// <summary>
    /// Spawn factories at random position of game field
    /// with considering player spawn position and another factories posistions
    /// </summary>
    /// <returns></returns>
    public List<GameObject> SpawnFactories(List<FactorySettings> settings)
    {

        List<GameObject> factories = new List<GameObject>();
        for (int i = 0; i < settings.Count; i++)
        {            
            GameObject factory = SpawnFactory(settings[i]);

            factories.Add(factory);
        }

        return factories;
    }

    
    /// <summary>
    /// Spawn factoriy at random position of game field
    /// with considering player spawn position and another factories posistions
    /// </summary>
    /// <returns></returns>
    public GameObject SpawnFactory(FactorySettings settings)
    {

        //spawnSample
        Vector3 spawnPos = new Vector3(
            Random.Range(-30.0f, 30.0f),
            0.0f,                
            Random.Range(-30.0f, 30.0f)
        );
        Vector3 playerSpawn = new Vector3(0.0f, 0.0f, 0.0f);

        //rerandom spawn position while it is less than 10 from center or less 10 from another factory
        while (_spawnedPositions.Any(x => 
            (x - spawnPos).magnitude < 10.0f || 
            (x - playerSpawn).magnitude < 5.0f))
        {
            spawnPos = new Vector3(
                Random.Range(-40.0f, 30.0f),
                0.0f,                
                Random.Range(-40.0f, 30.0f));
        }

        _spawnedPositions.Add(spawnPos);
        Quaternion spawnRot = Quaternion.Euler(new Vector3(0.0f, 210.0f, 0.0f));
        GameObject factory = GameObject.Instantiate<GameObject>(_factoryPrefab, spawnPos, spawnRot);
        factory.GetComponent<FactoryController>().Settings = settings;
        

        return factory;
    }

    #endregion

    #region Private methods
        
    #endregion
    
}
