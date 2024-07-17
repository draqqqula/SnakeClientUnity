using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class AbilityButtonController : MonoBehaviour
{
    public Image ProgressCircle;
    public Button Button;
    public SpriteRenderer AbilityIcon;
    public Sprite[] Textures;

    public float CooldownExpires { get; set; } = 0;
    public bool OnCooldown { get; set; } = false;
    public float Duration { get; set; } = 0;

    void Start()
    {
        ProgressCircle.fillAmount = 0;
    }

    void Update()
    {
        if (!OnCooldown)
        {
            return;
        }
        if (Time.time >= CooldownExpires)
        {
            OnCooldown = false;
            ProgressCircle.fillAmount = 0;
            Button.interactable = true;
            return;
        }
        ProgressCircle.fillAmount = (CooldownExpires - Time.time) / Duration; 
    }

    public void SetCooldown(float duration)
    {
        Button.interactable = false;
        CooldownExpires = Time.time + duration;
        OnCooldown = true;
        Duration = duration;
    }

    public void SetAbilityIcon(int index)
    {
        if (index < 0 || index > Textures.Length)
        {
            return;
        }
        AbilityIcon.sprite = Textures[index];
    }
}
