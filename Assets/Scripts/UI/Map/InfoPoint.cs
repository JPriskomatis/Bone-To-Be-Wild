using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace UI
{
    public class InfoPoint : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField] private GameObject infoPanel;
        public void OnPointerEnter(PointerEventData eventData)
        {
            infoPanel.SetActive(true);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            infoPanel.SetActive(false);
        }
    }

}