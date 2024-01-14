using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponRockScript : MonoBehaviour
{
    private Item item;
    private SpriteRenderer placeForItem;

    private void Start()
    {
        placeForItem = transform.GetChild(0).GetComponent<SpriteRenderer>();
    }

    private void chooseItem()
    {
               
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {

        }
    }
}
