using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameCoreController
{
    public class GridCellCtrl : MonoBehaviour
    {
        private BackgroundCellVisuals _backgroundCellVisuals;
        private GrassLevel1Visuals _grassLevel1Visuals;
        private GrassLevel2Visuals _grassLevel2Visuals;
        private HoneyVisuals _honeyVisuals;
        private ScalableVisuals _scalableVisuals;
        private float _animSpeed;

        public void InitHierarchy(float animSpeed)
        {
            _backgroundCellVisuals = GetComponentInChildren<BackgroundCellVisuals>(true);
            if (_backgroundCellVisuals == null) throw new System.Exception("no BackgroundCellVisuals");
            _grassLevel1Visuals = GetComponentInChildren<GrassLevel1Visuals>(true);
            if (_grassLevel1Visuals == null) throw new System.Exception("no GrassLevel1Visuals");
            _grassLevel2Visuals = GetComponentInChildren<GrassLevel2Visuals>(true);
            if (_grassLevel2Visuals == null) throw new System.Exception("no GrassLevel2Visuals");
            _honeyVisuals = GetComponentInChildren<HoneyVisuals>(true);
            if (_honeyVisuals == null) throw new System.Exception("no HoneyVisuals");
            _scalableVisuals = GetComponentInChildren<ScalableVisuals>(true);
            if (_scalableVisuals == null) throw new System.Exception("no ScalableVisuals");
            _animSpeed = animSpeed;

            _scalableVisuals.gameObject.SetActive(true);
            _backgroundCellVisuals.gameObject.SetActive(true);
            _grassLevel1Visuals.gameObject.SetActive(false);
            _grassLevel2Visuals.gameObject.SetActive(false);
            _honeyVisuals.gameObject.SetActive(false);

        }

        public void SetCellEnabled(Sequence tweenSeq)
        {
            tweenSeq.Insert(
                0f,
                transform.DOScale(
                    Vector3.one,
                    _animSpeed
                )
            );
        }

        public void SetCellDisabled(Sequence tweenSeq)
        {
            tweenSeq.Insert(
                0f,
                transform.DOScale(
                    Vector3.zero,
                    _animSpeed
                )
            );
        }

        public void RemoveGrass(Sequence tweenSeq)
        {
            if (_grassLevel1Visuals.gameObject.activeInHierarchy)
            {
                tweenSeq.Insert(
                    0f,
                    _grassLevel1Visuals.transform.DOScale(
                        Vector3.zero,
                        _animSpeed
                    ).OnComplete(() => { 
                        _grassLevel1Visuals.gameObject.SetActive(false);  
                    })
                );
            }

            if (_grassLevel2Visuals.gameObject.activeInHierarchy)
            {
                tweenSeq.Insert(
                    0f,
                    _grassLevel2Visuals.transform.DOScale(
                        Vector3.zero,
                        _animSpeed
                    ).OnComplete(() => {
                        _grassLevel2Visuals.gameObject.SetActive(false);
                    })
                );
            }

        }

        public void SetGrassLevel1(Sequence tweenSeq)
        {
            if (!_grassLevel1Visuals.gameObject.activeInHierarchy)
            {
                _grassLevel1Visuals.gameObject.SetActive(true);
                tweenSeq.Insert(
                    0f,
                    _grassLevel1Visuals.transform.DOScale(
                        Vector3.one,
                        _animSpeed
                    )
                );
            }

            if (_grassLevel2Visuals.gameObject.activeInHierarchy)
            {
                tweenSeq.Insert(
                    0f,
                    _grassLevel2Visuals.transform.DOScale(
                        Vector3.zero,
                        _animSpeed
                    ).OnComplete(() => {
                        _grassLevel2Visuals.gameObject.SetActive(false);
                    })
                );
            }
        }

        public void SetGrassLevel2(Sequence tweenSeq)
        {
            if (!_grassLevel2Visuals.gameObject.activeInHierarchy)
            {
                _grassLevel2Visuals.gameObject.SetActive(true);
                tweenSeq.Insert(
                    0f,
                    _grassLevel2Visuals.transform.DOScale(
                        Vector3.one,
                        _animSpeed
                    )
                );
            }

            if (_grassLevel1Visuals.gameObject.activeInHierarchy)
            {
                tweenSeq.Insert(
                    0f,
                    _grassLevel1Visuals.transform.DOScale(
                        Vector3.zero,
                        _animSpeed
                    ).OnComplete(() => {
                        _grassLevel1Visuals.gameObject.SetActive(false);
                    })
                );
            }
        }

        public void RemoveHoney(Sequence tweenSeq)
        {
            if (_honeyVisuals.gameObject.activeInHierarchy)
            {
                tweenSeq.Insert(
                    0f,
                    _honeyVisuals.transform.DOScale(
                        Vector3.zero,
                        _animSpeed
                    ).OnComplete(() => {
                        _honeyVisuals.gameObject.SetActive(false);
                    })
                );
            }
        }

        public void SetHoney(Sequence tweenSeq)
        {
            if (!_honeyVisuals.gameObject.activeInHierarchy)
            {
                _honeyVisuals.gameObject.SetActive(true);
                tweenSeq.Insert(
                    0f,
                    _grassLevel1Visuals.transform.DOScale(
                        Vector3.zero,
                        _animSpeed
                    )
                );
            }
        }

    }
}
