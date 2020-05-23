using UnityEngine;

public class ElectricalDischarge : MonoBehaviour
{
    // ----------------------------------------------- Data members ----------------------------------------------
    public int numberOfElecs;
    public float spawnTime;
    public GameObject[] elecs;
    // ----------------------------------------------- End Data members ------------------------------------------

    // --------------------------------------------------- Methods -----------------------------------------------
    // --------------------------------------------------------------------
    // Use this for initialization
    void Start()
    {
        SpawnElec();
    }
    // --------------------------------------------------------------------
    public void SpawnElec()
    {
        for (int i = 0; i < numberOfElecs; i++)
        {
            GameObject elec = (GameObject)Instantiate(Resources.Load("Electricity"));
            Vector3 spawnPosition = Random.onUnitSphere * ((this.transform.localScale.x) * 0.5f) + this.transform.position;
            elec.transform.position = spawnPosition;
            elec.transform.localScale = new Vector3(1f, 1f, 1f);
            elec.transform.rotation = Quaternion.LookRotation(elec.transform.position - this.transform.position);
        }
    }
    // --------------------------------------------------------------------
    public void HideElec()
    {
        elecs = GameObject.FindGameObjectsWithTag("Electrical");

        foreach (GameObject elec in elecs)
        {
            elec.SetActive(false);
        }
    }
    // --------------------------------------------------------------------
    // --------------------------------------------------- End Methods --------------------------------------------
}
