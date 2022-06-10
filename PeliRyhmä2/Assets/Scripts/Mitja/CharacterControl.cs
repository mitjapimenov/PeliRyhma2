using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameProject
{
    public class CharacterControl : MonoBehaviour
{
    public float Speed;
    public Animator animator;


    void Update()
    {
        if(VirutalInputManager.Instance.MoveRight && VirutalInputManager.Instance.MoveLeft)
        {
            animator.SetBool("Move", false);
            return;
        }
        if(!VirutalInputManager.Instance.MoveRight && !VirutalInputManager.Instance.MoveLeft)
        {
            animator.SetBool("Move", false);
        }

        if(VirutalInputManager.Instance.MoveRight)
        {
            this.gameObject.transform.Translate(Vector3.forward * Speed * Time.deltaTime);
            this.gameObject.transform.rotation = Quaternion.Euler(0f, 0f, 0f); //Pelaaja kääntyy meno suuntaan
            animator.SetBool("Move", true);
        }



            if(VirutalInputManager.Instance.MoveLeft)
        {
            this.gameObject.transform.Translate(Vector3.forward * Speed * Time.deltaTime);
            this.gameObject.transform.rotation = Quaternion.Euler(0f, 180f, 0f); //Pelaaja kääntyy meno suuntaan.
            animator.SetBool("Move", true);
        }

        if(VirutalInputManager.Instance.StartRun)
        {
            this.gameObject.transform.Translate(Vector3.forward * Speed * Time.deltaTime);
            animator.SetBool("Run", true);
        }
        if(!VirutalInputManager.Instance.StartRun)
        {
            //this.gameObject.transform.Translate(Vector3.forward * Speed * Time.deltaTime);
            animator.SetBool("Run", false);
        }
        
    }
}
}
