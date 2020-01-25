using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPathing : MonoBehaviour
{
    [SerializeField] WaveConfig waveConfig;
    List<Transform> waypoints;
    int waypointIdx = 0;

    void Start()
    {
        waypoints = waveConfig.GetWayPoints();
        transform.position = waypoints[waypointIdx].transform.position;
    }

    void Update()
    {
        Move();
    }

    public void SetWaveConfig(WaveConfig waveConfig)
    {
        this.waveConfig = waveConfig;
    }

    private void Move()
    {
        if (waypointIdx <= waypoints.Count - 1)
        {
            var targetPosition = waypoints[waypointIdx].transform.position;
            var movementThisFrame = waveConfig.GetMoveSpeed() * Time.deltaTime;
            transform.position = Vector2.MoveTowards(
                transform.position,
                targetPosition,
                movementThisFrame
            );
            if (transform.position == targetPosition)
            {
                waypointIdx++;
            }
        }    
        else
        {
            Destroy(gameObject);
        }

    }
}
