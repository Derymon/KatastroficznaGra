using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//edytowany 16.12.2020

public class HUDGracza : MonoBehaviour
{
    public Texture tCelownik;
    bool czyWyswietlicCelownik;
    bool czyWyswietlicEkwipunek;
    public bool czyHandel;
    public bool czyRozmowa;
    public Ekwipunek ekwipunekObiekt;
    public KontrolerGracza gracz;

    // Start is called before the first frame update
    void Start()
    {
        czyWyswietlicCelownik = true;
        ekwipunekObiekt = this.gameObject.GetComponent<Ekwipunek>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.I))
        {
            czyWyswietlicCelownik = !czyWyswietlicCelownik;
            czyWyswietlicEkwipunek = !czyWyswietlicEkwipunek;
        }
        if(Input.GetKeyUp(KeyCode.G))
        {
            czyRozmowa = false;
        }
        if (czyWyswietlicEkwipunek || czyHandel || czyRozmowa)
        {
            gracz.GetComponentInChildren<KontrolerGracza>().SetRuchZablokowany(true);
            Cursor.lockState = CursorLockMode.None;
        }
        else
        {
            gracz.GetComponentInChildren<KontrolerGracza>().SetRuchZablokowany(false);
            Cursor.lockState = CursorLockMode.Locked;
        }
    }

    public void AktywujHandel()
    {
        czyHandel = !czyHandel;
    }

    public void AktywujRozmowe()
    {
        czyRozmowa = true;
    }

    void OnGUI()
    {
        if (czyWyswietlicEkwipunek)
        {
            ekwipunekObiekt.WyświetlEkwipunek(1, "Gracza");
        }
        if(czyHandel)
        {
            ekwipunekObiekt.WyświetlEkwipunek(2, "Gracza");
        }
        if (czyWyswietlicCelownik)
        {
            //wyswietlanie "celownika"
            GUI.DrawTexture(new Rect((Screen.width * 0.5f) - (tCelownik.width * 0.5f), (Screen.height * 0.5f) - (tCelownik.height * 0.5f) - 15, tCelownik.width, tCelownik.height), tCelownik);
        }
        
    }
}
