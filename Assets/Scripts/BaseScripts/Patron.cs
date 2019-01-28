using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patron : MonoBehaviour {

	[HideInInspector]
	public Animator anim;
	Rigidbody2D rb;

	void Start () {
		anim = GetComponent<Animator> ();
		rb = GetComponent<Rigidbody2D> ();
		StartCoroutine ("Timer");
	}

    [HideInInspector]
    public int input;
    [SerializeField]
    protected float moveSpeed;

	void Update () {
		rb.velocity = new Vector2 (input * moveSpeed, rb.velocity.y);
	}

	void OnTriggerEnter2D (Collider2D target) {
		TrueHit (target);
	}
		
    [SerializeField]
    protected float attackPoints = 10f;
	public virtual void TrueHit (Collider2D target) {
		if (target.CompareTag ("Enemy")) {
			target.GetComponent<Damage> ().DefaultDamage(attackPoints, input);
			Destroy (gameObject);
		}
	}

	public virtual void SetDirection (int direction) {
		input = direction;
		if (direction < 0f) {
			FlipObject();
		}
	}

    [SerializeField]
	float lifeTime = 3f;
	public virtual IEnumerator Timer() {
		yield return new WaitForSeconds (lifeTime);
		Destroy (gameObject);
	}

	private void FlipObject()
	{
		//получаем размеры персонажа
		Vector3 theScale = transform.localScale;
		//зеркально отражаем персонажа по оси Х
		theScale.x *= -1;
		//задаем новый размер персонажа, равный старому, но зеркально отраженный
		transform.localScale = theScale;
	}
}
