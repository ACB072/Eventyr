using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public GameObject dBox;         // Objeto
    public Text dText;              // Texto dialogo
    public Text dialogueName;       // Texto nombre
    public Animator animator;       // Animador
    public bool dialogActive;       // Comprueba si el dialogo esta activo

    public void ShowBox(char letter)
    {
        dialogActive = true;
        dBox.SetActive(true);
        dText.text += letter;
    }
}
