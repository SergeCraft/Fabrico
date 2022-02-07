using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    #region Fields


    #endregion
    

    #region Public variables

    public GameSettings Settings;

    public ResourceController ResourceController;
    public List<Factory> Factories { get; private set; }

	#endregion


	#region MonoBehaviours interface impl

	private void Awake()
	{
        Settings = new GameSettings();
    }

	/// <summary>
	/// Start is called before the first frame update
	/// </summary>
	void Start()
    {        
        ResourceController = GameObject.Find("Resources").GetComponent<ResourceController>();
        SpawnFactories();
    }

    /// <summary>
    /// Update is called once per frame
    /// </summary>
    void Update()
    {
        
    }

	#endregion

	#region Private methods

    private void SpawnFactories()
	{
        Factories = new List<Factory>();
        FactorySpawner spawner = new FactorySpawner(Settings.FactorySpawnerSettings);
        foreach (FactorySettings stg in Settings.FactoriesSettings)
		{
            Factories.Add(new Factory(stg, spawner));
		};

    }

	#endregion

}


/// <summary>
/// Resource types enumeration. Supports 4 types: Unknown, Resource1, Resource2, Resource3
/// </summary>
public enum ResourceTypes
{
    Unknown,
    Resource1,
    Resource2,
    Resource3

}
