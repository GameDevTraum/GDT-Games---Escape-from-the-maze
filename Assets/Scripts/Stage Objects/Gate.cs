using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gate : MonoBehaviour
{
    /* Este Script se encarga de controlar la apertura de la puerta.
     * Cuando el personaje entra en contacto con el Collider, este Script
     * le pregunta al GameControl si el personaje tiene la llave, en caso afirmativo
     * la puerta se abre.
     * 
     */

    private GameControl gameControl;
    private string playerTag="Player";
    [SerializeField]
    private GameObject gateLeft;
    [SerializeField]
    private GameObject gateRight;

    
    [SerializeField]
    private float anglesToOpen;
    [SerializeField]
    private int steps;

    private float rotationStep;
    private bool rotating;
    private bool opened;
    private bool opening;

    void Start()
    {
        gameControl = FindObjectOfType<GameControl>();
       
        
        NewGame();
    }

    public void NewGame()
    {
        opened = false;
        opening = false;
        gateRight.transform.localEulerAngles = Vector3.zero;
        gateLeft.transform.localEulerAngles = Vector3.zero;
    }


    private void Update()
    {
        rotationStep = anglesToOpen / steps;

        /*if (Input.GetKeyDown(KeyCode.O))
        {
            OpenGate();
        }*/

    }


    void FixedUpdate()
    {

        if (opening)
        {
            
            
                if (gateRight.transform.localEulerAngles.z < anglesToOpen)
                {
                    gateLeft.transform.Rotate(0, 0, -rotationStep);
                    gateRight.transform.Rotate(0, 0, rotationStep);
                }
                else
                {
                    opening = false;
                    
                    gateRight.transform.localEulerAngles = new Vector3(0,0,anglesToOpen);
                    gateLeft.transform.localEulerAngles = new Vector3(0, 0, -anglesToOpen);
                }
         
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == playerTag)
        {
            if (gameControl.PlayerHasKey())
            {
                if (!opened)
                {
                    OpenGate();
                }
                
            }
        }
    }

    private void OpenGate()
    {
        opening = true;
        gateLeft.GetComponent<AudioSource>().Play();
        gateRight.GetComponent<AudioSource>().Play();
        opened = true;
        

    }



}
