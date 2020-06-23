using System.Collections;
using UnityEngine;

public class TeleportSend : MonoBehaviour
{
    public TeleportReceive teleportToGoTo;
    public bool hasBeenEntered, changeScene, demo, isOn, activateRoom, fadeOutMusic;
    public PlayerMovement playerMovement;
    public Animator anim;
    public Transform bip, telePos, sparkPos, destinationPos, newCamPos;
    public AudioSource aSrc1, aSrc2, audioToFadeOut;
    Rigidbody bipRigidbody;

    public SkinnedMeshRenderer bipMesh;
    public Material burnoutMat;
    int BurnoutID;
    public GameObject fader, roomToActivate, roomToDeactivate, roomToActivate2, roomToActivate3, roomToDeActivate2, roomToDeActivate3;
    public CameraMovement mainCam;
    
    private void Start()
    {
        aSrc1 = GetComponent<AudioSource>();
        bipRigidbody = bip.GetComponent<Rigidbody>();
        BurnoutID = Shader.PropertyToID("_BurnOut");
        bipMesh = GameObject.Find("Bip Mesh").GetComponent<SkinnedMeshRenderer>();
        fader = GameObject.Find("Fader");
        playerMovement = bip.GetComponent<PlayerMovement>();
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !hasBeenEntered && isOn)
        {
            hasBeenEntered = true;
            StartCoroutine(Teleport());
        }
    }
    
    public IEnumerator Teleport()
    {
        if (fadeOutMusic)
            StartCoroutine(AudioFadeOut.FadeOut(audioToFadeOut, 4.0f));
        playerMovement.isHandlingInput = false;
        mainCam.IsFixed = true;
        bipRigidbody.velocity = new Vector3(0, 0, 0);
        bipRigidbody.isKinematic = true;
        StartCoroutine(MoveBip());
        yield return new WaitForSeconds(1f);
        aSrc1.Play();
        aSrc2.Play();
        PlayParticlesA();
        yield return new WaitForSeconds(0.5f);
        PlayParticlesB();
        SwapMats();
        foreach (Material mat in bipMesh.materials)
            StartCoroutine(BurnOut(mat));
        yield return new WaitForSeconds(2.5f);
        bipMesh.enabled = false;
        fader.GetComponent<ScreenFader>().StartCoroutine("FadeToBlack");  // Fade to black.
        yield return new WaitForSeconds(2f);
        if (changeScene)
        {
            if (demo)
                LocalSceneManager.Instance.LoadScene("Main Menu Web");  //Load DEMO END
            else
                LocalSceneManager.Instance.LoadScene("2 Vent Lab");  //Load Pop cutscene in Act 2 - Room 13.
        }
        else
        {
            TransportBip();
            if (activateRoom)
            {
                roomToActivate.SetActive(true);
                if (roomToActivate2 != null)
                    roomToActivate2.SetActive(true);
                if (roomToActivate3 != null)
                    roomToActivate3.SetActive(true);
                if (roomToDeactivate != null)
                    roomToDeactivate.SetActive(false);
                if (roomToDeactivate != null)
                    roomToDeActivate2.SetActive(false);
                if (roomToDeActivate3 != null)
                    roomToDeActivate3.SetActive(false);
            }
        }
    }
    
    public void PlayParticlesA()
    {
        GameObject spark = (GameObject)Instantiate(Resources.Load("solo_gun_flames"));
        spark.transform.position = sparkPos.position;
        GameObject spark2 = (GameObject)Instantiate(Resources.Load("plasma_gun_flare"));
        spark2.transform.position = sparkPos.position;
    }
    
    public void PlayParticlesB()
    {
        GameObject plasma = (GameObject)Instantiate(Resources.Load("PlasmaExplosionEffect"));
        plasma.transform.position = sparkPos.position;
        GameObject spark3 = (GameObject)Instantiate(Resources.Load("Lightning Spark"));
        spark3.transform.position = sparkPos.position;
    }
    
    public void SwapMats()
    {
        Material[] intMaterials = new Material[bipMesh.materials.Length];
        for (int i = 0; i < intMaterials.Length; i++)
            intMaterials[i] = burnoutMat;
        bipMesh.materials = intMaterials;
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
    
    public IEnumerator BurnOut(Material mat)
    {
        float elapsedTime = 0;
        float time = 3f;
        while (elapsedTime < time)
        {
            mat.SetFloat(BurnoutID, Mathf.Lerp(0, 1f, elapsedTime / time));
            elapsedTime += Time.deltaTime;
            yield return null;
        }
    }
    
    public void TransportBip()
    {
        bip.transform.position = destinationPos.position;
        mainCam.gameObject.transform.position = newCamPos.position;
        mainCam.gameObject.transform.LookAt(bip);
        teleportToGoTo.TeleportInCall();
        hasBeenEntered = false;
    }
}