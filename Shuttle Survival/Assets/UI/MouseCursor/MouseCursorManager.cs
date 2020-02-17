using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MouseCursor { defaultCursor, hoverCursor, dialogueCursor, asteroidCursor}
public class MouseCursorManager : MonoBehaviour
{
    public static MouseCursorManager mouseCursorManager;
    [SerializeField] Texture2D defaultCursor;
    [SerializeField] Texture2D hoverCursor;
    [SerializeField] Texture2D dialogueCursor;
    [SerializeField] Texture2D pickaxeCursor;
    [SerializeField] Vector2 basicCursorHotspot = new Vector2(10, 4);
    [SerializeField] Vector2 bonusCursorHotspot = new Vector2(5, 2);

    private void Awake()
    {
        if(MouseCursorManager.mouseCursorManager == null)
        {
            MouseCursorManager.mouseCursorManager = this;
        }
        else
        {
            Destroy(this);
        }
        Cursor.SetCursor(defaultCursor, basicCursorHotspot, CursorMode.Auto);
    }

    public void SetCursor(MouseCursor mouseCursor)
    {
        Texture2D cursorTexture = defaultCursor;
        Vector2 cursorHotspot = basicCursorHotspot;
        switch (mouseCursor)
        {
            case MouseCursor.defaultCursor:
                cursorTexture = defaultCursor;
                break;
            case MouseCursor.hoverCursor:
                cursorTexture = hoverCursor;
                break;
            case MouseCursor.dialogueCursor:
                cursorTexture = dialogueCursor;
                cursorHotspot = bonusCursorHotspot;
                break;
            case MouseCursor.asteroidCursor:
                cursorTexture = pickaxeCursor;
                cursorHotspot = bonusCursorHotspot;
                break;
        }
        Cursor.SetCursor(cursorTexture, cursorHotspot, CursorMode.Auto);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
