using UnityEngine;
using System.Linq;
using System.Collections.Generic;


public class Factory
{
    #region Fields

        
    #endregion

    #region Properties

    public GameObject Model {get; private set;}

    public FactorySettings Settings {get; private set;}


    #endregion

    #region Constructors

    public Factory(FactorySettings settings, FactorySpawner spawner)
    {
        Settings = settings;
        Model = spawner.SpawnFactory(settings);
        FactoryController fController = Model.GetComponent<FactoryController>();
        fController.Settings = Settings;
        StorageReqController storageReqController = Model.GetComponentInChildren<StorageReqController>();
        storageReqController.RequiredResources = Settings.RequiredResources;
        storageReqController.Capacity = Settings.StorageCapacityRequired;
        StorageProdController storageProdController = Model.GetComponentInChildren<StorageProdController>();
        storageProdController.ProducingResource = Settings.ProducingResource;
        storageProdController.Capacity = Settings.StorageCapacityProduced;

        Model.transform.GetChild(0).GetComponent<MeshRenderer>().material.color = Settings.Color;
        Model.transform.GetChild(1).GetComponent<MeshRenderer>().material.color = Settings.Color;
        Model.transform.GetChild(2).GetComponent<MeshRenderer>().material.color = Settings.Color;
        Model.transform.GetChild(3).GetComponent<MeshRenderer>().material.color =
            Helper.GetColorByResourceType(Settings.RequiredResources.FirstOrDefault());
        Model.transform.GetChild(4).GetComponent<MeshRenderer>().material.color =
            Helper.GetColorByResourceType(Settings.RequiredResources.FirstOrDefault())/3;
        Model.transform.GetChild(5).GetComponent<MeshRenderer>().material.color = 
            Helper.GetColorByResourceType(Settings.ProducingResource);
        Model.transform.GetChild(6).GetComponent<MeshRenderer>().material.color = 
            Helper.GetColorByResourceType(Settings.ProducingResource)/3;


    }
        
    #endregion

    #region Public methods
        
    #endregion

    #region Private methods
        
    #endregion
}