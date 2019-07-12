using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class CancelQuit : MonoBehaviour,IPointerClickHandler {

    public void OnPointerClick(PointerEventData data) {
        this.transform.parent.gameObject.SetActive(false);
    }
}
