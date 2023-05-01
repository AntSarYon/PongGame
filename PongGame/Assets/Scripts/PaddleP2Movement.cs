using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaddleP2Movement : MonoBehaviour
{
    //Seteamos la velocidad del Paddle
    public float Speed;

    //Flag de Juego corriendo
    private bool running = false;

    //----------------------------------------------------
    void Awake()
    {
        Speed = 7f;
    }

    //----------------------------------------------------

    void Update()
    {
        //Si el juego esta corriendo
        if (running)
        {
            //1. Deteccion de Teclas (Arriba o Abajo)
            //2. Modificación de la posición hacia Arriba o Abajo
            //3. Limitar el Rango de movimiento del Paddle

            if (Input.GetKey(KeyCode.UpArrow)){
                ControlarMovimientoHaciaArriba();
            }

            else if(Input.GetKey(KeyCode.DownArrow)){
                ControlarMovimientoHaciaAbajo();
            }
        }
        //Si no está corriendo; no reconocemnos ninguna entrada.
    }

    //----------------------------------------------------------------

    private void ControlarMovimientoHaciaArriba()
    {
        transform.position = new Vector3(
            transform.position.x,
            Mathf.Clamp(
                transform.position.y + Speed * Time.deltaTime,
                -4.30f,
                3.50f
            ),
            transform.position.z
        );
    }

    //----------------------------------------------------------------

    private void ControlarMovimientoHaciaAbajo()
    {
        transform.position = new Vector3(
            transform.position.x,
            Mathf.Clamp(
                transform.position.y - Speed * Time.deltaTime,
                -4.30f,
                3.50f
            ),
            transform.position.z
        );
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