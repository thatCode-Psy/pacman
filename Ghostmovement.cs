﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghostmovement : MonoBehaviour {
    public bool goingHorizontal;
    public Rigidbody2D rb;
    public float speed;
    public Animator an;
    public bool cooldown;
	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody2D>();
        cooldown = false;
        an = GetComponent<Animator>();
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
                        an.SetTrigger("goup");
                    }
                    else
                    {
                        rb.velocity = new Vector2(0, -speed);
                        an.SetTrigger("godown");
                    }
                }
                else if (hit.collider == null)
                {
                    rb.velocity = new Vector2(0, speed);
                    an.SetTrigger("goup");
                }
                else {
                    rb.velocity = new Vector2(0, -speed);
                    an.SetTrigger("godown");
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
                        an.SetTrigger("goright");
                    }
                    else
                    {
                        rb.velocity = new Vector2(-speed, 0);
                        an.SetTrigger("goleft");
                    }
                }
                else if (hit.collider== null)
                {
                    rb.velocity = new Vector2(speed, 0);
                    an.SetTrigger("goright");
                }
                else
                {
                    rb.velocity = new Vector2(-speed, 0);
                    an.SetTrigger("goleft");
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
