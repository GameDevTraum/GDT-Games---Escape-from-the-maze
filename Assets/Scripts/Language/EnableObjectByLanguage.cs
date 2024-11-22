using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableObjectByLanguage : MonoBehaviour
{

    public bool[] enableStateByLanguage;
    

    public void SetLanguage(int language)
    {
        gameObject.SetActive(enableStateByLanguage[language]);
    }

}
