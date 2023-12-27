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

    public GameObject Indicator;
    public GameObject MainMenu;
    public GameObject Map;
    public Animation EmptiMana;
    public Animation AnimHealth;
    private bool isActiveMap = false;

    private void OnEnable()
    {
        EventSystem.EmptiMana += SetEmptiMana;
        EventSystem.Attack += SetMana;
        EventSystem.AttackHealth += SetHealth;
        EventSystem.EndGame += EndGame;
    }
    private void OnDisable()
    {
        EventSystem.EmptiMana -= SetEmptiMana;
        EventSystem.Attack -= SetMana;
        EventSystem.AttackHealth -= SetHealth;
        EventSystem.EndGame -= EndGame;
    }
    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        //Cursor.visible = false;

        SliderMana.maxValue = PlayerOptions.MaxMana;
        Mana = PlayerOptions.MaxMana;

        SliderHealth.maxValue = PlayerOptions.MaxHealth;
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
        AnimHealth.Play();
    }
    private void SetEmptiMana()
    {
        EmptiMana.Play();
    }
    private void Update()
    {
        if(Mana < PlayerOptions.MaxMana)
        {
            Mana = Mana + PlayerOptions.ManaInSecond;
            SliderMana.value = Mana;

            if (Mana < PlayerOptions.ManaForSword)
                EventSystem.SetReadySword(false);
            else
                EventSystem.SetReadySword(true);

            if (Mana < PlayerOptions.ManaForBow)
                EventSystem.SetReadyBow(false);
            else
                EventSystem.SetReadyBow(true);

            if (Mana < PlayerOptions.ManaForHammer)

                EventSystem.SetReadyHammer(false);
            else
                EventSystem.SetReadyHammer(true);

            if (Mana < PlayerOptions.ManaForTeleport)

                EventSystem.SetReadyTeleport(false);
            else
                EventSystem.SetReadyTeleport(true);
        }

        if (Health < PlayerOptions.MaxHealth)
        {
            Health = Health + PlayerOptions.HealthInSecond;
            SliderHealth.value = Health;
        }

        if (Input.GetKeyDown(KeyCode.M))
        {
            SetMapActive();
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SetMainMenuActive();
        }
    }
    public void GetMainMenu()
    {
        SceneManager.LoadScene(1);
    }
    public void EndGame()
    {
        Invoke("SetMainMenuActive", 2);
    }
    private void SetMapActive()
    {
        isActiveMap = !isActiveMap;
        Map.SetActive(isActiveMap);

        if (isActiveMap)
        {
            Cursor.lockState = CursorLockMode.None;
            PlayerOptions.EnebaPlayerController(false);
            Indicator.SetActive(false);
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
            PlayerOptions.EnebaPlayerController(true);
            Indicator.SetActive(true);
        }
    }
    public void SetMainMenuActive()
    {
        if (MainMenu.activeSelf == false)
        {
            MainMenu.SetActive(true);
            PlayerOptions.EnebaPlayerController(false);
            Cursor.lockState = CursorLockMode.None;
            Indicator.SetActive(false);
        }
        else
        {
            MainMenu.SetActive(false);
            PlayerOptions.EnebaPlayerController(true);
            Cursor.lockState = CursorLockMode.Locked;
            Indicator.SetActive(true);
        }
    }
}
