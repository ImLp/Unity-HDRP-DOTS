namespace UnityTemplateProjects
{
	using UnityEngine;

	/// <summary>
	/// Debug Camera state.
	/// </summary>
	public class DebugCameraState
	{
		/// <summary>
		/// Gets or sets the Pitch of the Camera.
		/// </summary>
		public float Pitch { get; set; }

		/// <summary>
		/// Gets or sets the Roll of the Camera.
		/// </summary>
		public float Roll { get; set; }

		/// <summary>
		/// Gets or sets the X position of the Camera.
		/// </summary>
		public float X { get; set; }

		/// <summary>
		/// Gets or sets the Y position of the Camera.
		/// </summary>
		public float Y { get; set; }

		/// <summary>
		/// Gets or sets the Yaw of the Camera.
		/// </summary>
		public float Yaw { get; set; }

		/// <summary>
		/// Gets or sets the Z position of the camera.
		/// </summary>
		public float Z { get; set; }

		/// <summary>
		/// Directly apply the given transform to this Debug Camera State.
		/// </summary>
		/// <param name="t">The transform to apply.</param>
		public void SetFromTransform(Transform t)
		{
			Vector3 eulerAngles = t.eulerAngles;
			this.Pitch = eulerAngles.x;
			this.Yaw = eulerAngles.y;
			this.Roll = eulerAngles.z;

			Vector3 position = t.position;
			this.X = position.x;
			this.Y = position.y;
			this.Z = position.z;
		}

		/// <summary>
		/// Translate by the provided vector translation.
		/// </summary>
		/// <param name="translation">The translation to apply to the camera.</param>
		public void Translate(Vector3 translation)
		{
			Vector3 rotatedTranslation = Quaternion.Euler(this.Pitch, this.Yaw, this.Roll) * translation;

			this.X += rotatedTranslation.x;
			this.Y += rotatedTranslation.y;
			this.Z += rotatedTranslation.z;
		}

		/// <summary>
		/// Linearly interpolate a camera to a given target by a given position percentage and rotation percentage.
		/// </summary>
		/// <param name="target">The target object to interpolate to.</param>
		/// <param name="positionLerpPct">the percentage of interpolation to apply to the cameras position.</param>
		/// <param name="rotationLerpPct">The percentage of interpolation to apply.</param>
		public void LerpTowards(DebugCameraState target, float positionLerpPct, float rotationLerpPct)
		{
			this.Yaw = Mathf.Lerp(this.Yaw, target.Yaw, rotationLerpPct);
			this.Pitch = Mathf.Lerp(this.Pitch, target.Pitch, rotationLerpPct);
			this.Roll = Mathf.Lerp(this.Roll, target.Roll, rotationLerpPct);

			this.X = Mathf.Lerp(this.X, target.X, positionLerpPct);
			this.Y = Mathf.Lerp(this.Y, target.Y, positionLerpPct);
			this.Z = Mathf.Lerp(this.Z, target.Z, positionLerpPct);
		}

		/// <summary>
		/// Update a target transform with the current transform of this object.
		/// </summary>
		/// <param name="t">The target transform to update.</param>
		public void UpdateTransform(Transform t)
		{
			t.eulerAngles = new Vector3(this.Pitch, this.Yaw, this.Roll);
			t.position = new Vector3(this.X, this.Y, this.Z);
		}
	}
}
