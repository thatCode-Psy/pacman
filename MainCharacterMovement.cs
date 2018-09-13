using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainCharacterMovement : MonoBehaviour {
    public float velocity;
    public int direction;
    public float currVelocity;
    public int score;
	// Use this for initialization
	void Start () {
        score = 0;
	}

    // Update is called once per frame
    void Update()
    {
        //input
        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(0);
        }
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
        if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S))
        {
            direction = 1;
            currVelocity = -velocity;
            transform.rotation = Quaternion.Euler(0, 0, 270);
        }

        if (Mathf.Abs(GetComponent<Rigidbody2D>().velocity.x) < .1f && Mathf.Abs(GetComponent<Rigidbody2D>().velocity.y) < .1f)
        {
            GetComponent<Animator>().SetBool("Moving", false);
        }
        else {
            GetComponent<Animator>().SetBool("Moving", true);
        }
        if (currVelocity != 0)
        {
            if (direction < 0)
                GetComponent<Rigidbody2D>().velocity = new Vector3(currVelocity, 0, 0);
            else
                GetComponent<Rigidbody2D>().velocity = new Vector3(0, currVelocity, 0);
        }
        else
        {
            GetComponent<Animator>().SetBool("Moving", false);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "ghost") {
            GetComponent<Animator>().SetBool("Die", true);
        }
        if (collision.gameObject.tag == "Pellet")
        {
            score++;
            Destroy(collision.gameObject);
        }
    }
}
