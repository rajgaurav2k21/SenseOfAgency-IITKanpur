using UnityEngine;

public class TargetCallScript : MonoBehaviour
{
    // Public variables for Target and Cube GameObjects
    public bool call = false;
    public GameObject Target;
    public GameObject Cube;

    // Public variable to control attraction force
    public float attractionForce = 10f;

    private Rigidbody CubeRigidbody;

    void Start()
    {
        // Get the Rigidbody component of the Cube GameObject
        CubeRigidbody = Cube.GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        // Calculate direction from the Cube to the Target
        Vector3 direction = Target.transform.position - Cube.transform.position;

        // Debug statement to check if call is true
        if (call)
        {
            Debug.Log("Call is true");

            // Apply attraction force to the Cube
            CubeRigidbody.AddForce(direction.normalized * attractionForce * Time.fixedDeltaTime);
        }
    }
}
