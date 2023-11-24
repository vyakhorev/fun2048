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

        private void ResetTurn()
        {
            foreach (GridCell cell_i in _gridCells)
            {
                cell_i.ResetTurn();
            }
        }

        public void DoMergeInDirection(GridDirection gridDirection)
        {
            const int max_recursion = 100;
            const int max_iters = 100;
            int iters = 0;
            ResetTurn();
            if (gridDirection == GridDirection.LEFT || gridDirection == GridDirection.RIGHT)
            {
                for (int line_y = 0; line_y < _Y; line_y++)
                {
                    // Line in the opposing to the swipe direction
                    List<GridCell> line = GetLineElements(line_y, gridDirection);
                    while (ApplyMergeToLine(line, 0, max_recursion))
                    {
                        iters += 1;
                        if (iters > max_iters) throw new Exception("max_iters");
                    }
                }
            }
            else if (gridDirection == GridDirection.DOWN || gridDirection == GridDirection.UP)
            {
                for (int line_x = 0; line_x < _X; line_x++)
                {
                    // Line in the opposing to the swipe direction
                    List<GridCell> line = GetLineElements(line_x, gridDirection);
                    while (ApplyMergeToLine(line, 0, max_recursion))
                    {
                        iters += 1;
                        if (iters > max_iters) throw new Exception("max_iters");
                    }
                }
            }
        }

        /*
         * Repeat until cannot move / merge anything in line
         */
        private bool ApplyMergeToLine(List<GridCell> line, int rd, int maxRd)
        {
            if (rd > maxRd) throw new Exception("max_recursion");

            GridCell? candidateCell = null;
            int candidateIdx = 0;
            for (int i = 0; i < line.Count; i++)
            {
                GridCell cell_i = line[i];
                if (!cell_i.IsEmpty() && cell_i.CanBeMovedThisTurn())
                {
                    candidateCell = cell_i;
                    candidateIdx = i;
                    break;
                }
            }

            if (candidateCell == null) { 
                // No candidates - switch to the next line / column
                // We may return false only once per line to indicate
                // that we're done with this line
                return false; 
            }

            if (candidateIdx == 0)
            {
                // This cell is facing an edge, cannot move futher
                // Switch to the next candidate
                candidateCell.SetCannotBeMovedThisTurn();
                return ApplyMergeToLine(line, rd+1, maxRd);
            }

            // Iterate backwards for each available spot
            for (int i = candidateIdx-1; i>=0; i--)
            {
                GridCell cell_i = line[i];
                // Reached the end of the line, move the chip
                if (cell_i.IsEmpty() && i == 0)
                {
                    DoMove(candidateCell, cell_i);
                    cell_i.SetCannotBeMovedThisTurn();
                    return true;
                }
                // A cell can be merged only once, no cascade merging in 2048
                else if (!cell_i.IsEmpty())
                {
                    if (!cell_i.MergedThisTurn())
                    {
                        // Try merging
                        bool merged = TryMerge(candidateCell, cell_i);
                        if (merged)
                        {
                            cell_i.SetMergedThisTurn();
                            return true;
                        }
                    }
                    // Failed to merge, try to move it close to the cell
                    if (i + 1 == candidateIdx)
                    {
                        // No movement, already close to another cell
                        candidateCell.SetCannotBeMovedThisTurn();
                        return true;
                    }
                    GridCell available_cell = line[i+1];
                    DoMove(candidateCell, available_cell);
                    available_cell.SetCannotBeMovedThisTurn();
                    return true;
                }

            }

            return false;
        }

        private void DoMove(GridCell cellFrom, GridCell cellTo)
        {
            AChip? chip = cellFrom.GetChip() ?? throw new Exception("nothing to move from cell " + cellFrom.GetCoords());
            if (!cellTo.IsEmpty())
            {
                throw new Exception("Attempt to move into non-empty cell from " + cellFrom.GetCoords() + " to " + cellTo.GetCoords());
            }
            _effects.Add(
                new ChipMoveEffect(
                    chip,
                    cellTo.GetCoords()
                )
            );
            cellTo.SetChip(chip);
            cellFrom.ClearChip();
        }

        private bool TryMerge(GridCell cellFrom, GridCell cellTo)
        {
            if (cellFrom == cellTo) throw new Exception("Attempt to merge cell with itself: " + cellFrom.GetCoords());
            AChip? chipFrom = cellFrom.GetChip() ?? throw new Exception("Attempt to merge an empty cell: " + cellFrom.GetCoords());
            AChip? chipTo = cellTo.GetChip() ?? throw new Exception("Attempt to merge with an empty cell: " + cellTo.GetCoords());
            
            if (chipFrom is NumberChip numberChipFrom &&
                chipTo is NumberChip numberChipTo &&
                numberChipFrom.GetNumericValue() == numberChipTo.GetNumericValue())
            {
                // Merge numeric cells
                numberChipFrom.IncreaseNumericValue(numberChipTo.GetNumericValue());

                _effects.Add(
                    new ChipMoveEffect(
                        chipFrom,
                        cellTo.GetCoords()
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
                        numberChipTo
                    )
                );

                cellTo.SetChip(numberChipFrom);
                cellFrom.ClearChip();
                return true;
            }
            return false;
        }

        private List<GridCell> GetLineElements(int lineNum, GridDirection gridDirection)
        {
            List<GridCell> lineCells = new List<GridCell>();
            if (gridDirection == GridDirection.RIGHT)
            {
                for (int idx = _X-1; idx >= 0; idx--)
                {
                    lineCells.Add(_gridCells[idx, lineNum]);
                }
            }
            else if (gridDirection == GridDirection.LEFT)
            {
                for (int idx = 0; idx < _X; idx++)
                {
                    lineCells.Add(_gridCells[idx, lineNum]);
                }
            }
            else if(gridDirection == GridDirection.UP)
            {
                for (int idy = _Y-1; idy >= 0; idy--)
                {
                    lineCells.Add(_gridCells[lineNum, idy]);
                }
            }
            else // if (gridDirection == GridDirection.DOWN)
            {
                for (int idy = 0; idy < _Y; idy++)
                {
                    lineCells.Add(_gridCells[lineNum, idy]);
                }
            }
            return lineCells;
        }

    }
}