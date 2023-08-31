using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLook : MonoBehaviour
{
	
	
	[SerializeField] private string mouseXInputName, mouseYInputName;
	[SerializeField] private float mouseSensitivity;
	[SerializeField] float crouchHeight;
	[SerializeField] float normalHeight = 0.9f;
	[SerializeField] private Transform playerBody;
	
	private float xAxisClamp;
	
	private void Awake(){
		
		LockCursor();
		xAxisClamp = 0.0f;
	}
	
	private void LockCursor(){
		
		Cursor.lockState = CursorLockMode.Locked;
	}
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
		Vector3 newScale = new Vector3(transform.localScale.x, normalHeight, transform.localScale.z);

		if (Input.GetKey(KeyCode.C)) {
            newScale.y = crouchHeight;

        }

        transform.localScale = newScale;

        CameraRotation();
    }
	
	private void CameraRotation()
	{
		float mouseX = Input.GetAxis(mouseXInputName) * mouseSensitivity * Time.deltaTime;
		float mouseY = Input.GetAxis(mouseYInputName) * mouseSensitivity * Time.deltaTime;
		
		xAxisClamp += mouseY;
		
		if(xAxisClamp > 90.0f)
		{
			xAxisClamp = 90.0f;
			mouseY = 0.0f;
			ClampXAxisRotationToValue(270.0f);
		}
		
		else if(xAxisClamp < -90.0f)
		{
			xAxisClamp = -90.0f;
			mouseY = 0.0f;
			ClampXAxisRotationToValue(90.0f);
		}
		
		transform.Rotate(Vector3.left * mouseY);
		playerBody.Rotate(Vector3.up * mouseX);
	}
	
	private void ClampXAxisRotationToValue(float value)
	{
		Vector3 eulerRotation = transform.eulerAngles;
		eulerRotation.x = value;
		transform.eulerAngles = eulerRotation;
	}
}
