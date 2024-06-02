using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "CharacterData", menuName = "ScriptableObjects/CharacterData", order = 1)]
public class CharacterData : ScriptableObject
{
    public string botName;
    public GameObject weapon;
    public GameObject bullet;
    public GameObject pants;
    public Color botColor;
}
