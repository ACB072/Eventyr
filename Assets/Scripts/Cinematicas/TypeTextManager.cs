using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TypeTextManager : MonoBehaviour
{
    public Animator anim;
    public string[] message;
    private Text textComp;
    public Canvas panel;

    // Use this for initialization
    void Start()
    {
        textComp = GetComponent<Text>();
        textComp.text = "";
        StartCoroutine(TypeText());
    }

    IEnumerator TypeText()
    {
        yield return new WaitForSeconds(2);

        for(int i = 0; i < message.Length; i++)
        {
            foreach (char letter in message[i].ToCharArray())
            {
                textComp.text += letter;
                yield return null;
            }

            yield return new WaitForSeconds(2);

            textComp.text = "";
        }

        textComp.text = ". . .";

        anim.SetBool("Fade", true);

        panel.enabled = false;
    }
}
