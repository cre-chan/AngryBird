using UnityEngine;
using UnityEngine.EventSystems;


namespace UI.InGameUIs
{
    public class ConfirmQuit : MonoBehaviour, IPointerClickHandler
    {

        public void OnPointerClick(PointerEventData data)
        {
            LevelLoader.Quit();
        }
    }
}
