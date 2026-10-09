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
            foreach(int tfItemInventoryNum in tfControlle.tfItemInventory)
            {
                tfTimeSliderObj[tfItemInventoryNum].SetActive(true);
            }
        }
    }

    public void AddTfImage(int tfNum)
    {
        tfTimeSliderObj[tfNum].SetActive(true);
    }
}

