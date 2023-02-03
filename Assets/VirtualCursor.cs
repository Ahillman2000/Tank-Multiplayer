using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VirtualCursor : MonoBehaviour
{
    [SerializeField] private Texture2D virtualCursorVisual;

    private void Awake()
    {
        ChangeCursor(virtualCursorVisual);
        Cursor.lockState = CursorLockMode.Confined;
    }

    private void ChangeCursor(Texture2D cursorVisual)
    {
        Vector2 hotspot = new Vector2(cursorVisual.width / 2, cursorVisual.height / 2);
        Cursor.SetCursor(cursorVisual, hotspot, CursorMode.Auto);
    }
}
