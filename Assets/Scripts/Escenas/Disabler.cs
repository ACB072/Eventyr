using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Disabler : MonoBehaviour
{
    public GameObject DewinCharacter, CladdCharacter;
    // Start is called before the first frame update
    void Start()
    {

    }

    void OnTrigger2D(Collider2D other)
    {
        DewinCharacter.SetActive(false);
        CladdCharacter.SetActive(false);
    }
}
