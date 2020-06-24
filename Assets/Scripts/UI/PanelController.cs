using UnityEngine;
using System.Collections;
using System.IO;
using UnityEngine.UI;
using Scripts.UI;
using Scripts.Game.Game_Logic;

namespace Scripts.UI
{
    public class PanelController : MonoBehaviour
    {
        private Animator anim;
        public AudioSource aSrc;
        public GameObject save1btn, save2btn, save3btn, newGameBtn, deleteBtn, playBtn, emptySlot1, emptySlot2, emptySlot3;
        public AudioSource mainMusic;
        public ScreenFader screenFader;
        public GameObject bip;

        // Use this for initialization
        void Start()
        {
            anim = GetComponent<Animator>();
            StartCoroutine(InitialSlideIn());
            ManageSlots();

            Button btn1 = newGameBtn.GetComponent<Button>();
            btn1.onClick.AddListener(CreateNewGame);

            Button btn2 = deleteBtn.GetComponent<Button>();
            btn2.onClick.AddListener(DeleteSave);

            Button btn3 = playBtn.GetComponent<Button>();
            btn3.onClick.AddListener(LoadSavedGame);

            screenFader = GameObject.Find("Fader").GetComponent<ScreenFader>();
            bip = GameObject.Find("Bip");
            bip.transform.position = new Vector3(0f, 0.5f, 0f);
            bip.transform.rotation = Quaternion.Euler(0.0f, 180.0f, 0.0f);
        }
        public IEnumerator InitialSlideIn()
        {
            Cursor.visible = false;
            yield return new WaitForSeconds(4.5f);
            MoveMainPanelIn();
            aSrc.Play();
            Cursor.visible = true;
        }
        void CreateNewGame()
        {
            StartCoroutine(AudioFadeOut.FadeOut(mainMusic, 2f));
            screenFader.StartCoroutine(screenFader.FadeToBlack());
            GameManager.Instance.CreateNewGame();
        }

        void DeleteSave()
        {
            GameManager.Instance.DeleteSave(GameManager.Instance.save.Name);
            ManageSlots();
            MoveReallyDeletePanelOut();
            MovePlayPanelIn();
        }

        public void LoadSavedGame()
        {
            StartCoroutine(AudioFadeOut.FadeOut(mainMusic, 2f));
            StartCoroutine(LoadSavedGameCo());
        }

        public IEnumerator LoadSavedGameCo()
        {
            screenFader.StartCoroutine(screenFader.FadeToBlack());
            yield return new WaitForSeconds(2f);
            GameManager.Instance.Load(GameManager.Instance.save.Name);
        }

        public void ManageSlots()
        {
            if (File.Exists(Application.dataPath + "/Saves/Save1.bip"))
            {
                save1btn.SetActive(true);
                emptySlot1.SetActive(false);
            }
            else
            {
                save1btn.SetActive(false);
                emptySlot1.SetActive(true);
            }

            if (File.Exists(Application.dataPath + "/Saves/Save2.bip"))
            {
                save2btn.SetActive(true);
                emptySlot2.SetActive(false);
            }
            else
            {
                save2btn.SetActive(false);
                emptySlot2.SetActive(true);
            }

            if (File.Exists(Application.dataPath + "/Saves/Save3.bip"))
            {
                save3btn.SetActive(true);
                emptySlot3.SetActive(false);
            }
            else
            {
                save3btn.SetActive(false);
                emptySlot3.SetActive(true);
            }
        }

        public void ToggleCurosor(bool isVisible)
        {
            if (isVisible)
                Cursor.visible = true;
            else
                Cursor.visible = false;
        }

        public void MoveMainPanelIn()
        {
            anim.enabled = true;
            anim.Play("MainPanelSlideIn");
        }

        public void MoveMainPanelOut() => anim.Play("MainPanelSlideOut");

        public void MovePlayPanelIn() => anim.Play("PlayPanelSlideIn");

        public void MovePlayPanelOut() => anim.Play("PlayPanelSlideOut");

        public void MoveQuitPanelIn() => anim.Play("QuitPanelSlideIn");

        public void MoveQuitPanelOut() => anim.Play("QuitPanelSlideOut");

        public void MoveDeletePanelIn() => anim.Play("DeletePanelSlideIn");

        public void MoveDeletePanelOut() => anim.Play("DeletePanelSlideOut");

        public void MoveReallyDeletePanelIn() => anim.Play("ReallyDeletePanelSlideIn");

        public void MoveReallyDeletePanelOut() => anim.Play("ReallyDeletePanelSlideOut");

        public void MoveCreatePanelIn() => anim.Play("CreatePanelSlideIn");

        public void MoveCreatePanelOut() => anim.Play("CreatePanelSlideOut");
    }
}