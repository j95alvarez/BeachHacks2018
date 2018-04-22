using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Modules are defined by their "exits" where they may be connected to other objects
//GetExits returns all possible connecting places on an object (As defined in prefab)
public class Module : MonoBehaviour {

    public string objTag;
    public int weight;
    private void OnValidate()
    {
        objTag = gameObject.tag;
    }
    public ModuleConnector[] GetExits()
    { return GetComponentsInChildren<ModuleConnector>(); }
}
