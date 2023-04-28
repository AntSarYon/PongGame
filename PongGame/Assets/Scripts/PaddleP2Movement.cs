using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaddleP2Movement : MonoBehaviour
{
    //Seteamos la velocidad del Paddle
    public float Speed = 1f;

    //Flag de Juego corriendo
    private bool running = false;

    //------------------------------------------------------------

    void Update()
    {
        //Si el juego esta corriendo
        if (running)
        {
            //Si se deecta la flecha ARRIBA...
            if(Input.GetKey(KeyCode.UpArrow)){
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
            //Si se detecta la flecha ABAJO...
            else if(Input.GetKey(KeyCode.DownArrow)){
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

    //----------------------------------------------------------------

    public void Run()
    {
        running = true;
    }

    public void Stop()
    {
        running = false;
    }
}
