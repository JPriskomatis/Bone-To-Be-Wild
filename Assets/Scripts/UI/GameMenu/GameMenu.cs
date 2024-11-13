using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.PlayerLoop;

namespace GameMnu
{
    public class GameMenu : MonoBehaviour
    {
        [SerializeField] private GameObject ejetaLogo;


        public void Start()
        {
            StartCoroutine(FadeInAndOut(ejetaLogo));
        }

        IEnumerator FadeInAndOut(GameObject logo)
        {
            yield return new WaitForSeconds(2f);
            //Fade it;
            logo.GetComponent<CanvasGroup>().DOFade(1, 2f);

            yield return new WaitForSeconds(3f);

            logo.GetComponent<CanvasGroup>().DOFade(0, 2f);
        }

        public void EnterGame()
        {
            SceneTransition.Instance.GoToScene(ConstantValues.GAME_SCENE);
        }

        
    }

}