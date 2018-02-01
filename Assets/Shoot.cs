using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour {

    public Rigidbody2D rb;
    public float fireRate = 0;
    private int Ammo = 3;
    float timeToFire;
    private string AmmoObject = "Items/Bow/Bow_Arrow";
    private void Start()
    {
        Ammo = 3;
        rb = GetComponent<Rigidbody2D>();
    }

    public void AddAmmo()
    {
        Ammo += 3;
        Debug.Log("Reload ammo = " + Ammo);
    }

    public int shoot(float time, Vector2 direction,Vector3 position, Quaternion rotation)
    {
        Debug.Log("J'ai tenu : " + time + "J'ai encore :" + Ammo);
        Instantiate(Resources.Load(AmmoObject, typeof(GameObject)),position,rotation);
        
        rb.AddForce(new Vector2(10, 10), ForceMode2D.Impulse);
        Ammo = Ammo - 1;
        return Ammo;
    }
}
