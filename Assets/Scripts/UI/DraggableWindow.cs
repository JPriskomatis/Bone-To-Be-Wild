using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace UI
{
    public class DraggableWindow : MonoBehaviour, IDragHandler
    {
        private Canvas canvas;
        private RectTransform rectTransform;

        private void Start()
        {
            canvas = GetComponentInParent<Canvas>();
            rectTransform = GetComponent<RectTransform>();
        }

        void IDragHandler.OnDrag(UnityEngine.EventSystems.PointerEventData eventData)
        {
            rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
        }
    }

}