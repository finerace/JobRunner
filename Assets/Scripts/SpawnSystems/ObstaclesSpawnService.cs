using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using Random = UnityEngine.Random;

public class ObstaclesSpawnService : MonoBehaviour
{
    [SerializeField] private Transform[] obstaclesSpawnPoints;
    private List<Transform> spawnedObstacles = new List<Transform>();
    [SerializeField] private float obstaclesSpeed = 1;
    [SerializeField] private float defaultTickValue = 1;
    
    [Space]
    
    [SerializeField] private int obstacleSpawnCof = 5;
    [SerializeField] private int goodObstacleSpawnCof = 15;

    [SerializeField] private int additionalClearLineSpawnCof = 15;
    [SerializeField] private int clearLinesMaxAgeTick = 15;
    [SerializeField] private int clearLinesTickGap = 2;
    
    private List<(int, int)> currentClearLines = new List<(int, int)>();
    private List<(int, int)> notActiveClearLines = new List<(int, int)>();
    
    [Space] 
    
    [SerializeField] private GameObject badObstacle;
    [SerializeField] private GameObject goodObstacle;
    [SerializeField] private float obstaclesDestroyValue;

    private void Start()
    {
        StartCoroutine(SpawnObstacleUpdater());
    }

    IEnumerator SpawnObstacleUpdater()
    {
        while (true)
        {
            yield return new WaitForSeconds(defaultTickValue/obstaclesSpeed);
            
            SpawnObstacles();
        }
    }

    private void SpawnObstacles()
    {
        AddTickAgeToClearLines(currentClearLines);
        AddTickAgeToClearLines(notActiveClearLines);
        void AddTickAgeToClearLines([NotNull] List<(int, int)> clearLines)
        {
            if (clearLines == null) 
                throw new ArgumentNullException(nameof(clearLines));
            
            for (var i = 0; i < clearLines.Count; i++)
            {
                var clearLine = clearLines[i];
                var newClearLine = (clearLine.Item1,clearLine.Item2+1);

                clearLines.Remove(clearLine);
                clearLines.Add(newClearLine);
            }
        }
        
        var isObstaclesSpawn = Random.Range(0,obstacleSpawnCof) == 0;

        if (!isObstaclesSpawn)
        {
            DestroyOldClearLines();
            return;
        }
        
        if (currentClearLines.Count < 1)
        {
            AddNewClearLine();            
            return;
        }
        
        if(Random.Range(0,additionalClearLineSpawnCof) == 0)
            AddNewClearLine();
        
        void AddNewClearLine()
        {
            var newClearLine = (Random.Range(0, obstaclesSpawnPoints.Length), 0);
            currentClearLines.Add(newClearLine);
        }            
        
        SpawnObstaclesOnClearLines();
        void SpawnObstaclesOnClearLines()
        {
            for (var i = 0; i < obstaclesSpawnPoints.Length; i++)
            {
                var spawnPoint = obstaclesSpawnPoints[i];
                
                bool IsSpawnPointBlocked(int spawnIndex)
                {
                    foreach (var clearLineIndex in currentClearLines)
                    {
                        if (clearLineIndex.Item1 == spawnIndex)
                            return true;
                    }
                    
                    return false;
                }
                if(IsSpawnPointBlocked(i))
                    continue;
                
                if(notActiveClearLines.Count > 0)
                    return;
                
                var isObstacleGood = Random.Range(0, goodObstacleSpawnCof) == 0;
                GameObject toSpawnObstacle;

                if (!isObstacleGood)
                    toSpawnObstacle = badObstacle;
                else
                    toSpawnObstacle = goodObstacle;

                var spawnedObstacle = Instantiate(toSpawnObstacle,spawnPoint);
                spawnedObstacles.Add(spawnedObstacle.transform);
            }
        }
        
        DestroyOldClearLines();
        void DestroyOldClearLines()
        {
            for (int i = 0; i < currentClearLines.Count; i++)
            {
                var clearLine = currentClearLines[i];
                
                if (clearLine.Item2 >= clearLinesMaxAgeTick - clearLinesTickGap)
                {
                    currentClearLines.Remove(clearLine);
                    notActiveClearLines.Add(clearLine);
                }
            }

            for (int i = 0; i < notActiveClearLines.Count; i++)
            {
                var clearLine = notActiveClearLines[i];
                
                if (clearLine.Item2 >= clearLinesMaxAgeTick)
                    notActiveClearLines.Remove(clearLine);
            }
        }
    }

    private void Update()
    {
        MoveObstacles();
        void MoveObstacles()
        {
            var timeStep = Time.deltaTime * obstaclesSpeed;

            for (var i = 0; i < spawnedObstacles.Count; i++)
            {
                if (spawnedObstacles[i] == null)
                {
                    spawnedObstacles.Remove(spawnedObstacles[i]);
                    continue;                    
                }

                var obstacleT = spawnedObstacles[i];
                
                obstacleT.position =
                    Vector3.MoveTowards(obstacleT.position, obstacleT.position + Vector3.down, timeStep);

                if (!(obstacleT.position.y <= obstaclesDestroyValue))
                    continue;
                
                spawnedObstacles.Remove(obstacleT);
                Destroy(obstacleT.gameObject);
            }
        }
    }

    public void DestroyAllObstacles()
    {
        for (int i = 0; i < spawnedObstacles.Count; i++)
        {
            var obstacle = spawnedObstacles[i];

            Destroy(obstacle.gameObject);
        }    
    }

    public void SetSpeed(float speed)
    {
        obstaclesSpeed = speed;
        
    }
    
}
