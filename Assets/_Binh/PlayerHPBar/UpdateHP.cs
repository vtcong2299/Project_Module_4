using UnityEngine.UI;

public class UpdateHP :Singleton<UpdateHP>
{
    Slider HPBar;
    void Start()
    {
        HPBar = GetComponent<Slider>();
    }
    private void Update()
    {
        HPBar.maxValue = DataPlayer.Instance.hpMax;
    }
    public void ToUpdateHP(float currentHP)
    {
        HPBar.value = currentHP;
    }
}