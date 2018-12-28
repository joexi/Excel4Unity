using UnityEngine;
using System.Collections;
using LitJson;

public class TestItem : DataItem  {
	public int ID;
	public override int Identity(){ return ID; }
	public string Name;
	public int Type;

    public override void Setup(JsonData data) {
		base.Setup(data);
		ID = int.Parse(data["ID"].ToString());
		Name = data["Name"].ToString();
		Type = int.Parse(data["Type"].ToString());

    }

	public TestItem () {
	
	}
}
