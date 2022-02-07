using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StorageProdController : MonoBehaviour
{
    #region Properties

    public ResourceTypes ProducingResource { get; set; }
    public List<Resource> ResourcesAtStorage { get; private set; }
    public int Capacity { get; set; }

    public bool IsFull { get; private set; }

    #endregion

    #region MonoBehaviour impl

    // Start is called before the first frame update
    void Start()
    {
        ResourcesAtStorage = new List<Resource>(Capacity);
    }

    // Update is called once per frame
    void Update()
    {
        CheckStorageIsFull();
    }

    #endregion

    #region Private methods

    private void CheckStorageIsFull() 
    {
         if(ResourcesAtStorage.Count >= Capacity) { IsFull = true; } else { IsFull = false; };
    }

	#endregion
}
