using UnityEngine;

public class F3DBurnoutExample : MonoBehaviour 
{
    MeshRenderer[] children;
    int BurnoutID;
    
    // Use this for initialization
    void Start() 
	{
        BurnoutID = Shader.PropertyToID("_BurnOut");
		children = GetComponentsInChildren<MeshRenderer>();
	}
    
    // Update is called once per frame.
    void Update() => BurnOut();	// Currently just set to disappear....
    
    // Appear with a cool effect.
    public void BurnIn()
	{
		// For each child of the parent object...
		for (int i = 0; i < children.Length; i++)
		{
			// Go from invisible to visible.
			children[i].material.SetFloat(BurnoutID, Mathf.Lerp(2f, 0, (Time.time) / 2));

			// At this stage we may want to swap either the material of Bip for his normal one or 
			// instantiate a new Bip with normal material and make the burnin one burnout?
		}
	}
    
    // Disappear with a cool effect.
    public void BurnOut()
	{
		for (int i = 0; i < children.Length; i++)
			children[i].material.SetFloat(BurnoutID, Mathf.Lerp(0, 2f, (Time.time) / 2));
	}
}