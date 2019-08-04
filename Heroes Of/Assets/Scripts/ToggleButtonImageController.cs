using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ToggleButtonImageController : MonoBehaviour {
   public Button button;
   public Sprite soundOn;
   public Sprite soundOff;
   public UnityAction<bool> action;

   void Start() {
       action += ToggleBackgroundImage;
   }
   
   void ToggleBackgroundImage(bool IsMuted) {
       if(IsMuted) {
           button.GetComponent<Image>().sprite = soundOff;
       } else {
           button.GetComponent<Image>().sprite = soundOn;
       }
   }
}
