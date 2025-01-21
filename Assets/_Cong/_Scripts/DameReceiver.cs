using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DameReceiver : MonoBehaviour
{
    public float hp = 1f;

    public virtual bool IsDead()
    {
        return this.hp <= 0;
    }
    public virtual void Receiver(float damage)
    {
        hp -= damage;
    }
}
