using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SpriteColorManger
{
    public static IEnumerator HitColor(SpriteRenderer spriteRenderer, Color newColor, Color originalColor)
    {
        if(spriteRenderer != null)
        {
            spriteRenderer.color = newColor;

            yield return new WaitForSeconds(0.5f);

            spriteRenderer.color = originalColor;
        }
    }

    public static void DieColor(SpriteRenderer spriteRenderer, Color newColor)
    {
        if(spriteRenderer != null)
        {
            spriteRenderer.color = newColor;
        }
    }
}
