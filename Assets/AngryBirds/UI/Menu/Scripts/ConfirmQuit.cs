using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ConfirmQuit : MonoBehaviour,IPointerClickHandler {

    public void OnPointerClick(PointerEventData data) {
        LevelLoader.Quit();
    }
}
