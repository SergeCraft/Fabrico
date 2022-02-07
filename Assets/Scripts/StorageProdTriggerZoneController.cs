using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StorageProdTriggerZoneController : MonoBehaviour
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
        Debug.Log($"Trigger: {other.gameObject.name} in ProdTriggerZone");
        if(other.name == "Player")
            _factoryController.SetPlayerInProdZone(true, other.gameObject);
    }

    private void OnTriggerExit(Collider other)
    {
        Debug.Log($"Trigger: {other.gameObject.name} out of ProdTriggerZone");
        if(other.name == "Player")
            _factoryController.SetPlayerInProdZone(false, other.gameObject);
    }
}
