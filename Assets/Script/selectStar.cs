using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class selectStar : MonoBehaviour {
  public Toggle[] star;
  public GameObject[] starGameObject;
  private bool isSelect;
	// Use this for initialization
	void Start () {
    isSelect = false;
	}
	
	// Update is called once per frame
	void Update () {
    CheckSelect();
	}

  void CheckSelect()
  {
    for (int i = 0;i < star.Length;i++) {
      isSelect = star[i].isOn;
      if (isSelect&&!starGameObject[i].activeInHierarchy) {
        starGameObject[i].SetActive(true);
        for (int j = 0;j < starGameObject.Length;j++) {
          if (i == j) continue;
          starGameObject[j].SetActive(false);
        }
      }
    }
  }
}
