using UnityEngine;

public class ChangeCurser : MonoBehaviour
{
    public Texture2D clickCurser;
    public CursorMode cursorMode = CursorMode.Auto;
    public Vector2 hotSpot = Vector2.zero;
    private Texture2D currentCurser;

    // Start is called before the first frame update
    void Start()
    {
        currentCurser = null;
        UserInput.onPointProduct += onChangeCurserListner;
    }

    private void OnDestroy()
    {
        UserInput.onPointProduct -= onChangeCurserListner;
    }

    private void onChangeCurserListner(bool isPointingProduct)
    {
        if (isPointingProduct) 
        {
            currentCurser = clickCurser;
        }
        else
        {
            currentCurser = null;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (currentCurser == null)
            Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto); // Unity default cursor
        else
            Cursor.SetCursor(currentCurser, hotSpot, cursorMode);
    }
}
