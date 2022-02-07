using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceController : MonoBehaviour
{
	#region Fields

	private ResourceSettings _settings;

    private GameController _gameController; 

    #endregion

    #region Variables

    public ResourceSpawner Spawner;

    public List<Resource> ResourceList;

	#endregion

	#region MonoBehaviuor impl

	// Start is called before the first frame update
	void Start()
    {
        _gameController = GameObject.Find("Game").GetComponent<GameController>();
        _settings = _gameController.Settings.ResourceSettings;

        ResourceList = new List<Resource>();
        Spawner = new ResourceSpawner(_settings.ResourceSpawnerSettings);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    #endregion

    #region Public methods

    public Resource AddResource(
        string name,
        ResourceTypes type,
        Vector3 pos,
        Vector3 rot
        )
	{
        if(name == null || name == "") name = $"res{ResourceList.Count}";

        Resource res = Spawner.SpawnResource(
            name,
            type,
            pos,
            rot);
        ResourceList.Add(res);
        return res;
	}

    public void RemoveResource (Resource res)
	{
        Destroy(res.ResourceController);
        ResourceList.Remove(res);
        
    }

	#endregion

	#region Private methods

    private void TestGivePlayerResource()
	{
        GameObject player = GameObject.Find("Player");
        Resource res = AddResource(
            "",
            ResourceTypes.Resource1,
            player.transform.position,
            player.transform.rotation.eulerAngles
            );
        player.GetComponent<PlayerController>().Backpack.TryUploadResource(res);
        res = AddResource(
            "",
            ResourceTypes.Resource1,
            player.transform.position,
            player.transform.rotation.eulerAngles
            );
        player.GetComponent<PlayerController>().Backpack.TryUploadResource(res);
        res = AddResource(
            "",
            ResourceTypes.Resource3,
            player.transform.position,
            player.transform.rotation.eulerAngles
            );
        player.GetComponent<PlayerController>().Backpack.TryUploadResource(res);
	}

	#endregion
}
