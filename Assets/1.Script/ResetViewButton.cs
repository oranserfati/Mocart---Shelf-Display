using System;
using UnityEngine;

public class ResetViewButton : MonoBehaviour
{
    public static event Action<Transform> onClickReset = delegate { };
    // Start is called before the first frame update
    void Start()
    {
        UserInput.onProductClick += ActiveButton;
        DeactiveButton(null);
    }

    private void OnDestroy()
    {
        UserInput.onProductClick -= ActiveButton;
    }

    private void ActiveButton(Transform t)
    {
        gameObject.SetActive(true);
    }

    private void DeactiveButton(Transform t)
    {
        gameObject.SetActive(false);
    }

    public void onClickResetButton()
    {
        DeactiveButton(null);
        onClickReset?.Invoke(null);
    }
}
