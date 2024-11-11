using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TextInput : MonoBehaviour
{
    public TMP_InputField nameLabel;
    public TMP_InputField priceLable;
    public static event Action<String> onTextChange = delegate { };


    public void SetTexts(string pName, string price)
    {
        nameLabel.text = pName;
        priceLable.text = price;
    }

    public void OnChangeName()
    {
        onTextChange?.Invoke("Name");
    }

    public void OnChangePrice()
    {
        onTextChange?.Invoke("Price");
        if (!priceLable.text.EndsWith("$"))
            priceLable.text += "$";
    }
}
