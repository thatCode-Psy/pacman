using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCharacterMovement : MonoBehaviour {
    public float velocity;
    public int direction;
    public float currVelocity;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        //input
        if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
        {
            direction = -1;
            currVelocity = velocity;
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
        {
            direction = 1;
            currVelocity = velocity;
            transform.rotation = Quaternion.Euler(0, 0, 90);
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))
        {
            direction = -1;
            currVelocity = -velocity;
            transform.rotation = Quaternion.Euler(0, 0, 180);
        }
        if (Input.GetKeyDown(KeyCode.DownArrow) ||Input.GetKeyDown(KeyCode.S)) {
            direction = 1;
            currVelocity = -velocity;
            transform.rotation = Quaternion.Euler(0, 0, 270);
        }


        if (currVelocity != 0) {
            if (direction < 0)
                transform.position += new Vector3(currVelocity, 0, 0);
            else
                transform.position += new Vector3(0, currVelocity, 0);
        }
        
	}
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (direction < 0)
            transform.position -= new Vector3(currVelocity/2, 0, 0);
        else
            transform.position -= new Vector3(0, currVelocity/2, 0);

         currVelocity = 0;
    }
}
