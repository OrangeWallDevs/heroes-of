using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ToggleButtonImageController : MonoBehaviour {
   public Button button;
   public Sprite imageOn;
   public Sprite imageOff;
   public UnityAction<bool> action;
   
   void Awake() {
       action += ToggleBackgroundImage;
   }
   
   void ToggleBackgroundImage(bool IsMuted) {
       if(!IsMuted) {
           GetComponent<Image>().sprite = imageOff;
       } else {
           GetComponent<Image>().sprite = imageOn;
       }
   }
}
