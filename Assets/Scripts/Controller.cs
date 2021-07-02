using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
	public GameObject[] shots;
	int currentShot = 0, caughtShot;
    public GameObject aimObject;
    public Camera playerCam;
    public float pushMulti;
    public AimAlignView visAligner;
    bool isPlaying = false, isTracking = false;
    public LayerMask controlPlane;

    public GameObject GetCurShot()
    {
        return shots[currentShot];
    }

    // Start is called before the first frame update
    void Start()
    {
        foreach (GameObject g in shots)
        {
            g.SetActive(false);
        }
            Catcher.OnCatch.AddListener(CaughtBall);
    }

    public void StartGame()
    {
        isPlaying = true;
        isTracking = false;
		currentShot = 0;
        caughtShot = 0;
        foreach(GameObject g in shots)
		{
			g.SetActive(false);
            //g.GetComponent<Rigidbody>().useGravity = false;
            g.GetComponent<Rigidbody2D>().gravityScale = 0;
		}
    }

    // Update is called once per frame
    void Update()
    {
        if(isPlaying && !isTracking && currentShot < shots.Length)
        {
            CheckInput();
        }

        if(isTracking)
        {
            TrackInput();
        }
    }

    void CheckInput()
    {
        if (Input.GetMouseButton(0))
        {
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
            if (hit)
            {
                Debug.LogWarning("Clicked: " + hit.transform.position);
                Vector3 point = hit.point;
                //point.x += 0.5f;
                shots[currentShot].GetComponent<Rigidbody2D>().velocity = Vector2.zero;
                shots[currentShot].GetComponent<Rigidbody2D>().angularVelocity = 0;
                shots[currentShot].transform.position = point;
                aimObject.transform.position = point;
                shots[currentShot].SetActive(true);

                isTracking = true;

                visAligner.StartTracking();


            }
        }
    }

    void TrackInput()
    {
        if (Input.GetMouseButton(0))
        {
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

            if (hit)
            {
                Vector3 point = hit.point;
                //point.x += 0.5f;
				shots[currentShot].GetComponent<Rigidbody2D>().velocity = Vector2.zero;
				shots[currentShot].GetComponent<Rigidbody2D>().angularVelocity = 0;
                
                aimObject.transform.position = point;
            }
        }
        else
        {
            KickBall();
            currentShot++;
            isTracking = false;
        }
        
    }

    void CaughtBall()
	{
        caughtShot++;
        if(caughtShot >= shots.Length)
        {
            GameState.OnEnd.Invoke();
            return;
        }
    }

    void KickBall()
    {
		shots[currentShot].GetComponent<Rigidbody2D>().gravityScale = 1;
        visAligner.StopTracking();
        Vector3 pushDir = shots[currentShot].transform.position - aimObject.transform.position;
        pushDir *= pushMulti;
		shots[currentShot].GetComponent<Rigidbody2D>().AddForce(pushDir, ForceMode2D.Impulse);
    }

    public void EndGame()
    {
        isPlaying = false;
    }
}
