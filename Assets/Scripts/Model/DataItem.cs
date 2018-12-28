using UnityEngine;
using System.Collections;
using LitJson;

public class DataItem  {
    
    public DataItem()
    {

    }

    public virtual void Setup(JsonData data) {
        
    }

    public virtual int Identity()
    {
        return 0;
    }

	public virtual int IndexIdentity()
	{
		return 0;
	}

    public virtual string StringIdentity()
    {
        return string.Empty;
    }
}
