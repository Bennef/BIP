using UnityEngine;
using System.Collections;
using System;
using System.Xml.Serialization;

[Serializable]
public class SpokenLine
{
    // ----------------------------------------------- Data members ----------------------------------------------
    public string Speaker;                  // Who is saying this line?
    public string Text;                     // The text for the subtitle.
    public string AudioFileName;            // Filename of the Audio File to be played.

    [NonSerialized, XmlIgnore]
    public bool hasSpoken;                  // Has this line been said? Not serialized, used by Characters.
    [NonSerialized, XmlIgnore]
    public AudioClip audioClip;             // AudioClip itself. Not serializable, loaded at runtime based on file name.
    // ----------------------------------------------- End Data members ------------------------------------------

    // --------------------------------------------------- Methods -----------------------------------------------
    // --------------------------------------------------------------------
    // For serialization, all objects must have default, parameterless constructors.
    public SpokenLine()
    {

    }
    // --------------------------------------------------------------------
    // However, actually adding elements to the file programmatically requires a way of assigning the values.
    public SpokenLine(string speaker, string text, string audioFileName)
    {
        Speaker = speaker;
        Text = text;
        AudioFileName = audioFileName;
        audioClip = Resources.Load<AudioClip>("Audio/" + AudioFileName) as AudioClip;
    }
    // --------------------------------------------------------------------
    // --------------------------------------------------- End Methods --------------------------------------------
}
