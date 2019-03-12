using UnityEngine;
using System.Collections;
using LitJson;

public class sheet2Item : DataItem  {
	public int ID;
	public override int Identity(){ return ID; }
	public string Name;
	public int Type;

    public override void Setup(JsonData data) {
		base.Setup(data);
	

    }

	public sheet2Item () {
	
	}
}
