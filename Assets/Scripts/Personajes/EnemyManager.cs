using System.Collections;
using UnityEngine;
using System;

public class EnemyManager : MonoBehaviour
{
    private YsbridManager Ysbrid;
    public int enemyLevel, enemyHealth, enemyPhysic, enemyMagic, enemyCharges;
    public string enemyName;
    public bool Terrestre = false;

    public GameObject[] items;
    public GameObject[] HitosPatronMovimiento;
    public float[] VelocidadesPatronMovimiento;

    private Transform thisTransform;
    private Rigidbody2D thisRigidbody;
    private int HitoSiguiente = 0;


    private Transform target; // Objetivo a seguir
    public float enemySpeed; // Velocidad del enemigo al seguir a un objetivo
                             // Public variables

    // Properties
    // Nos indicará si el objeto puede continuar hacia el siguiente hito del patrón definido
    public bool CanGoToNextMilestone { get; set; }

    void Start()
    {
        Ysbrid = YsbridManager.ysbridInstance;

       

        DestroyEnemy();

        thisTransform = transform;
        thisRigidbody = GetComponentInParent<Rigidbody2D>();
        CanGoToNextMilestone = true;

        target = GameObject.FindGameObjectWithTag("Ysbrid").GetComponent<Transform>();
    }

    private bool IrHaciaHito(Vector3 PosicionHito, float Velocidad)
    {
        // Calculamos la distancia entre el hito y el objeto
        Vector3 VectorHaciaObjetivo = PosicionHito - thisTransform.position;
        if (Terrestre)
        {
            // Si estamos en modo 'Terrestre', calculamos la distancia ignorando el eje Y
            VectorHaciaObjetivo = new Vector3(VectorHaciaObjetivo.x, 0, VectorHaciaObjetivo.z);
        }

        // Con esta condición comprobamos si el objeto aún no ha llegado a las coordenadas del hito
        if (Math.Abs(VectorHaciaObjetivo.x) > 0.2F ||
            Math.Abs(VectorHaciaObjetivo.y) > 0.2F ||
            Math.Abs(VectorHaciaObjetivo.z) > 0.2F)
        {
            // Calculamos el vector de movimiento hacia el hito
            VectorHaciaObjetivo.Normalize();
            VectorHaciaObjetivo *= Velocidad;
            VectorHaciaObjetivo = new Vector3(VectorHaciaObjetivo.x,
                                              VectorHaciaObjetivo.y,
                                              VectorHaciaObjetivo.z);

            // Movemos el objeto hacia el hito
            thisTransform.Translate(VectorHaciaObjetivo * Time.deltaTime, Space.World);

            // El objeto aún no ha llegado al hito
            return false;
        }
        else
        {
            // El objeto ha llegado al hito
            return true;
        }
    }



    void Update()
    {
        // Activamos o desactivamos la gravedad en función de la variable 'Terrestre'
        // thisRigidbody.useGravity = Terrestre;

        // Calculamos la velocidad hacia el siguiente hito (si no hubiese velocidad definida para
        // alguno de los hitos, asumiremos que es 0 y por tanto el objeto quedará parado)
        float VelocidadHaciaHito = 0;
        try
        {
            VelocidadHaciaHito = VelocidadesPatronMovimiento[HitoSiguiente];
        }
        catch
        {
            VelocidadHaciaHito = 0;
        }


        // Comprobamos si podemos ir hacia el siguiente hito
        if (CanGoToNextMilestone)
        {
            try
            {
                if (transform.position.x - target.position.x < 3 && transform.position.y - target.position.y < 3)
                {
                    transform.position = Vector2.MoveTowards(transform.position, target.position, enemySpeed * Time.deltaTime);
                }
                // Movemos al objeto hacia el siguiente hito
                if (IrHaciaHito(HitosPatronMovimiento[HitoSiguiente].transform.position, VelocidadHaciaHito))
                {
                    // Justo cuando lleguemos a un hito, paramos al objeto
                    CanGoToNextMilestone = false;

                    // Activamos el/los script/s de comportamiento correspondiente/s al hito actual (los que
                    // su nombre empiecen contengan la palabra 'Patron').
                    // Explicaremos estos scripts más adelante.
                    bool patronFound = false;
                    MonoBehaviour[] milestoneScripts = HitosPatronMovimiento[HitoSiguiente].GetComponents<MonoBehaviour>();
                    foreach (MonoBehaviour script in milestoneScripts)
                    {
                        if (script.GetType().Name.Contains("Patron"))
                        {
                            patronFound = true;
                            script.enabled = true;
                        }
                    }

                    // Si no encontramos ningún script de comportamiento en el hito, continuamos al siguiente
                    if (!patronFound)
                    {
                        CanGoToNextMilestone = true;
                    }

                    // Calculamos cual será el próximom hito
                    if (HitoSiguiente != HitosPatronMovimiento.Length - 1)
                    {
                        HitoSiguiente++;
                    }
                    else
                    {
                        HitoSiguiente = 0;
                    }
                }
            }
            catch
            {
                HitoSiguiente++;
            }
        }
    }

    void DestroyEnemy()
    {
        int probability = UnityEngine.Random.Range(0, items.Length);

        if (Ysbrid.x == 1 && gameObject.tag == "Enemy1")
        {
            Destroy(this.gameObject);
            Instantiate(items[probability], transform.position, Quaternion.identity);
        }
        else if (Ysbrid.x == 2 && gameObject.tag == "Enemy2")
        {
            Destroy(this.gameObject);
            Instantiate(items[probability], transform.position, Quaternion.identity);
        }
        else if (Ysbrid.x == 3 && gameObject.tag == "Enemy3")
        {
            Destroy(this.gameObject);
            Instantiate(items[probability], transform.position, Quaternion.identity);
        }
        else if (Ysbrid.x == 4 && gameObject.tag == "Enemy4")
        {
            Destroy(this.gameObject);
            Instantiate(items[probability], transform.position, Quaternion.identity);
        }
        else if (Ysbrid.x == 5 && gameObject.tag == "Enemy5")
        {
            Destroy(this.gameObject);
            Instantiate(items[probability], transform.position, Quaternion.identity);
        }

        if (gameObject.tag == "DDD" && Ysbrid.isDevilDewinDead == true)
        {
            Destroy(gameObject);
            Instantiate(items[probability], transform.position, Quaternion.identity);
        }

        if (gameObject.tag == "DCD" && Ysbrid.isDevilCladdDead == true)
        {
            Destroy(gameObject);
            Instantiate(items[probability], transform.position, Quaternion.identity);
        }
    }
}

