using UnityEngine;

namespace Scripts.Game.Puzzles
{
    public class ElectricalDischarge : MonoBehaviour
    {
        public int numberOfElecs;
        public float spawnTime;
        public GameObject[] elecs;

        // Use this for initialization
        void Start() => SpawnElec();

        public void SpawnElec()
        {
            for (int i = 0; i < numberOfElecs; i++)
            {
                GameObject elec = (GameObject)Instantiate(Resources.Load("Electricity"));
                Vector3 spawnPosition = Random.onUnitSphere * (transform.localScale.x * 0.5f) + transform.position;
                elec.transform.position = spawnPosition;
                elec.transform.localScale = new Vector3(1f, 1f, 1f);
                elec.transform.rotation = Quaternion.LookRotation(elec.transform.position - transform.position);
            }
        }

        public void HideElec()
        {
            elecs = GameObject.FindGameObjectsWithTag("Electrical");

            foreach (GameObject elec in elecs)
                elec.SetActive(false);
        }
    }
}