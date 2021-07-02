using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bouncer : MonoBehaviour
{
    public int pointValue;

    private void OnCollisionEnter(Collision collision)
    {
        if(pointValue > 0) ScoreTracker.OnScore.Invoke(pointValue);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (pointValue > 0) ScoreTracker.OnScore.Invoke(pointValue);
    }
}
