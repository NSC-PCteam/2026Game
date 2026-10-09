using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TfControlle : MonoBehaviour
{
    public TfTimeSlider tfTimeSlider;
    public static TfControlle Instance { get; private set; }
    public float tfTime;
    public float tfInterval;
    public string tfPhase;
    public List<Slider> tfSlider;
    public List<Image> sliderColor;
    public List<int> tfItemLayerNum;
    public List<int> tfItemInventory;
    public List<float> usingTfTime = new List<float>(){0f, 0f, 0f, 0f, 0f, 0f};
    public List<string> canTfPhase = new List<string>(){"canTf", "canTf", "canTf", "canTf", "canTf", "canTf"};

    //シーンをまたいでスクリプトをアタッチしたゲームオブジェクトの保存
    void Awake()
    {
        // すでに存在していたら破棄
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;

        // シーンをまたいで保持
        DontDestroyOnLoad(gameObject);
    }

    //初期変身状態
    void Start()
    {
        tfPhase="idle";
        tfItemInventory.Clear();
    }

    //変身操作処理
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.KeypadPlus))
        {
            if(tfTimeSlider.tfCanvas.activeSelf)
            {
                tfTimeSlider.tfCanvas.SetActive(false);
            }
            else
            {
                tfTimeSlider.tfCanvas.SetActive(true);
            }
        }
        // if(Input.GetKeyUp(KeyCode.KeypadPlus))
        // {
        //     tfTimeSlider.tfCanvas.SetActive(false);
        // }

        if(Input.GetKeyDown(KeyCode.Keypad5))
        {
            TfColorReset(tfPhase);
            tfPhase = "idle";
        }
        else if(Input.GetKeyDown(KeyCode.Keypad7))
        {
            foreach(int tfItemInventoryNum in tfItemInventory)
            {
                if(tfItemInventoryNum==tfItemLayerNum[0])
                {
                    if(canTfPhase[0]=="canTf")
                    {
                        TfColorReset(tfPhase);
                        tfPhase = "dash";
                        StartCoroutine(TfTime(tfPhase, 0));
                    }
                }
            }
        }
        else if(Input.GetKeyDown(KeyCode.Keypad8))
        {
            foreach(int tfItemInventoryNum in tfItemInventory)
            {
                if(tfItemInventoryNum==tfItemLayerNum[1])
                {
                    if(canTfPhase[1]=="canTf")
                    {
                        TfColorReset(tfPhase);
                        tfPhase = "doubleJump";
                        StartCoroutine(TfTime(tfPhase, 1));
                    }
                }
            }
        }
        else if(Input.GetKeyDown(KeyCode.Keypad9))
        {
            foreach(int tfItemInventoryNum in tfItemInventory)
            {
                if(tfItemInventoryNum==tfItemLayerNum[2])
                {
                    if(canTfPhase[2]=="canTf")
                    {
                        TfColorReset(tfPhase);
                        tfPhase = "swim";
                        StartCoroutine(TfTime(tfPhase, 2));
                    }
                }
            }
        }
        else if(Input.GetKeyDown(KeyCode.Keypad4))
        {
            foreach(int tfItemInventoryNum in tfItemInventory)
            {
                if(tfItemInventoryNum==tfItemLayerNum[3])
                {
                    if(canTfPhase[3]=="canTf")
                    {
                        TfColorReset(tfPhase);
                        tfPhase = "small";
                        StartCoroutine(TfTime(tfPhase, 3));
                    }
                }
            }
        }
        else if(Input.GetKeyDown(KeyCode.Keypad6))
        {
            foreach(int tfItemInventoryNum in tfItemInventory)
            {
                if(tfItemInventoryNum==tfItemLayerNum[4])
                {
                    if(canTfPhase[4]=="canTf")
                    {
                        TfColorReset(tfPhase);
                        tfPhase = "climb";
                        StartCoroutine(TfTime(tfPhase, 4));
                    }
                }
            }
        }
    }

    private IEnumerator TfTime(string phase, int tfNum)
    {
        canTfPhase[tfNum] = "interval";
        usingTfTime[tfNum]=0f;
        sliderColor[tfNum].color = new Color(0f, 255f, 0f, 255f);
   
        while(usingTfTime[tfNum]<tfTime)
        {
            usingTfTime[tfNum] += Time.fixedDeltaTime;
            tfSlider[tfNum].value = 1-usingTfTime[tfNum]/tfTime;
            yield return new WaitForFixedUpdate();
        }

        if(tfPhase==phase) tfPhase="idle";
        sliderColor[tfNum].color = new Color(175f, 175f, 175f, 255f);

        while(usingTfTime[tfNum]<tfInterval+tfTime)
        {
            usingTfTime[tfNum] += Time.fixedDeltaTime;
            tfSlider[tfNum].value = (usingTfTime[tfNum]-tfTime)/tfInterval;
            yield return new WaitForFixedUpdate();
        }

        sliderColor[tfNum].color = new Color(0f, 255f, 0f, 255f);

        canTfPhase[tfNum] = "canTf";
    }

    public void TfItemGet(int getTfItemLayerNum)
    {
        Debug.Log($"getTfItemLayerNum={getTfItemLayerNum}");
        foreach(int tfItemNum in tfItemLayerNum)
        {
            Debug.Log($"tfItemNum={tfItemNum}");
            if(tfItemNum==getTfItemLayerNum)
            {
                bool isGetItem=false;
                foreach(int tfItemInventoryNum in tfItemInventory)
                {
                    if(tfItemInventoryNum==tfItemNum)
                    {
                        Debug.Log("この変身アイテムは取得済み");
                        isGetItem=true;
                        break;
                    }
                }

                if(!isGetItem)
                {
                    Debug.Log($"変身アイテムを取得、レイヤー番号は{getTfItemLayerNum}");
                    tfItemInventory.Add(getTfItemLayerNum);
                    if(tfItemLayerNum[0]==getTfItemLayerNum)
                    {
                        tfTimeSlider.AddTfImage(0);
                        sliderColor[0].color = new Color(0f, 255f, 0f, 255f);
                    }
                    else if(tfItemLayerNum[1]==getTfItemLayerNum)
                    {
                        tfTimeSlider.AddTfImage(1);
                        sliderColor[1].color = new Color(0f, 255f, 0f, 255f);
                    }
                    else if(tfItemLayerNum[2]==getTfItemLayerNum)
                    {
                        tfTimeSlider.AddTfImage(2);
                        sliderColor[2].color = new Color(0f, 255f, 0f, 255f);
                    }
                    else if(tfItemLayerNum[3]==getTfItemLayerNum)
                    {
                        tfTimeSlider.AddTfImage(3);
                        sliderColor[3].color = new Color(0f, 255f, 0f, 255f);
                    }
                    else if(tfItemLayerNum[4]==getTfItemLayerNum)
                    {
                        tfTimeSlider.AddTfImage(4);
                        sliderColor[4].color = new Color(0f, 255f, 0f, 255f);
                    }  
                }
                break;
            }
            else
            {
                Debug.Log("このアイテムのレイヤーは変身アイテムに入っていない");
            }
        }
    }

    private void TfColorReset(string tfPhase)
    {
        switch(tfPhase)
        {
            case "dash":
                sliderColor[0].color = new Color(175f, 175f, 175f, 255f);
                break;
            case "doubleJump":
                sliderColor[1].color = new Color(175f, 175f, 175f, 255f);
                break;
            case "swim":
                sliderColor[2].color = new Color(175f, 175f, 175f, 255f);
                break;
            case "small":
                sliderColor[3].color = new Color(175f, 175f, 175f, 255f);
                break;
            case "climb":
                sliderColor[4].color = new Color(175f, 175f, 175f, 255f);
                break;
        }
    }
}
