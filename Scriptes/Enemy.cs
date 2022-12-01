using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] float moveingSpeed = 1f;
    Rigidbody2D myRigbody;
    void Start()
    {
        myRigbody = GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        Move();
    }
    private void Move()
    {
        if (facingRight())
        {
            myRigbody.velocity = new Vector2(moveingSpeed, 0f);
        }
        else
        {
            myRigbody.velocity = new Vector2(-moveingSpeed, 0f);
        }
    }

    private bool facingRight()
    {
        return transform.localScale.x > 0;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Coin") { return; }
        var x = Mathf.Abs(transform.localScale.x);
        var y = transform.localScale.y;
        transform.localScale = new Vector2(-Mathf.Sign(myRigbody.velocity.x) * x, y);
    }









}
