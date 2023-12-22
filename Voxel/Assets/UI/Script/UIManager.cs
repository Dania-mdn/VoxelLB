using UnityEngine.UI;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public PlayerOptions PlayerOptions;
    public Slider SliderMana;
    private float Mana;
    
    public Slider SliderHealth;
    private float Health;

    public GameObject Map;
    private bool isActiveMap = false;

    private void OnEnable()
    {
        EventSystem.Attack += SetMana;
        EventSystem.AttackHealth += SetHealth;
    }
    private void OnDisable()
    {
        EventSystem.Attack -= SetMana;
        EventSystem.AttackHealth -= SetHealth;
    }
    private void Start()
    {
        SliderMana.value = PlayerOptions.MaxMana;
        Mana = PlayerOptions.MaxMana;

        SliderHealth.value = PlayerOptions.MaxHealth;
        Health = PlayerOptions.MaxHealth;
    }
    private void SetMana(float DeltaMana)
    {
        Mana = Mana - DeltaMana;
        SliderMana.value = Mana;
    }
    private void SetHealth(float DeltaHealth)
    {
        Health = Health - DeltaHealth;
        SliderHealth.value = Health;
    }
    private void Update()
    {
        if(Mana < PlayerOptions.MaxMana)
        {
            Mana = Mana + PlayerOptions.ManaInSecond;
            SliderMana.value = Mana;
            if (Mana < PlayerOptions.ManaForWeapon)
                EventSystem.SetReadyAction(false);
            else
                EventSystem.SetReadyAction(true);
        }

        if (Health < PlayerOptions.MaxHealth)
        {
            Health = Health + PlayerOptions.HealthInSecond;
            SliderHealth.value = Health;
            //if (Health < PlayerOptions.HealthForWeapon)
            //    EventSystem.SetReadyAction(false);
            //else
            //    EventSystem.SetReadyAction(true);
        }

        if (Input.GetKeyDown(KeyCode.M))
        {
            SetMapActive();
        }
    }
    private void SetMapActive()
    {
        isActiveMap = !isActiveMap;
        Map.SetActive(isActiveMap);
    }
}
