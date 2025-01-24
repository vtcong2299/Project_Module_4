using UnityEngine.UI;

public class UpdateHP :Singleton<UpdateHP>
{
    Slider HPBar;
    public void ToUpdateHP(float currentHP)
    {
        if (HPBar == null)
        {
            HPBar = GetComponent<Slider>();
            SetHPBarMaxValue();
        }
        HPBar.value = currentHP;
    }

    public void SetHPBarMaxValue()
    {
        HPBar.maxValue = DataPlayer.Instance.hpMax;
    }
}