using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public static float Vertical
    {
        get { return Input.GetAxis("Vertical"); }
    }

    public static float Horizontal
    {
        get { return Input.GetAxis("Horizontal"); }
    }

    public static bool attractKey
    {
        get
        {
            return Input.GetKey(KeyCode.Mouse0);
        }
    }
    public static bool repelKey
    {
        get
        {
            return Input.GetKey(KeyCode.Mouse1);
        }
    }
}
