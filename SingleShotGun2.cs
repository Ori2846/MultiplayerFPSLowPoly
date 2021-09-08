using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using System.IO;

public class SingleShotGun2 : Gun
{
    [SerializeField] Camera cam;
    public GameObject bullet;
    public Transform Point;
    public int force;
    PhotonView PV;
    //Rigidbody br;
    void Awake()
    {
        PV = GetComponent<PhotonView>();
    }
    public override void Use()
    {
        PV.RPC("Shoot", RpcTarget.All);

        //Shoot();
        Debug.Log("Using gun" + itemInfo.itemName);
    }
    [PunRPC]
    void Shoot()
    {
        GameObject bullet = Instantiate((GameObject)Resources.Load("Bullet"),Point.transform.position, Point.transform.rotation);
        bullet.GetComponent<Bullet>().damage = 50;
        Rigidbody br = bullet.GetComponent<Rigidbody>();
        br.AddRelativeForce(Vector3.forward * force * Time.deltaTime, ForceMode.Impulse);
        //PhotonNetwork.Instantiate("Bullet", transform.position, transform.rotation);
        //Ray ray = cam.ViewportPointToRay(new Vector3(0.5f, 0.5f));
        //ray.origin = cam.transform.position;
        //if(Physics.Raycast(ray, out RaycastHit hit))
        //{
        //    hit.collider.gameObject.GetComponent<Idamageable>()?.TakeDamage(((GunInfo)itemInfo).damage);
        //}
    }
}
