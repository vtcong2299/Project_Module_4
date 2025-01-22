using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerReceiveDame : DameReceiver
{
    [SerializeField] DataPlayer dataPlayer;

    public void StartPlayerDameReceive()
    {
        hp = dataPlayer.hpMax;
    }
    private void Update()
    {
        UpdateHP.Instance.ToUpdateHP(hp);
    }

    private void OnCollisionEnter(Collision other) {
        if (other.gameObject.CompareTag("Enemy"))
        {
            Receiver(other.gameObject.GetComponent<Enemy>().GetDamage());
        }
    }
}
