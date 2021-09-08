﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;
using System.IO;


public class MenuManager : MonoBehaviour
{
    public static MenuManager Instance;


    [SerializeField] Menu [] menus;

    void Awake()
    {
        Instance = this;
    }
    public void OpenMenu(string menuName)
    {
        for(int i = 0; i < menus.Length; i++)
        {
            if(menus[i].menuName == menuName)
            {
                menus[i].Open();
            }
            else if (menus[i].open)
            {
                CloseMenu(menus[i]);
            }
        }
    }
    public void OpenMenu(Menu menu)
    {
        for(int i = 0; i < menus.Length; i++)
        {
            if (menus[i].open)
            {
                CloseMenu(menus[i]);
            }
        }
        menu.Open();
    }
    public void CloseMenu(Menu menu)
    {
        menu.Close();
    }
    public void OpenScene(string Scene)
    {
        PhotonNetwork.Disconnect();
        Destroy(RoomManager.Instance.gameObject);
        SceneManager.LoadScene(Scene);
    }
    public void OpenScene2(string Scene)
    {
        SceneManager.LoadScene(Scene);
    }
}
