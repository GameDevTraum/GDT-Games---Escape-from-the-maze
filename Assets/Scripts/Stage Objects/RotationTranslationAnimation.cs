using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationTranslationAnimation : MonoBehaviour
{

    /*ESPAÑOL
     *Solución por GameDevTraum
    * 
    * Artículo: https://gamedevtraum.com/es/descargar-assets/scripts-para-unity/script-de-rotacion-y-traslacion-para-gameobjects-en-unity/
    * Página: https://gamedevtraum.com/es/
    * Canal: https://youtube.com/c/GameDevTraum
    * 
    * Visita la página para encontrar más soluciones, Assets y artículos
   */

    /*ENGLISH
    *Solution by GameDevTraum
    * 
    * Website: https://gamedevtraum.com/en/
    * Channel: https://youtube.com/c/GameDevTraum
    * 
    * Visit the website to find more articles, solutions and assets
    */

    /*DEUTSCH
    *Lösung von GameDevTraum
    * 
    * Webseite: https://gamedevtraum.com/de/
    * Kanal: https://youtube.com/c/GameDevTraum
    * 
    * Besuch die Website, um weitere Artikel, Lösungen und Hilfsmittel zu finden. 
    *
    */


    private enum RotationAxis {X,Y,Z};

    [SerializeField]
    private RotationAxis rotationAxis;

    [SerializeField]
    [Range(-5f, 5f)]
    private float rotationSpeed;
    [SerializeField]
    [Range(0f, 50f)]
    private float upDownAmplitude;
    
    [SerializeField]
    [Range(0f, 5f)]
    private float upDownFrequency;

    float radIncrement;
    float rad=0;
    private float initialX,initialY,initialZ;

    void Start()
    {
        radIncrement = 2 * Mathf.PI*upDownFrequency;
        initialX = transform.position.x;
        initialY = transform.position.y;
        initialZ = transform.position.z;
    }

    void FixedUpdate()
    {
        Movement();
        Rotation();       
         
    }

    private void Rotation()
    {
        switch (rotationAxis)
        {
            case RotationAxis.X:
                transform.Rotate(rotationSpeed * Vector3.right);
                break;
            case RotationAxis.Y:
                transform.Rotate(rotationSpeed * Vector3.up);
                break;
            case RotationAxis.Z:
                transform.Rotate(rotationSpeed * Vector3.forward);
                break;
        }
    }

    private void Movement()
    {
#if UNITY_EDITOR
        radIncrement = 2 * Mathf.PI * upDownFrequency;
#endif
        transform.position = new Vector3(transform.position.x, initialY + upDownAmplitude * Mathf.Sin(rad), transform.position.z);

        rad += radIncrement * Time.fixedDeltaTime;
        if (rad > 2 * Mathf.PI)
        {
            rad = 0;
        }
    }

}
