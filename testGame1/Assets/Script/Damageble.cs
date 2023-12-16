using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damageble : IDamageble
{
    public void GetDamage(float Health, float Damage)
    {
        Health -= Damage;
    }
}
