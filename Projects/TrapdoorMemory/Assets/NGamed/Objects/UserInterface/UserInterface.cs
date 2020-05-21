using UnityEngine;

public class UserInterface : MonoBehaviour {
	public Camera viewer;


	public Vector3 getPointOnPlane(Vector3 point, float height) {
		Ray ray = viewer.ScreenPointToRay(point);
		Plane plane = new Plane(Vector3.up, -height);

		float distance;

		if(plane.Raycast(ray, out distance)) {
			return ray.GetPoint(distance);
		}
		else {
			return Vector3.zero;
		}
	}
}