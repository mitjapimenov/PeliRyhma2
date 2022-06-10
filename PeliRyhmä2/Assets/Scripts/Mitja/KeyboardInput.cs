using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameProject
{
    public class KeyboardInput : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKey(KeyCode.D))
        {
            VirutalInputManager.Instance.MoveRight = true;
        }
        else
        {
            VirutalInputManager.Instance.MoveRight = false;
        }

        if (Input.GetKey(KeyCode.LeftShift))
        {
            VirutalInputManager.Instance.StartRun = true;
        }
        else if (!Input.GetKey(KeyCode.LeftShift))
        {
            VirutalInputManager.Instance.StartRun = false;
        }


        if (Input.GetKey(KeyCode.A))
        {
            VirutalInputManager.Instance.MoveLeft = true;
        }
        else
        {
            VirutalInputManager.Instance.MoveLeft = false;
        }
    }
}
}
