using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceSelectListener : MonoBehaviour {

    public ObjectEvent placeSelectEvent;
    public GameObject heroFeedbackImage;
    private GameObject heroFeedbackImageInstance;

    private GameObject parentGameObject;
 
    // Start is called before the first frame update
    void Start() {
    
        placeSelectEvent.RegisterListener(obj => PlaceSelect(obj as PositionAndState));
    }

    // Update is called once per frame
    void Update(){
    }

    void PlaceSelect(PositionAndState heroPositionAndState) {

        if(heroPositionAndState.IsAtPosition){
            if(heroFeedbackImageInstance != null)
            Destroy(parentGameObject);
            Destroy(heroFeedbackImageInstance);

            return;
        }        

        if(heroFeedbackImageInstance != null ) {
            parentGameObject.transform.position 
            = heroPositionAndState.Position;
        } 
        else {
            parentGameObject = new GameObject();
            parentGameObject.transform.position = heroPositionAndState.Position;
            
            heroFeedbackImageInstance 
            = (GameObject)  Instantiate(heroFeedbackImage,
            heroPositionAndState.Position ,Quaternion.identity);
            heroFeedbackImageInstance.transform.SetParent(parentGameObject.transform);
            heroFeedbackImageInstance.GetComponent<Animation>().Play();
        }
        
    }

}
