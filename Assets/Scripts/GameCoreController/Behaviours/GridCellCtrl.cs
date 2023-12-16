using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameCoreController
{
    public class GridCellCtrl : MonoBehaviour
    {
        private CmpBackgroundCellVisuals _backgroundCellVisuals;
        private CmpGrassLevel1Visuals _grassLevel1Visuals;
        private CmpGrassLevel2Visuals _grassLevel2Visuals;
        private CmpHoneyVisuals _honeyVisuals;
        private CmpScalableVisuals _scalableVisuals;

        public void InitHierarchy()
        {
            _backgroundCellVisuals = GetComponentInChildren<CmpBackgroundCellVisuals>(true);
            if (_backgroundCellVisuals == null) throw new System.Exception("no BackgroundCellVisuals");
            _grassLevel1Visuals = GetComponentInChildren<CmpGrassLevel1Visuals>(true);
            if (_grassLevel1Visuals == null) throw new System.Exception("no GrassLevel1Visuals");
            _grassLevel2Visuals = GetComponentInChildren<CmpGrassLevel2Visuals>(true);
            if (_grassLevel2Visuals == null) throw new System.Exception("no GrassLevel2Visuals");
            _honeyVisuals = GetComponentInChildren<CmpHoneyVisuals>(true);
            if (_honeyVisuals == null) throw new System.Exception("no HoneyVisuals");
            _scalableVisuals = GetComponentInChildren<CmpScalableVisuals>(true);
            if (_scalableVisuals == null) throw new System.Exception("no ScalableVisuals");

            _scalableVisuals.gameObject.SetActive(true);
            _backgroundCellVisuals.gameObject.SetActive(true);
            _grassLevel1Visuals.gameObject.SetActive(false);
            _grassLevel2Visuals.gameObject.SetActive(false);
            _honeyVisuals.gameObject.SetActive(false);

        }

        public void SetCellEnabled()
        {
            transform.localScale = Vector3.one;
        }

        public void SetEvenBackgroundColor(Vector2Int xy)
        {
            int m = (xy.x + xy.y) % 2;
            string variant;
            if (m == 0)
            {
                variant = "varA";
            } 
            else
            {
                variant = "varB";
            }

            foreach (Transform backgroundVariant in _backgroundCellVisuals.transform)
            {
                if (backgroundVariant.gameObject.name == variant)
                {
                    backgroundVariant.gameObject.SetActive(true);
                } 
                else
                {
                    backgroundVariant.gameObject.SetActive(false);
                }
            }
        }

        public void SetCellDisabled()
        {
            transform.localScale = Vector3.zero;
        }

        public void RemoveGrass(Sequence tweenSeq)
        {
            if (_grassLevel1Visuals.gameObject.activeInHierarchy)
            {
                tweenSeq.Insert(
                    0f,
                    _grassLevel1Visuals.transform.DOScale(
                        Vector3.zero,
                        0.2f
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
                        0.2f
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
                        0.2f
                    )
                );
            }

            if (_grassLevel2Visuals.gameObject.activeInHierarchy)
            {
                tweenSeq.Insert(
                    0f,
                    _grassLevel2Visuals.transform.DOScale(
                        Vector3.zero,
                        0.2f
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
                        0.2f
                    )
                );
            }

            if (_grassLevel1Visuals.gameObject.activeInHierarchy)
            {
                tweenSeq.Insert(
                    0f,
                    _grassLevel1Visuals.transform.DOScale(
                        Vector3.zero,
                        0.2f
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
                        0.2f
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
                    _honeyVisuals.transform.DOScale(
                        Vector3.one,
                        0.2f
                    )
                );
            }
        }

    }
}
