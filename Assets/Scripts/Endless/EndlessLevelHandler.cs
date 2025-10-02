using System.Collections;
using UnityEngine;

public class EndlessLevelHandler : MonoBehaviour
{
    [SerializeField]
    GameObject[] sectionsPrefabs;
    
    GameObject[] sectionsPool = new GameObject[20];
    
    GameObject[] sections = new GameObject[10];

    [SerializeField] private Transform playerCarTransform;
    
    WaitForSeconds waitFor100ms = new WaitForSeconds(0.1f);

    // How long sections are (prefabs)
    private const float sectionLength = 100;
    void Start()
    {
        //playerCarTransform = GameObject.FindGameObjectWithTag("Player").transform;

        int prefabIndex = 0;

        // Creating a pool for endless sections
        for (int i = 0; i < sectionsPool.Length; i++)
        {
            sectionsPool[i] = Instantiate(sectionsPrefabs[prefabIndex]);
            sectionsPool[i].SetActive(false);
            
            prefabIndex++;

            // Looping the prefab index if we run out of prefabs (change later)
            if (prefabIndex > sectionsPrefabs.Length - 1)
            {
                prefabIndex = 0;
            }
        }
        
        // Adding the first sections to the road
        for (int i = 0; i < sections.Length; i++)
        {
            // Get a random section
            GameObject randomSection = GetRandomSelectionFromPool();
            
            // Move it into position and set it to active
            randomSection.transform.position = new Vector3(sectionsPool[i].transform.position.x, 0, i * sectionLength);
            randomSection.SetActive(true);
            
            // Set the section in the array
            sections[i] = randomSection;
        }

        StartCoroutine(UpdateLessOftenCo());
    }

    IEnumerator UpdateLessOftenCo()
    {
        while (true)
        {
            UpdateSectionsPositions();
            yield return waitFor100ms;
        }
    }

    void UpdateSectionsPositions()
    {
        for (int i = 0; i < sections.Length; i++)
        {
            // Check if section is too far behind
            if (sections[i].transform.position.z - playerCarTransform.position.z < -sectionLength)
            {
                // Store the last position of the section and disable it
                Vector3 lastSectionPosition = sections[i].transform.position;
                sections[i].SetActive(false);
                
                sections[i] = GetRandomSelectionFromPool();
                
                // Move the new section into place and activate it
                sections[i].transform.position = new Vector3(lastSectionPosition.x, 0, lastSectionPosition.z
                    + sectionLength * sections.Length);
                sections[i].SetActive(true);
            }
        }
    }

    private GameObject GetRandomSelectionFromPool()
    {
        // Pick a random index and hope its available
        int randomIndex = Random.Range(0, sections.Length);

        bool isNewSectionFound = false;

        while (!isNewSectionFound)
        {
            // Check if the section is not active, in that case we have found a section
            if (!sectionsPool[randomIndex].activeInHierarchy)
            {
                isNewSectionFound = true;
            }
            else
            {
                // If it was active we need to try to find another one so we increase the index
                randomIndex++;
                
                // Ensure that we loop around if we reach the end of an array
                if (randomIndex > sections.Length - 1)
                {
                    randomIndex = 0;
                }
            }
        }
        return sectionsPool[randomIndex];
    }
}
