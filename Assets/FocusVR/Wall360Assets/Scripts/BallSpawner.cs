using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using static UnityEngine.Mathf;
public class BallSpawner : MonoBehaviour
{
    [SerializeField] GameObject ballPrefab;
    private Vector3 center;
    public Vector3 offset;

    [HideInInspector] public List<BallBehaviour> activatedBallList = new List<BallBehaviour>();
    List<BallBehaviour> deactivatedBallList = new List<BallBehaviour>();

    public int QuantityWanted = 3;
    [ContextMenu(nameof(UpdateBallsCount))]
    public void Test()
    {
        UpdateBallsCount(QuantityWanted); 
    }
    public int NumberOfActivatedBalls=>activatedBallList.Count;
    public int NumberOfDeactivatedBalls => deactivatedBallList.Count;
    public void UpdateBallsCount(int numberOfBalls)
    {
        if (numberOfBalls > NumberOfActivatedBalls)
            ActivateBalls(numberOfBalls - NumberOfActivatedBalls);
        else if (numberOfBalls < NumberOfActivatedBalls)
            DeactivateBalls(NumberOfActivatedBalls - numberOfBalls);
        UpdatePos();
    }
    private void DeactivateBalls(int numberOfBalls)
    {

        if (NumberOfActivatedBalls < numberOfBalls)
        {
            throw new Exception("There aren't enough balls");
        }

        Queue<BallBehaviour> ballsToDeactivate=new Queue<BallBehaviour>(activatedBallList.GetRange(0, numberOfBalls));
        while (ballsToDeactivate.TryDequeue(out BallBehaviour currentBall))
        {
            currentBall.gameObject.SetActive(false);
            deactivatedBallList.Add(currentBall);
            activatedBallList.Remove(currentBall);
        }
    }
    private void ActivateBalls(int numberOfBalls)
    {
        if (NumberOfDeactivatedBalls < numberOfBalls)
            Spawn(numberOfBalls - NumberOfDeactivatedBalls);


        Queue<BallBehaviour> ballsToActivate = new Queue<BallBehaviour>(deactivatedBallList.GetRange(0, numberOfBalls));

        while (ballsToActivate.TryDequeue(out BallBehaviour currentBall))
        {
            currentBall.gameObject.SetActive(true);
            activatedBallList.Add(currentBall);
            deactivatedBallList.Remove(currentBall);
        }
    }

    private void Spawn(int numberOfBalls)
    {
        for (int i = 0; i < numberOfBalls; i++)
        {
            BallBehaviour ballBehaviour = Instantiate(ballPrefab).GetComponent<BallBehaviour>();
            deactivatedBallList.Add(ballBehaviour);
        }
    }

    private void UpdatePos()
    {
        
        for (int i = 0; i < NumberOfActivatedBalls; i++)
        {
            float angle = ((float)i / NumberOfActivatedBalls) * 360f*Deg2Rad;
            GameObject currentBallWePlace= activatedBallList[i].gameObject;

            Vector3 newPos = new Vector3(offset.x * Sin(angle),offset.y,offset.z * Cos(angle));
            currentBallWePlace.transform.position = newPos;
        }
    }
}
