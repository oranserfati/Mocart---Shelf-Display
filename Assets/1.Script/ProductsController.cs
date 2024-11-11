using System.Collections.Generic;
using UnityEngine;

public class ProductsController : MonoBehaviour
{
    public List<GameObject> productPrefabs = new List<GameObject>();
    public GameObject dummyPrefab;
    public float yOffset = 0.45f;

    private List<Vector3> socketsPosition = new List<Vector3>();
    private List<GameObject> products = new List<GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        ProductFetcher.onFetchProducts += OnFetchProductsListener;
        socketsPosition = GetComponent<Shelf>().GetSocketPositon();
    }

    private void OnFetchProductsListener(List<Product> p)
    {
        DestroyAllProducts();
        // Instantiate products
        for (int i = 0; i < p.Count; i++)
        {
            // Get product num
            int productNum = int.Parse(p[i].name.Split(' ')[1]);
            GameObject newProduct = Instantiate(productPrefabs[Mathf.CeilToInt(productNum / 2f) - 1], transform.Find("Products"));
            newProduct.transform.position = socketsPosition[i] + new Vector3(0, yOffset, 0);
            products.Add(newProduct);
        }

        // Fill remaining sockets with dummies
        if (p.Count < 3) 
        {
            for (int i = 0; i < 3 - p.Count; i++)
            {
                GameObject newProduct = Instantiate(dummyPrefab, transform.Find("Products"));
                newProduct.transform.position = socketsPosition[p.Count + i] + new Vector3(0, yOffset, 0);
                products.Add(newProduct);
            }
        }
    }

    private void DestroyAllProducts()
    {
        if (products.Count > 0) 
        {
            for (int i = 0; i < products.Count; i++)
            {
                Destroy(products[i]);
            }
            products.Clear();
        }
    }
}
