using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour {

    public float fireRate = 0;
    public float minShootVelocity = 5;
    private int Ammo = 3;
    float timeToFire;
    private string AmmoObject = "Items/Bow/Bow_Arrow";
    


    private void Start()
    {
        Ammo = 3;
        
    }

    public void AddAmmo()
    {
        Ammo += 3;
        Debug.Log("Reload ammo = " + Ammo);
    }

    public int shoot(float time,Vector3 position, Rigidbody2D character,bool FacingRight)
    {
        Debug.Log("J'ai tenu : " + time + "J'ai encore :" + Ammo+ " ");
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction = (mousePos - character.position).normalized;

        float angle = GetAngle(character.position,mousePos) ;
        angle = (FacingRight) ? -angle : 180 + angle ;

        
       
        if (FacingRight)
            direction.x = Mathf.Abs(direction.x);
        else
        {
            if (direction.x > 0)
                direction.x -= 2 * direction.x; 
        }

        

        GameObject projectile = Instantiate(Resources.Load(AmmoObject, typeof(GameObject)),(Vector2)position+direction, Quaternion.Euler(0f, 0f, angle)) as GameObject;

        projectile.GetComponent<Rigidbody2D>().velocity = direction * (minShootVelocity + projectile.GetComponent<ArrowAttack>().shootVelocity * -time) ;
        
        Ammo = Ammo - 1;
        return Ammo;
    }

    float GetAngle(Vector2 v1, Vector2 v2)
    {
        return Mathf.Atan2(v1.y - v2.y, Mathf.Abs(v1.x - v2.x)) * Mathf.Rad2Deg;
    }




}
