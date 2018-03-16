using UnityEngine; 
using System.Collections; 

public class DropItem : MonoBehaviour 
{ 
	public Transform item; 
	public int chance; 

	public DropItem(Transform _item, int _chance) 
	{ 
		item = _item; 
		chance = _chance; 
	} 

	public void CreateDropItem(Vector3 position) 
	{ 
		var drop_item = Instantiate(item) as Transform; 
		drop_item.position = position; 
	} 
} 

