using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * RESUMEN DE FUNCIONAMIENTO:
 *  - Se hará un seguimiento de la posición  de la Bola
 *  - En abse a la posición de la bola, el Paddle se moverá hacia arriba o hacia abajo
 *  - Cuando la bola se aprxime al Paddle de la IA, este decidirá si necesita moverse hacia abajo
 *    o arriba en base a si la Bola se encuentra por debajo o encima de su posición. 
 *  - También hace un cálculo para determinar si es conveniente quedarse quieto. 
 */

public class ComputerPaddle : MonoBehaviour
{
    //Velocidad
    private float Speed;

    //Referencia al RigidBody de la Bola
    [SerializeField] private Rigidbody2D BolaRB;

    //Flag de Juego corriendo
    private bool running = false;
    public bool Running {set => running = value; }

    //-------------------------------------------------------------
    private void Awake()
    {
        Speed = 7f;
    }

    //------------------------------------------------------------------

    void Update()
    {
        //Si el juego esta corriendo
        if (running == true)
        {
            //Si la Bola se dirige hacia nuestro lado...
            if (BolaRB.velocity.x > 0)
            {
                //Si la Bola se está moviendo hacia abajo
                if (BolaRB.velocity.y < 0)
                {
                    //Si la Bola se encuentra en una posición inferior a la nuestra
                    if (BolaRB.position.y < transform.position.y)
                    {
                        //Nos desplazamos hacia abajo
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
                    //Si la Bola se encuentra en una posición superior a la nuestra
                    else if (BolaRB.position.y > transform.position.y)
                    {
                        //Si la diferencia entre la altura de la Bola y la del Paddle excede a 1 unidad...
                        if (Mathf.Abs(BolaRB.position.y - transform.position.y) > 1)
                        {
                            //Nos desplazamos hacia arriba
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

                        //Caso contrario, nos quedamos quietos
                    }
                }
                //Si la Bola se está moviendo hacia arriba
                else if (BolaRB.velocity.y > 0)
                {
                    //Si la Bola se encuentra en una posición superior a la nuestra
                    if (BolaRB.position.y > transform.position.y)
                    {
                        //Nos desplazamos hacia arriba
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
                    //Si la Bola se encuentra en una posición inferior a la nuestra
                    else if (BolaRB.position.y < transform.position.y)
                    {
                        //Si la diferencia entre la altura de la Bola y la del Paddle excede a 1 unidad...
                        if (Mathf.Abs(BolaRB.position.y - transform.position.y) > 1)
                        {
                            //Nos desplazamos hacia abajo
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
                    }

                    //Caso contrario, nos quedamos quietos

                }
                //Si la Bola esta moviendose de frente...
                else
                {
                    //Si al sumar sus alturas se obtiene un valor positivo
                    if (BolaRB.position.y + transform.position.y > 0)
                    {
                        //Nos desplazamos hacia abajo
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
                    //Si al sumar sus alturas se obtiene un valor negativo
                    else if (BolaRB.position.y + transform.position.y < 0)
                    {
                        //Nos desplazamos hacia arriba
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
                }

                //Caso contrario, nos quedamos quietos

            } 
        }
    }

    //----------------------------------------------------------

    public void Run()
    {
        Running = true;
    }

    public void Stop()
    {
        Running = false;
    }
}
