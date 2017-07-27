using UnityEngine;
using UnityEngine.UI;

public class WeaponSwitching : MonoBehaviour {

	public int selectedWeapon = 0;
	public Button button;
	public Sprite[] image;
	private bool change=false;
	public Button fire;
	private Animator m_Anim;     
	public Transform player;

	void Start () {
		button.GetComponent<Image>().sprite = image[0];
		m_Anim = player.GetComponent<Animator>();
		SelectWeapon ();

	}
	
	// Update is called once per frame
	void Update () {
		if (change)
			SelectWeapon ();
	}

	public void TaskOnClick(){
		int previousSelectedWeapon = selectedWeapon;

		if(selectedWeapon>= transform.childCount-1)
			selectedWeapon=0;
		else
			selectedWeapon++;


		if (previousSelectedWeapon != selectedWeapon) {
			//SelectWeapon ();
			change = true;
		}

			}
	void SelectWeapon(){

		int i = 0;
		change = false;
		foreach (Transform weapon in transform) {
			if (i == selectedWeapon) {
				weapon.gameObject.SetActive (true);
				button.GetComponent<Image>().sprite = image[i];
				if (i == 2){
					m_Anim.SetBool("Fire", true);
                    m_Anim.SetBool("Bow", false);
                    fire.gameObject.SetActive (true);
				}
				else{
					fire.gameObject.SetActive (false);
                    if (i == 1)
                    {
                        m_Anim.SetBool("Bow", true);
                    }
                    else
                    {
                        m_Anim.SetBool("Bow", false);//catapult
                    }
                    m_Anim.SetBool("Fire", false);
                }
                
			}
			else
				weapon.gameObject.SetActive (false);
			i++;
		}
	}


}
