using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RollProductsButton : MonoBehaviour
{
    public static event Action<Transform> onClickRoll= delegate { };

    private void Start()
    {
        UserInput.onProductClick += DeactiveButton;
        ResetViewButton.onClickReset += ActiveButton;
    }

    private void OnDestroy()
    {
        UserInput.onProductClick -= DeactiveButton;
        ResetViewButton.onClickReset -= ActiveButton;
    }

    private void ActiveButton(Transform t)
    {
        gameObject.SetActive(true);
    }

    private void DeactiveButton(Transform t) 
    {
        gameObject.SetActive(false);
    }


    public void onClickRollButton()
    {
        onClickRoll?.Invoke(null);
    }
}
