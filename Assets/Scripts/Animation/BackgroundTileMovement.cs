using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundTileMovement : MonoBehaviour
{
    [SerializeField] private float movementSpeed = 1;
    [SerializeField] private SpriteRenderer background;
    [SerializeField] private float backgroundSizeClamp = 1000;

    [SerializeField] private Sprite[] backgroundPerLevel;
    private ObstaclesDescPassageService obstaclesDescPassageService;

    private void Start()
    {
        obstaclesDescPassageService = ObstaclesDescPassageService.instance;
        
        obstaclesDescPassageService.onLevelUpdate += SetNewBackground;
    }

    private void Update()
    {
        var timeStep = Time.deltaTime * movementSpeed;

        background.size += new Vector2(0,timeStep);

        if (background.size.y >= backgroundSizeClamp)
        {
            background.size = Vector2.one * 3;
        }
    }

    private void SetNewBackground(int newLevel)
    {
        background.sprite = backgroundPerLevel[newLevel];
    }

    public void SetSpeed(float speed)
    {
        movementSpeed = speed;
    }
    
}
