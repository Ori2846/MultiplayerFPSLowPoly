using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using System.IO;

public class Bullet : MonoBehaviour
{
    public int damage;
    GameObject Blood;
    GameObject ImpactStone;
    private int BulletTimer = 2;
    public float BulletMass;
    Rigidbody rb;
    Collider Collider_;
    public float speed;
    public float velocity_;
    //public Vector3 Center;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(RemoveBullet());
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //velocity_ = GetComponentInChildren<CapsuleCollider>().height;
        GetComponentInChildren<CapsuleCollider>().height = velocity_;
        //GetComponentInChildren<CapsuleCollider>().center = Center;
        rb = GetComponent<Rigidbody>();
        rb.mass = BulletMass;
        speed = Vector3.Dot(rb.velocity, transform.forward);
        if ( speed > 60 )
        {
            velocity_ = 20;
            //Center.y = velocity_/2;
        } else if (speed < 60 )
        {
            velocity_ = Random.Range(1,15);
            //Center.y = velocity_/2;
        }
    }
    IEnumerator RemoveBullet()
    {
        yield return new WaitForSeconds(BulletTimer);
        Destroy(gameObject);
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Structure")
        {
            //GameObject ImpactStone = Instantiate((GameObject)Resources.Load("BulletStoneImpact"),gameObject.transform.position, gameObject.transform.rotation);
            Destroy(gameObject);
            //pornhub.com/live
        }
        if(other.tag == "Player")
        {
            GameObject Blood = Instantiate((GameObject)Resources.Load("Blood"),gameObject.transform.position, gameObject.transform.rotation);
            other.gameObject.GetComponent<Idamageable>()?.TakeDamage(damage);
        }
    }
}
