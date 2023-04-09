using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

public class ObstaclesDescPassageService : MonoBehaviour
{
    public static ObstaclesDescPassageService instance;
    [SerializeField] private string[] startProfessionsGoodObstacle;
    [SerializeField] private string[] nextLevelsProfessionGoodObstacle;
    [SerializeField] private string[] badObstaclesDesc;
    [SerializeField] private int maxLevel;
    private int currentLevel;
    public int CurrentLevel => currentLevel;

    public event Action<int> onLevelUpdate; 

    private void Awake()
    {
        instance = this;
    }

    public string GetGoodDesc()
    {
        if (currentLevel != 0) 
            return nextLevelsProfessionGoodObstacle[currentLevel-1];
        
        var randomProfessionName = startProfessionsGoodObstacle[Random.Range(0, startProfessionsGoodObstacle.Length)];
        return randomProfessionName;
    }

    public string GetBadDesc()
    {
        var randomBadDesc = badObstaclesDesc[Random.Range(0, badObstaclesDesc.Length)];
        return randomBadDesc;
    }
    
    public void GoToNextLevel()
    {
        if(currentLevel < maxLevel)
            currentLevel++;

        if (currentLevel >= maxLevel)
            SceneManager.LoadScene(2);
    }
    public void GoToFirstLevel()
    {
        currentLevel = 0;
    }

    public void OnGoodObstacleCollide(int newLevel)
    {
        onLevelUpdate?.Invoke(newLevel);
    }
    
}
