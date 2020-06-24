using UnityEngine;
using System.Collections;

namespace Scripts.Game.Level_Dynamics
{
    [RequireComponent(typeof(LineRenderer))]

    public class RaycastReflection : MonoBehaviour
    {
        public bool isOn, prismHit, receiverAHit, receiverBHit;
        public float updateFrequency = 3f;
        public int laserDistance;
        public int maxBounce;
        private float timer = 0;
        private LineRenderer mLineRenderer;
        public LayerMask layerMask;
        public bool hasHit;
        public RaycastReflection violet, indigo, blue, green, yellow, orange, red;
        public Light prismLight;

        // Use this for initialization
        void Start()
        {
            timer = 0;
            mLineRenderer = gameObject.GetComponent<LineRenderer>();
        }

        // Update is called once per frame
        void Update()
        {
            if (isOn == true)
            {
                mLineRenderer.enabled = true;
                if (timer >= updateFrequency)
                {
                    timer = 0;
                    StartCoroutine(RedrawLaser());
                    if (transform.name == "Light Beam")
                        CheckPrismHit();
                }
                timer += Time.deltaTime;
            }
            else
            {
                mLineRenderer.enabled = false;
                prismHit = false;
            }
        }

        IEnumerator RedrawLaser()
        {
            int laserReflected = 1; //How many times it got reflected
            int vertexCounter = 1; //How many line segments are there
            bool loopActive = true; //Is the reflecting loop active?

            Vector3 laserDirection = transform.forward; //direction of the next laser
            Vector3 lastLaserPosition = transform.localPosition; //origin of the next laser

            mLineRenderer.positionCount = 1;
            mLineRenderer.SetPosition(0, transform.position);

            while (loopActive)
            {
                prismHit = false;
                receiverAHit = false;
                receiverBHit = false;

                // If beam hits anything...
                if (Physics.Raycast(lastLaserPosition, laserDirection, out RaycastHit hit, laserDistance, layerMask))
                {
                    if (hit.collider.transform.CompareTag("Mirror"))  // Reflect the beam off mirror.
                    {
                        laserReflected++;
                        vertexCounter += 3;
                        mLineRenderer.positionCount = vertexCounter;
                        mLineRenderer.SetPosition(vertexCounter - 3, Vector3.MoveTowards(hit.point, lastLaserPosition, 0.01f));
                        mLineRenderer.SetPosition(vertexCounter - 2, hit.point);
                        mLineRenderer.SetPosition(vertexCounter - 1, hit.point);
                        lastLaserPosition = hit.point;
                        Vector3 prevDirection = laserDirection;
                        laserDirection = Vector3.Reflect(laserDirection, hit.normal);
                    }
                    else if (hit.transform.CompareTag("Prism") && transform.name == "Light Beam")  // If original white beam hits prism.
                    {
                        laserReflected++;
                        vertexCounter += 3;
                        mLineRenderer.positionCount = vertexCounter;
                        mLineRenderer.SetPosition(vertexCounter - 3, Vector3.MoveTowards(hit.point, lastLaserPosition, 0.01f));
                        mLineRenderer.SetPosition(vertexCounter - 2, hit.point);
                        mLineRenderer.SetPosition(vertexCounter - 1, hit.point);
                        lastLaserPosition = hit.point;
                        Vector3 prevDirection = laserDirection;
                        prismHit = true;
                        loopActive = false;
                    }
                    else if (hit.transform.name == "Unlit Bulb A")  // Beam hits beam receiver.
                    {
                        vertexCounter += 2;
                        mLineRenderer.positionCount = vertexCounter;
                        mLineRenderer.SetPosition(vertexCounter - 2, Vector3.MoveTowards(hit.point, lastLaserPosition, 0.01f));
                        mLineRenderer.SetPosition(vertexCounter - 1, hit.point);
                        lastLaserPosition = hit.point;
                        Vector3 prevDirection = laserDirection;
                        receiverAHit = true;
                        loopActive = false;
                    }
                    else if (hit.transform.name == "Unlit Bulb B")  // Beam hits beam receiver.
                    {
                        vertexCounter += 2;
                        mLineRenderer.positionCount = vertexCounter;
                        mLineRenderer.SetPosition(vertexCounter - 2, Vector3.MoveTowards(hit.point, lastLaserPosition, 0.01f));
                        mLineRenderer.SetPosition(vertexCounter - 1, hit.point);
                        lastLaserPosition = hit.point;
                        Vector3 prevDirection = laserDirection;
                        receiverBHit = true;
                        loopActive = false;
                    }
                    else  // Block the beam.
                    {
                        vertexCounter += 2;
                        mLineRenderer.positionCount = vertexCounter;
                        mLineRenderer.SetPosition(vertexCounter - 2, Vector3.MoveTowards(hit.point, lastLaserPosition, 0.01f));
                        mLineRenderer.SetPosition(vertexCounter - 1, hit.point);
                        lastLaserPosition = hit.point;
                        Vector3 prevDirection = laserDirection;
                        loopActive = false;
                    }
                }
                if (laserReflected > maxBounce)
                    loopActive = false;
            }
            yield return new WaitForEndOfFrame();
        }

        public void ActivatePrism()
        {
            if (red.isOn == false)
            {
                violet.isOn = true;
                indigo.isOn = true;
                blue.isOn = true;
                green.isOn = true;
                yellow.isOn = true;
                orange.isOn = true;
                red.isOn = true;
                prismLight.enabled = true;
            }
        }

        public void DeActivatePrism()
        {
            if (red.isOn == true)
            {
                violet.isOn = false;
                indigo.isOn = false;
                blue.isOn = false;
                green.isOn = false;
                yellow.isOn = false;
                orange.isOn = false;
                red.isOn = false;
                prismLight.enabled = false;
            }
        }

        public void CheckPrismHit()
        {
            if (prismHit == true)
                ActivatePrism();
            else
                DeActivatePrism();
        }
    }
}