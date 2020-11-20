namespace UnityTemplateProjects
{
	using UnityEditor;
	using UnityEngine;

	/// <summary>
	/// A simple debug camera controller that can be attached to do fly-cam style navigation on a scene.
	/// </summary>
	public class DebugCameraController : MonoBehaviour
	{
		private readonly DebugCameraState interpolatingCameraState = new DebugCameraState();

		private readonly DebugCameraState targetCameraState = new DebugCameraState();

		[SerializeField,]
		[Header("Movement Settings"),]
		[Tooltip("Exponential boost factor on translation, controllable by mouse wheel."),]
		private float boost = 3.5f;

		[SerializeField,]
		[Tooltip("Whether or not to invert our Y axis for mouse input to rotation."),]
		private bool invertY;

		[SerializeField,]
		[Header("Rotation Settings"),]
		[Tooltip("X = Change in mouse position.\nY = Multiplicative factor for camera rotation."),]
		private AnimationCurve mouseSensitivityCurve =
			new AnimationCurve(new Keyframe(0f, 0.5f, 0f, 5f), new Keyframe(1f, 2.5f, 0f, 0f));

		[SerializeField,]
		[Tooltip("Time it takes to interpolate camera position 99% of the way to the target."),]
		[Range(0.001f, 1f),]
		private float positionLerpTime = 0.2f;

		[SerializeField]
		[Tooltip("Time it takes to interpolate camera rotation 99% of the way to the target."),]
		[Range(0.001f, 1f),]
		private float rotationLerpTime = 0.01f;

		private void OnEnable()
		{
			this.targetCameraState.SetFromTransform(this.transform);
			this.interpolatingCameraState.SetFromTransform(this.transform);
		}

		private Vector3 GetInputTranslationDirection()
		{
			var direction = default(Vector3);
			if (Input.GetKey(KeyCode.W))
			{
				direction += Vector3.forward;
			}

			if (Input.GetKey(KeyCode.S))
			{
				direction += Vector3.back;
			}

			if (Input.GetKey(KeyCode.A))
			{
				direction += Vector3.left;
			}

			if (Input.GetKey(KeyCode.D))
			{
				direction += Vector3.right;
			}

			if (Input.GetKey(KeyCode.Q))
			{
				direction += Vector3.down;
			}

			if (Input.GetKey(KeyCode.E))
			{
				direction += Vector3.up;
			}

			return direction;
		}

		private void Update()
		{
#if ENABLE_LEGACY_INPUT_MANAGER

			// Exit Sample
			if (Input.GetKey(KeyCode.Escape))
			{
				Application.Quit();
#if UNITY_EDITOR
				EditorApplication.isPlaying = false;
#endif
			}

			// Hide and lock cursor when right mouse button pressed
			if (Input.GetMouseButtonDown(1))
			{
				Cursor.lockState = CursorLockMode.Locked;
			}

			// Unlock and show cursor when right mouse button released
			if (Input.GetMouseButtonUp(1))
			{
				Cursor.visible = true;
				Cursor.lockState = CursorLockMode.None;
			}

			// Rotation
			if (Input.GetMouseButton(1))
			{
				var mouseMovement = new Vector2(
				    Input.GetAxis("Mouse X"),
				    Input.GetAxis("Mouse Y") * (this.invertY ? 1 : -1));

				float mouseSensitivityFactor = this.mouseSensitivityCurve.Evaluate(mouseMovement.magnitude);

				this.targetCameraState.Yaw += mouseMovement.x * mouseSensitivityFactor;
				this.targetCameraState.Pitch += mouseMovement.y * mouseSensitivityFactor;
			}

			// Translation
			Vector3 translation = this.GetInputTranslationDirection() * Time.deltaTime;

			// Speed up movement when shift key held
			if (Input.GetKey(KeyCode.LeftShift))
			{
				translation *= 10.0f;
			}

			// Modify movement by a boost factor (defined in Inspector and modified in play mode through the mouse scroll wheel)
			this.boost += Input.mouseScrollDelta.y * 0.2f;
			translation *= Mathf.Pow(2.0f, this.boost);

#elif USE_INPUT_SYSTEM
			// TODO: make the new input system work
#endif

			this.targetCameraState.Translate(translation);

			// Framerate-independent interpolation
			// Calculate the lerp amount, such that we get 99% of the way to our target in the specified time
			float positionLerpPct = 1f - Mathf.Exp(Mathf.Log(1f - 0.99f) / this.positionLerpTime * Time.deltaTime);
			float rotationLerpPct = 1f - Mathf.Exp(Mathf.Log(1f - 0.99f) / this.rotationLerpTime * Time.deltaTime);
			this.interpolatingCameraState.LerpTowards(this.targetCameraState, positionLerpPct, rotationLerpPct);

			this.interpolatingCameraState.UpdateTransform(this.transform);
		}
	}
}
