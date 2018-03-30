using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dot : MonoBehaviour {

	[Header("Board Variables")]
	public int column;
	public int row;
	public int previousColumn;
	public int previousRow;
	public int targetX;
	public int targetY;
	public bool isMatched = false;

	private GameObject otherDot;
	private Board board;
	private Vector2 firstTouchPosition;
	private Vector2 finalTouchPosition;
	private Vector2 tempPosition;
	public float swipeAngle = 0;

	// Use this for initialization
	void Start () {
		board = FindObjectOfType<Board> ();
		targetX = (int)transform.position.x;
		targetY = (int)transform.position.y;
		row = targetY;
		column = targetX;

		previousRow = row;
		previousColumn = column;
	}
	
	// Update is called once per frame
	void Update () {
		FindMatches ();
		if (isMatched) {
			SpriteRenderer mySprite = GetComponent<SpriteRenderer> ();
			mySprite.color = new Color (0f, 0f, 0f, .2f);
		}

		targetX = column;
		targetY = row;
		if (Mathf.Abs (targetX - transform.position.x) > .1) {
			// Move towards the target
			tempPosition = new Vector2(targetX, transform.position.y);
			transform.position = Vector2.Lerp (transform.position, tempPosition, .4f);
		} else {
			// Directly set the position
			tempPosition = new Vector2(targetX, transform.position.y);
			transform.position = tempPosition;
			board.allDots [column, row] = this.gameObject;
		}

		if (Mathf.Abs (targetY - transform.position.y) > .1) {
			// Move towards the target
			tempPosition = new Vector2(transform.position.x, targetY);
			transform.position = Vector2.Lerp (transform.position, tempPosition, .4f);
		} else {
			// Directly set the position
			tempPosition = new Vector2(transform.position.x, targetY);
			transform.position = tempPosition;
			board.allDots [column, row] = this.gameObject;
		}
	}

	public IEnumerator CheckMoveCo() {
		yield return new WaitForSeconds (.5f);

		if (otherDot != null) {
			if (!isMatched && !otherDot.GetComponent<Dot> ().isMatched) {
				otherDot.GetComponent<Dot> ().row = row;
				otherDot.GetComponent<Dot> ().column = column;
				row = previousRow;
				column = previousColumn;
			}

			otherDot = null;
		}
	}

	private void OnMouseDown(){
		firstTouchPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);		// Screen coordinates to board coordinates
		//Debug.Log (firstTouchPosition);				// Debug statement
	}

	private void OnMouseUp()
	{
		finalTouchPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		CalculateAngle ();
	}

	// Calcuate between first touch to final touch
	void CalculateAngle()
	{
		swipeAngle = Mathf.Atan2 (finalTouchPosition.y - firstTouchPosition.y, finalTouchPosition.x - firstTouchPosition.x) * 100 / Mathf.PI;
		Debug.Log (swipeAngle);
		MovePieces ();
	}

	void MovePieces()
	{
		if (swipeAngle > -10 && swipeAngle <= 9 && column < board.width - 1) {
			// Right Swipe
			otherDot = board.allDots[column + 1, row];
			otherDot.GetComponent<Dot> ().column -= 1;
			column += 1;
		} else if (swipeAngle > 30 && swipeAngle <= 70 && row < board.height - 1) {
			// Up Swipe
			otherDot = board.allDots[column, row + 1];
			otherDot.GetComponent<Dot> ().row -= 1;
			row += 1;
		} else if (swipeAngle > 80 && swipeAngle <= 100 || swipeAngle > -99 && swipeAngle <= -91 && column > 0) {
			// Left Swipe
			otherDot = board.allDots[column - 1, row];
			otherDot.GetComponent<Dot> ().column += 1;
			column -= 1;
		} else if (swipeAngle < -40 && swipeAngle >= -65 && row > 0) {
			// Down Swipe
			otherDot = board.allDots[column, row - 1];
			otherDot.GetComponent<Dot> ().row += 1;
			row -= 1;
		}
		StartCoroutine (CheckMoveCo());
	}

	void FindMatches() 
	{
		if (column > 0 && column < board.width - 1) {
			GameObject leftDot1 = board.allDots[column - 1, row];
			GameObject rightDot1 = board.allDots[column + 1, row];
			if (leftDot1.tag == this.gameObject.tag && rightDot1.tag == this.gameObject.tag){
				leftDot1.GetComponent<Dot>().isMatched = true;
				rightDot1.GetComponent<Dot>().isMatched = true;
				isMatched = true;
			}
		}

		if (row > 0 && row < board.height - 1) {
			GameObject upDot1 = board.allDots[column, row + 1];
			GameObject downDot1 = board.allDots[column, row - 1];
			if (upDot1.tag == this.gameObject.tag && downDot1.tag == this.gameObject.tag){
				upDot1.GetComponent<Dot>().isMatched = true;
				downDot1.GetComponent<Dot>().isMatched = true;
				isMatched = true;
			}
		}
	}
}
