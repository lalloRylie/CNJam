using UnityEngine;
using System.Collections;

public class RunChargeBar : MonoBehaviour {

    public RectTransform foregroundBarRectTrans = null;
    public GameObject playerGO = null;
    public Player_Attack playerAttackScript = null;

	// Use this for initialization
	void Start () {
        float chargeAmount = DataCore.Remap(Mathf.Clamp(playerAttackScript.playerCharge, 0f, 30f), 0f, 30f, 0f, 1f);

        foregroundBarRectTrans.localScale = new Vector3(1f, chargeAmount, 1f);
	}
	
	// Update is called once per frame
	void Update () {
        float chargeAmount = DataCore.Remap(Mathf.Clamp(playerAttackScript.playerCharge, 0f, 30f), 0f, 30f, 0f, 1f);

        foregroundBarRectTrans.localScale = new Vector3(1f, chargeAmount, 1f);

	}
}
