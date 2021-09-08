using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu : MonoBehaviour
{
    public string menuName; 
    public bool open;
    public bool LoadingScreen;

    public void Open()
    {
        open = true;
        gameObject.SetActive(true);
    }
    public void Close()
    {
        open = false;
        gameObject.SetActive(false);
    }
    void OnEnable()
    {
        StartCoroutine(Timer_());
    }
    void OnDisable()
    {
        StopCoroutine(Timer_());
    }
    IEnumerator Timer_()
    {
        if(LoadingScreen)
        {
            Debug.Log("works");
            yield return new WaitForSeconds(5f);
            Launcher.Instance.OnCreateRoomFailed(32, "ErrorCode(1) Roomfull");
        }
    }
}
