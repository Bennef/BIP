using Scripts.Player;
using System;

namespace Scripts.Serialization
{
    [Serializable]
    public class Snapshot
    {
        public PowerUps powerUps;
        public CompassionChoices compChoices;
        public SerializedVector3 currentCheckpointPos;
        public string Name, SceneName;

        public Snapshot()
        {
            powerUps = new PowerUps();
            compChoices = new CompassionChoices();
            Name = "Save0";
            currentCheckpointPos = new SerializedVector3(0, 0, 0);
        }
    }
}