using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private int levelNumber;

    private void Start()
    {
        UserManager.playerLevel = levelNumber;
        Debug.Log("Nivel actual: " + UserManager.playerLevel);
    }
}
