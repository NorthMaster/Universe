using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class NewBehaviourScript1 : MonoBehaviour,IBeginDragHandler,IEndDragHandler
{
	[SerializeField] RectTransform[] m_Pages;
	[SerializeField] RectTransform m_Panel;

	private bool m_IsDragging = false;
	private int m_PageWidth = 700;
	private float []m_DisToCenter;
	private int m_MinPageNum = 0;

	public float m_Speed = 10f;

	private void Start()
	{
		m_DisToCenter = new float[]{0,0,0,0,0};
	}

	public void OnBeginDrag (PointerEventData eventData)
	{
		m_IsDragging = true;
	}

	public void OnEndDrag (PointerEventData eventData)
	{
		m_IsDragging = false;
	}

	public void Update()
	{
		for(int i=0;i<5;++i)
		{
			m_DisToCenter[i] = Mathf.Abs(0 - m_Pages[i].position.x);
		}

		float minDis = Mathf.Min(m_DisToCenter);
		for(int i=0; i<5;++i)
		{
			if(m_DisToCenter[i] == minDis)
			{
				m_MinPageNum = i;
				break;
			}
		}
		if(!m_IsDragging)
			LerpToCenter(m_MinPageNum * -m_PageWidth);
	}

	private void LerpToCenter(float posX)
	{
		float pos = Mathf.Lerp(m_Panel.anchoredPosition.x,posX,Time.deltaTime*m_Speed);
		Vector2 newPosition = new Vector2(pos,m_Panel.anchoredPosition.y);
		m_Panel.anchoredPosition = newPosition;   
	}

	public void OnClick()
	{
		Debug.Log("点击事件");
	}


}
