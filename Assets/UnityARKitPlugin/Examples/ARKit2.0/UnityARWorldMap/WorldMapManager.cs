using System.IO;
using UnityEngine;
using UnityEngine.XR.iOS;

public class WorldMapManager : MonoBehaviour
{
    [SerializeField]
    UnityARCameraManager m_ARCameraManager;
    public Material planesMaterial;
    public Material particlesMaterial;

    ARWorldMap m_LoadedMap;

	serializableARWorldMap serializedWorldMap;

    // Use this for initialization
    void Start ()
    {
        UnityARSessionNativeInterface.ARFrameUpdatedEvent += OnFrameUpdate;
    }

    ARTrackingStateReason m_LastReason;

    void OnFrameUpdate(UnityARCamera arCamera)
    {
        if (arCamera.trackingReason != m_LastReason)
        {
            Application.SetStackTraceLogType(LogType.Log, StackTraceLogType.None);
            Debug.LogFormat("worldTransform: {0}", arCamera.worldTransform.column3);
            Debug.LogFormat("trackingState: {0} {1}", arCamera.trackingState, arCamera.trackingReason);
            m_LastReason = arCamera.trackingReason;
        }
    }

    static UnityARSessionNativeInterface session
    {
        get { return UnityARSessionNativeInterface.GetARSessionNativeInterface(); }
    }

    static string path
    {
        get { return Path.Combine(Application.persistentDataPath, "myFirstWorldMap.worldmap"); }
    }

    void OnWorldMap(ARWorldMap worldMap)
    {
        if (worldMap != null)
        {
            worldMap.Save(path);
            Debug.LogFormat("ARWorldMap saved to {0}", path);
        }
    }

    public void Save()
    {
        session.GetCurrentWorldMapAsync(OnWorldMap);
    }

    public void Load()
    {
        Debug.LogFormat("Loading ARWorldMap {0}", path);
        var worldMap = ARWorldMap.Load(path);
        if (worldMap != null)
        {
            m_LoadedMap = worldMap;
            Debug.LogFormat("Map loaded. Center: {0} Extent: {1}", worldMap.center, worldMap.extent);

            UnityARSessionNativeInterface.ARSessionShouldAttemptRelocalization = true;

            var config = m_ARCameraManager.sessionConfiguration;
            config.worldMap = worldMap;
			UnityARSessionRunOption runOption = UnityARSessionRunOption.ARSessionRunOptionRemoveExistingAnchors | UnityARSessionRunOption.ARSessionRunOptionResetTracking;

			Debug.Log("Restarting session with worldMap");
			session.RunWithConfigAndOptions(config, runOption);

            startExperiment();
        }
    }


	void OnWorldMapSerialized(ARWorldMap worldMap)
	{
		if (worldMap != null)
		{
			//we have an operator that converts a ARWorldMap to a serializableARWorldMap
			serializedWorldMap = worldMap;
			Debug.Log ("ARWorldMap serialized to serializableARWorldMap");
		}
	}


	public void SaveSerialized()
	{
		session.GetCurrentWorldMapAsync(OnWorldMapSerialized);
	}

	public void LoadSerialized()
	{
		Debug.Log("Loading ARWorldMap from serialized data");
		//we have an operator that converts a serializableARWorldMap to a ARWorldMap
		ARWorldMap worldMap = serializedWorldMap;
		if (worldMap != null)
		{
			m_LoadedMap = worldMap;
			Debug.LogFormat("Map loaded. Center: {0} Extent: {1}", worldMap.center, worldMap.extent);

			UnityARSessionNativeInterface.ARSessionShouldAttemptRelocalization = true;

			var config = m_ARCameraManager.sessionConfiguration;
			config.worldMap = worldMap;
			UnityARSessionRunOption runOption = UnityARSessionRunOption.ARSessionRunOptionRemoveExistingAnchors | UnityARSessionRunOption.ARSessionRunOptionResetTracking;

			Debug.Log("Restarting session with worldMap");
			session.RunWithConfigAndOptions(config, runOption);

            startExperiment();
		}

	}

    private void startExperiment()
    {
        Color color = planesMaterial.color;
        color.a = 0;
        planesMaterial.color = color;
        color = particlesMaterial.color;
        color.a = 0;
        particlesMaterial.color = color;
        GameObject.Find("HitCube").GetComponent<UnityARHitTestExample>().enabled = false;
        GameObject.Find("GeneratePlanes").gameObject.SetActive(false);
        GameObject.Find("Canvas").SetActive(false);
        GameObject.Find("PointCloudParticleExample").SetActive(false);
        UnityARSessionNativeInterface.ARAnchorAddedEvent -= FindObjectOfType<UnityARGeneratePlane>().unityARAnchorManager.AddAnchor;
        UnityARSessionNativeInterface.ARAnchorUpdatedEvent -= FindObjectOfType<UnityARGeneratePlane>().unityARAnchorManager.UpdateAnchor;
        UnityARSessionNativeInterface.ARAnchorRemovedEvent -= FindObjectOfType<UnityARGeneratePlane>().unityARAnchorManager.RemoveAnchor;
        FindObjectOfType<GeneratorRunner>().isRunning = true;
    }
}
