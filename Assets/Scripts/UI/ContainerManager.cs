using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ContainerManager : MonoBehaviour
{
    /// <summary>
    /// List of containers, with the first being the default.
    /// </summary>
    public List<GameObject> containers = new List<GameObject>();

    /// <summary>
    /// Defaults to first container in list.
    /// </summary>
    private GameObject currentActiveContainer;

    void Start()
    {
        if(containers.Count > 0)
        {
            ResetContainers();
        }
    }

    /// <summary>
    /// Disables all containers but the first in the list.
    /// </summary>
    private void ResetContainers()
    {
        for (int i = 0; i < containers.Count; i++)
        {
            if (i == 0)
            {
                containers[i].SetActive(true);
            }
            else
            {
                containers[i].SetActive(false);
            }
        }
        currentActiveContainer = containers[0];
    }

    /// <summary>
    /// Disables current container and activates the specified container
    /// </summary>
    /// <param name="containerIndex">Index of the container within the list.</param>
    public void SelectContainer(GameObject container)
    {
        if (containers.Contains(container))
        {
            currentActiveContainer.SetActive(false);
            currentActiveContainer = container;
            currentActiveContainer.SetActive(true);
        }
        else
        {
            Debug.LogError("The specified container to load does not exist in the list of containers");
        }
    }
}
