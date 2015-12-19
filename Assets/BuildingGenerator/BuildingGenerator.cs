using UnityEngine;
using System.Collections;

public class BuildingGenerator : MonoBehaviour {
	public Transform building_prefab;
	public Transform building_prefab2;
	public int number_of_rings;
	public int distance_between_buildings;
	public int highest_floor;
	public float floor_variability;
	public int calculation_method;

	private Vector3 _building_position;
	private int _top_floor;
	private int _ring_number;
	private int _average_floor;
	private float _chance_to_create_building;

	// Use this for initialization
	void Start () {
		CreateBuildingRing(number_of_rings);
	}

	void CreateBuildingRing(int number_of_rings) {
		for (int x = -1*number_of_rings; x<=number_of_rings; x++) {
			for(int z = -1*number_of_rings; z<=number_of_rings; z++) {
				_ring_number = Mathf.Max(Mathf.Abs (x),Mathf.Abs (z));
				_chance_to_create_building = 1-Mathf.Sin(Mathf.Pow(_ring_number/(float)number_of_rings,2));
				_average_floor = _calculateAverageFloor(x, z);
				if (Random.value < _chance_to_create_building) {
					InstantiateBuilding(_average_floor,x*distance_between_buildings,z*distance_between_buildings);
				}
			}
		}
	}

	private int _calculateAverageFloor(int x, int z) {
		switch (calculation_method) 
		{
			case 1:
				return (int)((number_of_rings-_ring_number+1)/(float)number_of_rings*highest_floor);
				break;
			case 2:
				return (int)(_chance_to_create_building*highest_floor);
				break;
			case 3:
				return (int)(1/((float)_ring_number+1)*highest_floor);
				break;
			case 4:
				return (int)Mathf.Ceil (Mathf.PerlinNoise (x/10f,z/10f)*highest_floor*2);
				break;
			default:
				return 1;
				break;
		}
	}

	void InstantiateBuilding(int _average_floor, int x, int z) {
		_building_position = new Vector3 (x, 3.4f, z);
		_top_floor = Random.Range ((int)Mathf.Ceil (_average_floor*(1f-floor_variability)),(int)Mathf.Ceil (_average_floor*(1f+floor_variability)));

		Instantiate (building_prefab, _building_position, Quaternion.identity);
		for (int floor_counter = 1; floor_counter <= _top_floor; floor_counter++) {
			_building_position += new Vector3 (0, 4.7f, 0);
			Instantiate (building_prefab2, _building_position, Quaternion.identity);
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
