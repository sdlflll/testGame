using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    public Rigidbody2D ballRb;
    private float _standartBallDamage = 15;
    private Collider2D _ballCollider;
    void Awake ()
    {
        ballRb = GetComponent<Rigidbody2D>();
        _ballCollider = GetComponent<Collider2D>();
        Physics2D.IgnoreLayerCollision(7, 6, ignore: true);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag != "Player" || collision.gameObject.tag != "Ball")
        {
            if (collision.gameObject.GetComponent<Enemy>())
            {
                collision.gameObject.GetComponent<Enemy>().GetDamage(_standartBallDamage);
                Destroy(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }
}
