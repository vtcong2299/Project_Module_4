using UnityEngine;
using UnityEngine.UI;

public class UpdateHP : MonoBehaviour
{
    Slider HPBar;
    void Start()
    {
        HPBar = GetComponent<Slider>();
        HPBar.maxValue = 1000;//DataPlayer.Instance.;
        HPBar.value = HPBar.maxValue;
    }

    public void ToUpdateHP(float currentHP)
    {
        HPBar.value = currentHP;
    }
}