using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;
using System.IO;


public class Pause : MonoBehaviour
    {
        public static bool paused = false;
        private bool disconnecting = false;

        public void TogglePause()
        {
            if (disconnecting) return;

            paused = !paused;

            transform.GetChild(0).gameObject.SetActive(paused);
            Cursor.lockState = (paused) ? CursorLockMode.None : CursorLockMode.Confined;
            Cursor.visible = paused;
        }

        public void Quit()
        {
                    PhotonNetwork.Disconnect();
        Destroy(RoomManager.Instance.gameObject);
        SceneManager.LoadScene(0);
            //disconnecting = true;
            //PhotonNetwork.Disconnect();
            //Destroy(RoomManager.Instance.gameObject);
            //Destroy(RoomManager.Instance.gameObject);
            //PhotonNetwork.LeaveRoom();
            //SceneManager.LoadScene(0);
        }
    }
