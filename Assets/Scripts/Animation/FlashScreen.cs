using System;
using UnityEngine;

public class FlashScreen : MonoBehaviour
{
    [SerializeField] private SpriteRenderer flashSprite;
    [SerializeField] private float animationSpeed;

    private void Update()
    {
        var timeStep = Time.deltaTime * animationSpeed;

        var currentColor = flashSprite.color;

        currentColor.a = Mathf.Lerp(currentColor.a, 0, timeStep);
        flashSprite.color = currentColor;
    }

    public void GetFlash()
    {
        flashSprite.color = Color.white;
    }
}
