using UnityEngine;
using System.Collections;

public class Health : MonoBehaviour 
{	
	// Manages health.
	public float value;			// The amount of health left before death.
	public float maxValue;		// The maximum health of the thing.
	public bool isInvincible;	// True if thing is invincible.
		
	// Use this for initialization.
	void Start() 
	{	
		value = 255;
		maxValue = 255;
	}
	
	// Deals damage to the thing.
	public virtual void TakeDamage(float _value)
	{
		if (isInvincible) 
			return;
		Invincible();
		this.value -= _value;
		this.value = Mathf.Clamp (value, 0, maxValue);
	}
	
	// Increase the health of the thing.
	public void IncreaseValue(float _value)
	{
		this.value += _value;
		this.value = Mathf.Clamp (value, 0, maxValue);
	} 
	
	public void SetHealth(float _value)
	{
		this.value = _value;
		this.value = Mathf.Clamp (value, 0, maxValue);
	}
	
	protected void Invincible() => StartCoroutine(InvincibleCo());

	// Make Bip invincible for a short time after getting hit.
	IEnumerator InvincibleCo()
	{
		isInvincible = true;
		yield return new WaitForSeconds(0.2f);
		isInvincible = false; 
	}
	
	public void Reset() => value = maxValue;
}