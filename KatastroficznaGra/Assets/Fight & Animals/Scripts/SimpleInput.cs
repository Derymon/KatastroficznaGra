using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleInput : MonoBehaviour
{
    public float speed=10;
    public float sensivity=5;
    public HumanoidFightController fc;
    public CharacterMotor chm;

    public Weapon[] tempInv;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 offset = new Vector3();
        Vector3 offsetMouse = new Vector3();

        offset.x = Input.GetAxis("Horizontal");
        offset.z = Input.GetAxis("Vertical");
        offsetMouse.y = -Input.GetAxis("Mouse Y");
        offsetMouse.x = Input.GetAxis("Mouse X");

        if (fc != null)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Ray r = Camera.main.ScreenPointToRay(new Vector2(Screen.width/2, Screen.height/2));
                RaycastHit rh;
                Physics.Raycast(r, out rh);
                fc.attack(rh.collider.transform);
            }
            fc.block = Input.GetMouseButton(1);
            if(Input.GetKeyDown(KeyCode.Alpha1))
            {
                fc.activeWeapon = tempInv[0];
            }
            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                fc.activeWeapon = tempInv[1];
            }
            if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                fc.activeWeapon = tempInv[2];
            }
            if (Input.GetKeyDown(KeyCode.Alpha4))
            {
                fc.activeWeapon = tempInv[3];
            }
        }

        chm.move(offset);
        Camera.main.transform.eulerAngles += Vector3.right * offsetMouse.y * sensivity;
        transform.eulerAngles += Vector3.up * offsetMouse.x * sensivity;
    }
}
