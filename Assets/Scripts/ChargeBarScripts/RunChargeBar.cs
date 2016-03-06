using UnityEngine;
using System.Collections;

public class RunChargeBar : MonoBehaviour {

    public RectTransform foregroundBarRectTrans = null;
    public GameObject playerGO = null;
    public Player_Attack playerAttackScript = null;

    float chargeAmount = 0f;
    public float displayedChargeAmount = 0f;

	// Use this for initialization
	void Start () {
        displayedChargeAmount = Mathf.Lerp(displayedChargeAmount, playerAttackScript.playerCharge, Time.deltaTime * 5f);

        chargeAmount = DataCore.Remap(Mathf.Clamp(displayedChargeAmount, 0f, 30f), 0f, 30f, 0f, 1f);

        foregroundBarRectTrans.localScale = new Vector3(1f, chargeAmount, 1f);
    }
	
	// Update is called once per frame
	void Update () {
        displayedChargeAmount = Mathf.Lerp(displayedChargeAmount, playerAttackScript.playerCharge, Time.deltaTime * 5f);

        chargeAmount = DataCore.Remap(Mathf.Clamp(displayedChargeAmount, 0f, 30f), 0f, 30f, 0f, 1f);

        foregroundBarRectTrans.localScale = new Vector3(1f, chargeAmount, 1f);
	}
}
