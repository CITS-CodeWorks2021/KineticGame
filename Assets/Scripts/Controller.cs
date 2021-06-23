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
            g.GetComponent<Rigidbody>().useGravity = false;
		}
    }

    // Update is called once per frame
    void Update()
    {
        if(isPlaying && !isTracking && currentShot < shots.Length)
        {
            if (Input.GetMouseButton(0))
            {
                Ray tappedLocRay =
                    playerCam.ScreenPointToRay(
                        new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0)
                        );

                if (Physics.Raycast(tappedLocRay, out RaycastHit hitSomething, Mathf.Infinity, controlPlane))
                {
					
                    Vector3 point = hitSomething.point;
                    point.x += 0.5f;
					shots[currentShot].GetComponent<Rigidbody>().velocity = Vector3.zero;
					shots[currentShot].GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
					shots[currentShot].transform.position = point;
                    aimObject.transform.position = point;
                    shots[currentShot].SetActive(true);

                    isTracking = true;

                    StartCoroutine(TrackInput());
                    visAligner.StartTracking();

					
				}
            }
        }
    }

    IEnumerator TrackInput()
    {
        while (Input.GetMouseButton(0))
        {
            Ray tappedLocRay =
                playerCam.ScreenPointToRay(
                    new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0)
                    );

            if (Physics.Raycast(tappedLocRay, out RaycastHit hitSomething, Mathf.Infinity, controlPlane))
            {
                Vector3 point = hitSomething.point;
                point.x += 0.5f;
				shots[currentShot].GetComponent<Rigidbody>().velocity = Vector3.zero;
				shots[currentShot].GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
                
                aimObject.transform.position = point;
            }
            yield return new WaitForEndOfFrame();
        }

        KickBall();
        currentShot++;
        isTracking = false;
        yield return null;
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
		shots[currentShot].GetComponent<Rigidbody>().useGravity = true;
        visAligner.StopTracking();
        Vector3 pushDir = shots[currentShot].transform.position - aimObject.transform.position;
        pushDir *= pushMulti;
		shots[currentShot].GetComponent<Rigidbody>().AddForce(pushDir, ForceMode.Impulse);
    }

    public void EndGame()
    {
        isPlaying = false;
    }
}
