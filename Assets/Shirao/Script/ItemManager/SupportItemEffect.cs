using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SupportItemEffect : MonoBehaviour
{
    public TfControlle tfControlle;
    public PlayerControlle playerControlle;
    public string invinciblePhase;
    public List<int> supportItemLayerNum;
    [SerializeField] private float invincibleTime;
    [SerializeField] private float tfPlusTime;
    [SerializeField] private float tfPulsTimeTime;
    [SerializeField] private int healPoint;
    [SerializeField] private int lifePoint;

    void Start()
    {
        invinciblePhase="idle";
    }

    void Update()
    {
        
    }

    //補助アイテム　無敵　変身時間増加　回復　残機増加
    public IEnumerator SupportEffect(int supportItemNum)
    {
        switch(supportItemNum)
        {
            case 15:
                invinciblePhase="invincible";
                yield return new WaitForSeconds(invincibleTime);
                invinciblePhase="idle";
                break;
            case 16:
                tfControlle.tfTime+=tfPlusTime;
                yield return new WaitForSeconds(tfPulsTimeTime);
                tfControlle.tfTime-=tfPlusTime;
                break;
            case 17:
                playerControlle.lifePoint+=lifePoint;
                break;
            case 18:
                break;
            
        }
    }
}
