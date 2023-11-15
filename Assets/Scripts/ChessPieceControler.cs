

using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;

namespace NRKernal.NRExamples
{
    /// <summary> A chess piece controler. </summary>
    public class ChessPieceControler : MonoBehaviour, IPointerClickHandler
    {
        private bool is_selected = false;
        private Quaternion rotation;
        private float timer;
        private bool isDestroyed = false;
        private float destroyCounter = 0.0f;
        private float placedCounter = 0.0f;
        private int layer;
        private bool justPlaced = false;
        private float scale = 0.01f;
        private float pieceLaserPosition = 1.0f;

        void Awake()
        {
            rotation = this.rotation;
            layer = this.gameObject.layer;
            Debug.Log(layer);
        }

        /// <summary> Updates this object. </summary>
        void Update()
        {
            if (is_selected)
            {
                
                // Get controller laser origin.
                var handControllerAnchor = NRInput.DomainHand == ControllerHandEnum.Left ? ControllerAnchorEnum.LeftLaserAnchor : ControllerAnchorEnum.RightLaserAnchor;
                Transform laserAnchor = NRInput.AnchorsHelper.GetAnchor(NRInput.RaycastMode == RaycastModeEnum.Gaze ? ControllerAnchorEnum.GazePoseTrackerAnchor : handControllerAnchor);

                pieceLaserPosition += Input.mouseScrollDelta.y * scale;

                transform.position = laserAnchor.transform.position + laserAnchor.transform.forward * pieceLaserPosition + new Vector3 (0,0.075f, 0);
                transform.rotation = rotation;

                RaycastHit hitResult;
                if (Physics.Raycast(new Ray(laserAnchor.transform.position, laserAnchor.transform.forward), out hitResult, 10, ~3 & 8 | 9) && NRInput.GetButtonDown(ControllerButton.TRIGGER))
                {
                    // place the chess piece at the pointer hit
                    this.transform.position = hitResult.point + new Vector3(0.0f, 0.075f, 0.0f);
                    this.transform.rotation = rotation;
                    is_selected = false;
                    this.gameObject.layer = layer;
                    justPlaced = true;
                }
            }

            if (justPlaced)
            {
                placedCounter += 0.1f;
                if (placedCounter > 2)
                {
                    justPlaced = false;
                    placedCounter = 0.0f;
                }
            }

            if (isDestroyed)
            {
                gameObject.GetComponent<Renderer>().material.SetFloat("_Weight", destroyCounter);
                destroyCounter += 0.01f;
                if (destroyCounter > 1)
                {
                    Destroy(this.gameObject);
                }
            }
        }

        // on Click Select the chess piece
        public void OnPointerClick(PointerEventData eventData)
        {
            if(!is_selected && !justPlaced)
            {
                is_selected = true;
                if (layer == 7)
                {
                    this.gameObject.layer = 9;
                }
                else
                {
                    this.gameObject.layer = 8;
                }
            }
        }

        void OnCollisionEnter(Collision Colider)
        {
            if (!is_selected && (layer == 6 && Colider.gameObject.layer == 9 || layer == 7 && Colider.gameObject.layer == 8))
            {
                Destroy(GetComponent<Rigidbody>(), 0.2f);
                isDestroyed = true;
            }
        }
    }
}