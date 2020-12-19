using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//edytowany 16.12.2020

public class Ekwipunek : MonoBehaviour
{
    public bool czyEkwipunekNPC;
    bool czyWyswietlicEkwipunek = false;
    public List<Item> itemy;
    public List<int> iloscItemy;

    public Texture tEkwipunek;
    public GUIStyle stylekwipunek;

    public void UsunItem(Item itemik)
    {
        for(int x =0; x<itemy.Count; x++)
        {
            if(itemik == itemy[x])
            {
                if(iloscItemy[x]>=1)
                {
                    iloscItemy[x]--;
                }
                else
                {
                    iloscItemy.RemoveAt(x);
                    itemy.RemoveAt(x);
                }
                break;
            }
        }
    }

    public void UsunItem(int testID)
    {
        for (int x = 0; x < itemy.Count; x++)
        {
            if (testID == itemy[x].itemID)
            {
                if (iloscItemy[x] >= 1)
                {
                    iloscItemy[x]--;
                }
                else
                {
                    iloscItemy.RemoveAt(x);
                    itemy.RemoveAt(x);
                }
                break;
            }
        }
    }

    public void DodajItem(Item itemToAdd)
    {
        //Debug.Log("Add Item item w inventory");
        // Go through all the item slots...
        for (int i = 0; i < itemy.Count; i++)
        {
            // ... if the item slot is empty...
            if (itemy[i] == null)
            {
                // kod do usunięcia z momentem, kiedy będzie pewność, że nie będzie wolnych slotów w przyszłości, jest to forma zabezpieczenia
                // ... set it to the picked up item and set the image component to display the item's sprite.
                itemy[i] = itemToAdd;
                return;
            }
            if (itemy[i] == itemToAdd)
            {
                Debug.Log("Już go masz");
                iloscItemy[i]++;
                return;
            }
        }
        //Jeżeli nie było pustego slotu dodaj do nowego slotu
        itemy.Add(itemToAdd);
        iloscItemy.Add(1);

    }

    public bool SprawdzItemID(int ID)
    {
        for (int x = 0; x < itemy.Count; x++)
        {
            if (ID == itemy[x].itemID)
            {
                return true;
            }
        }
        return false;
    }

    public void WyświetlEkwipunek(int typ, string imie)
    {
        float KorektaX=0;
        float KorektaSZ=0;
        float KorektaTypu=0;    //polowa ekranu czy full

        switch (typ)
        {
            case 1:
                KorektaSZ = 0;
                KorektaTypu = 1.0f;
                KorektaX = 0.0f;
                break;
            case 2:
                KorektaSZ = 0;
                KorektaTypu = 0.5f;
                KorektaX = 0.0f;
                break;
            case 3:
                KorektaSZ = 0;
                KorektaTypu = 0.5f;
                KorektaX = Screen.width * 0.5f;
                break;
            default:
                Debug.Log("Deafault w switch");
                break;
        }
        
            //Event e = Event.current;
            GUI.TextArea(new Rect(40 + KorektaX, 20, Screen.width*KorektaTypu - 80, Screen.height - 100), "Ekwipunek " + imie, 200);
            GUI.DrawTexture(new Rect(40 + KorektaX, 40, Screen.width*KorektaTypu - 80, Screen.height - 100), tEkwipunek);
            //pomyśleć o 

            GUI.Label(new Rect(100 + KorektaX, 50, 200, 30), "Nazwa", stylekwipunek);
            GUI.Label(new Rect(300 + KorektaX, 50, 50, 30), "Ilość", stylekwipunek);

            //wyświetlanie slotów ekwipunku
            for (int x = 0; x < itemy.Count; x++)
            {
                //Rect slotRect = new Rect(50, 50+x*40, 30, 30);
                GUI.DrawTexture(new Rect(50 + KorektaX, 90 + x * 40, 30, 30), itemy[x].itemIkona);
                GUI.Label(new Rect(100 + KorektaX, 90 + x * 40, 200, 30), itemy[x].itemNazwa, stylekwipunek);
                GUI.Label(new Rect(300 + KorektaX, 90 + x * 40, 50, 30), iloscItemy[x].ToString(), stylekwipunek);

                /*if (x < invent.itemy.Count)
                {
                    if (invent.itemy[firstshowitem + x] != null)
                    {
                        GUI.DrawTexture(slotRect, invent.itemy[firstshowitem + x].ikona);
                        if (firstshowitem + x < 4)
                        {
                            GUI.Box(slotRect, invent.ilosci[firstshowitem + x].ToString(), skin.GetStyle("Box"));
                        }
                        else
                        {
                            GUI.Box(slotRect, invent.ilosci[0].ToString(), skin.GetStyle("Box"));
                        }

                        if (slotRect.Contains(e.mousePosition))
                        {
                            if (e.isMouse && e.type == EventType.MouseDown)
                            {
                                if (e.clickCount == 2)
                                {
                                    Debug.Log("Podwojny Click");
                                    //invent.StackInventory(invent.activeItem);
                                }
                                else
                                {
                                    invent.activeItem = firstshowitem + x;
                                }

                            }
                        }
                    }
                }
                else
                {
                    //Debug.Log("Null");
                }*/
        }
        //}

    }

    void OnGUI()
    {
        //WyświetlEkwipunek(1);

        if (czyWyswietlicEkwipunek)
        {
            Event e = Event.current;
            GUI.TextArea(new Rect(40, 20, Screen.width - 80, Screen.height-100), "Ekwipunek Gracza", 200);
            GUI.DrawTexture(new Rect(40, 40, Screen.width - 80, Screen.height - 100), tEkwipunek);
            //pomyśleć o 

            GUI.Label(new Rect(100, 50, 200, 30), "Nazwa", stylekwipunek);
            GUI.Label(new Rect(300, 50, 50, 30), "Ilość", stylekwipunek);

            //wyświetlanie slotów ekwipunku
            for (int x = 0; x < itemy.Count; x++)
            {
                //Rect slotRect = new Rect(50, 50+x*40, 30, 30);
                GUI.DrawTexture(new Rect(50, 90 + x * 40, 30, 30), itemy[x].itemIkona);
                GUI.Label(new Rect(100, 90+x*40, 200, 30), itemy[x].itemNazwa, stylekwipunek);
                GUI.Label(new Rect(300, 90 + x * 40, 50, 30), iloscItemy[x].ToString(), stylekwipunek);
                


            }
        }

    
    }
}
