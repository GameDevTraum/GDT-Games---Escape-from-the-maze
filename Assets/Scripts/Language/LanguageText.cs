using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class LanguageText : MonoBehaviour
{
    public TMPro.TMP_Text myText;
    [TextArea]
    public string[] languageText;

    private void Awake()
    {
        myText = GetComponent<TMPro.TMP_Text>();
    }

    public void SetLanguage(int language)
    {
        if (myText == null)
        {
            myText = GetComponent<TMPro.TMP_Text>();
        }
        myText.text = languageText[language];

    }
    
}
