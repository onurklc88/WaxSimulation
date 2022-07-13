using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "PlayerData", menuName = "ScriptableObjects/PlayerData", order = 1)]

public class PlayerScriptable : ScriptableObject
{
    public int playerMoney;
    public int gameScore;
    public int levelNumber;
}