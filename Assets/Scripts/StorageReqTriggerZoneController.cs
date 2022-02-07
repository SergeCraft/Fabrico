using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StorageReqTriggerZoneController : MonoBehaviour
{
    private FactoryController _factoryController;

    // Start is called before the first frame update
    void Start()
    {
        _factoryController = transform.parent.GetComponent<FactoryController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

	private void OnTriggerEnter(Collider other)
	{
        Debug.Log($"Trigger: {other.gameObject.name} in ReqTriggerZone");
        if (other.name == "Player") 
            _factoryController.SetPlayerInReqZone(true, other.gameObject);
	}

	private void OnTriggerExit(Collider other)
	{
        Debug.Log($"Trigger: {other.gameObject.name} out of ReqTriggerZone");
        if(other.name == "Player")
            _factoryController.SetPlayerInReqZone(false, other.gameObject);
    }

}
