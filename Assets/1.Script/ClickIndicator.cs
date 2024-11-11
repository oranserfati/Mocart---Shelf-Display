using System.Collections;
using UnityEngine;

public class ClickIndicator : MonoBehaviour
{
    public GameObject targetObject; 
    public float distanceThreshold = 5.0f;
    public float enableDelay = 0.5f;
    private Camera mainCamera;

    private bool disableIndicator = true;

    void Start()
    {
        ProductFetcher.onFetchProducts += _ => EnableIndicator(); // Lambda to discard the parameter send in the event
        ResetViewButton.onClickReset += _ => EnableAfterDelay(1); // Delay the object will be actived only after we dont reset the camera
        UserInput.onProductClick += _ => DisableIndicator();
        mainCamera = Camera.main;
    }

    private void OnDestroy()
    {
        ProductFetcher.onFetchProducts -= _ => EnableIndicator();
        ResetViewButton.onClickReset += _ => EnableAfterDelay(1);
        UserInput.onProductClick -= _ => DisableIndicator();
    }

    void Update()
    {
        if (!disableIndicator)
        {
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);

            Vector3 objectPosition = targetObject.transform.position;

            Vector3 mouseWorldPosition = ray.origin + ray.direction * (objectPosition.z - ray.origin.z);

            Vector2 mousePos2D = new Vector2(mouseWorldPosition.x, mouseWorldPosition.y);

            Vector2 targetPos2D = new Vector2(objectPosition.x, objectPosition.y);

            float distance = Vector2.Distance(mousePos2D, targetPos2D);

            if (distance <= distanceThreshold)
            {
                if (!targetObject.activeSelf)
                {
                    targetObject.SetActive(true);
                }
            }
            else
            {
                if (targetObject.activeSelf)
                {
                    targetObject.SetActive(false);
                }
            }
        }
        else
        {
            if (targetObject.activeSelf)
            {
                targetObject.SetActive(false);
            }
        }
    }

    private void DisableIndicator()
    {
        disableIndicator = true;
    }

    private void EnableIndicator()
    {
        disableIndicator = false;
    }

    public void EnableAfterDelay(float delay)
    {
        StartCoroutine(AfterDelay(delay));
    }

    // Coroutine to activate the GameObject after 1 second
    private IEnumerator AfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay); // Wait for 1 second
        EnableIndicator(); // Activate the GameObject
    }
}
