using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using TMPro;
using Photon.Realtime;
using System.Linq;

public class Shop : MonoBehaviour
{

    public int WeaponSelected_;

    public void WeaponSelect(int WeaponSelected)
    {
        WeaponSelected_ = WeaponSelected;
    }
    private Animator animator;
    private string currentState;
    const string SHOP_OPEN_MENU = "GunShopOpen";
    const string SHOP_CLOSE_MENU = "GunShopClose";

    public static Shop Instance;
    private void Awake()
    {
    Instance = this;
    }
    void Start()
    {
        Animator animator = GetComponent<Animator>();
    }
    void ChangeAnimationState(string newState)
    {
        Animator animator = GetComponent<Animator>();
        if(currentState == newState) return;
        animator.Play(newState);
        currentState = newState;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    public void OpenShopMenu()
    {
        ChangeAnimationState(SHOP_OPEN_MENU);
        Debug.Log("Open");
    }
    public void CloseShopMenu()
    {
        ChangeAnimationState(SHOP_CLOSE_MENU);
    }
    public void WeaponSelectGlock()
    {
            PlayerPrefs.SetInt("Weapon"+ WeaponSelected_, 0);
            Debug.Log("Weapon" + WeaponSelected_ + "Selected as Glock");
    }
    public void WeaponSelectAK()
    {
            PlayerPrefs.SetInt("Weapon" + WeaponSelected_, 1);
            Debug.Log("Weapon" + WeaponSelected_ + "Selected as AK");
    }
}
