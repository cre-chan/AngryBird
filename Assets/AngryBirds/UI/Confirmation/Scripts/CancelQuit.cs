using UnityEngine;
using UnityEngine.EventSystems;


namespace UI.InGameUIs
{
    public class CancelQuit : MonoBehaviour, IPointerClickHandler
    {

        public void OnPointerClick(PointerEventData data)
        {
            this.transform.parent.gameObject.SetActive(false);
        }
    }
}
