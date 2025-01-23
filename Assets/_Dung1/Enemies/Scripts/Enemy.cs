using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float HP;

    [SerializeField] private float damage; //Sát thưởng của quái gây lên người chơi

    [SerializeField] private float exp; //Kinh nghiệm rớt ra khi quái chết
    private Animator animator;

    public bool isDead;

    float HPSave;

    private void Awake() {
        animator = GetComponent<Animator>();
        HPSave = HP;
    }

    List<IOnEnemyDie> onDie;
    Transform playerTransform;
    public void SetDependencies(ITransformGettable player, List<IOnEnemyDie> enemyDieCalls)
    {
        onDie = enemyDieCalls;
        playerTransform = player._transform;
        SetUpBehaviours();
    }

    private void SetUpBehaviours()
    {
        foreach (var behaviour in animator.GetBehaviours<EnemyBehaviourBase>())
        {
            behaviour.SetPlayerTransform(playerTransform);
        }
    }

    public void Revive()
    {
        SetUpBehaviours();
        HP = HPSave;
        isDead = false;
    }

    public void TakeDamage(float damageAmount)
    {
        if (isDead){
            return;
        }

        HP -= damageAmount;

        if (HP <= 0)
        {
            isDead = true;
            foreach (var act in onDie)
            {
                act.OnEnemyDie(exp);
            }
            animator.SetTrigger("isDead");
            StartCoroutine(DeactiveAfterDelay());
        } else {
            animator.SetTrigger("damaged");
            // Viết hàm hiện damage ở đây
        }
    }

    public float GetDamage()
    {
        return damage;
    }

    private IEnumerator DeactiveAfterDelay()
    {
        yield return new WaitForSeconds(1f);
        gameObject.SetActive(false);
    }
}
