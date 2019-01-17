using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;
 
public class DragableUI : MonoBehaviour, IDragHandler
{
    RectTransform m_transform = null;

	private RectTransform canvasRectTransform;

    private bool clampedToLeft;
    private bool clampedToRight;
    private bool clampedToTop;
    private bool clampedToBottom;
 
     // Use this for initialization
    void Start () {
        m_transform = GetComponent<RectTransform>();
		Canvas canvas = GetComponentInParent<Canvas>();
		if (canvas != null)
            canvasRectTransform = canvas.transform as RectTransform;

		clampedToLeft = false;
        clampedToRight = false;
        clampedToTop = false;
        clampedToBottom = false;
    }
 
    public void OnDrag(PointerEventData eventData)
    {
        m_transform.position += new Vector3(eventData.delta.x, eventData.delta.y);
      	ClampToWindow();
		
		Vector2 clampedPosition = m_transform.localPosition;
        if (clampedToRight)
            {
                clampedPosition.x = (canvasRectTransform.rect.width * 0.5f) - (m_transform.rect.width * (1 - m_transform.pivot.x));
            }
        else if (clampedToLeft)
            {
                clampedPosition.x = (-canvasRectTransform.rect.width * 0.5f) + (m_transform.rect.width * m_transform.pivot.x);
            }

        if (clampedToTop)
            {
                clampedPosition.y = (canvasRectTransform.rect.height * 0.5f) - (m_transform.rect.height * (1 - m_transform.pivot.y));
            }
        else if (clampedToBottom)
            {
                clampedPosition.y = (-canvasRectTransform.rect.height * 0.5f) + (m_transform.rect.height * m_transform.pivot.y);
            }
        m_transform.localPosition = clampedPosition;
        // magic : add zone clamping if's here.

    }

	void ClampToWindow()
    {
        Vector3[] canvasCorners = new Vector3[4];
        Vector3[] panelRectCorners = new Vector3[4];
        canvasRectTransform.GetWorldCorners(canvasCorners);
        m_transform.GetWorldCorners(panelRectCorners);

        if (panelRectCorners[2].x > canvasCorners[2].x)
        {
            Debug.Log("Panel is to the right of canvas limits");
            if (!clampedToRight)
            {
                clampedToRight = true;
            }
        }
        else if (clampedToRight)
        {
            clampedToRight = false;
        }
        else if (panelRectCorners[0].x < canvasCorners[0].x)
        {
            Debug.Log("Panel is to the left of canvas limits");
            if (!clampedToLeft)
            {
                clampedToLeft = true;
            }
        }
        else if (clampedToLeft)
        {
            clampedToLeft = false;
        }

        if (panelRectCorners[2].y > canvasCorners[2].y)
        {
            Debug.Log("Panel is to the top of canvas limits");
            if (!clampedToTop)
            {
                clampedToTop = true;
            }
        }
        else if (clampedToTop)
        {
            clampedToTop = false;
        }
        else if (panelRectCorners[0].y < canvasCorners[0].y) 
        {
            Debug.Log("Panel is to the bottom of canvas limits");
            if (!clampedToBottom)
            {
                clampedToBottom = true;
            }
        }
        else if (clampedToBottom)
        {
            clampedToBottom = false;
        }
    }
}