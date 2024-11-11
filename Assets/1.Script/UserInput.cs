using System;
using UnityEngine;

public class UserInput : MonoBehaviour
{
    public static event Action<Transform> onProductClick = delegate { };
    public static event Action<bool> onPointProduct = delegate { };

    public LayerMask productLayer; 
    private bool isPointingProduct = false;

    // Update is called once per frame
    void Update()
    {
        // Cast ray from the mouse position
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        // Perform the raycast and check if it hits the specified layer
        if (Physics.Raycast(ray, out hit, 1000, productLayer))
        {
            if (!isPointingProduct)
            {
                isPointingProduct = true;
                onPointProduct?.Invoke(true);
            }

            if (Input.GetMouseButtonDown(0))
            {
                onProductClick?.Invoke(hit.collider.transform);
            }   
        }
        else
        {
            // No hit on the specified layer
            if (isPointingProduct)
            {
                isPointingProduct = false;
                onPointProduct?.Invoke(false);
            }
        }
    }
}
