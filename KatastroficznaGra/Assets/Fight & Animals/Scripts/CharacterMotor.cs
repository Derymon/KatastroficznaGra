using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMotor : MonoBehaviour
{
    public float speed=5;

    public void move(Vector3 offset)
    {
        transform.Translate(offset*speed*Time.deltaTime);
    }
}
