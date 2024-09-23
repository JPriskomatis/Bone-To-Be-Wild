using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

namespace UI
{
    public class BloodSplash : MonoBehaviour
    {
        public static BloodSplash instance;

        
        public Image splashImage;
        public Sprite[] bloodImages;

        private void Awake()
        {
            if (instance != null && instance != this)
            {
                Destroy(this);
            }
            else
                instance = this;
        }

        public void SetBloodScreen(float healthPercentage)
        {
            if (healthPercentage <= 20f)
            {
                splashImage.sprite = bloodImages[4];
            }
            else if (healthPercentage <= 40f)
            {
                splashImage.sprite = bloodImages[3];
            }
            else if (healthPercentage <= 60f)
            {
                splashImage.sprite = bloodImages[2];
            }
            else if (healthPercentage <= 80f)
            {
                splashImage.sprite = bloodImages[1];
            }
            else
            {
                splashImage.sprite = bloodImages[0];
            }

            splashImage.gameObject.SetActive(true);

            // Fade in the image
            StartCoroutine(FadeImage(true));
        }


        IEnumerator FadeImage(bool fadeAway)
        {
            // fade from opaque to transparent
            if (fadeAway)
            {
                // loop over 1 second backwards
                for (float i = 1; i >= 0; i -= Time.deltaTime)
                {
                    // set color with i as alpha
                    splashImage.color = new Color(1, 1, 1, i);
                    yield return null;
                }
            }
            // fade from transparent to opaque
            else
            {
                // loop over 1 second
                for (float i = 0; i <= 1; i += Time.deltaTime)
                {
                    // set color with i as alpha
                    splashImage.color = new Color(1, 1, 1, i);
                    yield return null;
                }
            }
        }
    }

}