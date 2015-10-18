using UnityEngine;
using System.Collections.Generic;

public class SelectorScript : MonoBehaviour {

    private List<GameObject> selectedAgents;
    private GameObject selected;

	// Use this for initialization
	void Start ()
    {
        selectedAgents = new List<GameObject>();
        System.Console.WriteLine("Starting");
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit))
        {
            System.Console.WriteLine("Hit of type:" + hit.transform.tag.ToString());
            if (hit.transform.tag == "Agents" && Input.GetMouseButtonDown(0))
            {
                selected = hit.transform.gameObject;
                if (selectedAgents.Contains(selected))
                {
                    selected.SendMessage("Selected", false);
                    selectedAgents.Remove(selected);
                }
                else
                {
                    selected.SendMessage("Selected", true);
                    selectedAgents.Add(selected);
                }
            }   

            if (hit.transform.tag == "Ground")
            {
                if (Input.GetMouseButtonDown(0))
                {
                    foreach (GameObject i in selectedAgents)
                    {
                        i.SendMessage("Selected", false);
                    }
                    selectedAgents.Clear();
                }

                else if (Input.GetMouseButtonDown(1))
                {
                    foreach (GameObject i in selectedAgents)
                    {
                        i.SendMessage("SetDestination", hit.point);
                    }
                }
            }
        }
    }
}
