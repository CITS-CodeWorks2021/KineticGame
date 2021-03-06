using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Catcher : MonoBehaviour
{
    public int pointValue;

	public static UnityEvent OnCatch = new UnityEvent();

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            if(pointValue > 0) ScoreTracker.OnScore.Invoke(pointValue);
            other.gameObject.SetActive(false);
			OnCatch.Invoke();
        }
    }
}
