using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceSpawner
{

    #region Fields

    private GameObject _resourcePrefab;
    private GameObject _resources;

    #endregion

    #region Properties

    public ResourceSpawnerSettings Settings {get; private set;}

    #endregion


    #region Constructors

    public ResourceSpawner(ResourceSpawnerSettings settings)
    {
        Settings = settings;
        _resourcePrefab = Resources.Load<GameObject>(Settings.PrefabPath);
        _resources = GameObject.Find("Game/Resources");
    }
        
    #endregion

    #region Public methods

    public Resource SpawnResource(string name, ResourceTypes type, Vector3 pos, Vector3 rot)
    {
        GameObject model = GameObject.Instantiate<GameObject>(
            _resourcePrefab,
            pos,
            Quaternion.Euler(rot));

        model.GetComponent<MeshRenderer>().material.color = Helper.GetColorByResourceType(type);
        model.transform.SetParent(_resources.transform);

        return new Resource(name, model, pos, rot, type);
    }
    #endregion

    #region Private methods

        
    #endregion

}
