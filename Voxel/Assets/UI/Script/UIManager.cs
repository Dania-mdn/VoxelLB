using UnityEngine.UI;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public Slider SliderMana;
    public float MaxMana;
    private float Mana;
    public float ManaInSecond;

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
        SliderMana.value = MaxMana;
        Mana = MaxMana;
    }
    private void SetMana(float DeltaMana)
    {
        Mana = Mana - DeltaMana;
    }
    private void Update()
    {
        if(Mana < MaxMana)
        {
            Mana = Mana + ManaInSecond;
            SliderMana.value = Mana;
        }
    }
}
