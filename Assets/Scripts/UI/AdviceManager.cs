using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AdviceManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        int rand = Random.Range(0, 11);

        switch(rand)
        {
            case 0:
                GetComponent<Text>().text = "Hola chikis <3";
                break;

            case 1:
                GetComponent<Text>().text = "No olvides inicializar las variables";
                break;

            case 2:
                GetComponent<Text>().text = "APP Hotel patrocina este juego";
                break;

            case 3:
                GetComponent<Text>().text = "Keanu Reeves mola";
                break;

            case 4:
                GetComponent<Text>().text = "No olvides documentarte!";
                break;

            case 5:
                GetComponent<Text>().text = "En Java SI hay punteros";
                break;

            case 6:
                GetComponent<Text>().text = "Has probado el puchero legendario?";
                break;

            case 7:
                GetComponent<Text>().text = "Violacion de segmento";
                break;

            case 8:
                GetComponent<Text>().text = "Vas a perder 10 segundos de tu vida con esta pantalla de carga...";
                break;

            case 9:
                GetComponent<Text>().text = "Wuba Luba Dub Dub!!!";
                break;

            case 10:
                GetComponent<Text>().text = "Assemble!";
                break;
        }
    }
}
