using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class Ball : MonoBehaviour
{
    public Rigidbody2D ballRb;
    private float _standartBallDamage = 15;
    private Collider2D _ballCollider;
    private PlayableDirector _ballDeathTimeline;
    void Awake ()
    {
        _ballDeathTimeline = GetComponent<PlayableDirector>();
        ballRb = GetComponent<Rigidbody2D>();
        _ballCollider = GetComponent<Collider2D>();
        Physics2D.IgnoreLayerCollision(7, 6, ignore: true);
    }
    public void BallDeath()
    {
        Destroy(gameObject);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag != "Player" || collision.gameObject.tag != "Ball")
        {
            if (collision.gameObject.GetComponent<Enemy>())
            {
                collision.gameObject.GetComponent<Enemy>().GetDamage(_standartBallDamage);
                _ballDeathTimeline.Play();
            }
            else
            {
                _ballDeathTimeline.Play();
            }
        }
    }
}
