using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "UIContainer", menuName = "ScriptableObjects/UIContainer", order = 1)]

public class UIContainer : ScriptableObject
{
    public GameObject failScreen;
    public GameObject passScreen;
    public GameObject mainCanvas;
    public GameObject moneyText;
}