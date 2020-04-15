using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class EnemyController : Damageable, IPooledObject
{
    public float speed;
    public float visionDistance;
    public float stepLength;
    public float timeBetweenSteps;
    public int reward;

    public float pos1X;
    public float pos2X;
    private float minPosY = 0.1f;

    public Transform target;

    private float distanceToTarget;
    private bool isGoingToTarget;

    private SpriteRenderer _renderer;
    private float goingToPosX;

    public void OnObjectSpawn() {
        if (_renderer == null)
            _renderer = GetComponent<SpriteRenderer>();

        isGoingToTarget = false;
        goingToPosX = pos1X;

        StartCoroutine("GoDown");
    }

    private void Update()
    {

        if (isGoingToTarget)
        {
            GoToTarget();
            return;
        }

        distanceToTarget = Vector2.Distance(this.transform.position, target.position);
        if (distanceToTarget < visionDistance)
        {
            isGoingToTarget = true;
            GoToTarget();
        }
        else //Move in X
        {
            MoveInHorizontalAxe();
        }
    }

    private IEnumerator GoDown()
    {
        while (!isGoingToTarget)
        {
            yield return new WaitForSeconds(timeBetweenSteps);
            GoDownOneLine();
        }
    }

    private void GoDownOneLine()
    {
        //this.transform.Translate(Vector2.down * stepLength);
        if (!isGoingToTarget)
        {
            this.transform.Translate(Vector2.down * stepLength);
        }
    }

    private void MoveInHorizontalAxe()
    {
        GoTo(new Vector2(goingToPosX, this.transform.position.y));

        if(this.transform.position.x == goingToPosX)
        {
            ChangeHorizontalDirection();
        }
    }

    private void ChangeHorizontalDirection()
    {
        goingToPosX = (goingToPosX == pos1X) ? pos2X : pos1X;
    }

    private void GoToTarget()
    {
        GoTo(target.position);
    }

    private void GoTo(Vector2 goToPosition)
    {
        this.transform.position = Vector2.MoveTowards(this.transform.position, goToPosition, speed * Time.deltaTime);

        if (this.transform.position.y < minPosY) //enemy arrived to the line where the player is, then die
            Inactivate();
    }

    public override void Die()
    {
        Inactivate();
        ScoreController.Instance.AddReward(reward);
    }

    private void Inactivate()
    {
        isGoingToTarget = false;
        this.gameObject.SetActive(false);
    }

    public void SetSprite(Sprite sprite)
    {
        _renderer.sprite = sprite;
    }
}
