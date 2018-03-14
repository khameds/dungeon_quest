using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour {

    public float fireRate = 0.5f;
    public float minShootVelocity = 5;
    private int Ammo = 0;
    float timeToFire;
    public string AmmoObject;
    


    private void Start()
    {
        Ammo = 3;
        //fireRate = 0.5f;
    }

    public void AddAmmo()
    {
        Ammo += 2;
        Debug.Log("Reload ammo = " + Ammo);
    }

    public int shoot(string weapon, int playerNumber, float time, Vector3 position, Rigidbody2D character, bool FacingRight)
    {
        Debug.Log("J'ai encore :" + Ammo + " ");
        Debug.Log("Item: " + weapon);
        Vector2 mousePos;

        if (playerNumber == 0) //Mouse
        {
            mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }
        else //Gamepad
        {
            mousePos = new Vector2(character.position.x + GamepadManagement.getStateByUserNumber(playerNumber).ThumbSticks.Right.X, character.position.y + GamepadManagement.getStateByUserNumber(playerNumber).ThumbSticks.Right.Y);
        }

        Vector2 direction = (mousePos - character.position).normalized;

        float angle = GetAngle(character.position, mousePos);

        angle = (FacingRight) ? -angle : 180 + angle;

        if (FacingRight)
            direction.x = Mathf.Abs(direction.x);
        else
        {
            if (direction.x > 0)
                direction.x -= 2 * direction.x;
        }

        if (time < -1.2f)
            time = -1.2f;





        if(weapon.Equals("Shotgun"))
        {
            for (int i=-3;i<3;i++)
                {
                    if (i != 0)
                    {
                        GameObject projectile = Instantiate(Resources.Load(AmmoObject, typeof(GameObject)), (Vector2)position + direction + new Vector2(0,i*0.15f), Quaternion.Euler(0f, 0f, angle)) as GameObject;
                        projectile.GetComponent<Rigidbody2D>().velocity = (direction + new Vector2(0, i * 0.15f)) * (minShootVelocity + projectile.GetComponent<Shotgun_Attack>().shootVelocity);
                    }
                }
        }
        
        if (weapon.Equals("Bow"))
        {
                GameObject projectile = Instantiate(Resources.Load(AmmoObject, typeof(GameObject)), (Vector2)position + direction, Quaternion.Euler(0f, 0f, angle)) as GameObject;
                projectile.GetComponent<Rigidbody2D>().velocity = direction * (minShootVelocity + projectile.GetComponent<ArrowAttack>().shootVelocity * -time);
        }
        
        Ammo = Ammo - 1;
        return Ammo;
    }

    float GetAngle(Vector2 v1, Vector2 v2)
    {
        return Mathf.Atan2(v1.y - v2.y, Mathf.Abs(v1.x - v2.x)) * Mathf.Rad2Deg;
    }




}
