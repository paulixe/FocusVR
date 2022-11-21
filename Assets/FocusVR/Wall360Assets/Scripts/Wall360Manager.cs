using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(BallSpawner))]
public class Wall360Manager : MonoBehaviour
{
   
    public List<BallBehaviour> balls => ballSpawner.activatedBallList;
    [SerializeField] float activationTime=1f;
    int initialquantity = 4;
    BallSpawner ballSpawner;



    float elapsedTimeSinceLastActivation;

    private void Awake()
    {
        ballSpawner = GetComponent<BallSpawner>();
    }
    private void Start()
    {
        ballSpawner.UpdateBallsCount(initialquantity);
    }
    private void Update()
    {
        elapsedTimeSinceLastActivation+=Time.deltaTime;
        if (elapsedTimeSinceLastActivation > activationTime)
        {
            elapsedTimeSinceLastActivation = 0;
            ActivateRandomBall();
        }
    }
    private void ActivateRandomBall()
    { 
        foreach(BallBehaviour ball in balls)
                ball.IsActivated = false;

        int ballIndex = Random.Range(0, balls.Count);
        balls[ballIndex].IsActivated = true;
    }
}
