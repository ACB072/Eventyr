
using System;
using UnityEngine;

[Serializable]
public class AllPersistentData
{
    // Atributos serializables camara
    public float cameraXPosition, cameraYPosition;

    // Atributos serializables Ysbrid
    public int ysbridHealth_, ysbridMana_, ysbridCoins_, ysbridKeys_, ysbridCharges_,
       ysbridLevel_, ysbridPhysicLevel_, ysbridMagicLevel_, currentScene_;
    public float ysbridXPosition_, ysbridYPosition_;
    public bool hasSword_, hasTorch_;


    // Atributos serializables Dewin
    public int dewinHealth_, dewinMana_,
        dewinLevel_, dewinPhysicLevel_, dewinMagicLevel_;
    public float dewinXPosition_, dewinYPosition_;


    // Atributos serializables Claddwyd
    public int claddHealth_, claddMana_,
        claddLevel_, claddPhysicLevel_, claddMagicLevel_;
    public float claddXPosition_, claddYPosition_;

    // Atributos serializables objetos
    public bool isBroken_;

}
