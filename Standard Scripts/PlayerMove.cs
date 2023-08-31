using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
	
	[SerializeField] private string horizontalInputName;
	[SerializeField] private string verticalInputName;
	[SerializeField] private float movementSpeed;
	[SerializeField] float crouchHeight = 0.6f;
	[SerializeField] float normalHeight = 0.9f;
	
	
	private CharacterController charController;
	
	[SerializeField] private AnimationCurve jumpFallOff;
	[SerializeField] private float jumpMultiplier;
	[SerializeField] private KeyCode jumpKey;
	private bool isJumping;
	
	private void Awake()
	{
		charController = GetComponent<CharacterController>();
	}
    // Start is called before the first frame update
    void Start()
    {
            Cursor.visible = false;
    		Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
		        Vector3 newScale = new Vector3(transform.localScale.x, normalHeight, transform.localScale.z);

        if (Input.GetKey(KeyCode.LeftControl)) {
			movementSpeed = 4;
            newScale.y = crouchHeight;
        }

       transform.localScale = newScale;
        PlayerMovement();
    }
	
	public void PlayerMovement()
	{
		float horizInput = Input.GetAxis(horizontalInputName) * movementSpeed;
		float vertInput = Input.GetAxis(verticalInputName) * movementSpeed;
		
		Vector3 forwardMovement = transform.forward * vertInput;
		Vector3 rightMovement = transform.right * horizInput;
		
		
		charController.SimpleMove(forwardMovement + rightMovement);
		if (Input.GetKey(KeyCode.LeftShift)){
			movementSpeed = 10;
		}
		else{
			movementSpeed = 6;
		}
		
		JumpInput();
	}
	
	
	private void JumpInput()
	{
		if(Input.GetKeyDown(jumpKey) && !isJumping){
			
			isJumping = true;
			StartCoroutine(JumpEvent());
		}
	}
	
	private IEnumerator JumpEvent(){
		
		charController.slopeLimit = 90.0f;
		
		float timeInAir = 0.0f;
		
		do{
			
			float jumpForce = jumpFallOff.Evaluate(timeInAir);
			charController.Move(Vector3.up * jumpForce * jumpMultiplier * Time.deltaTime);
			timeInAir += Time.deltaTime;
			
			yield return null;
		} while(!charController.isGrounded && charController.collisionFlags != CollisionFlags.Above);
		
		charController.slopeLimit = 45.0f;
		isJumping = false;
		
	}
}
