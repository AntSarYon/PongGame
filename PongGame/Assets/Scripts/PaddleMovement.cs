using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaddleMovement : MonoBehaviour
{
    //Seteamos la velocidad del Paddle
    public float Speed = 1f;

    //Flag de Juego corriendo
    private bool running = false;

    //----------------------------------------------------

    void Update()
    {
        //Si el juego esta corriendo
        if (running)
        {
            //Si se deecta la tecla W...
            if(Input.GetKey(KeyCode.W)){
                transform.position = new Vector3(
                    transform.position.x,
                    Mathf.Clamp(
                        transform.position.y + Speed * Time.deltaTime,
                        -4f,
                        4f
                    ),
                    transform.position.z
                );
            }
            //Si se detecta la tecla S...
            else if(Input.GetKey(KeyCode.S)){
                transform.position = new Vector3(
                    transform.position.x,
                    Mathf.Clamp(
                        transform.position.y - Speed * Time.deltaTime,
                        -4f,
                        4f
                    ),
                    transform.position.z
                );
            }
        }
    }

    //------------------------------------------------------

    public void Run() 
    {
        running = true;
    }

    public void Stop()
    {
        running = false;
    }
}
