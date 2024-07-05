using UnityEngine;
using UnityEngine.EventSystems;

public class VRInputModule : MonoBehaviour {
    public Camera vrUICamera; // Reference to your UI camera

    void Update() {
        if (Input.GetMouseButtonDown(0)) { // Change to your preferred input method
            // Cast a ray from the mouse position
            Ray ray = vrUICamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            // Check if the ray hits a UI element
            if (Physics.Raycast(ray, out hit)) {
                GameObject hitObject = hit.collider.gameObject;

                // Check if the hit object is an input field or a UI element that can receive focus
                if (hitObject.GetComponent<InputField>() != null) {
                    // Set the selected UI element as the current selected
                    EventSystem.current.SetSelectedGameObject(hitObject);
                }
            }
        }
    }
}
