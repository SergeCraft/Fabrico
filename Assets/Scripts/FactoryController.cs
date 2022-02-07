using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class FactoryController : MonoBehaviour
{
    #region Fields

    private List<float> _startTimes;
    private Production _production;
    private StorageReqController _storageReq;
    private StorageProdController _storageProd;
    private ResourceController _resourceController;
    private Vector3 _storageProdLocation;

    private bool _playerInReqZone;
    private bool _playerInProdZone;

    private List<Translation> _translations;
    private Translation _actualTranslation;

    private Text _infoPanel;

    #endregion


    #region Variables

    public FactorySettings Settings;

    public FactoryStates State;


    #endregion

    #region Monobehaviour impl

    // Start is called before the first frame update
    void Start()
    {
        _infoPanel = GameObject.Find("Canvas/Scroll View/Viewport/Content").GetComponent<Text>();
        _storageReq = GetComponent<StorageReqController>();
        _storageProd = GetComponent<StorageProdController>();
        _resourceController = GameObject.Find("Game/Resources").GetComponent<ResourceController>();
        _production = new Production(
            Settings.ProducingResource,
            Settings.RequiredResources,
            Settings.ProducingTime);
        _storageProdLocation = transform.GetChild(5).transform.position;
        _playerInReqZone = false;
        _playerInProdZone = false;
        _translations = new List<Translation>();
        

		State = FactoryStates.Starting;

    }

    // Update is called once per frame
    void Update()
    {
        CheckState();
        PerformActions();
        PerformTranslations();

    }

	#endregion

	#region Public methods

    public void SetPlayerInReqZone (bool isInZone, GameObject player)
	{
        _playerInReqZone = isInZone;
        if(_playerInReqZone)
		{
            CheckPlayerHaveResources(player);
		}
		else
		{
            TryAbortTranslations(player, TranslationDirections.ToFactory);
        };
	}

    public void SetPlayerInProdZone (bool isInZone, GameObject player)
	{
        _playerInProdZone = isInZone;
        if(_playerInProdZone)
		{
            CheckPlayerHaveFreeSpace(player);
        }
		else
		{
            TryAbortTranslations(player, TranslationDirections.ToPlayer);
        };

	}

	#endregion

	#region Private methods
    private void TryBeginProduction()
    {
        TryConsumeResources();
        CheckProdStorage();

    }

    private void CheckState()
	{
        switch(State)
		{
            case FactoryStates.Finished:
                return;

            case FactoryStates.Producing:
                return;


            default:
                TryBeginProduction();
                return;
		};
	}

    private FactoryStates SetState(FactoryStates newState)
    {
        if(State == newState) return State;
        switch(newState)
        {
            case FactoryStates.PausedLowResStorageFull:
                _infoPanel.text += $"{Settings.Name} is stopped - low resources and output storage is full\n";
                break;
            case FactoryStates.PausedLowRequiredResources:
                if(State == FactoryStates.PausedLowResStorageFull) break;
                    _infoPanel.text += $"{Settings.Name} is stopped - low resources\n";
                break;
            case FactoryStates.PausedStorageFull:
                if(State == FactoryStates.PausedLowResStorageFull) break;
                _infoPanel.text += $"{Settings.Name} is stopped - output storage is full\n";
                break;
            default:
                break;
        };
        return newState;
	}

	private void TryConsumeResources()
	{
        foreach (ResourceTypes res in _storageReq.RequiredResources)
		{
            if(_storageReq.ResourcesAtStorage.Find(x => x.Type == res) == null)
            {
                if (State == FactoryStates.PausedStorageFull)
				{
                    State = SetState(FactoryStates.PausedLowResStorageFull);                    
                    return;
                };
                State = SetState(FactoryStates.PausedLowRequiredResources);
                return;
            };
		}
        if(State == FactoryStates.PausedStorageFull)
		{
            return;
		}
        ConsumeResources();
        State = SetState(FactoryStates.Producing);
	}

	private void ConsumeResources()
	{
        foreach (ResourceTypes type in _storageReq.RequiredResources)
		{
            Resource resToRemove = _storageReq.ResourcesAtStorage.Find(x => x.Type == type);

            _storageReq.ResourcesAtStorage.Remove(resToRemove);
            _resourceController.RemoveResource(resToRemove);
		}
	}

    private void CheckProdStorage()
	{
        if(_storageProd.IsFull)
        {
            if (State == FactoryStates.PausedLowRequiredResources)
			{
                State = SetState(FactoryStates.PausedLowResStorageFull);
			}
            State = SetState(FactoryStates.PausedStorageFull);
            return;
        };
        if(State == FactoryStates.PausedLowRequiredResources) return;
        State = SetState(FactoryStates.Producing);
	}

    private void PerformActions()
	{
        switch(State)
		{
            case FactoryStates.Producing:
                _production.ElapsedTime += Time.deltaTime;
                if(_production.ElapsedTime > _production.ProductionTime)
                    State = SetState(FactoryStates.Finished);
                break;
            case FactoryStates.Finished:
                _production.ElapsedTime = 0.0f;
                _storageProd.ResourcesAtStorage.Add(
                    _resourceController.AddResource(
                    null,
                    _production.ResourceType,
                   _storageProdLocation + new Vector3(0.0f, 3.0f, 0.0f),
                    Vector3.left
                    )
                );
                State = SetState(FactoryStates.Starting);
                break;
            default: break;
		}
	}

    public void PerformTranslations()
    {
        if (_actualTranslation == null & _translations.Count > 0)
		{
            _actualTranslation = _translations.First();
		};
        if (_actualTranslation != null)
		{
            switch(_actualTranslation.State)
			{
                case TranslationStates.NotStarted:
                    _actualTranslation.Begin();
                    break;
                case TranslationStates.InProgress:
                    _actualTranslation.Continue();
                    break;
                case TranslationStates.Finished:
                    _translations.Remove(_actualTranslation);
                    _actualTranslation = _translations.FirstOrDefault();
                    break;
                case TranslationStates.Aborting:
                    _actualTranslation.Aborting();
                    break;
                case TranslationStates.Aborted:
                    _actualTranslation = null;
                    break;
                default:
                    Debug.Log($"Actual translation state undefined");
                    break;
			}
		}
    }

    private void CheckPlayerHaveResources(GameObject player)
	{
        List<Resource> availalbeResources = new List<Resource>();
        foreach (ResourceTypes res in _production.RequiredResourceTypes)
		{
            availalbeResources.AddRange(
                player.GetComponent<PlayerController>().Backpack.Lagguage.Where(x => x.Type == res)
                );
		};
        if (availalbeResources.Count > 0)
		{
            BeginTranslateResources(availalbeResources, player, TranslationDirections.ToFactory);
		}
        else
        {
            return;
		}
	}


    private void CheckPlayerHaveFreeSpace(GameObject player)
    {
        Backpack backpack = player.GetComponent<PlayerController>().Backpack;
        int availalbeSpace = backpack.Capacity - backpack.Lagguage.Count;

        List<Resource> availableResources = _storageProd.ResourcesAtStorage.Take(availalbeSpace).ToList();
        
        if(availableResources.Count > 0)
        {
            BeginTranslateResources(availableResources,  player, TranslationDirections.ToPlayer);
        }
        else
        {
            return;
        }
    }

    private void BeginTranslateResources(
        List<Resource> availableResources,
        GameObject player,
        TranslationDirections dir)
	{
        PlayerController pc = player.GetComponent<PlayerController>();
        foreach (Resource res in availableResources)
		{
            _translations.Add(new Translation(
                res,
                1.0f,
                this,
                pc,
                dir
                ));
		}
	}


    private void TryAbortTranslations(GameObject player, TranslationDirections dir)
    {
        Debug.Log("Aborting translations...");
        if(_actualTranslation != null) _actualTranslation.SetState(TranslationStates.Aborting);
        if(_translations.Count > 0)
        {
            List<Translation> translationsToRemove =
                _translations.Where(x => x.Direction == dir).ToList();
            foreach(Translation tr in translationsToRemove)
            {
                _translations.Remove(tr);
            }
        }

        Debug.Log("Aborting translations done.");
    }

    

    private void AssignResourceToReqStorage(Resource res)
	{
        _storageReq.ResourcesAtStorage.Add(res);
	}

    private void RemoveResourceFromProdStorage(Resource res)
	{
        _storageProd.ResourcesAtStorage.Remove(res);
	}

    #endregion

    #region Nested classes

    private class Production
    {
        #region Fields

        #endregion

        #region Properties

        public ResourceTypes ResourceType { get; private set; }

        public List<ResourceTypes> RequiredResourceTypes { get; private set; }

        public float ProductionTime { get; private set; }

        public float ElapsedTime { get; set; }


		#endregion

		#region Constructors

		public Production(
            ResourceTypes prodRes,
            List<ResourceTypes> reqRes,
            float prodTime)
		{
            ResourceType = prodRes;
            RequiredResourceTypes = reqRes;
            ProductionTime = prodTime;
            ElapsedTime = 0.0f;
		}

		#endregion

	}

    private class Translation
	{
        private Vector3 _lastPosition;
		public Resource Resource { get; private set; }
        public float ElapsedTime { get; private set; }
        public float RequiredTime { get; private set; }
        public FactoryController FactoryController { get; private set; } 
        public PlayerController PlayerController { get; private set; }

        public TranslationDirections Direction { get; private set; }

        public TranslationStates State { get; private set; }

        public GameObject Target { get; private set; }
        public GameObject Emitter { get; private set; }


		public Translation(
            Resource res,
            float reqTime,
            FactoryController fc,
            PlayerController pc,
            TranslationDirections dir)
		{
            Resource = res;
            RequiredTime = reqTime;
            ElapsedTime = 0.0f;
            FactoryController = fc;
            PlayerController = pc;
            Direction = dir;
            State = TranslationStates.NotStarted;
            _lastPosition = res.ResourceController.transform.position;

            if (Direction == TranslationDirections.ToFactory)
            {
                Target = fc.transform.GetChild(3).gameObject;
                Emitter = pc.transform.gameObject;
            }
			else
            {
                Target = pc.transform.gameObject;
                Emitter = fc.transform.GetChild(5).gameObject; 
            }
		}

        public void Begin()
        {
            Debug.Log($"Begining translation of {Resource.Name} ...");

            State = TranslationStates.InProgress;

            Debug.Log($"Begining translation of {Resource.Name} done.");
        }


        public void Continue()
		{
            ElapsedTime += Time.deltaTime;
            Vector3 targetPosition = Direction == TranslationDirections.ToFactory ?
                Target.transform.position + new Vector3(0.0f, 2.0f, 0.0f) :
                Target.transform.position + new Vector3(0.0f, 1.0f, 0.0f);

            Resource.ResourceController.transform.position = Vector3.Lerp(
                Emitter.transform.position,
                targetPosition,
                (ElapsedTime / RequiredTime)
                 );
            _lastPosition = Resource.ResourceController.transform.position;
            if (ElapsedTime > RequiredTime)
			{
                Finish();
			}
		}
        public void Finish()
		{
            Debug.Log($"Finishing translation of {Resource.Name} ...");

            State = TranslationStates.Finished;
            if(Direction == TranslationDirections.ToFactory)
            {
                PlayerController.Backpack.TryUnloadResource(Resource);
                FactoryController.AssignResourceToReqStorage(Resource);
                Resource.ResourceController.transform.SetParent(GameObject.Find("Game/Resources").transform);
            }
			else
			{
                FactoryController.RemoveResourceFromProdStorage(Resource);
                PlayerController.Backpack.TryUploadResource(Resource);
                Resource.ResourceController.transform.SetParent(GameObject.Find("Player/Backpack").transform);
            }

            Debug.Log($"Finishing translation of {Resource.Name} done");
        }

        public void Aborting()
        {
            Debug.Log($"Aborting translation of {Resource.Name} ...");

            ElapsedTime += Time.deltaTime;
            Vector3 EmissionPosition = 
                Direction == TranslationDirections.ToFactory ?
                Emitter.transform.position + new Vector3(0.0f, 0.0f, 0.0f) :
                Emitter.transform.position + new Vector3(0.0f, 2.0f, 0.0f);


            Resource.ResourceController.transform.position = Vector3.Lerp(
                _lastPosition,
                EmissionPosition,
                (ElapsedTime / RequiredTime));
            if (ElapsedTime > RequiredTime)
			{
                Abort();
			}

            Debug.Log($"Aborting translation of {Resource.Name} done.");
        }
        public void Abort()
		{
            State = TranslationStates.Aborted;
            Debug.Log($"Translation of {Resource.Name} aborted.");
        }

        public void SetState(TranslationStates state)
		{
            State = state;
		}

	}

	#endregion
}


