using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceGame
{
    public class Rotator : MonoBehaviour
    {
        [SerializeField] float _rotationSpeed;
        // Start is called before the first frame update      

		private void FixedUpdate()
		{
			transform.Rotate(0, 0, _rotationSpeed * Time.deltaTime);
		}
	}
}
