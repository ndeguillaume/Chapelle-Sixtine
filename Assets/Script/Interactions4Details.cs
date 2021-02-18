using HoloToolkit.Unity.InputModule;
using UnityEngine;

public class Interactions4Details : MonoBehaviour, IInputHandler, IFocusable, IPointerSpecificFocusable
{
    private GameObject clone = null;
    private const float DefaultSizeFactor = 3.5f;
    public float SizeFactor = DefaultSizeFactor;

    public GameObject detail1 = null;
    public GameObject detail2 = null;
    public GameObject detail3 = null;
    public GameObject detail4 = null;

    private Vector3 scaleDetail;

    //public float SizeFactor = 100.0f;
    //public float maxHitDistance = 500.0f;

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

    public void followingGaze ()
    {
        Vector3 spawnPosition = Camera.main.transform.position + Camera.main.transform.forward * 2.0F;
        spawnPosition.Set(spawnPosition.x - 0.8F, spawnPosition.y, spawnPosition.z);
        clone.transform.position = spawnPosition;
        clone.transform.LookAt(Camera.main.transform);

        Vector3 spawnPosition2 = Camera.main.transform.position + Camera.main.transform.forward * 2.0F;
        spawnPosition2.Set(spawnPosition2.x + 0.4F, spawnPosition2.y + 0.5F, spawnPosition2.z);
        detail1.transform.position = spawnPosition2;
        detail1.transform.LookAt(Camera.main.transform, Vector3.up);

        Vector3 spawnPosition3 = Camera.main.transform.position + Camera.main.transform.forward * 2.0F;
        spawnPosition3.Set(spawnPosition3.x + 1.2F, spawnPosition3.y + 0.5F, spawnPosition3.z);
        detail2.transform.position = spawnPosition3;
        detail2.transform.LookAt(Camera.main.transform, Vector3.up);

        Vector3 spawnPosition4 = Camera.main.transform.position + Camera.main.transform.forward * 2.0F;
        spawnPosition4.Set(spawnPosition4.x + 0.4F, spawnPosition4.y - 0.5F, spawnPosition4.z);
        detail3.transform.position = spawnPosition4;
        detail3.transform.LookAt(Camera.main.transform, Vector3.up);

        Vector3 spawnPosition5 = Camera.main.transform.position + Camera.main.transform.forward * 2.0F;
        spawnPosition5.Set(spawnPosition5.x + 1.2F, spawnPosition5.y - 0.5F, spawnPosition5.z);
        detail4.transform.position = spawnPosition5;
        detail4.transform.LookAt(Camera.main.transform, Vector3.up);
    }

    public void spawnInteractions ()
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
            detail3.SetActive(false);
            detail4.SetActive(false);
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
            spawnPosition2.Set(spawnPosition2.x + 0.4F, spawnPosition2.y + 0.5F, spawnPosition2.z);
            detail1.transform.position = spawnPosition2;
            detail1.transform.LookAt(Camera.main.transform, Vector3.up);
            makeSmallerDetail(detail1.gameObject);

            detail2.SetActive(true);
            Vector3 spawnPosition3 = Camera.main.transform.position + Camera.main.transform.forward * 2.0F;
            spawnPosition3.Set(spawnPosition3.x + 1.2F, spawnPosition3.y + 0.5F, spawnPosition3.z);
            detail2.transform.position = spawnPosition3;
            detail2.transform.LookAt(Camera.main.transform, Vector3.up);
            makeSmallerDetail(detail2.gameObject);

            detail3.SetActive(true);
            Vector3 spawnPosition4 = Camera.main.transform.position + Camera.main.transform.forward * 2.0F;
            spawnPosition4.Set(spawnPosition4.x + 0.4F, spawnPosition4.y - 0.5F, spawnPosition4.z);
            detail3.transform.position = spawnPosition4;
            detail3.transform.LookAt(Camera.main.transform, Vector3.up);
            makeSmallerDetail(detail3.gameObject);

            detail4.SetActive(true);
            Vector3 spawnPosition5 = Camera.main.transform.position + Camera.main.transform.forward * 2.0F;
            spawnPosition5.Set(spawnPosition5.x + 1.2F, spawnPosition5.y - 0.5F, spawnPosition5.z);
            detail4.transform.position = spawnPosition5;
            detail4.transform.LookAt(Camera.main.transform, Vector3.up);
            makeSmallerDetail(detail4.gameObject);

            Variables.cloneList.Add(clone);
        }
        Variables.spawn = !Variables.spawn;
    }

    public void OnInputDown(InputEventData eventData)
    {
        
    }

    public void OnInputUp(InputEventData eventData)
    {
        if (eventData.PressType == InteractionSourcePressInfo.Select)
        {
            spawnInteractions();
        }
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
        InputManager.Instance.PushFallbackInputHandler(gameObject);
        scaleDetail = detail1.transform.localScale;
    }

    // Update is called once per frame
    void Update ()
    {
        if (clone != null)
        {
            followingGaze();
        }
    }

    public void OnFocusEnter()
    {
        //Debug.Log("HIT");
    }

    public void OnFocusExit()
    {
        //Debug.Log("END HIT");
    }

    public void OnFocusEnter(PointerSpecificEventData eventData)
    {
        //Debug.Log("HIT");
    }

    public void OnFocusExit(PointerSpecificEventData eventData)
    {
        //Debug.Log("END HIT");
    }
}