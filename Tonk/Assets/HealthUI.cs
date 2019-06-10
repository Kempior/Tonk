using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthUI : MonoBehaviour
{
	[SerializeField]
	RectTransform HealthTransform;
	[SerializeField]
	Text HealthText;

	[SerializeField]
	float startOffset = 0;
	[SerializeField]
	float endOffset = 0;

	float startPosition;

	public void Start()
	{
		if (HealthTransform != null)
			startPosition = HealthTransform.position.x;
	}

	public void SetHP(float currentHP, float maxHP)
	{
		SetHPText(currentHP);
		SetHPBar(currentHP / maxHP);
	}

	public void SetHPText(float hp)
	{
		HealthText.text = hp.ToString("0.00");
	}

	public void SetHPBar(float percentage)
	{
		float width = HealthTransform.sizeDelta.x;
		float startingPoint = -width + startPosition + startOffset;

		Debug.Log($"{startingPoint} + {percentage} * {width}");
		HealthTransform.localPosition = new Vector3
		{
			x = startingPoint + percentage * width
		};
	}

	public void Update()
	{
		SetHP(Input.GetAxis("Vertical") * 80, Input.GetAxis("Vertical") * 80 + 1);
	}
}
