using UnityEngine;

public class NFTChecker : MonoBehaviour
{
    public Transform scrollViewContent;
    public Transform ignoredObject;
    public GameObject targetGameObject; // Reference to the GameObject you want to activate
    public GameObject HackText;
    private float timer;
    private bool isChecking;

    void Start()
    {
        timer = 0f;
        isChecking = true;
    }

    void Update()
    {
        timer += Time.deltaTime;

        if (isChecking && timer <= 5f)
        {
            CheckScrollViewContent();
        }
        else
        {
            isChecking = false;
            targetGameObject.SetActive(false); // Deactivate the targetGameObject

            if (!HasChildObjects())
            {
                ActivateGameObject();
            }
        }
    }

    void CheckScrollViewContent()
    {
        int childCount = scrollViewContent.childCount;
        bool hasChildObjects = false;

        for (int i = 0; i < childCount; i++)
        {
            Transform child = scrollViewContent.GetChild(i);
            if (child != ignoredObject)
            {
                hasChildObjects = true;
                break;
            }
        }

        if (hasChildObjects)
        {
            Debug.Log("The scroll view content has child objects.");
            targetGameObject.SetActive(true); // Activate the targetGameObject
        }
        else
        {
            Debug.Log("The scroll view content does not have any child objects.");
        }
    }

    bool HasChildObjects()
    {
        int childCount = scrollViewContent.childCount;

        for (int i = 0; i < childCount; i++)
        {
            Transform child = scrollViewContent.GetChild(i);
            if (child != ignoredObject)
            {
                return true;
            }
        }

        return false;
    }

    void ActivateGameObject()
    {
        HackText.SetActive(true);
        // Set the desired GameObject to active
        // For example:
        // targetGameObject.SetActive(true);
    }
}
