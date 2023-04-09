using System;
using UnityEngine;

public class MainGameService : MonoBehaviour
{

    private ObstaclesDescPassageService obstacleService;
    [SerializeField] private FlashScreen flashScreen;
    [SerializeField] private ObstaclesSpawnService obstaclesSpawnService;
    [SerializeField] private BackgroundTileMovement backgroundTileMovement;

    [SerializeField] private float[] perLevelGameSpeed;
    
    private void Start()
    {
        obstacleService = ObstaclesDescPassageService.instance;
        
        obstacleService.onLevelUpdate += OnLevelUpdate;
    }

    private void OnLevelUpdate(int newLevel)
    {
        flashScreen.GetFlash();
        obstaclesSpawnService.DestroyAllObstacles();
        
        SetNewSpeed(perLevelGameSpeed[newLevel]);
    }

    private void SetNewSpeed(float speed)
    {
        obstaclesSpawnService.SetSpeed(speed);
        backgroundTileMovement.SetSpeed(speed);
    }
}
