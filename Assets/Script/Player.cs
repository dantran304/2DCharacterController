using UnityEngine;
using System.Collections;


public class Player : MonoBehaviour
{

	float speed = 9f;

	Rigidbody2D rigid;
	Animator anim;
	Vector3 localscale;

	bool facingRight = true;

	void Start ()
	{
		rigid = GetComponent<Rigidbody2D> ();
		anim = GetComponent<Animator> ();
	}
		
	// Update is called once per frame
	void FixedUpdate ()
	{
		// Move (hInput);
	}

	#region moving

	float hInput = 0;

	void Move (float horizontalInput)
	{
		Vector2 moveVector = rigid.velocity;
		moveVector.x = horizontalInput * speed;

		anim.SetFloat ("speed", Mathf.Abs (moveVector.x));

		rigid.velocity = moveVector;

		if (moveVector.x > 0 && !facingRight) {
			Flip ();
		} else if (moveVector.x < 0 && facingRight) {
			Flip ();
		}
	}


	public void StartMoving (float horizontalInput)
	{
		hInput = horizontalInput;
		Move (hInput);
	}





	void Flip ()
	{
		facingRight = !facingRight;
		localscale = transform.localScale;
		localscale.x *= -1;
		transform.localScale = localscale;
	}

	#endregion

	#region combo attack

	int comboCount = 0;
	//đếm chỉ số combo hiện tại
	bool isCombo;
	//biến bool xác định player có thực hiện combo attack hay chỉ đánh đơn
	bool isNext = true;
	//biến bool xác định player có đánh tiếp hay ko

	public void Attack ()
	{
		
		//Debug.Log("combo count = " + comboCount);
		comboCount++;
		if (comboCount > 3) {
			comboCount = 1;
		}
		//Debug.Log("combo : " + comboCount);

		switch (comboCount) {
		case 1:
			StopCoroutine ("SetComboTrue");
			StopCoroutine ("SetComboFalse");
			isNext = false;
			anim.Play ("Atk1");
				//Debug.Log ("Combo 1");
			StartCoroutine ("SetComboFalse");
			break;

		case 2:
			StopCoroutine ("SetComboTrue");
			StopCoroutine ("SetComboFalse");
			isNext = false;
			anim.Play ("Atk2");
				//Debug.Log ("Combo 2");
			StartCoroutine ("SetComboFalse");
			break;

		case 3:
			StopCoroutine ("SetComboTrue");
			StopCoroutine ("SetComboFalse");
			isNext = false;
			anim.Play ("Atk3");
				//Debug.Log ("Combo 3");
			StartCoroutine ("SetComboFalse");
			break;

		default:
			break;
		}
		StopCoroutine ("ResetComboCount");

		StartCoroutine ("ResetComboCount");
	}

	
	IEnumerator SetComboTrue ()
	{
		yield return new WaitForSeconds (1f);
		isCombo = true;
	}

	IEnumerator SetComboFalse ()
	{
		yield return new WaitForSeconds (0.4f);
		isNext = true;
		isCombo = false;
	}

	IEnumerator ResetComboCount ()
	{
		//if (isCombo == false)
		//{
		yield return new WaitForSeconds (0.9f);
		comboCount = 0;
		isNext = true;
		anim.Play ("Idle");
		//Debug.Log("Reset Combo count!!");
		//}
	}

	#endregion



}
