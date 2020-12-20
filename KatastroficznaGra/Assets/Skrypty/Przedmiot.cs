using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//edytowany 2.12.2020

public class Przedmiot : MonoBehaviour
{
    //public int ID;
    public bool przelacznik; //czy działa trigger
    public bool przelacznik2; //czy wyswietlic co widzisz
    public bool czyZniknie;
    public float timer;
    public int kiedyZniknie;
    public Item Item;
    //zastanowic sie na zmiennymi przy rozdrabnianiu skały

    void Update()
    {

        if (czyZniknie == true)
        {
            timer += Time.deltaTime;

            if (timer > kiedyZniknie)
            {
                Destroy(this.gameObject);
            }
        }

    }
    /*
    void OnMouseEnter()
    {
        przelacznik2 = true;
    }

    void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0) && przelacznik)
        {
            //gracz.GetComponentInChildren<Inventory>().SendMessage("AddItem", Item);
            Destroy(this.gameObject);
        }
    }

    void OnMouseExit()
    {
        przelacznik2 = false;
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Player")
        {
            przelacznik = true;
            gracz = col.gameObject;
        }
    }

    void OnTriggerExit(Collider col)
    {
        if (col.tag == "Player")
        {
            przelacznik = false;
        }
    }

    void OnGUI()
    {
        if (przelacznik2)
        {
            GUI.Label(new Rect(Screen.width / 2, Screen.height / 2, 100, 30), this.name);
        }
    }*/
}
