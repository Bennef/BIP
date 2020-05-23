using UnityEngine;
using System.Collections;

public class Test : MonoBehaviour 
{
	MeshRenderer[] children;
	int BurnoutID;

	// Use this for initialization
	void Start () 
	{
		BurnoutID = Shader.PropertyToID("_BurnOut");
		children = GetComponentsInChildren<MeshRenderer>();
	}
	 
	// Update is called once per frame
	void Update () 
	{
		if (Input.GetKeyDown(KeyCode.A))
		{
			BurnIn();
		}

		if (Input.GetKeyDown(KeyCode.S))
		{
			BurnOut();
		}
	}

	// Appear.
	public void BurnIn()
	{
		for(int i = 0; i < children.Length; i++)
		{
			children[i].material.SetFloat(BurnoutID, Mathf.Lerp(0, 2f, (Time.time) / 2));
		}
	}

	// Disappear.
	public void BurnOut()
	{
		for(int i = 0; i < children.Length; i++)
		{
			children[i].material.SetFloat(BurnoutID, Mathf.Lerp(2f, 0, (Time.time) / 2));
		}
	}
}
