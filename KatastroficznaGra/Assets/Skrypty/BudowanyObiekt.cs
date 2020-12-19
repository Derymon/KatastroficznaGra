using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//edytowany 29.11.2020

public class BudowanyObiekt : MonoBehaviour
{
    public string Nazwa;
    //lista materiałów potrzebnych do budowy obiektu
    public List<PotrzebneSurowce> listaPotrzebnychSurowcow = new List<PotrzebneSurowce>();
    float MaxPotrzebneMaterialy;
    public Material FinalnyMaterial;
    public Collider collider;
    float DostarczoneMaterialy;
    public BudowanyObiekt budowanyObiekt;

    Color colorStart = new Color(1.0f, 1.0f, 1.0f, 0.5f); //biały przezroczysty
    Color colorEnd = new Color(0.0f, 1.0f, 0.0f, 0.5f); //zielony przezroczysty
    Color w_color = Color.white;
    //public float duration = 10.0f;
    public Renderer rend;

    //kontrolna zmienna tekstowa dla komunikatów
    public string TekstWiadomosci = " ";

    void Start()
    {
        rend = GetComponent<Renderer>();
        collider = GetComponent<Collider>();
        collider.isTrigger = true;
        DostarczoneMaterialy = 0;
        MaxPotrzebneMaterialy = ZliczMaterialy();
    }

    void Update()
    {
        if(MaxPotrzebneMaterialy-DostarczoneMaterialy <= 0)
        {
            if (MaxPotrzebneMaterialy - DostarczoneMaterialy < 0)
            {
                Debug.Log("Dostarczone Materialy na minusie");
            }
            rend.material = FinalnyMaterial;
            collider.isTrigger = false;
            Destroy(this);
        }
        else
        {
            float lerp = DostarczoneMaterialy / MaxPotrzebneMaterialy;
            w_color = Color.Lerp(colorStart, colorEnd, lerp);
            rend.material.color = w_color;
        }
    }

    public void DostarczMaterialy(int itemID) //dostarcz materialy ze zmienna Item
    {

        UsunZListyPotrzeb(itemID);
        //float zmienna = MaxPotrzebneMaterialy - DostarczoneMaterialy;
        TekstWiadomosci = "Brakuje jeszcze: ";
        for (int x = 0; x < listaPotrzebnychSurowcow.Count; x++)
        {
            TekstWiadomosci += listaPotrzebnychSurowcow[x].itemy.itemNazwa + ": " + listaPotrzebnychSurowcow[x].ilosc.ToString() + " ";
        }
        //Debug.Log("Brakuje jeszcze: " + (MaxPotrzebneMaterialy - DostarczoneMaterialy).ToString());
    }


    public float ZliczMaterialy()
    {
        float wynik = 0;
        for (int x = 0; x < listaPotrzebnychSurowcow.Count; x++)
        {
            wynik += listaPotrzebnychSurowcow[x].ilosc;
        }
        return wynik;
    }

    public void UsunZListyPotrzeb(int itemID)
    {
        for(int x = 0; x < listaPotrzebnychSurowcow.Count; x++)
        {
            if(itemID == listaPotrzebnychSurowcow[x].itemy.itemID)
            {
                listaPotrzebnychSurowcow[x].ilosc--;
                if(listaPotrzebnychSurowcow[x].ilosc == 0)
                {
                    listaPotrzebnychSurowcow.RemoveAt(x);
                }
                DostarczoneMaterialy++;
                break;
            }
        }
    }

    public bool SprawdzItem(Item itemdoSPR)
    {
        for (int x = 0; x < listaPotrzebnychSurowcow.Count; x++)
        {
            if (itemdoSPR == listaPotrzebnychSurowcow[x].itemy)
            {
                return true;
            }
        }
        return false;
    }

    void OnGUI()
    {
        //GUI.TextArea(new Rect(10, 10, 400, 100), TekstWiadomosci, 300);
    }
}
