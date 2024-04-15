using UnityEngine;

public class SpotItem : MonoBehaviour
{
    public int spotIndex;

    public bool isSpotted;

    public GameObject successIcon;
    public Transform showSpot;
    [HideInInspector]
    public Transform spotPos;

    // Start is called before the first frame update
    private void Start()
    {
        isSpotted = false;
        spotPos = transform.parent.Find("CirclePlace" + spotIndex);
    }

    public void SetSpotItem()
    {
        isSpotted = true;
        Vector2 correctPos = new Vector2(spotPos.position.x, spotPos.position.y);
        Instantiate(successIcon, correctPos, Quaternion.identity);
    }
}