using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//edytowany 18.12.2020

[CreateAssetMenu]
public class BlokRozmowy : ScriptableObject
{
    public string Tekst;
    public int IDBloku;
    public List<string> OpcjeWyboru;

    public string PobierzTekst()
    {
        return Tekst;
    }
}
