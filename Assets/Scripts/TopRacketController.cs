using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TopRacketController : MonoBehaviour {

    public float margin = 1.76f;

    public void SetRacketToCenter() {
        transform.position = new Vector2(0, margin);
    }

    public void OnSliderDrag(UnityEngine.UI.Slider slider) {
        transform.position = new Vector3(slider.value, transform.position.y, transform.position.z);
    }
}