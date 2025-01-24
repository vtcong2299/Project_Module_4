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
        UpdateHP.Instance.ToUpdateHP(hp);
    }
    public void HealHp(float damage)
    {
        hp += damage * (dataPlayer.lifeStealPercentMax/100);
        if (hp > dataPlayer.hpMax)
        {
            hp = dataPlayer.hpMax;
        }
        UpdateHP.Instance.ToUpdateHP(hp);
    }
    private void OnCollisionEnter(Collision other) {
        if (IsDead())
        {
            return;
        }
        if (other.gameObject.CompareTag("Enemy"))
        {
            Receiver(other.gameObject.GetComponent<Enemy>().GetDamage());
            UpdateHP.Instance.ToUpdateHP(hp);
            if (IsDead())
            {
                anim.SetDead();
                StartCoroutine(DelayDead());
            }
        }
    }

    IEnumerator DelayDead()
    {
        yield return new WaitForSeconds(1.5f);
        GameManager.Instance.SetGameState(GameState.GameOver);
    }
}
