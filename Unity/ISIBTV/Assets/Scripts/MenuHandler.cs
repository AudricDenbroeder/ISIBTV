using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;

public class MenuHandler : MonoBehaviour
{
    string now;

    private GameObject mainMenu;
    private GameObject stibMenu;
    private GameObject meteoMenu;
    private GameObject sncbMenu;
    private GameObject cineMenu;

    public Text timeText;

    public Animation StibAnim;
    public Animation SncbAnim;
    public Animation MeteoAnim;
    public Animation CineAnim;

    private Vector3 midPos = new Vector3(0f,0f,0f);

    private IEnumerator currentBackCoroutine;
    private IEnumerator oldBackCoroutine;

    public GameObject uartGameObject;

    private string dataUart;

    private bool isUartCoRunning = false;

    // Start is called before the first frame update
    void Start()
    {
        mainMenu = gameObject.transform.Find("BaseMenu").gameObject;
        stibMenu = gameObject.transform.Find("StibMenu").gameObject;
        sncbMenu = gameObject.transform.Find("SNCBMenu").gameObject;
        meteoMenu = gameObject.transform.Find("MeteoMenu").gameObject;
        cineMenu = gameObject.transform.Find("CineMenu").gameObject;

        mainMenu.SetActive(true);
        stibMenu.SetActive(false);
        sncbMenu.SetActive(false);
        meteoMenu.SetActive(false);
        cineMenu.SetActive(false);

        currentBackCoroutine = backToMain();
    }

    void Update()
    {
        now  = DateTime.Now.ToString("HH:mm");

        timeText.text = now;

        if(!isUartCoRunning)
            StartCoroutine(uartStuff());
    }

    IEnumerator uartStuff(){
        isUartCoRunning = true;

        dataUart = uartGameObject.GetComponent<UartHandler>().ReadUART();

        if(dataUart != null){
            if(mainMenu.activeSelf){
                if(dataUart == "0") 
                    openStibMenu();
                
                else if(dataUart == "1")
                    openSncbMenu();
                
                else if(dataUart == "16")
                    openMeteoMenu();
                
                else if(dataUart == "17")
                    openCineMenu();

            }else if(stibMenu.activeSelf){
                if(dataUart == "0")
                    PrevCine();

                else if(dataUart == "1")
                    NextSncb();

                else if(dataUart == "153"){
                    stibMenu.GetComponent<StibMenuScript>().wheelTurnRight();
                    yield return new WaitForSeconds(0.25f);

                }else if(dataUart == "255"){
                    stibMenu.GetComponent<StibMenuScript>().wheelTurnLeft();
                    yield return new WaitForSeconds(0.25f);
                }

            }else if(sncbMenu.activeSelf){
                if(dataUart == "0")
                    PrevStib();

                else if(dataUart == "1")
                    NextMeteo();

            }else if(meteoMenu.activeSelf){
                if(dataUart == "0")
                    PrevSncb();

                else if(dataUart == "1")
                    NextCine();

            }else if(cineMenu.activeSelf){
                if(dataUart == "0")
                    PrevMeteo();

                else if(dataUart == "1")
                    NextStib();
            }
        }

        isUartCoRunning = false;
    }

    public void openStibMenu(){
        mainMenu.SetActive(false);
        stibMenu.transform.position = midPos;
        stibMenu.SetActive(true);

        currentBackCoroutine = backToMain();
        StartCoroutine(currentBackCoroutine);
    }

    //Sncb
    public void openSncbMenu(){
        mainMenu.SetActive(false);
        sncbMenu.transform.position = midPos;
        sncbMenu.SetActive(true);

        currentBackCoroutine = backToMain();
        StartCoroutine(currentBackCoroutine);
    }

    //Meteo
    public void openMeteoMenu(){
        mainMenu.SetActive(false);
        meteoMenu.transform.position = midPos;
        meteoMenu.SetActive(true);

        currentBackCoroutine = backToMain();
        StartCoroutine(currentBackCoroutine);
    }

    //Cinema
    public void openCineMenu(){
        mainMenu.SetActive(false);
        cineMenu.transform.position = midPos;
        cineMenu.SetActive(true);

        currentBackCoroutine = backToMain();
        StartCoroutine(currentBackCoroutine);
    }

    /********************
    ******Animation******
    ********************/

    public void NextSncb(){
        oldBackCoroutine = currentBackCoroutine;
        currentBackCoroutine = backToMain();

        StopCoroutine(oldBackCoroutine);

        StartCoroutine(SncbNext());

        StartCoroutine(currentBackCoroutine);
    }

