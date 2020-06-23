using UnityEngine;

public class PadActivatesPlatform : MonoBehaviour
{
    public DoTweenTest[] DoTweenPlatforms;   // The platforms we want to move.
    public FollowPath[] MovingPlatforms;   // The platforms we want to move.
    public PressableSwitch pressurePad; // The pad Bip needs to stand on to activate the platforms.
    private bool complete;
    
    private void Update()
    {
        if (pressurePad.hasBeenPressed && !complete)
        {
            foreach (DoTweenTest platform in DoTweenPlatforms)
                platform.isOn = true;
            foreach(FollowPath platform in MovingPlatforms)
                platform.isActive = true;
            complete = true;
        }
    }
}