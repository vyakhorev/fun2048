using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.HID;

#nullable enable

namespace GameCoreController
{
    /*
     Stores grid with chips
     */
    public class ChipKeeper
    {
        private GridCell[,] _gridCells;
        private readonly int _X;
        private readonly int _Y;

        private float _cellSize;

        private List<AGridEffect> _effects;

        public ChipKeeper(int x, int y)
        {
            _X = x;
            _Y = y;
            _gridCells = new GridCell[_X, _Y];
            _effects = new List<AGridEffect>();
        }

        public void ResetCells()
        {
            for (int idx = 0; idx < _X; idx++)
            {
                for (int idy = 0; idy < _Y; idy++)
                {
                    _gridCells[idx, idy] = new GridCell(
                        new Vector2Int(idx, idy),
                        idx == 0 || idx == _X || idy == 0 || idy == _Y
                    );
                }
            }

            _effects.Add(
                new BoardResetEffect()
            );

        }

        public void SetCellSize(float newCellSize)
        {
            _cellSize = newCellSize;
        }

        public Vector2Int WorldToLogicalPos(Vector3 worldPos)
        {
            return new Vector2Int(
              Mathf.CeilToInt(worldPos.x / _cellSize),
              Mathf.CeilToInt(worldPos.z / _cellSize)
            );
        }

        public Vector3 LogicalToWorldPos(int x, int y)
        {
            return new Vector3(
              x * _cellSize,
              0f,
              y * _cellSize
            );
        }

        public void SetChip(int x, int y, AChip chip)
        {
            _gridCells[x, y].SetChip(chip);
        }

        public AChip? GetChip(int x, int y)
        {
            return _gridCells[x, y].GetChip();
        }

        public void ClearChip(int x, int y)
        {
            _gridCells[x, y].ClearChip();
        }

        public bool TrySpawnNewChipAtRandomPosition(AChip chip)
        {
            GridCell? cell = GetFreeRandomPosition();
            if (cell == null)
            {
                return false;
            }
            cell.SetChip(chip);

            _effects.Add(
                new ChipSpawnedEffect(chip, cell.GetCoords())
            );
            return true;
        }

        public GridCell? GetFreeRandomPosition()
        {
            var emptyGridCells = GetEmptyGridCellList();
            if (emptyGridCells.Count == 0)
            {
                return null;
            }

            int rndIdx = Utils.GlobalCtx
                .Instance
                .GetRandom()
                .Next(emptyGridCells.Count);
            
            return emptyGridCells[rndIdx];
        }

        public List<GridCell> GetEmptyGridCellList()
        {
            var emptyGridCells = new List<GridCell>();
            foreach (GridCell cell_i in _gridCells)
            {
                if (cell_i.IsEmpty())
                {
                    emptyGridCells.Add(cell_i);
                }
            }

            return emptyGridCells;

        }

        public List<AGridEffect> GetEffects()
        {
            return _effects;
        }

        public void ResetEffects() { 
            _effects = new List<AGridEffect>();
        }

        public void DoMergeInDirection(GridDirection gridDirection)
        {
            int safeStopCnt = 0;
            Debug.Log("Merging direction " + gridDirection);
            
            if (gridDirection == GridDirection.LEFT || gridDirection == GridDirection.RIGHT)
            {
                for (int line_y = 0; line_y < _Y; line_y++)
                {
                    while (ApplyMergeToLine(line_y, gridDirection)) 
                    {
                        Debug.Log("Merging line Y " + line_y);
                        safeStopCnt += 1;
                        if (safeStopCnt > 20)
                        {
                            throw new Exception("Safe stop Y");
                        }
                    };
                }
            }
            else if (gridDirection == GridDirection.DOWN || gridDirection == GridDirection.UP)
            {
                for (int line_x = 0; line_x < _X; line_x++)
                {
                    while (ApplyMergeToLine(line_x, gridDirection))
                    {
                        Debug.Log("Merging line X " + line_x);
                        safeStopCnt += 1;
                        if (safeStopCnt > 20)
                        {
                            throw new Exception("Safe stop X");
                        }
                    }
                }
            }
        }

        /*
         * Repeat until cannot move / merge anything in line
         */
        private bool ApplyMergeToLine(int lineNum, GridDirection gridDirection)
        {
            bool appliedSmth = false;
            GridCell? lastKnownNonEmptyCell = null;
            foreach (GridCell cell_i in GetLineElements(lineNum, gridDirection)) { 

                if (!cell_i.IsEmpty() && lastKnownNonEmptyCell == null)
                {
                    // Take the chip
                    lastKnownNonEmptyCell = cell_i;
                }
                else if (!cell_i.IsEmpty() && lastKnownNonEmptyCell != null)
                {
                    // Move or merge the chip
                    bool merged = false;

                    if (lastKnownNonEmptyCell.GetChip() is NumberChip numberChipFrom && 
                        cell_i.GetChip() is NumberChip numberChipTo)
                    {
                        if (numberChipFrom.GetNumericValue() == numberChipTo.GetNumericValue())
                        {
                            // Merge
                            numberChipTo.IncreaseNumericValue(numberChipFrom.GetNumericValue());

                            _effects.Add(
                                new ChipMoveEffect(
                                    lastKnownNonEmptyCell.GetChip(),
                                    cell_i.GetCoords()
                                )
                            );

                            _effects.Add(
                                new ChipNumberChangedEffect(
                                    numberChipFrom
                                )
                            );

                            _effects.Add(
                                new ChipsMergeEffect(
                                    numberChipFrom,
                                    numberChipTo
                                )
                            );


                            _effects.Add(
                                new ChipDeletedEffect(
                                    cell_i.GetChip()
                                )
                            );

                            lastKnownNonEmptyCell.ClearChip();
                            lastKnownNonEmptyCell = cell_i;
                            merged = true;
                            appliedSmth = true;
                        }
                    }

                    if (!merged)
                    {
                        // Move
                        GridCell? moveTo = GetReverseAdjecentCell(
                            cell_i.GetCoords(),
                            gridDirection
                        );
                        if (moveTo != null && moveTo != cell_i)
                        {
                            // If we have a place to move and this is a new cell
                            _effects.Add(
                                new ChipMoveEffect(
                                    lastKnownNonEmptyCell.GetChip(),
                                    cell_i.GetCoords()
                                )
                            );
                            appliedSmth = true;
                        }       
                    }
                }
            }
            return appliedSmth;
        }

