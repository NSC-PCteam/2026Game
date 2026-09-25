using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TfControlle : MonoBehaviour
{
    public static TfControlle Instance { get; private set; }
    public float tfTime;
    [SerializeField] private float tfInterval;
    public string tfPhase;
    public List<int> tfItemLayerNum;
    public List<int> tfItemInventory;
    public List<bool> canTf;

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
        for(int i=0; i<5; i++)
        {
            canTf.Add(true);
        }
    }

    //変身操作処理
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            tfPhase = "idle";
        }
        else if(Input.GetKeyDown(KeyCode.Alpha2) && canTf[0])
        {
            foreach(int tfItemInventoryNum in tfItemInventory)
            {
                if(tfItemInventoryNum==tfItemLayerNum[0])
                {
                    tfPhase = "dash";
                    StartCoroutine(TfTime(tfPhase));
                }
            }
        }
        else if(Input.GetKeyDown(KeyCode.Alpha3) && canTf[1])
        {
            foreach(int tfItemInventoryNum in tfItemInventory)
            {
                if(tfItemInventoryNum==tfItemLayerNum[1])
                {
                    tfPhase = "doubleJump";
                    StartCoroutine(TfTime(tfPhase));
                }
            }
        }
        else if(Input.GetKeyDown(KeyCode.Alpha4) && canTf[2])
        {
            foreach(int tfItemInventoryNum in tfItemInventory)
            {
                if(tfItemInventoryNum==tfItemLayerNum[2])
                {
                    tfPhase = "swim";
                    StartCoroutine(TfTime(tfPhase));
                }
            }
        }
        else if(Input.GetKeyDown(KeyCode.Alpha5) && canTf[3])
        {
            foreach(int tfItemInventoryNum in tfItemInventory)
            {
                if(tfItemInventoryNum==tfItemLayerNum[3])
                {
                    tfPhase = "small";
                    StartCoroutine(TfTime(tfPhase));
                }
            }
        }
        else if(Input.GetKeyDown(KeyCode.Alpha6) && canTf[4])
        {
            foreach(int tfItemInventoryNum in tfItemInventory)
            {
                if(tfItemInventoryNum==tfItemLayerNum[4])
                {
                    tfPhase = "climb";
                    StartCoroutine(TfTime(tfPhase));
                }
            }
        }
    }

    private IEnumerator TfTime(string phase)
    {
        switch(phase)
        {
            case "dash":   
                canTf[0]=false;
                break;
            case "doubleJump":
                canTf[1]=false;
                break;
            case "swim":
                canTf[2]=false;
                break;
            case "small":
                canTf[3]=false;
                break;
            case "climb":
                canTf[4]=false;
                break;
        }

        float time=0f;
   
        while(time<tfTime)
        {
            time += Time.fixedDeltaTime;
            yield return new WaitForFixedUpdate();
        }

        if(tfPhase==phase) tfPhase="idle";
   
        yield return new WaitForSeconds(tfInterval);

        switch(phase)
        {
            case "dash":   
                canTf[0]=true;
                break;
            case "doubleJump":
                canTf[1]=true;
                break;
            case "swim":
                canTf[2]=true;
                break;
            case "small":
                canTf[3]=true;
                break;
            case "climb":
                canTf[4]=true;
                break;
        }
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
                }
                break;
            }
            else
            {
                Debug.Log("このアイテムのレイヤーは変身アイテムに入っていない");
            }
        }
    }
}
