using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Backpack
{

    #region Fields

    private Transform _player;
    private Vector3 _offset;
    private Transform _backpackTransform;
    
   
    #endregion

    #region Properties
    
    public int Capacity {get; private set;}

    public List<Resource> Lagguage {get; private set;}

    #endregion

    #region Constructors

    public Backpack(int capacity, Transform player)
    {
        _player = player;
        _backpackTransform = player.GetChild(0).GetChild(0).transform;
        _offset = new Vector3(0.0f, 0.0f, 0.0f);
        Capacity = capacity;
        Lagguage = new List<Resource>(capacity);
    }
        
    #endregion

    #region Public methods
        
    public bool TryUploadResource (Resource resource)
    {
        if (Lagguage.Count >= Capacity) return false;
        Lagguage.Add(resource);
        resource.ResourceController.transform.SetParent(_player);
        return true;
    }

    public Resource TryUnloadResource(Resource resource)
    {        
        Lagguage.Remove(resource);
        return resource;
    }

    

    /// <summary>
    /// Method for recalc resources position in stack
    /// </summary>
    public void UpdateBackpackStack()
    {
        foreach (Resource res in Lagguage)
        {
            res.ResourceController.transform.position = new Vector3(
                _backpackTransform.position.x, 
                2.2f + Lagguage.IndexOf(res) * 0.4f,
                _backpackTransform.position.z + _offset.z
            );
            res.ResourceController.transform.rotation = _backpackTransform.rotation;
        }
    }

    #endregion


}


