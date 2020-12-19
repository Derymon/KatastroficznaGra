using UnityEngine;
using UnityEngine.UI;
using System.Collections;

// edytowany 17.11.2020

public class KontrolerGracza : MonoBehaviour
{

    //Obiekt odpowiedzialny za ruch gracza.
    public CharacterController characterControler;
    //Component odpowiedzialny za kontrolę staminy.
    //private StaminaUI stamina;

    //Predkosc biegania.
    public float aktualnaPredkosc = 0f;
    //Prędkość poruszania się gracza.
    public float predkoscPoruszania = 5.0f;
    //Predkosc biegania.
    public float predkoscBiegania = 10.0f;
    //Wysokość skoku.
    public float wysokoscSkoku = 7.0f;
    //Aktualna wysokosc skoku.
    public float aktualnaWysokoscSkoku = 0f;

    //Czulość myszki (Sensitivity)
    public float czuloscMyszki = 3.0f;
    public float myszGoraDol = 0.0f;
    //Zakres patrzenia w górę i dół.
    public float zakresMyszyGoraDol = 90.0f;

    /** 
    * Jeżeli 'true' to nie można się poruszać.
    * Opcja ustawiana jeżeli na przykład jakieś płutno (Canvas) z tagiem "Menu"  jest włączone (np. menu główne, opcje itd.) 
    * to poruszanie jest zablokowane.
    */
    private bool ruchZablokowany = false;

    /** 
	 * Pobranie prędkości poruszania się przód/tył.
	 * jeżeli wartość dodatnia to poruszamy się do przodu,
	 * jeżeli wartość ujemna to poruszamy się do tyłu.
	 */
    private float rochPrzodTyl;
    /** 
	 * Pobranie prędkości poruszania się lewo/prawo.
	 * jeżeli wartość dodatnia to poruszamy się w prawo,
	 * jeżeli wartość ujemna to poruszamy się w lewo.
	 */
    private float rochLewoPrawo;
    /** Zmienna dostarcza informację o tym czy gracz biegnie czy płynie.*/
    public bool czyBiegnie;
    public bool czyLata;

    /*Zmienne przy obsłudze w wodzie*/ //powstałe przy scalaniu PlayerController i GraczWodaKontroler
    public bool RuchWWodzie;
    // Prędkość z jaką gracz będzie się wynurzać.
    public float predkoscWynurzania = 0.03f;
    // Prędkość z jaką gracz będzie się zanurzać.
    public float predkoscZanurzania = 0.01f;

    public bool plynDoGory = false;

    //zmienne w sprawie kałuż i śniegu
    public bool czyRobiSlady;
    public float KiedyWyschnieM;
    public float Timer;

    //kontrolna zmienna tekstowa dla komunikatów
    public string TekstWiadomosci = " ";

    //flagi boolowskie
    public bool czyBlokadaPrzezEkwipunek = false;

    // Use this for initialization
    void Start()
    {
        //stamina = GetComponent<StaminaUI>();
        characterControler = GetComponent<CharacterController>();
        aktualnaPredkosc = predkoscPoruszania;
        RuchWWodzie = false;
        czyRobiSlady = false;
        czyLata = false;
        Timer = 0;
        //Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        if (!czyGraczMartwy() && !ruchZablokowany)
        {
            if (czyRobiSlady)
            {
                Timer += Time.deltaTime;
                if (Timer >= KiedyWyschnieM)
                {
                    Wysechl();
                }
            }
            klawiatura();
            myszka();
            //ruch();
        }
    }

