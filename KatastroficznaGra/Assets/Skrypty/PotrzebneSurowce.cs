using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PotrzebneSurowce
{

    public Item itemy;
    public int ilosc;

    public PotrzebneSurowce(Item itemik, int Ilosc)
    {
        itemy = itemik;
        ilosc = Ilosc;
    }
}
