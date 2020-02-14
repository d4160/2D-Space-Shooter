﻿namespace d4160.UI.Progress
{
	using UnityEngine;
	/// <summary>
	/// Simple behaviour to animate particles around to create a typical "Ajax Loader". this is actually very important to visual inform the user that something is happening
	/// or better say that the application is not frozen, so a animation of some sort helps reassuring the user that the system is idle and well.
	/// </summary>
	public class ParticlesProgressLoop : ProgressBarLoopBase
	{

		#region Public Variables

		[Tooltip("Angular Speed in degrees per seconds")]
		public float speed = 180f;

		[Tooltip("Radius os the loader")]
		public float radius = 1f;

		public GameObject particles;

		#endregion

		#region Private Variables

		Vector3 _offset;

		Transform _transform;

		Transform _particleTransform;

		bool _isAnimating;

		#endregion

		#region MonoBehaviour CallBacks

		/// <summary>
		/// MonoBehaviour method called on GameObject by Unity during early initialization phase.
		/// </summary>
		protected override void Awake()
		{
			base.Awake();

			// cache for efficiency
			_particleTransform = particles.GetComponent<Transform>();
			_transform = GetComponent<Transform>();
		}


		/// <summary>
		/// MonoBehaviour method called on GameObject by Unity on every frame.
		/// </summary>
		protected virtual void Update()
		{

			// only care about rotating particles if we are animating
			if (_isAnimating)
			{
				// we rotate over time. Time.deltaTime is mandatory to have a frame rate independant animation,
				_transform.Rotate(0f, 0f, speed * Time.deltaTime);

				// we move from the center to the desired radius to prevent the visual artifacts of particles jumping from their current spot, it's not very nice visually
				// so the particle is centered in the scene so that when it starts rotating, it doesn't jump and slowy we animate it to its final radius giving a smooth transition.
				_particleTransform.localPosition = Vector3.MoveTowards(_particleTransform.localPosition, _offset, 0.5f*Time.deltaTime);
			}
		}
		#endregion

		#region Public Methods

		/// <summary>
		/// Starts the loader animation. Becomes visible
		/// </summary>
		[ContextMenu("StartLoop")]
		public override void StartLoop()
		{
			_isAnimating = true;
			_offset = new Vector3(radius, 0f, 0f);
			particles.SetActive(true);
		}

		/// <summary>
		/// Stops the loader animation. Becomes invisible
		/// </summary>
        [ContextMenu("StopLoop")]
		public override void StopLoop()
		{
			particles.SetActive(false);
		}

		#endregion
	}
}