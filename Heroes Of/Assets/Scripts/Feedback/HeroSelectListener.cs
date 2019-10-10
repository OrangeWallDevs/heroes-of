using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroSelectListener : MonoBehaviour
{
    public BoolEvent heroSelectEvent;
    public GameObject heroFeedbackImage;
    private GameObject heroFeedbackImageInstance;
    private bool isHeroSelected;
    private Vector2 differencePosition;
    public Vector2 DifferencePosition { get => differencePosition;
     set => differencePosition = value; }

    // Start is called before the first frame update
    void Start(){
        heroSelectEvent.RegisterListener(HeroSelect);
        differencePosition.Set(0,1.5f);
        isHeroSelected = false;

    }

    // Update is called once per frame
    void Update() {

    }

    void HeroSelect(bool isHeroSelected){
        this.isHeroSelected = isHeroSelected;
        if(this.isHeroSelected){
            heroFeedbackImageInstance = (GameObject) Instantiate(heroFeedbackImage
            , this.transform.position + (Vector3) DifferencePosition
            , Quaternion.identity);

            heroFeedbackImageInstance.transform.SetParent(this.transform);
            
        } else {
            Destroy(heroFeedbackImageInstance);
        }
    }
}
