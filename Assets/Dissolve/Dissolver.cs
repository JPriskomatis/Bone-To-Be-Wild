using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dissolver : MonoBehaviour
{

    [SerializeField] private float dissolveDuration;
    [SerializeField] private float disoslveStrength;




    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.V))
        {
            StartCoroutine(MyDissolver());
        }
    }

    IEnumerator MyDissolver()
    {
        float elapsedTime = 0;
        Material dissolveMat = GetComponent<Renderer>().material;

        while(elapsedTime < dissolveDuration)
        {
            elapsedTime += Time.deltaTime;

            disoslveStrength = Mathf.Lerp(0, 1, elapsedTime / dissolveDuration);
            dissolveMat.SetFloat("_DissolveStrength", disoslveStrength);
            yield return null;
        }

    }

}
