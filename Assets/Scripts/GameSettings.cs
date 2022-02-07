using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameSettings
{
    #region Properties
        
    public FactorySpawnerSettings FactorySpawnerSettings {get; private set;}

    public List<FactorySettings> FactoriesSettings {get; private set;}

    public ResourceSettings ResourceSettings {get; private set;}
    #endregion
    
    #region Public Variables

    
        
    #endregion

    #region Constructors

    public GameSettings()
    {
        ResetToDefault();
    }
    #endregion

    #region Public methods

    public void ResetToDefault()
    {
        FactorySpawnerSettings = new FactorySpawnerSettings();

        FactoriesSettings = new List<FactorySettings>()
        {
			new FactorySettings(
                "Red factory",
				new List<ResourceTypes>(0),
			   ResourceTypes.Resource1,
				2.0f),
			new FactorySettings(
                "Yellow factory",
				new List<ResourceTypes>(1){ResourceTypes.Resource1},
				ResourceTypes.Resource2,
				3.0f),
			new FactorySettings(
                "Blue factory",
				new List<ResourceTypes>(1){ResourceTypes.Resource1, ResourceTypes.Resource2},
				ResourceTypes.Resource3,
				4.0f)
		};

        ResourceSettings = new ResourceSettings();
       
    }
        
    #endregion


}


#region Factoy settings

public class FactorySettings
{

	#region Fields


	#endregion

	#region Properties

	public string Name { get; private set; }
	public List<ResourceTypes> RequiredResources {get; private set;}

    public ResourceTypes ProducingResource {get; private set;}

    public float ProducingTime {get; private set;}

    public int StorageCapacityRequired {get; private set;}
    public int StorageCapacityProduced {get; private set;}

    public List<ResourceTypes> StorageRequired {get; private set;}    
    public List<ResourceTypes> StorageProduced {get; private set;}

    public Color Color { get; private set; }

    #endregion

    #region Constructors

    public FactorySettings()
    {
        ResetToDefault();
    }

    public FactorySettings(
        string name,
        List<ResourceTypes> reqResources,
        ResourceTypes prodResources,
        float prodTime,
        int reqStorageCapacity = 3,
        int prodStorageCapacity = 3
        )
    {
        Name = name;
        RequiredResources = reqResources;
        ProducingResource = prodResources;
        ProducingTime = prodTime;
        StorageCapacityRequired = reqStorageCapacity;
        StorageCapacityProduced = prodStorageCapacity;
        StorageProduced = new List<ResourceTypes>(StorageCapacityProduced);
        StorageRequired = new List<ResourceTypes>(StorageCapacityRequired);
        Color = GetColorByResourceType(ProducingResource);
    }
        
    #endregion

    #region Public methods

    public void ResetToDefault()
    {
        Name = "Unknown Factory";
        RequiredResources = new List<ResourceTypes>();
        ProducingResource = ResourceTypes.Unknown;
        ProducingTime = UnityEngine.Random.Range(1.0f, 20.0f);
        StorageCapacityRequired = 3;
        StorageCapacityProduced = 3;
        StorageRequired = new List<ResourceTypes>(StorageCapacityRequired);
        StorageProduced = new List<ResourceTypes>(StorageCapacityProduced);
        Color = GetColorByResourceType(ProducingResource);
    }
    
    public Color GetColorByResourceType(ResourceTypes type)
    {
        switch(type)
        {
            case ResourceTypes.Resource1:
                return Color.red;
            case ResourceTypes.Resource2:
                return Color.yellow;
            case ResourceTypes.Resource3:
                return Color.blue;
            default:
                return Color.gray;
        }
    }

    #region Private methods

    #endregion

    #endregion
}
public class FactorySpawnerSettings
{
    #region Properties

    public string PrefabPath {get; private set;}

    #endregion

    #region Constructors
        
    public FactorySpawnerSettings()
    {
        ResetToDefault();
    }

    #endregion

    #region Public methods
        
    public void ResetToDefault()
    {
        PrefabPath = "Assets/Resources/Prefabs/Factory";
    }

    #endregion
}

#endregion

#region Resource settings

public class ResourceSettings
{
    #region Properties
    
    public ResourceSpawnerSettings ResourceSpawnerSettings {get; private set;}

    #endregion

    #region Constructors

    public ResourceSettings()
    {
        ResetToDefault();
    }
        
    #endregion

    #region Public methods

    public void ResetToDefault()
    {
        ResourceSpawnerSettings = new ResourceSpawnerSettings();
    }
        
    #endregion


}

public class ResourceSpawnerSettings
{
    #region Properties
        
    public string PrefabPath { get; private set; }

    #endregion

    #region Constructors

    public ResourceSpawnerSettings()
    {
        ResetToDefault();
    }
        
    #endregion

    #region Public methods

    public void ResetToDefault()
    {
        PrefabPath = "Prefabs/Resource1";
    }
        
    #endregion
}

#endregion