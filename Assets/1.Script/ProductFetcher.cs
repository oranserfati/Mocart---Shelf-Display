using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;
using System;

public class ProductFetcher : MonoBehaviour
{
    private const string URL = "https://homework.mocart.io/api/products";
    public List<Product> productsList = new List<Product>();
    private bool isFetching = false;
    public static event Action<List<Product>> onFetchProducts = delegate { };

    private void Start()
    {
        RollProductsButton.onClickRoll += onClickRollListener;
    }

    private void OnDestroy()
    {
        RollProductsButton.onClickRoll -= onClickRollListener;
    }

    private async Task FetchProductsAsync()
    {
        if (isFetching)
        {
            Debug.Log("Fetch already in progress");
            return;
        }

        isFetching = true;

        using (UnityWebRequest request = UnityWebRequest.Get(URL))
        {
            var operation = request.SendWebRequest();
            while (!operation.isDone)
            {
                await Task.Yield();
            }

            if (request.result == UnityWebRequest.Result.Success)
            {
                // Parse JSON response into product list
                string jsonResponse = request.downloadHandler.text;
                ProductData productData = JsonUtility.FromJson<ProductData>(jsonResponse);
                productsList = productData.products;

                // Trigger event after successfully fetching products
                onFetchProducts?.Invoke(productsList);
            }
            else
            {
                Debug.LogError("Error fetching products: " + request.error);
            }
        }

        isFetching = false;
    }

    private async void onClickRollListener(Transform t)
    {
        // Fetch products only if not already fetching
        if (!isFetching)
        {
            await FetchProductsAsync();
        }
    }
}

[Serializable]
public class Product
{
    public string name;
    public string description;
    public float price;
}

[Serializable]
public class ProductData
{
    public List<Product> products;
}
