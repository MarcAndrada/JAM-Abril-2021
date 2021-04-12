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
}
