using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using UnityEngine.UI;

public class HUD : NetworkBehaviour
{
    public Slider HealthSlider;
    public Slider ExpSlider;
    public GameObject HealthBar;
    public GameObject ExpBar;
    public Text HealthText;
    public Text ExpText;
    public Text LevelText;

    private void Start()
    {
        HealthSlider = HealthBar.GetComponent<Slider>();
        ExpSlider = ExpBar.GetComponent<Slider>();
    }

    [TargetRpc]
    public void RpcSetHealth(NetworkConnection conn, int health, int maxHealth)
    {
        HealthSlider.value = health;
        HealthText.text = $"{health}/{maxHealth}";
    }
    [TargetRpc]
    public void RpcSetMaxHealth(NetworkConnection conn, int health, int maxHealth)
    {
        Debug.LogError("RpcSetMaxHealth called with health = " + health + " and maxHealth = " + maxHealth);
        HealthSlider.maxValue = maxHealth;
        HealthSlider.value = health;
        HealthText.text = $"{health}/{maxHealth}";
    }

    [TargetRpc]
    public void RpcSetExp(NetworkConnection conn, int exp, int level, int maxExp)
    {
        ExpSlider.value = exp;
        ExpText.text = $"{exp}/{maxExp}";
        LevelText.text = level.ToString();
    }

    [TargetRpc]
    public void RpcSetMaxExp(NetworkConnection conn, int exp, int level, int maxExp)
    {
        Debug.LogError("RpcSetMaxExp called with exp = " + exp + " and maxExp = " + maxExp);
        ExpSlider.maxValue = maxExp;
        ExpSlider.value = exp;
        ExpText.text = $"{exp}/{maxExp}";
        LevelText.text = level.ToString();
    }


}
