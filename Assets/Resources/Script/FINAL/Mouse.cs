using UnityEngine;

public class Mouse : MonoBehaviour
{
    public float moveSpeed = 0.1f;
    Animator animator;
    void Update()
    {
        float LeftRight = Input.GetAxis("Mouse X");
        float UpDown = Input.GetAxis("Mouse Y");
        float Wheel=Input.GetAxis("Mouse ScrollWheel");
        if(Wheel!=0f)
        {
            //animator.Play("wheel");
            Debug.Log("Wheel");
        }
        Vector3 moveDirection = new Vector3(UpDown, LeftRight, 0f);
        transform.Translate(moveDirection * moveSpeed * Time.deltaTime);

    }
}
