using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StorageReqController : MonoBehaviour
{

	#region Properties

	public List<ResourceTypes> RequiredResources { get; internal set; }
    public List<Resource> ResourcesAtStorage { get; private set; }
    public int Capacity { get; internal set; }

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
        
    }

	#endregion

}
