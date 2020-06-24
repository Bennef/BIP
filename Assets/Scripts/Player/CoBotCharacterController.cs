using Scripts.Game;
using Scripts.Game.Cameras;
using Scripts.Game.Game_Logic;
using Scripts.NPCs.AI;
using UnityEngine;

namespace Scripts.Player
{
    public class CoBotCharacterController : CharacterController
    {
        public Camera CoBotCam;             // A reference to CoBot's camera.
        public Camera MainCam;              // A reference to the main camera for Bip.
        public Transform aListener;         // A reference to the AudioListener.

        void OnEnable()
        {
            GetComponent<CoBotMind>().enabled = false;
            rb = GetComponent<Rigidbody>();
        }

        // If player is CoBot, be able to look around.
        void MouseLook(bool canLook) => GetComponent<SimpleSmoothMouseLook>().enabled = canLook;

        public override void HandleInput()
        {
            if (Input.GetButtonUp("Switch"))
                SwitchCharacter();
        }

        // Switch cameras.
        void SetCamera(Camera cam)
        {
            MainCam.enabled = !MainCam.enabled;
            CoBotCam.enabled = !CoBotCam.enabled;
            aListener.parent = cam.transform; // Set AudioListener to the active camera.
            aListener.localPosition = Vector3.zero;
            aListener.localRotation = Quaternion.identity;
        }

        void FixedUpdate()
        {
            GetComponent<Rigidbody>().velocity = new Vector3(0.0f, GetComponent<Rigidbody>().velocity.y, 0.0f);  // So he doesn't keep heading towards Bip.

            if (!CoBotCam.enabled)
                SetCamera(CoBotCam);

            if (!GameManager.Instance.IsPaused)
                MouseLook(true); // Enable looking around.
            else
                MouseLook(false); // Disable looking around.
        }

        public override void OnCharacterSwitch()
        {
            tag = Tags.Buddy; // Switch tag to "Buddy".
            if (!MainCam.enabled)
                SetCamera(MainCam);
            MouseLook(false);
            GetComponent<CoBotMind>().enabled = true;
            enabled = false;
        }
    }
}