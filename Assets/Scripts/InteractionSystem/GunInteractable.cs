using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;
using UnityEngine;

namespace TheSquad.InteractionSystem
{
    public class GunInteractable : NetworkBehaviour, IInteractable
    {
        public delegate void InteractAction();
        public event InteractAction onInteract;

        public void Interact(GameObject interactor)
        {
            

            //onInteract();
        }
    }
}