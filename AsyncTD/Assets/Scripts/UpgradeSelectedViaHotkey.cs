using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent (typeof(SelectionManager))]
public class UpgradeSelectedViaHotkey : MonoBehaviour
{
	private SelectionManager selectionManager;
	public Dictionary<CommandType, TowerType> commandToTowerType;

	void Start ()
	{
		selectionManager = GetComponent<SelectionManager> ();
		commandToTowerType = new Dictionary<CommandType, TowerType> ();
		commandToTowerType.Add (CommandType.BUILD_GREEN, TowerType.GREEN);
		commandToTowerType.Add (CommandType.BUILD_RED, TowerType.RED);
		commandToTowerType.Add (CommandType.BUILD_BLUE, TowerType.BLUE);
	}

	// Use this for initialization
	// Update is called once per frame
	void Update ()
	{
		
		CommandType command = CommandType.NONE;
		//TODO better input
		if (Input.GetKeyDown (KeyCode.Alpha1)) {
			command = CommandType.BUILD_GREEN;
		}
		if (Input.GetKeyDown (KeyCode.Alpha2)) {
			command = CommandType.BUILD_RED;
		}
		if (Input.GetKeyDown (KeyCode.Alpha3)) {
			command = CommandType.BUILD_BLUE;
		}
		if (Input.GetKeyDown (KeyCode.U)) {
			command = CommandType.UPGRADE;
		}
		if (command != CommandType.NONE) {
			if (selectionManager.currentSelectedObject == null) {
				Debug.Log ("Can't build, nothing selected.");
				return;
			}
			GameObject currentSelection = selectionManager.currentSelectedObject;
			GameObject newSelection = currentSelection;
			if (command == CommandType.UPGRADE) {
				Upgradeable upgrades = currentSelection.GetComponent<Upgradeable> ();
				if (upgrades != null) {
					newSelection = upgrades.Upgrade ();
				}
			} else {
				// Build Command
				BuildSlot buildSlot = currentSelection.GetComponent<BuildSlot> ();
				if (buildSlot != null) {
					newSelection = buildSlot.Build (commandToTowerType [command]);
				}
			}
			selectionManager.Select (newSelection);
		}
	}

}
