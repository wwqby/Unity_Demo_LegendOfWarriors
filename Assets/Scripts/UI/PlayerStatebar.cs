using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStatebar : MonoBehaviour
{
    public Image healthBar;
    public Image healthDelayBar;
    public Image powerBar;



    private void Update()
    {
        if (healthDelayBar.fillAmount > healthBar.fillAmount)
        {
            healthDelayBar.fillAmount -= Time.deltaTime*0.1f;
        }
    }

    /// <summary>
    /// 变化血量条
    /// </summary>
    /// <param name="presentHealth">current/max</param>
    public void ChangeHealth(float presentHealth)
    {
        healthBar.fillAmount = presentHealth;
    }
}
