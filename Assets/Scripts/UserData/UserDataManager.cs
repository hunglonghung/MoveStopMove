using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
public class UserDataManager : Singleton<UserDataManager>
{
    public UserData userData; 

    private void Awake()
    {
        LoadUserData(); 
    }

    public void SaveUserData()
    {
        string json = JsonUtility.ToJson(userData); 
        PlayerPrefs.SetString("", json);
        PlayerPrefs.Save(); 
    }

    public void LoadUserData()
    {
        if (PlayerPrefs.HasKey("")) 
        {
            string json = PlayerPrefs.GetString(""); 
            userData = JsonUtility.FromJson<UserData>(json); 
            {
                userData = new UserData(); 
                SaveUserData(); 
            }
        }
    }
}