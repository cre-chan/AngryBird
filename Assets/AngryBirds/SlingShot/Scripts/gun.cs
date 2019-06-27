using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gun : MonoBehaviour {

    public GameObject[] birdList;
    private int birdLength;
    private int whichBirdNum;
  //  private GameObject whichBird;
   // bird.BirdState whichBirdNumState;

    private Vector3 camareVector;
    public GameObject camare;


    public enum BirdState { wait, load, pull, shoot, dead };

    // Use this for initialization
    void Start () {

        birdLength = birdList.Length;
        whichBirdNum = 0;
        for (int i =1; i <birdLength; i++)
        {
            birdList[i].GetComponent<bird>().bs = bird.BirdState.wait;
        }


     
        birdList[whichBirdNum].GetComponent<bird>().bs = bird.BirdState.load;
       
        birdList[whichBirdNum].GetComponent<bird>().ReLoad();

        camareVector = camare.transform.position;

    }
	
	// Update is called once per frame
	void Update ()
    {
        if (whichBirdNum >= birdLength)
            return;
        switch (birdList[whichBirdNum].GetComponent<bird>().bs)
        {
            case bird.BirdState.wait:
                {
                    birdList[whichBirdNum].GetComponent<bird>().bs = bird.BirdState.load;
                    birdList[whichBirdNum].GetComponent<bird>().ReLoad();
                 
                    break;
                }
            case bird.BirdState.load:
                {

                    break;
                }
            case bird.BirdState.pull:
                {
                    birdList[whichBirdNum].GetComponent<bird>().PullPoisition();
                    break;
                }
            case bird.BirdState.shoot:
                {
                    if (birdList[whichBirdNum].GetComponent<bird>().IsDead())
                        birdList[whichBirdNum].GetComponent<bird>().bs = bird.BirdState.dead;
                    break;
                }
            case bird.BirdState.dead:
                {
                    if (whichBirdNum < birdLength)
                    {
                        whichBirdNum++;
                        if (whichBirdNum < birdLength)
                        {

                            birdList[whichBirdNum].GetComponent<bird>().bs = bird.BirdState.load;
                            birdList[whichBirdNum].GetComponent<bird>().ReLoad();
                            int i = 0;
                            i++;
                        }
                        else
                            break;
                    }
                    break;
                }
        }
        camareFollow();

    }

    private void OnMouseDown()
    {
        if (whichBirdNum >= birdLength)
            return;
        if (birdList[whichBirdNum].GetComponent<bird>().bs == bird.BirdState.load)
        {
            birdList[whichBirdNum].GetComponent<bird>().bs = bird.BirdState.pull;
        }
    }

    private void OnMouseUp()
    {
        if (whichBirdNum >= birdLength)
            return;
        if (birdList[whichBirdNum].GetComponent<bird>().bs == bird.BirdState.pull)
        {
            birdList[whichBirdNum].GetComponent<bird>().bs = bird.BirdState.shoot;
            birdList[whichBirdNum].GetComponent<bird>().ShootOut();
        }
    }

    void camareFollow()
    {
        if (whichBirdNum >= birdLength)
            return;
        if (birdList[whichBirdNum].GetComponent<bird>().bs == bird.BirdState.shoot)
        {
            float birdx = birdList[whichBirdNum].transform.position.x;
            camare.transform.position = new Vector3(birdx, camareVector.y, camareVector.z);
        }
        else
        {
            camare.transform.position = camareVector;
        }
    }
}
