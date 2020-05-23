using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class OverheadUI : MonoBehaviour 
{
    // ----------------------------------------------- Data members ----------------------------------------------
    public float AnimationSpeed;    // The speed of the grow/shrink.
    public Image image;             // The image GameObject that holds the sprite.
    // ----------------------------------------------- End Data members ------------------------------------------

    // --------------------------------------------------- Methods -----------------------------------------------
    // --------------------------------------------------------------------
    public IEnumerator Grow()
    {
        float growth = 0;
        while (transform.localScale.x < 1f)
        {
            growth += Time.deltaTime * AnimationSpeed;
            growth = Mathf.Clamp(growth, 0, 1);
            transform.localScale = new Vector3(growth, growth, growth);
            yield return null;
        }
        transform.localScale = new Vector3(1, 1, 1);
        yield return null;
    }
    // --------------------------------------------------------------------
    public IEnumerator Shrink()
    {
        float growth = 1;
        while (transform.localScale.x > 0f)
        {
            growth -= Time.deltaTime * AnimationSpeed;
            growth = Mathf.Clamp(growth, 0, 1);
            transform.localScale = new Vector3(growth, growth, growth); 
            yield return null;
        }
        ShrinkImmediately();
        yield return null;
    }
    // --------------------------------------------------------------------
	public void ShrinkImmediately()
	{
		transform.localScale = Vector3.zero;
	}
	// --------------------------------------------------------------------
    public void ShowUI(Sprite sprite, float displayTime)
    {
        image.sprite = sprite;
        StartCoroutine(CoShowTemporaryUI(image, displayTime));
    }
    // --------------------------------------------------------------------
    public IEnumerator CoShowTemporaryUI(Image image, float displayTime)
    {
        if (transform.localScale.x == 0f)
        {
            StartCoroutine(Grow());
            yield return new WaitForSeconds(displayTime);
            StartCoroutine(Shrink());
        }
    }
    // --------------------------------------------------------------------
    // --------------------------------------------------- End Methods --------------------------------------------
}
