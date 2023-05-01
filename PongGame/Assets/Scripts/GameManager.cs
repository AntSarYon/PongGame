using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class GameManager : MonoBehaviour
{
    //Referencias a los Textos de la UI
    public TextMeshProUGUI TextScore1;
    public TextMeshProUGUI TextScore2;
    public TextMeshProUGUI DVMessage;

    //Referencia al Script de la Bola
    public BallMovement ball;

    //Referencia a cada uno de los Paddle
    public PaddleMovement paddle1;
    public PaddleP2Movement paddle2;
    public ComputerPaddle paddleAI;

    [SerializeField] private AudioClip[] clipsIA = new AudioClip[2];
    [SerializeField] private float volumen;

    //Flag de IA Activada
    private bool aiActivada;

    //---------------------------------------------

    private void Awake()
    {
        volumen = 0.85f;
    }

    //---------------------------------------------

    private void Start()
    {
        //Le añadimos un Delegado al Evento OnGoal de la Bola
        ball.OnGoal += OnGoalDelegate;

        //Iniciamos con el Paddle2 activado y la IA desactivada
        paddle2.enabled = true;
        paddleAI.enabled = false;

        //Flag de Activación de IA inicializado en FALSE
        aiActivada = false;

        //Empezamos con el Juego detenido
        StopGame();
    }

    //---------------------------------------------

    private void Update()
    {
        //Iniciamos el juego al oprimir el Boton Space
        if (Input.GetKeyDown(KeyCode.Space)){
            StartGame();
        }

        // - - - - - - - - - - - - - - - - - - - -

        //Si se oprime uno de los botones Control...
        if (Input.GetKeyDown(KeyCode.LeftControl) || Input.GetKeyDown(KeyCode.RightControl))
        {
            //Activamos el mensaje de Estado de IA
            DVMessage.gameObject.SetActive(true);

            //Si la AI Esta Activada
            if (aiActivada)
            {
                DesactivarIA();
            }

            // - - - - - - - - - -- - - - - -

            //Si la AI esta Desactivada
            else
            {
                ActivarIA();
            }
            
        }
    }

    //---------------------------------------------------------

    private void ActivarIA()
    {
        //Desactivamos el Paddle2 y Activamos la IA
        paddle2.enabled = false;
        paddleAI.enabled = true;

        //Activamos el Flag
        aiActivada = true;

        //Activamos el Flag de Running de la IA manualmente
        paddleAI.Running = true;

        //Actualizamos el mensaje del Texto...
        DVMessage.text = "Darth Vader se ha unido al juego";

        //Reproducimos la Respiración de Vader
        GetComponent<AudioSource>().PlayOneShot(clipsIA[0], volumen);
    }

    //---------------------------------------------------------

    private void DesactivarIA()
    {
        //Activamos el Paddle2 y Desactivamos la IA
        paddle2.enabled = true;
        paddleAI.enabled = false;

        //Desactivamos el Flag
        aiActivada = false;

        //Actualizamos el mensaje del Texto
        DVMessage.text = "Darth Vader esta fuera del juego";

        //Reproducimos el Sable Apagandose
        GetComponent<AudioSource>().PlayOneShot(clipsIA[1], volumen);
    }

    //---------------------------------------------------------
    private void OnGoalDelegate(object sender, EventArgs e)
    {
        // Definimos Args como los argumentos recibidos a partir
        //del Evento OnGoal
        OnGoalArgs args = e as OnGoalArgs;

        //Dependiendo de a qué JUGADOR referencie el argumento "Jugador",
        //se suma el puntaje y se actualiza el Texto en Pantalla

        if (args.jugador == TipoJugador.JUG1)
        {
            // Agregar Puntaje al jugador 1
            int puntaje = int.Parse(TextScore1.text);
            TextScore1.text = (puntaje + 1).ToString();
        }else
        {
            // Agregar Puntaje al jugador 2
            int puntaje = int.Parse(TextScore2.text);
            TextScore2.text = (puntaje + 1).ToString();
        }
        StopGame();
    }

    //-----------------------------------------------------
    private void StartGame()
    {
        TextScore1.gameObject.SetActive(false);
        TextScore2.gameObject.SetActive(false);
        DVMessage.gameObject.SetActive(false);

        ball.Run();
        paddle1.Run();

        if (paddle2.enabled)
        {
            paddle2.Run();
        } 
        else paddleAI.Run();
    }

    //-------------------------------------------------------
    private void StopGame()
    {
        TextScore1.gameObject.SetActive(true);
        TextScore2.gameObject.SetActive(true);
        DVMessage.gameObject.SetActive(true);

        ball.Stop();
        paddle1.Stop();

        if (paddle2.enabled)
        {
            paddle2.Stop();
        }
        else paddleAI.Stop();

    }
}