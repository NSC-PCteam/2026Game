using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TfTimeSlider : MonoBehaviour
{
    public TfControlle tfControlle;
    public GameObject tfCanvas;
    public List<GameObject> tfTimeSliderObj;
    //public List<GameObject> tfTimeSliderObj;

    void Start()
    {
        tfCanvas.SetActive(false);
        foreach(GameObject slider in tfTimeSliderObj)
        {
            slider.SetActive(false);
        }
        if(tfControlle.tfItemInventory.Count>0)
        {
            foreach(string tfItemInventoryName in tfControlle.tfItemInventory)
            {
                switch(tfItemInventoryName)
                {
                    case "Dash":
                        tfTimeSliderObj[0].SetActive(true);
                        break;
                    case "Jump":
                        tfTimeSliderObj[1].SetActive(true);
                        break;
                    case "Swim":
                        tfTimeSliderObj[2].SetActive(true);
                        break;
                    case "Small":
                        tfTimeSliderObj[3].SetActive(true);
                        break;
                    case "Climb":
                        tfTimeSliderObj[4].SetActive(true);
                        break;
                }
            }
        }
    }

    public void AddTfImage(int tfNum)
    {
        tfTimeSliderObj[tfNum].SetActive(true);
    }
}

