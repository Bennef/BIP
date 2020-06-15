// Cinema Suite
using UnityEngine;
using UnityEngine.UI;

namespace CinemaDirector
{
    /// <summary>
    /// Transition from Clear to Black over time by overlaying a image.
    /// </summary>
    [CutsceneItem("Transitions", "Fade to Black", CutsceneItemGenre.GlobalItem)]
    public class FadeToBlack : CinemaGlobalAction
    {
        private Color From = Color.clear;
        private Color To = Color.black;

        /// <summary>
        /// Setup the effect when the script is loaded.
        /// </summary>
        void Awake()
        {
            Image image = gameObject.GetComponent<Image>();
            if (image == null)
            {
                image = gameObject.AddComponent<Image>();
                gameObject.transform.position = Vector3.zero;
                gameObject.transform.localScale = new Vector3(100, 100, 100);
                //image.texture = new Texture2D(1, 1);
                image.enabled = false;
                //image.pixelInset = new Rect(0f, 0f, Screen.width, Screen.height);
                image.color = Color.clear;
            }
        }

        /// <summary>
        /// Enable the overlay texture and set the Color to Clear.
        /// </summary>
        public override void Trigger()
        {
            Image image = gameObject.GetComponent<Image>();
            if (image != null)
            {
                image.enabled = true;
               // image.pixelInset = new Rect(0f, 0f, Screen.width, Screen.height);
                image.color = From;
            }
        }

        /// <summary>
        /// Firetime is reached when playing in reverse, disable the effect.
        /// </summary>
        public override void ReverseTrigger()
        {
            End();
        }

        /// <summary>
        /// Update the effect over time, progressing the transition
        /// </summary>
        /// <param name="time">The time this action has been active</param>
        /// <param name="deltaTime">The time since the last update</param>
        public override void UpdateTime(float time, float deltaTime)
        {
            float transition = time / Duration;
            FadeToColor(From, To, transition);
        }

        /// <summary>
        /// Set the transition to an arbitrary time.
        /// </summary>
        /// <param name="time">The time of this action</param>
        /// <param name="deltaTime">the deltaTime since the last update call.</param>
        public override void SetTime(float time, float deltaTime)
        {
            Image image = gameObject.GetComponent<Image>();
            if (image != null)
            {
                if (time >= 0 && time <= Duration)
                {
                    image.enabled = true;
                    UpdateTime(time, deltaTime);
                }
                else if (image.enabled)
                {
                    image.enabled = false;
                }
            }
        }

        /// <summary>
        /// End the effect by disabling the overlay texture.
        /// </summary>
        public override void End()
        {
            Image image = gameObject.GetComponent<Image>();
            if (image != null)
            {
                image.enabled = false;
            }
        }

        /// <summary>
        /// The end of the action has been triggered while playing the Cutscene in reverse.
        /// </summary>
        public override void ReverseEnd()
        {
            Image image = gameObject.GetComponent<Image>();
            if (image != null)
            {
                image.enabled = true;
                //image.pixelInset = new Rect(0f, 0f, Screen.width, Screen.height);
                image.color = To;
            }
        }

        /// <summary>
        /// Disable the overlay texture
        /// </summary>
        public override void Stop()
        {
            Image image = gameObject.GetComponent<Image>();
            if (image != null)
            {
                image.enabled = false;
            }
        }

        /// <summary>
        /// Fade from one colour to another over a transition period.
        /// </summary>
        /// <param name="from">The starting colour</param>
        /// <param name="to">The final colour</param>
        /// <param name="transition">the Lerp transition value</param>
        private void FadeToColor(Color from, Color to, float transition)
        {
            Image image = gameObject.GetComponent<Image>();
            if (image != null)
            {
                image.color = Color.Lerp(from, to, transition);
            }
        }

    }
}