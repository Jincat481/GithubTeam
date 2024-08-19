using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SpriteColorManger
{
    public static void ChangeColor(SpriteRenderer spriteRenderer, Color newColor)
    {
        if(spriteRenderer != null)
        {
            spriteRenderer.color = newColor;
        }
    }
}
