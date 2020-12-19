using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//edytowany 16.12.2020


public class Budowanie : MonoBehaviour
{
    public string TekstWiadomosci = " ";

    public float DYSTANS;
    public BudowanyObiekt budowanyObiekt;
    public Ekwipunek ekwipunekObiekt;
    public Przedmiot przedmiotObiekt;
    public KontaktNPC nPC;
    public bool czyInfooBudowli;
    public bool czyInfoNPC;
    public GUIStyle stylbudowanie;
    public HUDGracza hUD;

    Ray ray;
    RaycastHit hit;

    // Start is called before the first frame update
    void Start()
    {
        ekwipunekObiekt = this.gameObject.GetComponent<Ekwipunek>();
        hUD = this.gameObject.GetComponent<HUDGracza>();

    }

    void FixedUpdate()
    {
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
    }

    // Update is called once per frame
    void Update()
    {
        czyInfooBudowli = false;
        czyInfoNPC = false;
        if (Input.GetKeyUp(KeyCode.V))
        {
            //Screen.lockCursor = !Screen.lockCursor;
            Cursor.lockState = CursorLockMode.Locked;//kursor znikł
            //czyBudowanie = !czyBudowanie;
        }
        if (Input.GetKeyUp(KeyCode.B))
        {
            Cursor.lockState = CursorLockMode.Confined; //ruch kursora tylko w obrębie okna gry
        }
        if (Input.GetKeyUp(KeyCode.N))
        {
            Cursor.lockState = CursorLockMode.None;//brak ograniczeń
        }
        
        
        //layer mask służy do pomijania zderzaczy na odpowiednich warstwach
        int layerMask = 1 << 8;

        layerMask = ~layerMask;

        if (Physics.Raycast(ray, out hit, 2.0f, layerMask) && !hUD.czyHandel && !hUD.czyRozmowa)
        {
            //jeżeli jest raycast to wyswietl info o brakujących materialach
            if (hit.collider.gameObject.tag == "Budowa")
            {
                budowanyObiekt = hit.collider.gameObject.GetComponent<BudowanyObiekt>();
                czyInfooBudowli = true;
            }

            else if (hit.collider.gameObject.tag == "NPC")
            {
                nPC = hit.collider.gameObject.GetComponent<KontaktNPC>();
                czyInfoNPC = true;
                
                //Debug.Log("Widzisz NPC z bliska");
            }

            if (Input.GetMouseButtonUp(0)) //0 - lewy 1 - prawy
            {
                
                if (hit.collider.gameObject.tag == "Budowa")
                {
                    budowanyObiekt = hit.collider.gameObject.GetComponent<BudowanyObiekt>();    //pomógł zbigniew barczyński z unity3d polska; aczkolwiek w moim katastroficznym survivalu znalazłem tą linie
                    //ekwipunekObiekt = this.gameObject.GetComponent<Ekwipunek>();

                    for (int bo = 0; bo < budowanyObiekt.listaPotrzebnychSurowcow.Count; bo++)
                    {
                        for (int eo = 0; eo < ekwipunekObiekt.itemy.Count; eo++)
                        {
                            if (budowanyObiekt.listaPotrzebnychSurowcow[bo].itemy.itemID == ekwipunekObiekt.itemy[eo].itemID)
                            {
                                //Debug.Log("Test");
                                budowanyObiekt.DostarczMaterialy(ekwipunekObiekt.itemy[eo].itemID);
                                ekwipunekObiekt.UsunItem(ekwipunekObiekt.itemy[eo].itemID);
                                //Debug.Log("Test bo: " + bo + " eo: " + eo);
                                break;
                            }
                        }
                        break;
                    }
                }
                else if (hit.collider.gameObject.tag == "Rzecz")
                {
                    //ekwipunekObiekt = this.gameObject.GetComponent<Ekwipunek>();
                    przedmiotObiekt = hit.collider.gameObject.GetComponent<Przedmiot>();
                    ekwipunekObiekt.DodajItem(przedmiotObiekt.Item);
                }
                else if (hit.collider.gameObject.tag == "NPC")
                {
                    
                    //nPC.AktywujRozmowe();
                    //hUD.BlokadaRuchow(true);
                    //hUD.AktywujHandel();
                    //ekwipunekObiekt.WyświetlEkwipunek(2, "Gracz");
                }
                else
                {
                    Debug.Log("Tag: " + hit.collider.gameObject.tag);
                }
                
                
            }
            
        }

        if (czyInfoNPC && Input.GetKeyUp(KeyCode.E))
        {
            //hUD.AktywujHandel();
            hUD.AktywujRozmowe();
            nPC.AktywujRozmowe();
        }
    }

    

    void OnGUI()
    {
        if(czyInfooBudowli)
        {
            GUI.Box(new Rect(Screen.width / 2, Screen.height / 2, 200, 25), budowanyObiekt.Nazwa);
            GUI.Box(new Rect(Screen.width / 2, Screen.height / 2 + 30, 200, 25), "Potrzebne zasoby:");
            for (int x = 0; x < budowanyObiekt.listaPotrzebnychSurowcow.Count; x++)
            {
                GUI.Box(new Rect(Screen.width / 2, Screen.height / 2 + 60 + 30*x, 25, 25), budowanyObiekt.listaPotrzebnychSurowcow[x].itemy.itemIkona);
                string wiadomosc;
                wiadomosc = budowanyObiekt.listaPotrzebnychSurowcow[x].itemy.itemNazwa + ": " + budowanyObiekt.listaPotrzebnychSurowcow[x].ilosc;
                GUI.Box(new Rect(Screen.width / 2 + 30, Screen.height / 2 + 60 + 30 * x, 170, 25), wiadomosc);
            }
                //GUI.Label(new Rect(Screen.width / 2, Screen.height / 2, 200, 25), budowanyObiekt.Nazwa);
                /*string wiadomosc;
                wiadomosc = budowanyObiekt.Nazwa + "\n\nWymagane zasoby:\n";
                for(int x = 0; x < budowanyObiekt.listaPotrzebnychSurowcow.Count; x++)
                {
                    GUI.DrawTexture(new Rect(Screen.width / 2 + 50, Screen.height / 2 + 30 * x, 20, 20), budowanyObiekt.listaPotrzebnychSurowcow[x].itemy.itemIkona);
                    wiadomosc += "\t" + budowanyObiekt.listaPotrzebnychSurowcow[x].itemy.itemNazwa + ": " + budowanyObiekt.listaPotrzebnychSurowcow[x].ilosc + "\n";
                }
                GUI.TextArea(new Rect(Screen.width / 2 + 50, Screen.height / 2 - 50, 200, 100), wiadomosc, 180);*/
        }
        if(czyInfoNPC)
        {
            GUI.Box(new Rect(Screen.width / 2, Screen.height / 2, 200, 25), nPC.Imie);
        }
    }
}
