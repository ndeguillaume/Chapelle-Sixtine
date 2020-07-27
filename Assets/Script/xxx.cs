using HoloToolkit.Unity.InputModule;
using UnityEngine;
using UnityEngine.XR.WSA.Input;

public class xxx : MonoBehaviour
{
    private GameObject clone = null;
    private const float DefaultSizeFactor = 3.5f;
    private float SizeFactor = DefaultSizeFactor;

    public GameObject detail1 = null;
    public GameObject detail2 = null;

    private Vector3 scaleDetail;

    public void makeSmallerClone(GameObject gameObject)
    {
        Vector3 scale = gameObject.transform.localScale;
        scale /= SizeFactor;
        gameObject.transform.localScale = scale;
    }

    public void makeSmallerDetail(GameObject gameObject)
    {
        Vector3 scale;
        if (scaleDetail != null)
        {
            scale = scaleDetail;
        }
        else
        {
            scale = gameObject.transform.localScale;
        }
        scale /= SizeFactor;
        gameObject.transform.localScale = scale;
    }

    public void followingGaze()
    {
        Vector3 spawnPosition = Camera.main.transform.position + Camera.main.transform.forward * 2.0F;
        spawnPosition.Set(spawnPosition.x - 0.8F, spawnPosition.y, spawnPosition.z);
        clone.transform.position = spawnPosition;
        clone.transform.LookAt(Camera.main.transform);

        Vector3 spawnPosition2 = Camera.main.transform.position + Camera.main.transform.forward * 2.0F;
        spawnPosition2.Set(spawnPosition2.x + 0.8F, spawnPosition2.y + 0.5F, spawnPosition2.z);
        detail1.transform.position = spawnPosition2;
        detail1.transform.LookAt(Camera.main.transform, Vector3.up);

        Vector3 spawnPosition3 = Camera.main.transform.position + Camera.main.transform.forward * 2.0F;
        spawnPosition3.Set(spawnPosition3.x + 0.8F, spawnPosition3.y - 0.5F, spawnPosition3.z);
        detail2.transform.position = spawnPosition3;
        detail2.transform.LookAt(Camera.main.transform, Vector3.up);
    }

    public void spawnInteractions()
    {
        if (Variables.spawn == true)
        {
            foreach (GameObject obj in Variables.cloneList)
            {
                Destroy(obj);
            }
            Variables.cloneList.Clear();
            detail1.SetActive(false);
            detail2.SetActive(false);
        }
        if (Variables.spawn == false)
        {
            Vector3 spawnPosition = Camera.main.transform.position + Camera.main.transform.forward * 2.0F;
            spawnPosition.Set(spawnPosition.x - 0.8F, spawnPosition.y, spawnPosition.z + 2);
            clone = Instantiate(this.gameObject, spawnPosition, Camera.main.transform.rotation);
            clone.transform.LookAt(Camera.main.transform);
            makeSmallerClone(clone.gameObject);

            detail1.SetActive(true);
            Vector3 spawnPosition2 = Camera.main.transform.position + Camera.main.transform.forward * 2.0F;
            spawnPosition2.Set(spawnPosition2.x + 0.8F, spawnPosition2.y + 0.5F, spawnPosition2.z);
            detail1.transform.position = spawnPosition2;
            detail1.transform.LookAt(Camera.main.transform, Vector3.up);
            makeSmallerDetail(detail1.gameObject);

            detail2.SetActive(true);
            Vector3 spawnPosition3 = Camera.main.transform.position + Camera.main.transform.forward * 2.0F;
            spawnPosition3.Set(spawnPosition3.x + 0.8F, spawnPosition3.y - 0.5F, spawnPosition3.z);
            detail2.transform.position = spawnPosition3;
            detail2.transform.LookAt(Camera.main.transform, Vector3.up);
            makeSmallerDetail(detail2.gameObject);

            Variables.cloneList.Add(clone);
        }
        Variables.spawn = !Variables.spawn;
    }

    private void SourceManager_SourceUpdated(InteractionSourceUpdatedEventArgs obj)
    {
        InteractionSourcePose statePose = obj.state.sourcePose;

        if (obj.state.source.handedness == InteractionSourceHandedness.Right)
        {
            Debug.Log("InteractionSourceHandedness.Right");
        }

        if (obj.state.source.handedness == InteractionSourceHandedness.Left)
        {
            Debug.Log("InteractionSourceHandedness.Left");
        }

        if (obj.state.source.handedness == InteractionSourceHandedness.Unknown)
        {
            Debug.Log("InteractionSourceHandedness.Unknown");
        }
    }

    void SourceManager_SourceDetected(InteractionSourceDetectedEventArgs args)
    {
        // Source was detected
        // args.state has the current state of the source including id, position, kind, etc.
    }

    void SourceManager_SourceLost(InteractionSourceLostEventArgs state)
    {
        // Source was lost. This will be after a SourceDetected event and no other events for this
        // source id will occur until it is Detected again
        // args.state has the current state of the source including id, position, kind, etc.
    }

    void SourceManager_SourcePressed(InteractionSourcePressedEventArgs state)
    {
        // Source was pressed. This will be after the source was detected and before it is 
        // released or lost
        // args.state has the current state of the source including id, position, kind, etc.
    }

    void SourceManager_SourceReleased(InteractionSourceReleasedEventArgs state)
    {
        // Source was released. The source would have been detected and pressed before this point. 
        // This event will not fire if the source is lost
        // args.state has the current state of the source including id, position, kind, etc.
    }

    private void Awake()
    {
        if (SizeFactor <= 0.0f)
        {
            SizeFactor = DefaultSizeFactor;
        }
    }

    // Use this for initialization
    void Start()
    {
        //InputManager.Instance.PushFallbackInputHandler(gameObject);
        scaleDetail = detail1.transform.localScale;

        // Setting up events for the interaction manager
        InteractionManager.InteractionSourceDetected += SourceManager_SourceDetected;
        InteractionManager.InteractionSourceLost += SourceManager_SourceLost;
        InteractionManager.InteractionSourcePressed += SourceManager_SourcePressed;
        InteractionManager.InteractionSourceReleased += SourceManager_SourceReleased;
        InteractionManager.InteractionSourceUpdated += SourceManager_SourceUpdated;

        InteractionManager.GetCurrentReading();
    }

    // Update is called once per frame
    void Update()
    {
        InteractionManager.GetCurrentReading();

        //spawnInteractions();
        if (clone != null)
        {
            //followingGaze();
        }
    }

    void OnDestroy()
    {
        InteractionManager.InteractionSourceDetected -= SourceManager_SourceDetected;
        InteractionManager.InteractionSourceLost -= SourceManager_SourceLost;
        InteractionManager.InteractionSourcePressed -= SourceManager_SourcePressed;
        InteractionManager.InteractionSourceReleased -= SourceManager_SourceReleased;
        InteractionManager.InteractionSourceUpdated -= SourceManager_SourceUpdated;
    }

}