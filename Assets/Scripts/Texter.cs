using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Texter : MonoBehaviour
{
    public GameObject lineOne;
    public GameObject lineTen;
    public GameObject lineHun;

    public float score;

    float scoreOnes;
    float scoreTens;
    float scoreHuns;

    



    // Update is called once per frame
    void Update()
    {
        
        scoreTens = Mathf.Floor((score / 10f) % 10f);
        scoreHuns = Mathf.Floor((score / 100f) % 10f);
        scoreOnes = score % 10;



        Vector3 lOneTarget = new Vector3(lineOne.transform.localPosition.x, 26 - (2.5f * scoreOnes), lineOne.transform.localPosition.z);
        lineOne.transform.localPosition = Vector3.Lerp(lineOne.transform.position, lOneTarget, 0.01f);

        Vector3 lTenTarget = new Vector3(lineTen.transform.localPosition.x, 26 - (2.5f * scoreTens), lineTen.transform.localPosition.z);
        lineTen.transform.localPosition = Vector3.Lerp(lineTen.transform.position, lTenTarget, 0.01f);

        Vector3 lHunTarget = new Vector3(lineHun.transform.localPosition.x, 26 - (2.5f * scoreHuns), lineHun.transform.localPosition.z);
        lineHun.transform.localPosition = Vector3.Lerp(lineHun.transform.position, lHunTarget, 0.01f);
    }
}
