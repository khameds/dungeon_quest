using UnityEngine;

[CreateAssetMenu]
public class Item : ScriptableObject
{
    
    public Sprite descriptionSprite;
    public Sprite ammoSprite;
    public GameObject itemObject;

    public void init(string spr1,string spr2,string item,string name)
    {
        if(name.Equals("Bow"))
            descriptionSprite = Resources.LoadAll<Sprite>(spr1)[1];
        else
            descriptionSprite = Resources.Load<Sprite>(spr1);
        ammoSprite = Resources.Load<Sprite>(spr2);
        itemObject = Instantiate(Resources.Load(item) as GameObject);

        this.name = name;
    }
}
