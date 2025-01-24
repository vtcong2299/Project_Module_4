using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerReceiveDame : DameReceiver
{
    [SerializeField] DataPlayer dataPlayer;
    [SerializeField] PlayerAnim anim;

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
        hp += damage * (dataPlayer.lifeStealPercentMax/100);
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
