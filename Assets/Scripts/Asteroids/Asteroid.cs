using Unity.Collections;
using Unity.Jobs;
using UnityEngine;
using UnityEngine.Jobs;

public class Asteroid : MonoBehaviour
{
	[SerializeField]
	private int numberOfEntities;
	[SerializeField]
	private float startDistance;
	[SerializeField]
	private float startVelocity;
	[SerializeField]
	private float startMass;
	[SerializeField]
	private float gravitationModifier;
	[SerializeField]
	private GameObject celestialBodyPrefab;
	private NativeArray<Vector3> positions;
	private NativeArray<Vector3> velocities;
	private NativeArray<Vector3> accelerations;
	private NativeArray<float> masses;

	private TransformAccessArray transformAccessArray;
	private void Start()
	{
		positions = new NativeArray<Vector3>(numberOfEntities,
		Allocator.Persistent);
		velocities = new NativeArray<Vector3>(numberOfEntities,
		Allocator.Persistent);
		accelerations = new NativeArray<Vector3>(numberOfEntities,
		Allocator.Persistent);
		masses = new NativeArray<float>(numberOfEntities,
		Allocator.Persistent);
		Transform[] transforms = new Transform[numberOfEntities];
		for (int i = 0; i < numberOfEntities; i++)
		{
			positions[i] = Random.insideUnitSphere * Random.Range(0,
			startDistance);
			velocities[i] = Random.insideUnitSphere * Random.Range(0,
			startVelocity);
			accelerations[i] = Vector3.up;
			masses[i] = Random.Range(1, startMass);
			transforms[i] = Instantiate(celestialBodyPrefab,
			positions[i], Quaternion.identity).transform;
		}
		transformAccessArray = new TransformAccessArray(transforms);
	}

	private void Update()
	{
		GravitationJob gravitationJob = new GravitationJob()
		{
			Positions = positions,
			Velocities = velocities,
			Accelerations = accelerations,
			Masses = masses,
			GravitationModifier = gravitationModifier,
			DeltaTime = Time.deltaTime
		};

		JobHandle gravitationHandle = gravitationJob.Schedule(numberOfEntities, 0);
		gravitationHandle.Complete();
	}

	private void OnDestroy()
	{
		positions.Dispose();
		velocities.Dispose();
		accelerations.Dispose();
		masses.Dispose();
		transformAccessArray.Dispose();
	}
}
