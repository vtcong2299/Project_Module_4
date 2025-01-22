using UnityEngine;

public class CharacterBase : MonoBehaviour, IAttackable, IBeAttackedable
{
    public virtual int _damage => 0;

    public virtual void BeAttacked(int damage) { }

    public void BeAttacked(float damage)
    {
        throw new System.NotImplementedException();
    }
}