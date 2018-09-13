using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghostmovement : MonoBehaviour {
    public bool goingHorizontal;
    public Rigidbody2D rb;
    public float speed;
    public bool cooldown;
	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody2D>();
        cooldown = false;
	}
	
	// Update is called once per frame
	void Update () {
        if (Mathf.Abs(rb.velocity.x) < .1f && Mathf.Abs(rb.velocity.y) < .1f) {
            if (goingHorizontal)
            {
                RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.up,1f);
                RaycastHit2D hit2 = Physics2D.Raycast(transform.position, -Vector2.up,1f);
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
                RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.right,1f);
                RaycastHit2D hit2 = Physics2D.Raycast(transform.position, -Vector2.right,1f);
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
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "ghost")
        {
            rb.velocity = -rb.velocity;
        }
    }
}
