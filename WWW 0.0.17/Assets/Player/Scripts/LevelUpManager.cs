using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LevelUpManager : MonoBehaviour
{
    public MovementStateManager movementStateManager;

    public TextMeshProUGUI levelText;

    void Update()
    {
        // Обновите текст с текущим уровнем из MovementStateManager
        levelText.text = "LEVEL: " + movementStateManager.level.ToString();

        // Пример: если достигнуты условия для повышения уровня (можно изменить)
        if (Input.GetKeyDown(KeyCode.U))
        {
            LevelUp();
        }
    }

    void LevelUp()
    {
        // Увеличение уровня
        movementStateManager.IncreaseLevel();
    }
}
