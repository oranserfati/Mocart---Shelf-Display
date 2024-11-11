using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationIndicator : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        UserInput.onProductClick += ActiveIndicator;
        ResetViewButton.onClickReset += DeactiveIndicator;
        DeactiveIndicator(null);
    }
    void OnDestroy()
    {
        UserInput.onProductClick -= ActiveIndicator;
        ResetViewButton.onClickReset -= DeactiveIndicator;
    }

    private void ActiveIndicator(Transform t)
    {
        gameObject.SetActive(true);
    }

    private void DeactiveIndicator(Transform t)
    {
        gameObject.SetActive(false);
    }
}