    /**
	 * Metoda odpowiedzialna za poruszanie się na klawiaturze.
	 */
    private void klawiatura()
    {
        rochPrzodTyl = Input.GetAxis("Vertical") * aktualnaPredkosc;
        rochLewoPrawo = Input.GetAxis("Horizontal") * aktualnaPredkosc;

        //TekstWiadomosci = "RuchPT: " + Input.GetAxis("Vertical").ToString();

        //Skakanie
        // Jeżeli znajdujemy się na ziemi i została naciśnięta spacja (skok)
        if (Input.GetButton("Jump"))
        {
            if (RuchWWodzie == false)
            {
                if (characterControler.isGrounded) // skocz z ladu
                {
                    aktualnaWysokoscSkoku = wysokoscSkoku;
                }
                else
                {
                    //Debug.Log("Else RWoda = false");
                    aktualnaWysokoscSkoku += Physics.gravity.y * Time.deltaTime;
                }
            }
            else
            {   //ruch w wodzie

                if (!plynDoGory)
                {
                    //Debug.Log("if1");
                    aktualnaWysokoscSkoku = 0;
                    plynDoGory = true;
                }
                aktualnaWysokoscSkoku += predkoscWynurzania;

                if (!characterControler.isGrounded)
                {
                    //Debug.Log("if2");
                    //Jezeli jestesmy w powietrzu(skok)                       
                    aktualnaWysokoscSkoku -= predkoscZanurzania;
                    //plynDoGory = false;
                }
                else if (characterControler.isGrounded)
                {
                    //Debug.Log("if3");
                    plynDoGory = false;
                }
            }

        }
        else if (characterControler.isGrounded) //jezeli dotykasz gruntu ...
        {
            if (RuchWWodzie) //... w wodzie
            {
                //Debug.Log("EI if1");
            }
            else // ... po za woda
            {
                plynDoGory = false;
                //Debug.Log("EI else1");
            }

        }
        else if (!characterControler.isGrounded)
        {//Jezeli nie dotykamy gruntu ...
            if (RuchWWodzie) //... w wodzie
            {
                //Debug.Log("EI2 if1");
                aktualnaWysokoscSkoku -= predkoscZanurzania;
            }
            else // ... na lądzie, czyli jestesmy w powietrzu(skok)
            {
                //Fizyka odpowiadająca za grawitacje (os Y).
                aktualnaWysokoscSkoku += Physics.gravity.y * Time.deltaTime;
            }
        }


        //Bieganie
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            czyBiegnie = true;
        }
        else if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            czyBiegnie = false;
        }

        if (czyBiegnie )//&& !czyBrakStaminy())
        {
            aktualnaPredkosc = predkoscBiegania;
        }
        else
        {
            aktualnaPredkosc = predkoscPoruszania;
        }

        //blokada dla funkcji ekwipunek
        if (Input.GetKeyUp(KeyCode.I))
        {
            //czyBlokadaPrzezEkwipunek = !czyBlokadaPrzezEkwipunek;
        }

        //Tworzymy wektor odpowiedzialny za ruch.
        //rochLewoPrawo - odpowiada za ruch lewo/prawo,
        //aktualnaWysokoscSkoku - odpowiada za ruch góra/dół,
        //rochPrzodTyl - odpowiada za ruch przód/tył.
        Vector3 ruch = new Vector3(rochLewoPrawo, aktualnaWysokoscSkoku, rochPrzodTyl);
        //Aktualny obrót gracza razy kierunek w którym sie poruszamy (poprawka na obrót myszką abyśmy szli w kierunku w którym patrzymy).
        ruch = transform.rotation * ruch;
        //TekstWiadomosci = "TransRot: " + transform.rotation.ToString();
        //TekstWiadomosci = "AWskoku " + aktualnaWysokoscSkoku.ToString();

        characterControler.Move(ruch * Time.deltaTime);

    }

    /**
	 * Metoda odpowiedzialna za ruch myszką.
	 */
    private void myszka()
    {
        //Pobranie wartości ruchu myszki lewo/prawo.
        // jeżeli wartość dodatnia to poruszamy w prawo,
        // jeżeli wartość ujemna to poruszamy w lewo.
        float myszLewoPrawo = Input.GetAxis("Mouse X") * czuloscMyszki;
        transform.Rotate(0, myszLewoPrawo, 0);

        //Pobranie wartości ruchu myszki góra/dół.
        // jeżeli wartość dodatnia to poruszamy w górę,
        // jeżeli wartość ujemna to poruszamy w dół.
        myszGoraDol -= Input.GetAxis("Mouse Y") * czuloscMyszki;

        //Funkcja nie pozwala aby wartość przekroczyła dane zakresy.
        myszGoraDol = Mathf.Clamp(myszGoraDol, -zakresMyszyGoraDol, zakresMyszyGoraDol);
        //Ponieważ CharacterController nie obraca się góra/dół obracamy tylko kamerę.
        Camera.main.transform.localRotation = Quaternion.Euler(myszGoraDol, 0, 0);

        //TekstWiadomosci = "Wartosc Euler: " + Quaternion.Euler(myszGoraDol, 0, 0).ToString();

    }

    private void ruch()
    {
        rochPrzodTyl = Input.GetAxis("Vertical") * aktualnaPredkosc;
        rochLewoPrawo = Input.GetAxis("Horizontal") * aktualnaPredkosc;


        // latanie
        if (czyLata)
        {
            float m_DX = 0;
            float m_DY = 0;
            float m_DZ = 0;

            float zakres = transform.rotation.eulerAngles.y;
            TekstWiadomosci = "TREy: " + transform.rotation.eulerAngles.y.ToString();

            m_DX = Mathf.Sin(transform.rotation.eulerAngles.y / 180f * Mathf.PI);
            m_DY = Mathf.Sin(transform.rotation.eulerAngles.y / 180f * Mathf.PI);
            m_DZ = Mathf.Cos(transform.rotation.eulerAngles.y / 180f * Mathf.PI);
            if (rochPrzodTyl != 0)
            {
                m_DX = Mathf.Sin(transform.rotation.eulerAngles.y / 180f * Mathf.PI);
                m_DY = Mathf.Sin(transform.rotation.eulerAngles.y / 180f * Mathf.PI);
                m_DZ = Mathf.Cos(transform.rotation.eulerAngles.y / 180f * Mathf.PI);
            }
            else
            {
                m_DX = 0;
                m_DY = 0;
                m_DZ = 0;
            }
        }



        //TekstWiadomosci = "SinY: " + Mathf.Sin(transform.rotation.eulerAngles.y / 180f * Mathf.PI).ToString() + " CosY: " + Mathf.Cos(transform.rotation.eulerAngles.y / 180f * Mathf.PI).ToString() + "/nDy: " + m_DY.ToString();

        Vector3 ruch = new Vector3(rochLewoPrawo, aktualnaWysokoscSkoku, rochPrzodTyl);
        //Vector3 ruch = new Vector3(rochLewoPrawo + m_DX, aktualnaWysokoscSkoku + m_DY, rochPrzodTyl + m_DZ);
        //Aktualny obrót gracza razy kierunek w którym sie poruszamy (poprawka na obrót myszką abyśmy szli w kierunku w którym patrzymy).
        ruch = transform.rotation * ruch;
        //TekstWiadomosci = "TransRot: " + transform.rotation.ToString();
        

        characterControler.Move(ruch * Time.deltaTime);
    }


    /**
	 * Metoda zwraca informację o tym czy gracz ciągle żyje.
	 * 
	 * Zwraca 'true' jeżeli gracz jeszcze żyje w przeciwnym razei 'false'.
	 */


    public bool czyGraczMartwy()
    {
        /*Zdrowie zdrowie = gameObject.GetComponent<Zdrowie>();
        if (zdrowie != null && zdrowie.czyMartwy()) {
            return true;
        }*/
        return false;
    }

    public void SetRuchZablokowany(bool val)
    {
        this.ruchZablokowany = val;
    }

    /**
	 * Funkcja udostępnia informację o tym czy gracz wykonuje ruch (chodzi).
	 * Jeżeli gracz chodzi to zwraca 'true' jeżeli nie to 'false'.
	 */
    public bool czyGraczChodzi()
    {
        if (rochPrzodTyl != 0 || rochLewoPrawo != 0)
        {
            return true;
        }
        return false;
    }

    /**
	 * Funkcja dostarcza informacje o tym czy gracz biegnie.
	 * Zwraca 'true' jeżelli gracz biegnie w przeciwnym razie 'false'.
	 */
    public bool czyGraczBiegnie()
    {
        return czyBiegnie;
    }

    /*private bool czyBrakStaminy()
    {
        if (stamina != null)
        {
            return stamina.brakStaminy();
        }
        return false;
    }*/

    /*Funkcje wynikłe przy obsłudze wody i scalaniu z GraczWodaKontroler*/
    public void WszedlDoWody() //zalazek oddychania pod woda
    {
        //czyRobiSlady = true;
        Timer = 0;
    }

    public void WyszedlZWody()
    {
        czyRobiSlady = true;
        Timer = 0;
    }

    public void Wysechl()
    {
        czyRobiSlady = false;
        Debug.Log("Wyschłeś");
    }

    public void KiedyWyschnie(float wartosc)
    {
        KiedyWyschnieM = wartosc;
    }

    public void UstawRuchWWodzie(bool wartosc)
    {
        RuchWWodzie = wartosc;
    }

    public bool czyRuchWWodzie()
    {
        return RuchWWodzie;
    }

    void OnGUI()
    {
        //GUI.TextArea(new Rect(10, 10, 400, 100), TekstWiadomosci, 300);
    }

}