        public List<GridCell> GetLineElements(int lineNum, GridDirection gridDirection)
        {
            // TODO - iterator
            List<GridCell> lineCells = new List<GridCell>();
            if (gridDirection == GridDirection.LEFT)
            {
                for (int idx = _X-1; idx >= 0; idx--)
                {
                    lineCells.Add(_gridCells[idx, lineNum]);
                }
            }
            else if (gridDirection == GridDirection.RIGHT)
            {
                for (int idx = 0; idx < _X; idx++)
                {
                    lineCells.Add(_gridCells[idx, lineNum]);
                }
            }
            else if(gridDirection == GridDirection.DOWN)
            {
                for (int idy = _Y-1; idy >= 0; idy--)
                {
                    lineCells.Add(_gridCells[lineNum, idy]);
                }
            }
            else // if (gridDirection == GridDirection.UP)
            {
                for (int idy = 0; idy < _Y; idy++)
                {
                    lineCells.Add(_gridCells[lineNum, idy]);
                }
            }

            return lineCells;

        }

        private GridCell? GetReverseAdjecentCell(Vector2Int pos, GridDirection gridDirection)
        {
            if (gridDirection == GridDirection.LEFT)
            {
                if (pos.x + 1 < _X) return _gridCells[pos.x + 1, pos.y];
                return null;
            } 
            else if (gridDirection == GridDirection.RIGHT)
            {
                if (pos.x - 1 >= 0) return _gridCells[pos.x - 1, pos.y];
                return null;
            }
            else if (gridDirection == GridDirection.DOWN)
            {
                if (pos.y + 1 < _Y) return _gridCells[pos.x, pos.y + 1];
                return null;
            }
            else // if (gridDirection == GridDirection.UP)
            {
                if (pos.y - 1 >= 0) return _gridCells[pos.x, pos.y - 1];
                return null;
            }
        }

 

        /*
         * startXorY - start line, X for left/right, Y for up/down
         */
        private GridCell TraceLineFromPos(int lineNum, int startXorY, GridDirection gridDirection)
        {
            if (gridDirection == GridDirection.LEFT)
            {
                for (int idx = startXorY; idx >= 0; idx--)
                {
                    if (!_gridCells[lineNum, idx].IsEmpty())
                    {
                        return _gridCells[lineNum, idx];
                    }
                }
                return _gridCells[lineNum, 0];
            }

            else if (gridDirection == GridDirection.RIGHT)
            {
                for (int idx = startXorY; idx < _X; idx++)
                {
                    if (!_gridCells[lineNum, idx].IsEmpty())
                    {
                        return _gridCells[lineNum, idx];
                    }
                }
                return _gridCells[lineNum, _X - 1];
            }

            else if (gridDirection == GridDirection.DOWN)
            {
                for (int idy = startXorY; idy >= 0; idy--)
                {
                    if (!_gridCells[idy, lineNum].IsEmpty())
                    {
                        return _gridCells[idy, lineNum];
                    }
                }
                return _gridCells[0, lineNum];
            }

            else // (gridDirection == GridDirection.UP)
            {
                for (int idy = startXorY; idy < _Y; idy++)
                {
                    if (!_gridCells[idy, lineNum].IsEmpty())
                    {
                        return _gridCells[idy, lineNum];
                    }
                }
                return _gridCells[_Y - 1, lineNum];
            }
        }

        /*
         * Check first occuring chip / edge in a line by direction
         */
        private GridCell TraceLineFromEdge(int lineNum, GridDirection gridDirection)
        {

            if (gridDirection == GridDirection.LEFT)
            {
                for (int idx = _X - 1; idx >= 0; idx--)
                {
                    if (!_gridCells[lineNum, idx].IsEmpty())
                    {
                        return _gridCells[lineNum, idx];
                    }
                }
                return _gridCells[lineNum, 0];
            }

            else if (gridDirection == GridDirection.RIGHT)
            {
                for (int idx = 0; idx < _X; idx++)
                {
                    if (!_gridCells[lineNum, idx].IsEmpty())
                    {
                        return _gridCells[lineNum, idx];
                    }
                }
                return _gridCells[lineNum, _X-1];
            }

            else if (gridDirection == GridDirection.DOWN)
            {
                for (int idy = _Y - 1; idy >= 0; idy--)
                {
                    if (!_gridCells[idy, lineNum].IsEmpty())
                    {
                        return _gridCells[idy, lineNum];
                    }
                }
                return _gridCells[0, lineNum];
            }

            else // (gridDirection == GridDirection.UP)
            {
                for (int idy = 0; idy < _Y; idy++)
                {
                    if (!_gridCells[idy, lineNum].IsEmpty())
                    {
                        return _gridCells[idy, lineNum];
                    }
                }
                return _gridCells[_Y-1, lineNum];
            }

        }



    }
}