using UnityEngine;
using System.Collections;

namespace WizardCube
{
    public class LookAtMouse : MonoBehaviour
    {

        void Awake()
        {
            transform.rotation = new Quaternion(0, 0, 0, 0);
        }

        // Update is called once per frame
        void Update()
        {
			GameObject Canvas = GameObject.Find ("Canvas");
			Pause pause = Canvas.GetComponent<Pause> ();

			if (pause.paused) {
				
			
            //Get the Screen positions of the object
            Vector2 positionOnScreen = Camera.main.WorldToViewportPoint(transform.position);

            //Get the Screen position of the mouse
            Vector2 mouseOnScreen = (Vector2)Camera.main.ScreenToViewportPoint(Input.mousePosition);

            //Get the angle between the points
            float angle = AngleBetweenTwoPoints(positionOnScreen, mouseOnScreen);

            //Ta Daaa
            transform.rotation = Quaternion.Euler(new Vector3(-90, 0, -angle + 90));
			}
			}
        //magic
        float AngleBetweenTwoPoints(Vector3 a, Vector3 b)
        {
            return Mathf.Atan2(a.y - b.y, a.x - b.x) * Mathf.Rad2Deg;
        }

    }
}