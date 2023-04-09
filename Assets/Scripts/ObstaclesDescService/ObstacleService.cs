using System;
using TMPro;
using UnityEngine;

public class ObstacleService : MonoBehaviour
{
    private ObstaclesDescPassageService obstacleDescPassageService;
    [SerializeField] private int obstacleLevel;
    [SerializeField] private TMP_Text descLabel;
    [SerializeField] private bool isObstacleGood;

    private void Start()
    {
        obstacleDescPassageService = ObstaclesDescPassageService.instance; ;

        obstacleLevel = obstacleDescPassageService.CurrentLevel;
        descLabel.text = !isObstacleGood ? obstacleDescPassageService.GetBadDesc() : obstacleDescPassageService.GetGoodDesc();
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if(col.isTrigger)
            return;

        if (!isObstacleGood)
        {
            obstacleDescPassageService.GoToFirstLevel();
            obstacleDescPassageService.OnGoodObstacleCollide(obstacleDescPassageService.CurrentLevel);            
        }
        else if(obstacleDescPassageService.CurrentLevel == obstacleLevel)
        {
            obstacleDescPassageService.GoToNextLevel();
            obstacleDescPassageService.OnGoodObstacleCollide(obstacleDescPassageService.CurrentLevel);
        }
        
        Destroy(gameObject);
    }
}
