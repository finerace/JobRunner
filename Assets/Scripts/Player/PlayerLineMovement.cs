using System;
using UnityEngine;

public class PlayerLineMovement : MonoBehaviour
{
    [SerializeField] private Transform playerT;
    [SerializeField] private Transform[] moveLinesPoints;
    [SerializeField] private float playerLinesMoveSpeed;
    [SerializeField] private float playerMoveClamp = 0.025f;
    private int currentLineMovePoint;

    private void Start()
    {
        currentLineMovePoint = moveLinesPoints.Length / 2;
    }

    private void Update()
    {
        int GetPlayerMoveValue()
        {
            var resultValue = 0;

            if (Input.GetKeyDown(KeyCode.A))
                resultValue--;
        
            if (Input.GetKeyDown(KeyCode.D))
                resultValue++;

            return resultValue;
        }
        
        void SetNewLineMovePoint(int moveValue)
        {
            bool IsNowLineMovePointLast()
            {
                var isCurrentLineMovePointLast = false;

                if (moveValue < 0)
                    isCurrentLineMovePointLast = currentLineMovePoint <= 0;
                else
                    isCurrentLineMovePointLast = currentLineMovePoint >= moveLinesPoints.Length - 1;

                return isCurrentLineMovePointLast;
            }
            
            if(moveValue == 0 || IsNowLineMovePointLast())
                return;

            currentLineMovePoint += moveValue;
        }
        
        void MovePlayerToLineMovePoint()
        {
            var timeStep = Time.deltaTime * playerLinesMoveSpeed;

            playerT.position = Vector3.Lerp(playerT.position,moveLinesPoints[currentLineMovePoint].position,timeStep);
        }

        bool IsPlayerMoveNow()
        {
            var playerPointDistance 
                = Vector3.Distance(playerT.position,moveLinesPoints[currentLineMovePoint].position);
            
            return playerPointDistance > playerMoveClamp;
        }

        var isPlayerMoveNow = IsPlayerMoveNow();
        
        if (!isPlayerMoveNow)
        {
            var moveValue = GetPlayerMoveValue();

            SetNewLineMovePoint(moveValue);
        }
        
        MovePlayerToLineMovePoint();
    }

}
