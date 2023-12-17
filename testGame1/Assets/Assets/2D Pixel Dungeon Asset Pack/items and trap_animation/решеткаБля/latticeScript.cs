using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

public class latticeScript : MonoBehaviour
{
    private PlayableDirector _latticeTimeline;

    private void Awake()
    {
            _latticeTimeline = GetComponent<PlayableDirector>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            _latticeTimeline.Play();
        }
    }

    public void DeleteLettice()
    {
        Destroy(gameObject);
    }
}
