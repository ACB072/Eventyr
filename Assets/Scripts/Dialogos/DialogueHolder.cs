using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DialogueHolder : MonoBehaviour
{
    public string[] dialogue;           // Array de dialogos
    public string[] dialogueName;       // Array de nombres
    private int i = 0;
    private DialogueManager dMan;       // Objeto DialogueManager

    // Start is called before the first frame update
    void Start()
    {
        dMan = FindObjectOfType<DialogueManager>();
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (Input.GetKeyDown(KeyCode.E))
        {

            Debug.Log("Pushed");
            dMan.animator.SetBool("IsOpen", true);
            StopAllCoroutines();
            StartCoroutine(TypeSentence(dialogue[i]));
        }
       
        if (Input.GetKeyUp(KeyCode.Tab))
        {
            Debug.Log("Pushed2");
            dMan.animator.SetBool("IsOpen", false);
            i = 0;
            dMan.dBox.SetActive(false);
            dMan.dialogActive = false;
        }
    }
    IEnumerator TypeSentence(string dialogue)
    {
        dMan.dText.text = dialogueName[i] +"\n\n";
        foreach (char letter in dialogue.ToCharArray())
        {
            dMan.ShowBox(letter);
            yield return null;
        }
        i++;
    }
    void OnTriggerExit2D(Collider2D other)
    {
        dMan.dBox.SetActive(false);
        dMan.dialogActive = false;
        i=0;
    }

}
