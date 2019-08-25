using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MageTestAnimationKeys : MonoBehaviour
{

    public Transform mage;
    public IsometricCharacterAnimator mageAnimations;

    public TroopState state;

    void Start() {
        
    }

    // Update is called once per frame
    void Update() {

        if (Input.GetKeyDown(KeyCode.M)) { state = TroopState.MOVING; }
        else if (Input.GetKeyDown(KeyCode.A)) { state = TroopState.ATTACKING; }
        else if (Input.GetKeyDown(KeyCode.S)) { state = TroopState.WAITING; }

        switch (state) {

            case TroopState.MOVING:

                Vector2 moveToMouseDirection = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                mageAnimations.AnimateRun(moveToMouseDirection);
                transform.position = Vector3.Lerp(transform.position, moveToMouseDirection, Time.deltaTime);
                break;

            case TroopState.ATTACKING:

                Vector2 attackToMouseDirection = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                mageAnimations.AnimateAttack(attackToMouseDirection);
                break;

            case TroopState.WAITING:

                mageAnimations.AnimateStatic();
                break;

        }
        
    }
}
