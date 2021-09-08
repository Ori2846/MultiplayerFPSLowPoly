﻿using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Look : MonoBehaviour
{
        #region Variables

        public static bool cursorLocked = true;

        public Transform player;
        public Transform normalCam;
        public Transform weaponCam;
        public Transform weapon;

        public float xSensitivity;
        public float ySensitivity;
        public float maxAngle;
        public PhotonView photonView;

        private Quaternion camCenter;

        #endregion

        #region Monobehaviour Callbacks

        void Start()
        {
            xSensitivity--;
            ySensitivity--;
            camCenter = normalCam.localRotation; //set rotation origin for cameras to camCenter
        }

        void Update()
        {
            if (!photonView.IsMine) return;
            if (Pause.paused) return;

            SetY();
            SetX();

            UpdateCursorLock();

            weaponCam.rotation = normalCam.rotation;
        }

        #endregion

        #region Private Methods

        void SetY()
        {
            float t_input = Input.GetAxis("Mouse Y") * ySensitivity * Time.deltaTime;
            Quaternion t_adj = Quaternion.AngleAxis(t_input, -Vector3.right);
            Quaternion t_delta = normalCam.localRotation * t_adj;

            if (Quaternion.Angle(camCenter, t_delta) < maxAngle)
            {
                normalCam.localRotation = t_delta;
            }

            weapon.rotation = normalCam.rotation;
        }

        void SetX()
        {
            float t_input = Input.GetAxis("Mouse X") * xSensitivity * Time.deltaTime;
            Quaternion t_adj = Quaternion.AngleAxis(t_input, Vector3.up);
            Quaternion t_delta = player.localRotation * t_adj;
            player.localRotation = t_delta;
        }

        void UpdateCursorLock()
        {
            if (cursorLocked)
            {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;

                if (Input.GetKeyDown(KeyCode.Escape))
                {
                    cursorLocked = false;
                }
            }
            else
            {
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;

                if (Input.GetKeyDown(KeyCode.Escape))
                {
                    cursorLocked = true;
                }
            }
        }
        #endregion
}
