using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataPlayer : Singleton<DataPlayer>, IOnEnemyDie, IOnGameStart<IGameData>
{
    public int curentLevel;
    [SerializeField] int levelMax = 15;
    public float exp=0;
    public float expMax;
    [SerializeField] float[] exps = new float[] {};
     
    float damageDefault;
    float hpDefault;
    float attackCountdownDefault;
    float moveSpeedDefault;
    //float armorDefault;
    float lifeStealPercentDefault;

    public float damageMax;
    public float hpMax;
    public float currentAttackCountdown;
    public float moveSpeedMax;
    //public float armorMax;
    public float lifeStealPercentMax;
    [SerializeField] float damagePercentIncreased = 0;
    [SerializeField] float hpPercentIncreased = 0;
    [SerializeField] float attackSpeedPercentIncreased = 0;
    [SerializeField] float moveSpeedPercentIncreased = 0;
    //[SerializeField] float armorPercentIncreased = 0;

    IGameData gameData;

    public Action<IGameData> onGameStartAction => data => gameData = data;


    public void StartPlayerData()
    {
         damageDefault = gameData.data.damageDefault;
         hpDefault = gameData.data.hpDefault;
         attackCountdownDefault = gameData.data.attackCountdownDefault;
         moveSpeedDefault = gameData.data.moveSpeedDefault;
         //armorDefault = gameData.data.armorDefault;
         lifeStealPercentDefault = gameData.data.lifeStealPercentDefault;
         ResetDataPlayer();
         curentLevel = 1;
         PlayerCtrl.Instance.playerReceiveDame.StartPlayerDameReceive();
    }
    public void AddPowerUp(ConfigPowerUp buff)
    {
        damagePercentIncreased += buff.damage;
        hpPercentIncreased += buff.hp;
        attackSpeedPercentIncreased += buff.attackSpeed;
        moveSpeedPercentIncreased += buff.moveSpeed;
        //armorPercentIncreased += buff.armor;
        lifeStealPercentMax += buff.lifeSteal;
        RecalculateStats();
    }
    public void ResetDataPlayer()
    {
        damageMax = damageDefault;
        hpMax = hpDefault;
        currentAttackCountdown = attackCountdownDefault;
        moveSpeedMax = moveSpeedDefault;
        //armorMax = armorDefault;
        lifeStealPercentMax = lifeStealPercentDefault;
        damagePercentIncreased = 0;
        hpPercentIncreased = 0;
        attackSpeedPercentIncreased = 0;
        moveSpeedPercentIncreased = 0;
        //armorPercentIncreased = 0;
    }
    void RecalculateStats()
    {
        damageMax = damageDefault * ((damagePercentIncreased/100)+1);
        hpMax = hpDefault * ((hpPercentIncreased / 100)+1);
        currentAttackCountdown = attackCountdownDefault * (100 / (attackSpeedPercentIncreased + 100)) ;
        moveSpeedMax = moveSpeedDefault * ((moveSpeedPercentIncreased/100) +1);
        
        //armorMax = armorDefault * ((armorPercentIncreased / 100)+1);
    }
    public void LevelUp(float exp)
    {
        expMax = exps[curentLevel-1];
        if (this.exp > expMax)
        {
            UIManager.Instance.OnEnablePanelPowerUp();
            curentLevel++;
            if (curentLevel >= levelMax)
            {
                curentLevel = levelMax;
                return;
            }
            else
            {
                this.exp = 0;
            }
        }
        if (UIManager.Instance.hasPanelBuff) return;
        this.exp += exp;
    }

    public void OnEnemyDie(GameObject dyingEnemy, float exp)
    {
        LevelUp(exp);
    }
}
