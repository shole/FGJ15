using UnityEngine;
using System.Collections;

public class CubeMover : MonoBehaviour {
    public TouchInputReader input;
    public float moveForceModifier = 15f;
    public float pullForceModifier = 100f;

    public GameObject playerIndicatorPrefab;
    GameObject playerIndicator;
    Transform playerDirection;
    Transform reachTarget;
    TextMesh textMesh;

    public Color grabbedColor = Color.green;
    public Color releasedColor = Color.white;

    Rigidbody endObject = null;
    Rigidbody beginObject = null;

    public float maxTentacleStretch = 1.5f;
    public float maxTentacleDrift = 1f;

    private float normalTentacleLength;

    private Vector3 grabbingPos;

    void Awake()
    {
        playerIndicator = (GameObject)Instantiate(playerIndicatorPrefab);
        playerIndicator.transform.parent = transform;
        playerIndicator.transform.localPosition = Vector3.zero;
        playerDirection = playerIndicator.transform.FindChild("DirectionWheel");
        textMesh = playerIndicator.transform.FindChild("playerno").GetComponent<TextMesh>();
        UpdateNumberColor();
        normalTentacleLength = GetTentacleLength();
        Debug.Log("tentacle is " + normalTentacleLength);
    }

    public void SetInput(TouchInputReader reader)
    {
        input = reader;
        reachTarget = playerDirection.FindChild("ReachTarget");
        Debug.Log("playerid " + input.photonView.owner.ID);
        playerIndicator.transform.FindChild("playerno").gameObject.GetComponent<TextMesh>().text = "" + input.photonView.owner.ID;
        playerIndicator.transform.FindChild("playernoshadow").gameObject.GetComponent<TextMesh>().text = "" + input.photonView.owner.ID;
    }

    public bool HasInput()
    {
        return input != null;
    }

    private void UpdateNumberColor()
    {
        if (IsGrabbed())
        {
            textMesh.color = grabbedColor;
        }
        else
        {
            textMesh.color = releasedColor;
        }
    }

	// Update is called once per frame
	void Update () {
        HandleAutomaticRelease();
        HandleInput();
	}

    private void HandleAutomaticRelease()
    {
        if (!IsGrabbed())
        {
            return;
        }

        // if tentacle has stretched too long, release grab
        float currentLength = GetTentacleLength();
        if (currentLength > normalTentacleLength * maxTentacleStretch)
        {
            Grab(false);
            Debug.Log("stretched too long, releasing grab");
            return;
        }

        // if tentacle has moved too far from the original grabbing pos, release grab
        float driftAmount = (transform.position - grabbingPos).magnitude;
        if (driftAmount > normalTentacleLength * maxTentacleDrift)
        {
            Debug.Log("drifted too far, releasing grab");
            Grab(false);
        }
    }

    private void Grab(bool status)
    {
        rigidbody.isKinematic = status;
        UpdateNumberColor();

        if (status)
        {
            grabbingPos = transform.position;
        }
    }

    private bool IsGrabbed()
    {
        return rigidbody.isKinematic;
    }

    private void HandleInput()
    {
        if (input == null)
        {
            return;
        }

        if (input.lastX == 0 && input.lastY == 0)
        {
            return;
        }
        playerDirection.localRotation = Quaternion.AngleAxis(Mathf.Atan2(input.lastX, input.lastY) * 60, Vector3.forward);

        if (IsGrabbed())
        {
            // grabbing is on, and user is giving input, so cause a force

            // Points hopefully defined! APPLY FORCE
            if (FetchStartAndEndParts())
            {
                Vector3 direction = endObject.position - beginObject.position;

                beginObject.AddForce(direction * pullForceModifier);
            }
        }
        else
        {
            // just move the tentacle

            rigidbody.AddForce((reachTarget.position - transform.position).normalized * moveForceModifier);
            //rigidbody.AddForce(input.lastX * moveForceModifier, 0, input.lastY * moveForceModifier);
        }

        if (input.unhandledDoubleTap)
        {
            input.unhandledDoubleTap = false;
            Grab(!IsGrabbed());
        }
    }

    private bool FetchStartAndEndParts()
    {
        var list = this.GetComponentsInParent<Rigidbody>();

        endObject = null;
        beginObject = null;

        var i = 0;

        foreach (Rigidbody item in list)
        {
            // Split (in armature the current tentacle hierarchy has bones splitted by '|'
            // Ugly hax
            var nameParts = item.name.Split('|');

            if (nameParts.Length >= 2)
            {
                if (i == 0)
                {
                    endObject = item; // first bone
                }
                else
                {
                    beginObject = item; // last bone
                }

                i++;
            }
        }

        return endObject != null && beginObject != null;
    }

    private float GetTentacleLength()
    {
        if (!FetchStartAndEndParts())
        {
            return float.NaN;
        }

        return (endObject.position - beginObject.position).magnitude;
    }
}
