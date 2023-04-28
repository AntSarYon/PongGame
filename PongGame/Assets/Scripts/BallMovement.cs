using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

enum TipoJugador
{
    JUG1, JUG2
}

class OnGoalArgs : EventArgs
{
    public TipoJugador jugador;
}

// OBSERVADO
public class BallMovement : MonoBehaviour
{
    public event EventHandler OnGoal; 

    public Vector3 Speed;
    private Rigidbody2D rb;
    private bool running = false;

    private void Start()
    {
        rb = transform.GetComponent<Rigidbody2D>();
        //rb.velocity = new Vector2(Speed.x, Speed.y);
    }

    private void Update()
    {
        if (running)
        {
            rb.velocity = new Vector2(Speed.x, Speed.y);
        }else {
            rb.velocity = Vector2.zero;
        }
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.transform.CompareTag("Wall"))
        {
            Speed.y *= -1f;
        }else 
        {
            Speed.y = UnityEngine.Random.Range(-5f, 5f);
            Speed.x *= -1f;
        }
    }
    
    private void OnTriggerEnter2D(Collider2D collider)
    {
        Debug.Log("Gol!");
        OnGoalArgs args = new OnGoalArgs();
        if (rb.velocity.x < 0)
        {
            // Gol Jug 2;
            args.jugador = TipoJugador.JUG2;
        }else{
            // Gol Jug 1
            args.jugador = TipoJugador.JUG1;
        }

        OnGoal?.Invoke(this, args);

        transform.position = new Vector3(0f, 0f ,0f);

    }

    public void Run() 
    {
        running = true;
    }
    public void Stop()
    {
        running = false;
    }
}
