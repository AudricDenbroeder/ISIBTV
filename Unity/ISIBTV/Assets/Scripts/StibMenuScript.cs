using UnityEngine;
using UnityEngine.UI;

public class StibMenuScript : MonoBehaviour
{ 
    private uint i;

    private GameObject stib61;
    private GameObject stib92;
    private GameObject stib93;
    private GameObject stib2;
    private GameObject stib6;

    private GameObject temp;

    private GameObject[] gameobjArray = new GameObject[5];

    void Start()
    {
        gameobjArray[0] = GameObject.Find("stib93");
        gameobjArray[1] = GameObject.Find("stib92");
        gameobjArray[2] = GameObject.Find("stib2");
        gameobjArray[3] = GameObject.Find("stib6");
        gameobjArray[4] = GameObject.Find("stib61");

        setPositionAndSize();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow)){
            wheelTurnRight();
        }else if(Input.GetKeyDown(KeyCode.LeftArrow)){
            wheelTurnLeft();
        }
    }

    public void wheelTurnRight(){
        temp = gameobjArray[4];

            for(i=4; i>0; i--){
                gameobjArray[i] = gameobjArray[i-1];
            }
            gameobjArray[0] = temp;
            setPositionAndSize();
    }

    public void wheelTurnLeft(){
        temp = gameobjArray[0];

            for(i=0; i<4; i++){
                gameobjArray[i] = gameobjArray[i+1];
            }

            gameobjArray[4] = temp;
            setPositionAndSize();
    }

    void setPositionAndSize(){
        /*gameobjArray[0].transform.localPosition = new Vector3(-235f, -375f, 0f);
        gameobjArray[1].transform.localPosition = new Vector3(-125f, -375f, 0f);
        gameobjArray[2].transform.localPosition = new Vector3(0f, -360f, 0f);
        gameobjArray[3].transform.localPosition = new Vector3(125f, -375f, 0f);
        gameobjArray[4].transform.localPosition = new Vector3(235f, -375f, 0f);*/

        gameobjArray[0].transform.localPosition = new Vector3(-204f, -387f, 0f);
        gameobjArray[1].transform.localPosition = new Vector3(-123f, -310f, 0f);
        gameobjArray[2].transform.localPosition = new Vector3(0f, -271f, 0f);
        gameobjArray[3].transform.localPosition = new Vector3(123f, -310f, 0f);
        gameobjArray[4].transform.localPosition = new Vector3(204f, -387f, 0f);

        gameobjArray[0].transform.localScale = new Vector3(15f, 15f, 15f);
        gameobjArray[1].transform.localScale = new Vector3(15f, 15f, 15f);
        gameobjArray[2].transform.localScale = new Vector3(21f, 21f, 21f);
        gameobjArray[3].transform.localScale = new Vector3(15f, 15f, 15f);
        gameobjArray[4].transform.localScale = new Vector3(15f, 15f, 15f);

        gameobjArray[0].transform.localRotation = Quaternion.Euler(0f, 0f, 60f);
        gameobjArray[1].transform.localRotation = Quaternion.Euler(0f, 0f, 21f);
        gameobjArray[2].transform.localRotation = Quaternion.Euler(0f, 0f, 0f);
        gameobjArray[3].transform.localRotation = Quaternion.Euler(0f, 0f, -21f);
        gameobjArray[4].transform.localRotation = Quaternion.Euler(0f, 0f, -60f);
    }
}
