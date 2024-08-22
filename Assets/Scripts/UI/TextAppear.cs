using UnityEngine;
using TMPro;


namespace UI
{
    public static class TextAppear
    {
        private static TextMeshProUGUI text;


        public static void Initialize()
        {

            // Find the GameObject named "InteractionText" and get its TextMeshPro component
            GameObject interactionTextObject = GameObject.Find("InteractionText");
            if (interactionTextObject != null)
            {
                text = interactionTextObject.GetComponent<TextMeshProUGUI>();
                if (text == null)
                {
                    Debug.LogError("No TextMeshProUGUI component found on the InteractionText GameObject.");
                }
            }
            else
            {
                Debug.LogError("InteractionText GameObject is not found.");
            }
        }

        public static void SetText(string textToPut)
        {
            if (text != null)
            {
                text.gameObject.SetActive(true);
                text.text = textToPut;
            }
            else
            {
                Debug.LogError("TextMeshProUGUI component is not assigned.");
            }
        }

        public static void RemoveText()
        {
            if (text != null)
            {
                text.gameObject.SetActive(false);
            }
            else
            {
                Debug.LogError("TextMeshProUGUI component is not assigned.");
            }
        }
    }
}