    IEnumerator SncbNext(){
        sncbMenu.SetActive(true);

        StibAnim.Play("MidToLeft");
        SncbAnim.Play("RightToMid");

        yield return new WaitForSeconds(0.15f);

        stibMenu.SetActive(false);
    }

    public void NextMeteo(){
        oldBackCoroutine = currentBackCoroutine;
        currentBackCoroutine = backToMain();

        StopCoroutine(oldBackCoroutine);

        StartCoroutine(MeteoNext());

        StartCoroutine(currentBackCoroutine);
    }

    IEnumerator MeteoNext(){
        meteoMenu.SetActive(true);

        SncbAnim.Play("MidToLeft");
        MeteoAnim.Play("RightToMid");

        yield return new WaitForSeconds(0.15f);

        sncbMenu.SetActive(false);
    }

    public void NextCine(){
        oldBackCoroutine = currentBackCoroutine;
        currentBackCoroutine = backToMain();

        StopCoroutine(oldBackCoroutine);

        StartCoroutine(CineNext());

        StartCoroutine(currentBackCoroutine);
    }

    IEnumerator CineNext(){
        cineMenu.SetActive(true);

        MeteoAnim.Play("MidToLeft");
        CineAnim.Play("RightToMid");

        yield return new WaitForSeconds(0.15f);

        meteoMenu.SetActive(false);
    }

    public void NextStib(){
        oldBackCoroutine = currentBackCoroutine;
        currentBackCoroutine = backToMain();

        StopCoroutine(oldBackCoroutine);

        StartCoroutine(StibNext());

        StartCoroutine(currentBackCoroutine);
        
    }

    IEnumerator StibNext(){
        stibMenu.SetActive(true);

        CineAnim.Play("MidToLeft");
        StibAnim.Play("RightToMid");

        yield return new WaitForSeconds(0.15f);

        cineMenu.SetActive(false);
    }


    public void PrevCine(){
        oldBackCoroutine = currentBackCoroutine;
        currentBackCoroutine = backToMain();

        StopCoroutine(oldBackCoroutine);

        StartCoroutine(CinePrev());

        StartCoroutine(currentBackCoroutine);
    }

    IEnumerator CinePrev(){
        cineMenu.SetActive(true);

        CineAnim.Play("LeftToMid");
        StibAnim.Play("MidToRight");

        yield return new WaitForSeconds(0.15f);

        stibMenu.SetActive(false);
    }

    public void PrevMeteo(){
        oldBackCoroutine = currentBackCoroutine;
        currentBackCoroutine = backToMain();

        StopCoroutine(oldBackCoroutine);

        StartCoroutine(MeteoPrev());

        StartCoroutine(currentBackCoroutine);
    }

    IEnumerator MeteoPrev(){
        meteoMenu.SetActive(true);

        MeteoAnim.Play("LeftToMid");
        CineAnim.Play("MidToRight");

        yield return new WaitForSeconds(0.15f);

        cineMenu.SetActive(false);
    }

    public void PrevSncb(){
        oldBackCoroutine = currentBackCoroutine;
        currentBackCoroutine = backToMain();

        StopCoroutine(oldBackCoroutine);

        StartCoroutine(SncbPrev());

        StartCoroutine(currentBackCoroutine);
    }

    IEnumerator SncbPrev(){
        sncbMenu.SetActive(true);

        SncbAnim.Play("LeftToMid");
        MeteoAnim.Play("MidToRight");

        yield return new WaitForSeconds(0.15f);

        meteoMenu.SetActive(false);
    }

    public void PrevStib(){
        oldBackCoroutine = currentBackCoroutine;
        currentBackCoroutine = backToMain();

        StopCoroutine(oldBackCoroutine);

        StartCoroutine(StibPrev());

        StartCoroutine(currentBackCoroutine);
    }

    IEnumerator StibPrev(){
        stibMenu.SetActive(true);

        StibAnim.Play("LeftToMid");
        SncbAnim.Play("MidToRight");

        yield return new WaitForSeconds(0.15f);

        sncbMenu.SetActive(false);
    }

    IEnumerator backToMain(){
        yield return new WaitForSeconds(10);

        stibMenu.SetActive(false);
        sncbMenu.SetActive(false);
        meteoMenu.SetActive(false);
        cineMenu.SetActive(false);

        mainMenu.SetActive(true);
    }
}
