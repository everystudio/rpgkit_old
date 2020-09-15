using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace rpgkit
{
    public abstract class ReadHilightedButtonBase : MonoBehaviour, ISelectHandler
    {
        public int buttonValue;

        public abstract void OnSelect(BaseEventData eventData);
    }

}


