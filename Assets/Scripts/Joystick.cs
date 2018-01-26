#if UNITY_STANDALONE_WIN || UNITY_EDITOR
#define _WINDOWS_
#elif UNITY_ANDROID
#define _MOBILE_
#endif

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.EventSystems;

public class Joystick : MonoBehaviour, IDragHandler,
	IPointerUpHandler, IPointerDownHandler
{
	private Vector2 m_TouchBgInitPos = Vector2.zero;
	private RectTransform m_TouchPointObj = null;

	private float m_TouchBgRadius = 0.0f;
	private float m_radius = 0.0f;

	//private Rect m_TouchableRt = new Rect();
	private RectTransform m_backgroundRectTransform;

	private Vector2 m_TouchPos = Vector2.zero;

	private bool m_bPressed = false;
	private bool m_bPressedInTouchableRect = false;
	private Vector2 m_PressedPos = Vector2.zero;

	private GameObject m_Canvas;

	public float m_touchMoveSpeed = 500.0f;
	public GameObject m_joyStickBtn;
	public GameObject m_joyStickPoint;
	public GameObject m_touchableRect;

	public JBKernel m_target;


	void Awake()
	{
		m_Canvas = GameObject.Find("Canvas");
		
		RectTransform tran = m_joyStickBtn.GetComponent<RectTransform>();
		if (tran == null || tran.gameObject == null)
		{
			return;
		}

		m_TouchBgInitPos = tran.anchoredPosition;
		m_TouchBgRadius = Mathf.Max(tran.rect.width, tran.rect.height) * 0.5f;
		m_TouchBgInitPos += new Vector2(m_TouchBgRadius, m_TouchBgRadius);

		m_radius = m_TouchBgRadius - 5;

		m_TouchPointObj = m_joyStickPoint.GetComponent<RectTransform>();
		if (tran == null || tran.gameObject == null)
		{
			return;
		}

		m_backgroundRectTransform = m_Canvas.GetComponent<RectTransform>();
		//RectTransform touchableRectTransform = m_touchableRect.GetComponent<RectTransform>();
		//m_TouchableRt = new Rect(touchableRectTransform.offsetMin.x, touchableRectTransform.offsetMin.y, touchableRectTransform.sizeDelta.x, touchableRectTransform.sizeDelta.y);
	}

	void OnDestroy()
	{
	}

	void OnEnable()
	{
		m_bPressed = false;
	}

	void Update()
	{
		Vector2 delta = Vector2.zero;
		if (UpdateMoveDelta(ref delta))
		{
			//delta.Normalize();
			delta *= 20;
#if (UNITY_ANDROID || UNITY_IPHONE) && !UNITY_EDITOR
			m_target.MoveChar(delta);//.SendMessage("MoveChar", delta, SendMessageOptions.DontRequireReceiver);
#else
			m_target.MoveChar(delta);//.SendMessage("MoveChar", delta, SendMessageOptions.DontRequireReceiver);
#endif
		}
	}

	
	public void OnDrag(PointerEventData eventData)
	{
		OnDragProc(eventData.button, eventData.position);
	}
	
	public void OnPointerUp(PointerEventData eventData)
	{
		OnPointerUpProc(eventData.button);
	}

	public void OnPointerDown(PointerEventData eventData)
	{
		m_bPressedInTouchableRect = true;
		OnDragProc(eventData.button, eventData.position);
	}

	private void OnDragProc(PointerEventData.InputButton button, Vector2 position)
	{
		if (button == PointerEventData.InputButton.Left)
		{
			m_bPressed = true;
			
			Vector2 converted = new Vector2();
			RectTransformUtility.ScreenPointToLocalPointInRectangle(m_backgroundRectTransform,
			position, null, out converted);

			converted += new Vector2(m_backgroundRectTransform.rect.width / 2, m_backgroundRectTransform.rect.height / 2);
			
			//Debug.Log(position + ", " + converted);
			m_PressedPos = converted;
		}	
	}

	private void OnPointerUpProc(PointerEventData.InputButton button)
	{
		if (!m_bPressedInTouchableRect)
			return;

		if (button == PointerEventData.InputButton.Left)
		{
			m_bPressed = false;
			m_bPressedInTouchableRect = false;
			m_PressedPos = Vector2.zero;
		}
	}

	bool UpdateMoveDelta(ref Vector2 _delta)
	{
		bool touchDown = false;

#if _WINDOWS_
		if (Input.GetButton("Horizontal") || Input.GetButton("Vertical"))
		{
			float horzMove = Input.GetAxis("Horizontal");
			float vertMove = Input.GetAxis("Vertical");
			touchDown = true;


			_delta = new Vector2(horzMove, vertMove);
		}
#endif
		if (m_bPressed)
		{
			Vector2 mousePos = Vector2.zero;
			mousePos = m_PressedPos;

			if (mousePos == Vector2.zero)
				return touchDown;

			if (m_bPressedInTouchableRect == true)
			{
				Vector2 deltaPos = mousePos - m_TouchBgInitPos;
				float horzMove = deltaPos.x / m_TouchBgRadius;
				float vertMove = deltaPos.y / m_TouchBgRadius;

				_delta = new Vector2(horzMove, vertMove);

				_delta.x = Mathf.Clamp(_delta.x, -1, 1);
				_delta.y = Mathf.Clamp(_delta.y, -1, 1);

				float normal = deltaPos.magnitude;
				if (normal > m_radius)
					//deltaPos = deltaPos.normalized * m_radius;
					deltaPos = deltaPos / normal * m_radius;	// 속도가 빠르다고 함.

				m_TouchPos = deltaPos;
				touchDown = true;
			}
			else
			{
				m_TouchPos = Vector3.zero;
			}
		}
		else
		{
			m_TouchPos = Vector3.zero;
		}

		//if (m_TouchPointObj != null && m_ShowState != ShowState.HideEnd)
		{
			m_TouchPointObj.anchoredPosition = Vector2.MoveTowards(m_TouchPointObj.anchoredPosition, m_TouchPos,
				m_touchMoveSpeed * Time.deltaTime);
		}

		return touchDown;
	}
}

