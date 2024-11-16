using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

namespace UI
{
    public class StatInfoPoint : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        [Header("Stat Info")]
        [SerializeField] private string infoText;
        [SerializeField] private GameObject infoPointPrefab;
        private GameObject infoPoint;


        public void OnPointerEnter(PointerEventData eventData)
        {
            infoPointPrefab.GetComponentInChildren<TextMeshProUGUI>().text = infoText;

            Vector3 worldPosition = Camera.main.ScreenToWorldPoint(new Vector3(eventData.position.x, eventData.position.y, Camera.main.nearClipPlane));

            infoPoint = Instantiate(infoPointPrefab, worldPosition, Quaternion.identity, transform);

            infoPoint.transform.localPosition = new Vector3(100,100,0);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            Destroy(infoPoint);
        }
    }

}