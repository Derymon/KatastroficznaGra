using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Item : ScriptableObject
{
    
    public string itemNazwa;
    public int itemID;
    public string itemOpis;
    public Texture2D itemIkona;
    //public GameObject model;

    public Item()
    {
        //nic
    }

    public Item(int id, string nazwa, string opis)
    {
        itemNazwa = nazwa;
        itemOpis = opis;
        itemID = id;
    }
}
