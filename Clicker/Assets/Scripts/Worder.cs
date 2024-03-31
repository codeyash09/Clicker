using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Worder : MonoBehaviour
{
    public GameObject[] lines;


    public float[] destinations;

    


    


    // Update is called once per frame
    void Start()
    {



        StartCoroutine(FindSpot(lines[0], destinations[0], 0));





    }

    public void NextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }


    public IEnumerator FindSpot(GameObject line, float destination, int index)
    {
        float myY = 0;

        if (index < lines.Length)
        { 
            myY = 66 - (2.5f * destination);



            

        }


        line.GetComponent<Line>().y = myY;


        if (index < lines.Length - 1)
        {

            StartCoroutine(FindSpot(lines[index + 1], destinations[index + 1], index + 1));

        }


        yield return new WaitForSeconds(0f);
    }
}
