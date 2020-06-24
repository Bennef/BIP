using System;

[Serializable]
public class CompassionChoices
{
	private bool _didntStealFromPop;
	private bool _helpedZipEscape;
	private bool _didntSendCobotToDeath;
	private bool _savedMim;
	private bool _chosePop;

    public bool DidntStealFromPop { get => _didntStealFromPop; set => _didntStealFromPop = value; }
    public bool HelpedZipEscape { get => _helpedZipEscape; set => _helpedZipEscape = value; }
    public bool DidntSendCobotToDeath { get => _didntSendCobotToDeath; set => _didntSendCobotToDeath = value; }
    public bool SavedMim { get => _savedMim; set => _savedMim = value; }
    public bool ChosePop { get => _chosePop; set => _chosePop = value; }
}
