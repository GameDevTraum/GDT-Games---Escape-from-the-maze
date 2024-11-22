using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour
{
    /* Todos los items que el personaje puede recoger tienen asignado este script.
     * Se encarga de determinar el tipo de objeto, avisar al control cuando el personaje
     * los recoge y se autodestruye.
     */

    public enum Item {CAR,CAMERA,CANNON,BALLOON,EARTH,VIOLIN,KEY};
    
    [SerializeField]
    private Item item;

    private string playerTag="Player";
    private GameControl gameControl;

    private void Start()
    {
        gameControl = FindObjectOfType<GameControl>();
    }

    private void OnTriggerEnter(Collider other)
    {
       
        if (other.tag == playerTag)
        {
            //Collect Item
            //avisar al control
            gameControl.ItemFound(item);
            Destroy(gameObject);
        }
    }



}
