using UnityEngine;

public class EndlessLevelHandler : MonoBehaviour
{
    [SerializeField] private GameObject[] sectionPrefabs;  // Straight, curve, etc.
    [SerializeField] private Transform playerCarTransform;
    [SerializeField] private int visibleSections = 10;

    private GameObject[] activeSections;
    private int nextSpawnIndex = 0;

    void Start()
    {
        activeSections = new GameObject[visibleSections];

        // First section at origin
        GameObject first = Instantiate(sectionPrefabs[0], Vector3.zero, Quaternion.identity);
        activeSections[0] = first;

        // Build initial chain
        for (int i = 1; i < visibleSections; i++)
        {
            RoadSection prevRS = activeSections[i - 1].GetComponent<RoadSection>();

            GameObject nextGO = Instantiate(sectionPrefabs[Random.Range(0, sectionPrefabs.Length)]);
            RoadSection nextRS = nextGO.GetComponent<RoadSection>();

            SnapSection(prevRS, nextRS);
            activeSections[i] = nextGO;
        }
    }

    void Update()
    {
        // When player is far past the oldest section, recycle it
        if (Vector3.Distance(playerCarTransform.position, activeSections[nextSpawnIndex].transform.position) > 30f)
        {
            int lastIndex = (nextSpawnIndex + visibleSections - 1) % visibleSections;

            RoadSection prevRS = activeSections[lastIndex].GetComponent<RoadSection>();

            GameObject recycled = activeSections[nextSpawnIndex];
            RoadSection recycledRS = recycled.GetComponent<RoadSection>();

            SnapSection(prevRS, recycledRS);

            activeSections[nextSpawnIndex] = recycled;
            nextSpawnIndex = (nextSpawnIndex + 1) % visibleSections;
        }
    }

    /// <summary>
    /// Aligns a new section so its EntryPoint matches the previous ExitPoint.
    /// </summary>
    private void SnapSection(RoadSection prev, RoadSection next)
    {
        // Temporarily align to exit
        next.transform.position = prev.ExitPoint.position;
        next.transform.rotation = prev.ExitPoint.rotation;

        // Shift so Entry aligns with Exit
        Vector3 entryToRoot = next.transform.position - next.EntryPoint.position;
        next.transform.position += entryToRoot;
    }
}
