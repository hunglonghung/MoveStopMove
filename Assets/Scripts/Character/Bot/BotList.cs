using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "BotList", menuName = "ScriptableObjects/BotList", order = 2)]
public class BotList : ScriptableObject
{
    public List<CharacterData> bots;
}
