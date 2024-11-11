using System.Collections.Generic;
using UnityEngine;

public class Shelf : MonoBehaviour
{
    public List<Transform> Sockets = new List<Transform>();
    // Start is called before the first frame update
    void Start()
    {
        if (Sockets.Count != transform.Find("Sockets").childCount)
        {
            Sockets.Clear();
            foreach (Transform t in transform.Find("Sockets"))
            {
                Sockets.Add(t);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public List<Vector3> GetSocketPositon() 
    {
        List<Vector3>  tempList = new List<Vector3>();
        foreach (Transform t in Sockets) 
        {
            tempList.Add(t.position);
        }
        return tempList;
    }
}
