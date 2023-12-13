using UnityEngine.UI;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public PlayerOptions PlayerOptions;
    public Slider SliderMana;
    private float Mana;

    public GameObject Map;
    private bool isActiveMap = false;

    private void OnEnable()
    {
        EventSystem.Attack += SetMana;
    }
    private void OnDisable()
    {
        EventSystem.Attack -= SetMana;
    }
    private void Start()
    {
        SliderMana.value = PlayerOptions.MaxMana;
        Mana = PlayerOptions.MaxMana;
    }
    private void SetMana(float DeltaMana)
    {
        Mana = Mana - DeltaMana;
        SliderMana.value = Mana;
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

        if(Input.GetKeyDown(KeyCode.M))
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
