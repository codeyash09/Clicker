using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerMovement : MonoBehaviour
{
    Vector2 mousePos;

    public GameObject pointer;

    GameObject pointerGraphics;
    GameObject pointerGraphics2;
    GameObject pointerGraphicsPrimary;
    GameObject pointerGraphics3;


    public GameObject shatterParticle;

    public bool willCountScore = true;
    public bool willJump = true;

    bool smashing = false;
    bool moving = true;
    bool wibbly = false;


    bool checkForClick = false;


    public Texter myText;

    Camera mainCam;


   

    public float score;


    public float maxX;
    public float minX;
    public float maxY;
    public float minY;



    Vector2 target;
    


    private void Start()
    {
        pointerGraphics = pointer.transform.GetChild(0).transform.GetChild(0).gameObject;
        pointerGraphics2 = pointer.transform.GetChild(0).transform.GetChild(1).gameObject;
        pointerGraphicsPrimary = pointer.transform.GetChild(0).transform.GetChild(2).gameObject;
        pointerGraphics3 = pointer.transform.GetChild(0).transform.GetChild(3).gameObject;


        score = 0f;


        checkForClick = false;
        smashing = false;
        wibbly = false;
        moving = false;



        


       


        pointerGraphics.SetActive(false);
        pointerGraphics2.SetActive(false);
        pointerGraphics3.SetActive(false);

        mainCam = Camera.main;


        target = new Vector2(0, 0);

    }


    private void Update()
    {
        Quaternion rotation = Quaternion.LookRotation(
            transform.position - pointerGraphics2.transform.GetChild(1).position,
            pointerGraphics2.transform.GetChild(1).TransformDirection(Vector3.up)
        );
        pointerGraphics2.transform.GetChild(1).rotation = new Quaternion(0, 0, rotation.z, rotation.w);

        if (willCountScore)
        {
            myText.score = score;
        }

        if (checkForClick && willJump)
        {
            if (Input.GetMouseButtonDown(0))
            {
                moving = true;
                float x = Random.Range(0.25f, 0.75f);
                float y = Random.Range(0.25f, 0.75f);
                Vector3 pos = new Vector3(x, y, 0f);
                pos = Camera.main.ViewportToWorldPoint(pos);

                target = pos;

                transform.GetChild(0).transform.GetChild(0).localScale = new Vector3(10f, 10f, 10f);

                int rand = Random.Range(15, 50);

                for(int i = 0; i < rand; i++)
                {
                    GameObject shatterPart = Instantiate(shatterParticle, transform);
                    shatterPart.transform.parent = null;
                    shatterPart.transform.position = new Vector3(shatterPart.transform.position.x + Random.Range(-1f, 1f), shatterPart.transform.position.y + Random.Range(-1f, 1f));
                    shatterPart.transform.localEulerAngles = new Vector3(0, 0, Random.Range(0, 360));
                    shatterPart.transform.localScale = new Vector3(Random.Range(0.05f, 5), Random.Range(0.05f, 5), 1);

                    shatterPart.GetComponent<Rigidbody2D>().velocity = shatterPart.transform.right * Random.Range(1, 20);
             
                }

                score += 1;

                checkForClick = false;
            }
        }


        if (Input.GetMouseButtonDown(0) && !smashing)
        {
            smashing = true;


            pointerGraphics.transform.localScale = new Vector3(Random.Range(1, 7f), Random.Range(1, 7f), Random.Range(1, 7f));
            pointerGraphics2.transform.localScale = new Vector3(Random.Range(1, 7f), Random.Range(1, 7f), Random.Range(1, 7f));
            pointerGraphics3.transform.localScale = new Vector3(Random.Range(1, 7f), Random.Range(1, 7f), Random.Range(1, 7f));
            pointerGraphicsPrimary.SetActive(false);

            pointerGraphics2.transform.GetChild(1).GetComponent<SpriteRenderer>().color = Random.ColorHSV();
            pointerGraphics2.transform.GetChild(1).localScale = new Vector3(Random.Range(55, 55f), Random.Range(5, 20f), Random.Range(1, 1f));


            pointerGraphics.SetActive(true);
            pointerGraphics2.SetActive(true);
            pointerGraphics3.SetActive(true);







            pointerGraphics.transform.localEulerAngles = new Vector3(0, 0, Random.Range(0, 360));
            pointerGraphics2.transform.localEulerAngles = new Vector3(0, 0, Random.Range(0, 360));
            pointerGraphics3.transform.localEulerAngles = new Vector3(0, 0, Random.Range(0, 360));

            

        }
    }


    public void OnTriggerEnter2D(Collider2D collision)
    {
        checkForClick = true;

    }

    public void OnTriggerStay2D(Collider2D collision)
    {
        checkForClick = true;
    }





    // Update is called once per frame
    void FixedUpdate()
    {
        maxX = Screen.width / 2f;
        minX = -(Screen.width / 2f);
        maxY = Screen.height / 2f;
        minY = -(Screen.height / 2f);

        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);


        mousePos = new Vector3(mousePos.x, mousePos.y, 0);

        pointer.transform.position = mousePos;

        Cursor.visible = false;





        if (moving)
        {
            transform.position = target;

            transform.GetChild(0).transform.GetChild(0).localScale = Vector3.Lerp(transform.localScale, new Vector3(0.05f, 0.05f, 0.05f), 1f);


            if(transform.GetChild(0).transform.GetChild(0).localScale.x < 1.05f)
            {
                moving = false; 
            }
            
        }


        if (smashing)
        {
            ClickSmash();


            

        } else
        {
            mainCam.transform.rotation = Quaternion.Euler(0, 0, 0);
        }

        if (wibbly)
        {
            StartCoroutine(Shake(.15f, .4f));
            wibbly = false;
        }


        
    }

    void ClickSmash()
    {
        pointerGraphics.transform.localScale = Vector3.Lerp(pointerGraphics.transform.localScale, new Vector3(1, 1, 1), 0.25f);
        pointerGraphics2.transform.localScale = Vector3.Lerp(pointerGraphics.transform.localScale, new Vector3(1, 1, 1), 0.25f);
        pointerGraphics3.transform.localScale = Vector3.Lerp(pointerGraphics.transform.localScale, new Vector3(1, 1, 1), 0.25f);
        pointerGraphics2.transform.GetChild(1).GetComponent<SpriteRenderer>().color = Random.ColorHSV();
        pointerGraphics2.transform.GetChild(1).localScale = Vector3.Lerp(pointerGraphics2.transform.GetChild(1).localScale, new Vector3(55, 0, 1), 0.25f);


        if (pointerGraphics.transform.localScale.x < 1.25f)
        {
            smashing = false;
            wibbly = true;

            mainCam.transform.rotation = Quaternion.Euler(0, 0, 0);

            pointerGraphics.SetActive(false);
            pointerGraphics2.SetActive(false);
            pointerGraphics3.SetActive(false);
            pointerGraphicsPrimary.SetActive(true);




        }



    }


    public IEnumerator Shake(float duration, float magnitude)
    {
        Vector3 originalPos = mainCam.transform.localPosition;

        float elapsed = 0f;

        while (elapsed < duration)
        {
            float x = Random.Range(-1f, 1f) * magnitude;
            float y = Random.Range(-1f, 1f) * magnitude;

            mainCam.transform.localPosition = new Vector3(x, y, originalPos.z);

            elapsed += Time.deltaTime;

            yield return null;

        }

        mainCam.transform.localPosition = originalPos;
    }
    
}
