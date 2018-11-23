using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISaveData {
	void SetValue( System.String key, System.Single value);
	void OnBeforeSerialize( );
	void OnAfterDeserialize( );
}
