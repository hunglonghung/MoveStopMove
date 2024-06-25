using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
public class UserDataManager : Singleton<UserDataManager>
{
    public UserData userData;
    private const string userID = "UniqueUserID"; // Sử dụng một userID duy nhất

    private void Awake()
    {
        LoadUserID();
        LoadUserData();
    }


    private void LoadUserID()
    {
        if (!PlayerPrefs.HasKey(userID))
        {
            PlayerPrefs.SetString("UserID", userID);
            PlayerPrefs.Save();
        }
    }

    public void SaveUserData()
    {
        string json = JsonUtility.ToJson(userData);
        string userKey = GetUserKey();
        PlayerPrefs.SetString(userKey, json);
        PlayerPrefs.Save();
    }

    public void LoadUserData()
    {
        string userKey = GetUserKey();
        if (PlayerPrefs.HasKey(userKey))
        {
            string json = PlayerPrefs.GetString(userKey);
            userData = JsonUtility.FromJson<UserData>(json);
        }
        else
        {
            userData = new UserData();
            SaveUserData();
        }
    }

    private string GetUserKey()
    {
        return $"UserData_{userID}";
    }

    public void ResetUserDataToDefault()
    {
        userData = new UserData();
        SaveUserData();
    }
}