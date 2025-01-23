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
    public void HealHp(float damage)
    {
        hp += damage * dataPlayer.lifeStealPercentMax;
        if (hp > dataPlayer.hpMax)
        {
            hp = dataPlayer.hpMax;
        }
    }
    private void OnCollisionEnter(Collision other) {
        if (other.gameObject.CompareTag("Enemy"))
        {
            Receiver(other.gameObject.GetComponent<Enemy>().GetDamage());
        }
    }
}
