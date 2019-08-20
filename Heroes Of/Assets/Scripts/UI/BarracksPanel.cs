using UnityEngine;

public class BarracksPanel : MonoBehaviour { 

    private bool isOpen;
    private Animator animator;

    private void Start() {

        isOpen = gameObject.activeInHierarchy;

    }

    private void Awake() {

        animator = GetComponent<Animator>();

    }

    public void ToggleVisibilityBarracksMenu() {

        isOpen = !isOpen;

        if (isOpen) {

            gameObject.SetActive(isOpen);
            animator.Play("Open_Animation");

        }
        else {

            animator.Play("Hide_Animation");

        }

    }

    public void RemoveBarracksMenu() {

        gameObject.SetActive(isOpen);

    }

}
