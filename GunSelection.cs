using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunSelection : MonoBehaviour
{
    public GameObject[] Guns;
    public bool Primary;
    public bool Secondary;
    public int on;
    // Start is called before the first frame update
    void OnEnable()
    {

    }
    void Start()
    {
        if(!Primary)
        {
            gameObject.SetActive(false);
        } else
        {
            gameObject.SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(Primary)
        {
            on = PlayerPrefs.GetInt("Weapon1");       
        } else if (Secondary)
        {
            on = PlayerPrefs.GetInt("Weapon2");
        }
    for (int i = 0; i < Guns.Length; i++)
        {
            Guns[i].SetActive( i == on);
        }
    }
}
