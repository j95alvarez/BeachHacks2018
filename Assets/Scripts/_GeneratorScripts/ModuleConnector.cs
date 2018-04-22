using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*Module Connectors will define where pieces may connect, and are 
 * placed at the required points on prefabs to allow this to happen.*/
public class ModuleConnector : MonoBehaviour {

    //Tags on objects on which these are placed will determine types of connecting objects
    public string[] AllowedConnections;
    public bool IsDefault;

    //Changes are viewable in scene
    void OnDrawGizmos()
    {
        var scale = 1.0f;

        //Z-axis visual
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(transform.position, transform.position + transform.forward * scale);

        //X-axis visual
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position - transform.right * scale);
        Gizmos.DrawLine(transform.position, transform.position + transform.right * scale);

        //Y-axis visual
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(transform.position, transform.position + Vector3.up * scale);

        //Wrapping object in scene
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(transform.position, 0.125f);
    }
}
