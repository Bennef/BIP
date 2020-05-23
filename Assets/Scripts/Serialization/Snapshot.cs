using System;

[Serializable]
public class Snapshot
{
	// ----------------------------------------------- Data members ----------------------------------------------
	public PowerUps powerUps;
	public CompassionChoices compChoices;
    public SerializedVector3 currentCheckpointPos;

	public string Name;
	public string SceneName;
	// ----------------------------------------------- End Data members ------------------------------------------

	// --------------------------------------------------- Methods -----------------------------------------------
	// --------------------------------------------------------------------
	public Snapshot()
	{
		powerUps = new PowerUps();
		compChoices = new CompassionChoices();
		Name = "Save0";
		currentCheckpointPos = new SerializedVector3(0,0,0);
	}
	// --------------------------------------------------------------------
	// --------------------------------------------------- End Methods --------------------------------------------
}
