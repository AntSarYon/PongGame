using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

//------------------------------------
//ENUM DE OPCIONES PARA EL TIPO DE JUGADOR

enum TipoJugador
{
    JUG1, JUG2
}

//-------------------------------------------
//Definición de ARGUMENTOS del EVENTO OnGoal

class OnGoalArgs : EventArgs
{
    public TipoJugador jugador;
}

//**********************************************
//************ CLASE OBSERVADA *****************

public class BallMovement : MonoBehaviour
{
    //Gestor del Evento OnGoal
    public event EventHandler OnGoal;
    
    //Velocidad de la Bola
    public Vector3 Speed;

    //Referencia al RigidBody
    private Rigidbody2D rb;

    //Listas de Clip de audio
    [SerializeField] private AudioSource mAS;
    [SerializeField] private AudioClip[] ClipsChoque = new AudioClip[2];
    [SerializeField] private AudioClip[] ClipsLaser = new AudioClip[5];
    [SerializeField] private AudioClip clipGoal;

    //Volumen
    [SerializeField] private float volumen;

    //Flag de Juego corriendo
    private bool running = false;

    void Awake()
    {
        Speed = new Vector3(4, 0, 0);
        rb = transform.GetComponent<Rigidbody2D>();
        mAS = transform.GetComponent<AudioSource>();
        volumen = 0.65f;
    }

    //---------------------------------------------------------------
    private void Start()
    {

    }

    //---------------------------------------------------------------
    private void Update()
    {
        if (running)
        {
            rb.velocity = new Vector2(Speed.x, Speed.y);
        }
        else
        {
            rb.velocity = Vector2.zero;
        }
    }

    //---------------------------------------------------------------

    private void OnCollisionEnter2D(Collision2D col)
    {
        //Si choca con una Pared Superior o inferior...
        if (col.transform.CompareTag("Wall"))
        {
            //Reproducimos un Clip de impacto en Pared Aleatorio
            mAS.PlayOneShot(
                ClipsChoque[UnityEngine.Random.Range(0, 1)],
                volumen
            );

            Speed.y *= -1f;
        }

        //Si choca con un Paddle...
        else
        {
            //Reproducimos un Clip de impacto Laser Aleatorio
            mAS.PlayOneShot(
                ClipsLaser[UnityEngine.Random.Range(0, 4)],
                volumen
            );
            Speed.y = UnityEngine.Random.Range(-5f, 5f);
            Speed.x *= -1.025f;
        }


    }

    //---------------------------------------------------------------

    private void OnTriggerEnter2D(Collider2D collider)
    {
        //Reproducimos un Clip de impacto Laser Aleatorio
        mAS.PlayOneShot(
            clipGoal,
            volumen
        );

        OnGoalArgs args = new OnGoalArgs();
        if (rb.velocity.x < 0)
        {
            // Gol Jug 2;
            args.jugador = TipoJugador.JUG2;
        }
        else
        {
            // Gol Jug 1
            args.jugador = TipoJugador.JUG1;
        }

        OnGoal?.Invoke(this, args);

        Speed = new Vector3(4, 0, 0);
        transform.position = new Vector3(0f, 0f, 0f);

    }

    //---------------------------------------------------------------

    public void Run()
    {
        running = true;
    }

    public void Stop()
    {
        running = false;
    }
}