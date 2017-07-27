using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPath : MonoBehaviour {

    public enum FollowType
    {
        MoveTowards,
        Lerp
    }

    public FollowType type = FollowType.MoveTowards;
    public PathDefination path;
    public float speed = 1;
    public float maxDistanceToGoal = .1f;
	Vector3 theScale;

    private IEnumerator<Transform> _currentPoint;

    private void Start()
    {
		
        if (path == null)
        {
            Debug.LogError("Path Cannot be null ", gameObject);
            return;
        }

        _currentPoint = path.GetPathsEnumerator();
        _currentPoint.MoveNext();

        if (_currentPoint.Current == null)
        {
            return;
        }

        transform.position = _currentPoint.Current.position;


    }

    private void Update()
    {
		
        if(_currentPoint==null || _currentPoint.Current == null)
        {
            return;
        }

		if (type == FollowType.MoveTowards) {
			transform.position = Vector3.MoveTowards (transform.position, _currentPoint.Current.position, Time.deltaTime * speed);
		

			if ((transform.position.x < _currentPoint.Current.position.x)&&(theScale.x>=0)){
				theScale = transform.localScale;
				theScale.x *= -1;
				transform.localScale = theScale;
			}
			if((transform.position.x > _currentPoint.Current.position.x)&&(theScale.x<=0)){
				theScale = transform.localScale;
			    theScale.x *= -1;
			    transform.localScale = theScale;
			}
		}
		else if (type == FollowType.Lerp)
            transform.position = Vector3.Lerp(transform.position, _currentPoint.Current.position, Time.deltaTime * speed);


        var distanceSquared = (transform.position - _currentPoint.Current.position).sqrMagnitude;
        if (distanceSquared < maxDistanceToGoal * maxDistanceToGoal)
            _currentPoint.MoveNext();
    }
}
