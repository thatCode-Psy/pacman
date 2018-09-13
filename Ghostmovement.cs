using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghostmovement : MonoBehaviour {
    public bool goingHorizontal;
    public Rigidbody2D rb;
    public float speed;
	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {
        if (Mathf.Abs(rb.velocity.x) < .1f && Mathf.Abs(rb.velocity.y) < .1f) {
            if (goingHorizontal)
            {
                RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.up);
                RaycastHit2D hit2 = Physics2D.Raycast(transform.position, -Vector2.up);
                goingHorizontal = false;
                if (hit.collider == null && hit2.collider == null)
                {
                    if (Random.Range(0, 1f) > .5f)
                    {
                        rb.velocity = new Vector2(0, speed);
                    }
                    else
                    {
                        rb.velocity = new Vector2(0, -speed);
                    }
                }
                else if (hit.collider == null)
                {
                    rb.velocity = new Vector2(0, speed);
                }
                else {
                    rb.velocity = new Vector2(0, -speed);
                }
            }
            else {
                RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.right);
                RaycastHit2D hit2 = Physics2D.Raycast(transform.position, -Vector2.right);
                goingHorizontal = true;
                if (hit.collider == null && hit2.collider == null)
                {
                    if (Random.Range(0, 1) > .5f)
                    {
                        rb.velocity = new Vector2(speed, 0);
                    }
                    else
                    {
                        rb.velocity = new Vector2(-speed, 0);
                    }
                }
                else if (hit.collider== null)
                {
                    rb.velocity = new Vector2(speed, 0);
                }
                else
                {
                    rb.velocity = new Vector2(-speed, 0);
                }
            }
        }
        if (Random.Range(0, 1.0f) < .3f)
        {
            if (goingHorizontal)
            {
                RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.up,1f);
                RaycastHit2D hit2 = Physics2D.Raycast(transform.position, -Vector2.up,1f);
                if (hit.collider == null && hit2.collider != null)
                {
                    goingHorizontal = true;
                    if (Random.Range(0, 1f) > .5f)
                    {
                        rb.velocity = new Vector2(0, speed);
                    }
                    else
                    {
                        rb.velocity = new Vector2(0, -speed);
                    }
                }
                else if (hit.collider == null)
                {
                    goingHorizontal = true;
                    rb.velocity = new Vector2(0, speed);
                }
                else if (hit2.collider == null)
                {
                    goingHorizontal = true;
                    rb.velocity = new Vector2(0, -speed);
                }
            }
            else
            {
                RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.right,1f);
                RaycastHit2D hit2 = Physics2D.Raycast(transform.position, -Vector2.right,1f);
           
                if (hit.collider == null && hit2.collider == null)
                {
                    goingHorizontal = true;
                    if (Random.Range(0, 1) > .5f)
                    {
                        rb.velocity = new Vector2(speed, 0);
                    }
                    else
                    {
                        rb.velocity = new Vector2(-speed, 0);
                    }
                }
                else if (hit.collider == null)
                {
                    goingHorizontal = true;
                    rb.velocity = new Vector2(speed, 0);
                }
                else if (hit2.collider == null)
                {
                    goingHorizontal = true;
                    rb.velocity = new Vector2(-speed, 0);
                }
            }
        }
        if (goingHorizontal)
        {
            rb.constraints = RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezeRotation;
        }
        else {
            rb.constraints = RigidbodyConstraints2D.FreezePositionX |RigidbodyConstraints2D.FreezeRotation;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "ghost") {
            rb.velocity = -rb.velocity;
        }
    }
}
