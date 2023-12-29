using UnityEngine;
using UnityEngine.Playables;

public class Door : MonoBehaviour
{
    public GameObject DoorSprite;
    public Animator doorAnimator;
    public BoxCollider2D doorCollider;
    void Awake()
    {
        doorAnimator = DoorSprite.GetComponent<Animator>();
        doorCollider = DoorSprite.GetComponent<BoxCollider2D>();
    }
}
