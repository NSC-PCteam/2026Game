using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SupportItemEffect : MonoBehaviour
{
    public TfControlle tfControlle;
    public PlayerControlle playerControlle;
    public string invenciblePhase;
    //public List<int> supportItemLayerNum;
    [SerializeField] private float invencibleTime;
    [SerializeField] private float tfPlusTime;
    [SerializeField] private float tfPulsTimeTime;
    [SerializeField] private int healPoint;
    [SerializeField] private int lifePoint;

    void Start()
    {
        invenciblePhase="idle";
    }

    // void Update()
    // {
        
    // }

    //補助アイテム　無敵　変身時間増加　回復　残機増加
    public IEnumerator SupportEffect(string supportItemName)
    {
        //Debug.Log($"supportItemName={supportItemName}");
        switch(supportItemName)
        {
            case "TfTime":
                tfControlle.tfTime+=tfPlusTime;
                yield return new WaitForSeconds(tfPulsTimeTime);
                tfControlle.tfTime-=tfPlusTime;
                break;
            case "Invencible":
                //Debug.Log("無敵中");
                invenciblePhase="invencible";
                yield return new WaitForSeconds(invencibleTime);
                invenciblePhase="idle";
                break;
            case "Life":
                playerControlle.lifePoint+=lifePoint;
                break;
            case "Heal":
                break;
            
        }
    }
}
