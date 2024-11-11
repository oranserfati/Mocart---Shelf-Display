using System;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour
{
    public  List<TextInput> textInputs = new List<TextInput>();
    private List<TextInput> activeInputs = new List<TextInput>();

    // Start is called before the first frame update
    void Start()
    {
        ProductFetcher.onFetchProducts += onFetchProductsListener;
        UserInput.onProductClick += onHatClickListener;
        ResetViewButton.onClickReset += onClickResetListener;

    }

    private void OnDestroy()
    {
        ProductFetcher.onFetchProducts -= onFetchProductsListener;
        ResetViewButton.onClickReset += onClickResetListener;
    }
    private void onFetchProductsListener(List<Product> p)
    {
        activeInputs.Clear();
        for (int i = 0; i < textInputs.Count; i++) 
        {
            try
            {
                textInputs[i].SetTexts(p[i].name, p[i].price.ToString() + "$");
                textInputs[i].gameObject.SetActive(true);
                activeInputs.Add(textInputs[i]);
            }
            catch (Exception)
            {
                textInputs[i].gameObject.SetActive(false);
            }
        }
    }
    private void onHatClickListener(Transform transform)
    {
        foreach (var p in textInputs)
        {
            p.gameObject.SetActive(false);
        }
    }

    private void onClickResetListener(Transform t)
    {
        foreach (var item in activeInputs)
        {
            item.gameObject.SetActive(true);
        }
    }
}
