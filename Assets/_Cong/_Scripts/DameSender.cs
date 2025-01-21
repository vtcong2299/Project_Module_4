using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DameSender : MonoBehaviour
{
    private DameReceiver dameReceiver;
    public float damage = 1f;
    private void Awake()
    {
        dameReceiver = GetComponent<DameReceiver>();
    }
    public virtual void SendDame()
    {
        if (dameReceiver != null)
        {
            dameReceiver.Receiver(damage);
        }
        else
        {
            return;
        }
    }
}
