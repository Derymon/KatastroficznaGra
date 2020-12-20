using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//edytowany 18.12.2020

public class KontaktNPC : MonoBehaviour
{
    public string Imie;
    public Ekwipunek ekwipunekObiekt;
    bool AktywnaRozmowa;
    int AktywnyWybor;
    bool AktywnyHandel;
    string Wiadomosc;
    public int MaxKomunikatow;
    public Texture BialeTlo;

    [Header("Komunikaty:")]
    public string Powitalny;
    //public List<string> Komunikaty;

    public BlokRozmowy blok;
    

    // Start is called before the first frame update
    void Start()
    {
        AktywnyWybor = 0;
        AktywnaRozmowa = false;
        Wiadomosc = Powitalny;
        ekwipunekObiekt = this.gameObject.GetComponent<Ekwipunek>();
    }

    // Update is called once per frame
    void Update()
    {
        if(AktywnaRozmowa)
        {
            if (Input.GetKeyUp(KeyCode.W))
            {
                AktywnyWybor--;
                if (AktywnyWybor < 0) AktywnyWybor = 0;
            }
            if (Input.GetKeyUp(KeyCode.S))
            {
                AktywnyWybor++;
                if (AktywnyWybor > blok.OpcjeWyboru.Count-1) AktywnyWybor = blok.OpcjeWyboru.Count-1;
            }
            if(Input.GetKeyUp(KeyCode.Return))
            {
                Debug.Log("Wcisnieto enter");
            }
        }
        if(Input.GetKeyUp(KeyCode.F))
        {
            AktywnaRozmowa = false;
            Debug.Log("Dezaktywuje handel");
        }
    }

    public void AktywujRozmowe()
    {
        AktywnaRozmowa = true;
    }

    void OnGUI()
    {
        if(AktywnaRozmowa)
        {
            GUI.Box(new Rect(Screen.width / 2, Screen.height / 2 + 100, 50, 25), Imie);
            GUI.Box(new Rect(50, Screen.height / 2 + 150, Screen.width-100, 50), Wiadomosc);

            for(int x = 0; x<blok.OpcjeWyboru.Count;x++)
            {
                if (x != AktywnyWybor)
                {
                    //GUI.Box(new Rect(Screen.width / 2 + 75, Screen.height / 2 - 30 + x * 30, 200, 25), "Opcja " + x.ToString());
                    GUI.Box(new Rect(Screen.width / 2 + 75, Screen.height / 2 - 30 + x * 30, 250, 25), blok.OpcjeWyboru[x].ToString());
                }
                else
                { 
                    GUI.DrawTexture(new Rect(Screen.width / 2 + 75, Screen.height / 2 - 30 + x * 30, 250, 25), BialeTlo);
                    GUI.Box(new Rect(Screen.width / 2 + 75, Screen.height / 2 - 30 + x * 30, 250, 25), blok.OpcjeWyboru[x].ToString());
                    //GUI.Label(new Rect(Screen.width / 2 + 75, Screen.height / 2 - 30 + x * 30, 200, 25), x.ToString());
                    //GUI.Box (new Rect(Screen.width / 2 + 75, Screen.height / 2 - 30 + x * 30, 200, 25), new GUIContent(x.ToString(), BialeTlo));//do wykorzystania jeżeli ma byc ikona symbolizujaca handel jak w MD
                    //GUI.Box(new Rect(Screen.width / 2 + 75, Screen.height / 2 - 30 + x * 30, 200, 25), x.ToString());
                }
                
            }
            if (AktywnyHandel)
            {
                ekwipunekObiekt.WyświetlEkwipunek(3, Imie);
            }
        }
        
    }
}
