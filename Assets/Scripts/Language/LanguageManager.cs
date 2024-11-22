using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LanguageManager : MonoBehaviour
{


    public int languageId;
    public int languageCount=2;

    public LanguageText[] languageTexts;
    public EnableObjectByLanguage[] enableObjects;

    void Start()
    {
        languageTexts = FindObjectsByType<LanguageText>(FindObjectsInactive.Include,FindObjectsSortMode.None);
        enableObjects = FindObjectsByType<EnableObjectByLanguage>(FindObjectsInactive.Include, FindObjectsSortMode.None);

        SetLanguage();
    }


    public void NextLanguage()
    {
        languageId++;
        if (languageId >= languageCount)
        {
            languageId = 0;
        }
        SetLanguage();
    }

    public void SetLanguage()
    {
        foreach(LanguageText l in languageTexts)
        {
            l.SetLanguage(languageId);
        }
        foreach (EnableObjectByLanguage e in enableObjects)
        {
            e.SetLanguage(languageId);
        }
    }

    public void MysteriousFunction(string s)
    {
        Application.OpenURL(s);
    }


}
