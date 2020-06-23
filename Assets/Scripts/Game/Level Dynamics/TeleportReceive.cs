using System.Collections;
using UnityEngine;

public class TeleportReceive : MonoBehaviour
{
    public bool shouldFade, startPad;
    public PlayerMovement playerMovement;
    public Animator anim;
    public Transform bip, telePos, sparkPos, camPos;
    public AudioSource aSrc1, aSrc2;
    Rigidbody bipRigidbody;

    public SkinnedMeshRenderer bipMesh;
    public Material burnoutMat, faceMat, bodyMat;
    int BurnoutID;
    private GameObject fader;
    public CameraMovement mainCam;
    
    private void Start()
    {
        aSrc1 = GetComponent<AudioSource>();
        bipRigidbody = bip.GetComponent<Rigidbody>();
        BurnoutID = Shader.PropertyToID("_BurnOut");
        bipMesh = GameObject.Find("Bip Mesh").GetComponent<SkinnedMeshRenderer>();
        fader = GameObject.Find("Fader");
        if (startPad)
            StartCoroutine(TeleportIn());
    }
    
    public IEnumerator MoveBip()
    {
        float elapsedTime = 0;
        Vector3 startingPos = bip.transform.position;
        while (elapsedTime < 2f)
        {
            bip.transform.position = Vector3.Lerp(startingPos, telePos.position, Mathf.SmoothStep(0.0f, 1.0f, Mathf.SmoothStep(0.0f, 1.0f, elapsedTime)));
            elapsedTime += Time.deltaTime / 2f;
            elapsedTime += Time.deltaTime;
            yield return null;
        }
    }
    
    public void TeleportInCall() => StartCoroutine(TeleportIn());
    
    public IEnumerator TeleportIn()
    {
        mainCam.transform.position = camPos.position;
        mainCam.transform.LookAt(mainCam.target.transform.position);
        mainCam.isFixed = true;
        if (shouldFade)
            fader.GetComponent<ScreenFader>().StartCoroutine("FadeToClear");  
        bipMesh.enabled = false; 
        StartCoroutine(MoveBip()); 
        yield return new WaitForSeconds(1f);
        bipRigidbody.velocity = new Vector3(0, 0, 0);
        bipRigidbody.isKinematic = true;
        bipMesh.enabled = true;
        playerMovement.isHandlingInput = false;
        bipRigidbody.velocity = new Vector3(0, 0, 0);
        bipRigidbody.isKinematic = true;
        bip.transform.position = telePos.position;
        SwapMats();
        foreach (Material mat in bipMesh.materials)
            StartCoroutine(BurnIn(mat));
        GameObject spark = (GameObject)Instantiate(Resources.Load("solo_gun_flames"));
        spark.transform.position = sparkPos.position;
        yield return new WaitForSeconds(1.6f);
        aSrc1.Play();
        GameObject spark7 = (GameObject)Instantiate(Resources.Load("plasma_gun_flare"));
        spark7.transform.position = sparkPos.position;
        yield return new WaitForSeconds(1.3f);
        aSrc2.Play();
        yield return new WaitForSeconds(0.1f);
        GameObject spark0 = (GameObject)Instantiate(Resources.Load("solo_gun_flames"));
        spark0.transform.position = sparkPos.position;
        GameObject spark2 = (GameObject)Instantiate(Resources.Load("plasma_gun_flare"));
        spark2.transform.position = sparkPos.position;
        GameObject spark5 = (GameObject)Instantiate(Resources.Load("Lightning Spark"));
        spark5.transform.position = sparkPos.position;
        GameObject spark6 = (GameObject)Instantiate(Resources.Load("Teleport Sparks"));
        spark6.transform.position = sparkPos.position;
        yield return new WaitForSeconds(0.4f);
        GameObject plasma = (GameObject)Instantiate(Resources.Load("PlasmaExplosionEffect"));
        plasma.transform.position = sparkPos.position;
        GameObject sparks = (GameObject)Instantiate(Resources.Load("Teleport Sparks"));
        sparks.transform.position = sparkPos.position;
        GameObject spark3 = (GameObject)Instantiate(Resources.Load("Lightning Spark"));
        spark3.transform.position = sparkPos.position;
        SwapMatsBack();  
        yield return new WaitForSeconds(1f);
        playerMovement.isHandlingInput = true;
        bipRigidbody.isKinematic = false;
        mainCam.isFixed = false;
    }
    
    public void SwapMats()
    {
        Material[] intMaterials = new Material[bipMesh.materials.Length];
        for (int i = 0; i < intMaterials.Length; i++)
            intMaterials[i] = burnoutMat;
        bipMesh.materials = intMaterials;
    }
    
    public void SwapMatsBack()
    {
        Material[] intMaterials = new Material[bipMesh.materials.Length];
        for (int i = 0; i < intMaterials.Length; i++)
        {
            if (i == 0)
                intMaterials[i] = bodyMat;
            else
                intMaterials[i] = faceMat;
        }
        bipMesh.materials = intMaterials;
    }
    
    public IEnumerator BurnIn(Material mat)
    {
        float elapsedTime = 0;
        float time = 3f;
        while (elapsedTime < time)
        {
            mat.SetFloat(BurnoutID, Mathf.Lerp(1f, 0, elapsedTime / time));
            elapsedTime += Time.deltaTime;
            yield return null;
        }
    }
}