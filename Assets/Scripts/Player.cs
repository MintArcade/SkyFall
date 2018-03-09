using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

	[Range(-1,1)]
	public float horizontal;
	[Range(0,1)]
	public float vertical;

	public GameManager gameManager;

	public float horizontalForce, verticalForce;
	public float speed,hp;
	public GameObject toucanModel, toucanFeathers;
	
	public Animator animatorModel, animatorToucan;
	public float horizontalLerp=0.9f, verticalLerp;

	Vector2 vectorTouch;
	float _h,_v;
	Rigidbody rb;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {

		float dt = Time.deltaTime;

		animatorModel.SetFloat("Steer", horizontal);
		animatorModel.SetFloat("Up", vertical);
		speed = rb.velocity.magnitude;
		animatorToucan.speed = Mathf.Clamp(speed, 0, 1) * vertical * 5 + 1;

		if (transform.position.y < 20)
		{
			_v = Input.GetAxis("Vertical");
		}
		_h = Input.GetAxis("Horizontal");
//#if UNITY_IOS || UNITY_ANDORID
		if (Input.touchCount > 0)
		{
			_h = 0;
			_v = 0;
			for (int i = 0; i < Input.touchCount; i++)
			{

				Touch touch = Input.touches[i];
				vectorTouch.x = touch.position.x;
				vectorTouch.y = touch.position.y;

				if (touch.phase == TouchPhase.Ended)
				{
					_h = 0;
					_v = 0;
				}
				else
				{
					if (touch.position.x / Screen.width < 0.33f)
					{
						_h = _h - 1;
					}
					if (touch.position.x / Screen.width > 0.66f)
					{
						_h = _h + 1;
					}
					if (touch.position.x / Screen.width > 0.33f && touch.position.x / Screen.width < 0.66f)
					{
						_v = _v + 1;
					}
				}
			}
			_h = Mathf.Clamp(_h, -1, 1);
			_v = Mathf.Clamp(_v, 0, 1);
		}


		//#endif

		horizontal = Mathf.Lerp(horizontal, _h, horizontalLerp * dt);
		vertical = Mathf.Lerp(vertical, _v, verticalLerp * dt);

		vertical = Mathf.Clamp(vertical, 0, 1);

		if (transform.position.y>15)
		{
			vertical = Mathf.Lerp(vertical, -1, verticalLerp * dt * 1f);
		}

		if (transform.position.y > 20)
		{
			vertical = Mathf.Lerp(vertical, 0, verticalLerp * dt * 4f);
		}

	}

	private void FixedUpdate()
	{
		rb.AddForce(new Vector3(
			horizontal* horizontalForce,
			vertical*verticalForce,
			0
			),ForceMode.Force);
	}

	private void OnCollisionEnter(Collision collision)
	{
		hp = hp - rb.velocity.magnitude;
		if (hp<0)
		{
			toucanModel.SetActive(false);
			toucanFeathers.SetActive(true);
			gameManager.GameOver();
		} else
		{

		}
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Ring")
		{
			gameManager.AddToScore(1);
			if (other.GetComponent<Animator>())
			{
				other.GetComponent<Animator>().SetTrigger("Collect");
			}
			if (other.GetComponent<AudioSource>())
			{
				other.GetComponent<AudioSource>().Play();
			}
		}
	}
}
