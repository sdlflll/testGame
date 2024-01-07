using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponRockScript : MonoBehaviour
{
    private Weapon Weapon;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {

        }
    }
}
