using LevelData;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

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
        private int _lastChipId;

        private float _cellSize;

        private List<AGridEffect> _effects;

        private List<NumberChip> _mergedThisTurn;
        private HashSet<int> _chipsDeletedThisTurn;

        public ChipKeeper(int x, int y)
        {
            _X = x;
            _Y = y;
            _gridCells = new GridCell[_X, _Y];
            _effects = new List<AGridEffect>();
            _mergedThisTurn = new List<NumberChip>();
            _chipsDeletedThisTurn = new HashSet<int>();
        }

        public void ResetLevel()
        {
            _lastChipId = 0;
            for (int idx = 0; idx < _X; idx++)
            {
                for (int idy = 0; idy < _Y; idy++)
                {
                    GridCell cell = new GridCell(
                        new Vector2Int(idx, idy)
                    );
                    cell.Enable();
                    cell.SetGrassHealth(0);
                    cell.SetHoneyHealth(0);
                    _gridCells[idx, idy] = cell;
                }
            }

            _effects.Add(
                new BoardResetEffect()
            );

        }

        public GridCell GetGridCell(int x, int y)
        {
            return _gridCells[x, y];
        }

        public void ReportEffect(AGridEffect effect)
        {
            _effects.Add(effect);
        }

        public int GetNextChipId() {
            _lastChipId += 1;
            return _lastChipId; 
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

        public bool TrySpawnNewNumberChipAtRandomPosition()
        {
            NumberChip newChip = new NumberChip(2);
            newChip.SetChipId(GetNextChipId());

            GridCell? cell = GetFreeRandomPosition();
            if (cell == null)
            {
                return false;
            }
            cell.SetChip(newChip);

            _effects.Add(
                new ChipSpawnedEffect(newChip, cell.GetCoords())
            );
            return true;
        }

        public GridCell? GetFreeRandomPosition()
        {
            // TODO - LINQ
            var emptyGridCells = GetEmptyGridCellList();
            if (emptyGridCells.Count == 0)
            {
                return null;
            }

            int rndIdx = CoreUtils.GlobalCtx
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
                if (cell_i.IsFullyAvailable())
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
            _mergedThisTurn = new List<NumberChip>();
            _chipsDeletedThisTurn = new HashSet<int>();
        }

        public bool DoMergeInDirection(GridDirection gridDirection)
        {
            const int max_recursion = 100;
            const int max_iters = 100;
            int iters = 0;
            ResetTurn();
            bool atLeastOneChange = false;

            if (gridDirection == GridDirection.LEFT || gridDirection == GridDirection.RIGHT)
            {
                for (int line_y = 0; line_y < _Y; line_y++)
                {
                    // Line in the opposing to the swipe direction
                    List<GridCell> line = GetLineElements(line_y, gridDirection);
                    while (ApplyMergeToLine(line, 0, max_recursion))
                    {
                        iters += 1;
                        atLeastOneChange = true;
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
                        atLeastOneChange = true;
                        if (iters > max_iters) throw new Exception("max_iters");
                    }
                }
            }

            ApplySpawnBombRule();
            ApplySpawnNewHoneyRule();

            return atLeastOneChange;
        }

        public bool DoInteractionAt(Vector2Int at)
        {
            if (!(at.x >= 0 && at.x < _X && at.y >= 0 && at.y <_Y))
            {
                return false;
            }

            GridCell cell = _gridCells[at.x, at.y];
            if (cell.GetChip() is BombChip bombChip)
            {
                ResetTurn();
                DoDamageToBombChip(cell, bombChip);
                ActivateBomb(cell);
                ApplySpawnNewHoneyRule();
                return true;
            }
            return false;
        }

        /*
         * Repeat until cannot move / merge anything in line
         */
        private bool ApplyMergeToLine(List<GridCell> line, int rd, int maxRd)
        {
            if (rd > maxRd) throw new Exception("max_recursion");

            for (int i = 0; i < line.Count; i++)
            {
                GridCell cell_i = line[i];
                if (cell_i.IsHoney() && cell_i.CanBeMovedThisTurn())
                {
                    // Number chips trapped in honey cannot be moved the same turn
                    cell_i.SetCannotBeMovedThisTurn();
                    return ApplyMergeToLine(line, rd + 1, maxRd);
                }
            }

            GridCell? candidateCell = null;
            int candidateIdx = 0;
            for (int i = 0; i < line.Count; i++)
            {
                GridCell cell_i = line[i];
                if (!cell_i.IsEmpty() && cell_i.CanBeMovedThisTurn() && cell_i.IsEnabled())
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

            if (candidateCell.HasImmovableChip())
            {
                // This cannot be moved, go to the next one
                candidateCell.SetCannotBeMovedThisTurn();
                return ApplyMergeToLine(line, rd + 1, maxRd);
            }

            // Iterate backwards for each available spot
            for (int i = candidateIdx-1; i>=0; i--)
            {
                GridCell cell_i = line[i];
                // Reached the end of the line, move the chip
                if (cell_i.IsEmpty() && i == 0 && !cell_i.IsHoney() && cell_i.IsEnabled())
                {
                    DoMove(candidateCell, cell_i);
                    cell_i.SetCannotBeMovedThisTurn();
                    return true;
                }
                else if (!cell_i.IsEmpty() && cell_i.IsEnabled() && !cell_i.IsHoney())
                {
                    // A cell can be merged only once, no cascade merging in 2048
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
                else if (!cell_i.IsEnabled())
                {
                    // We cannot merge with a disabled cell
                    if (i + 1 == candidateIdx)
                    {
                        // No movement, already close to another cell
                        candidateCell.SetCannotBeMovedThisTurn();
                        return true;
                    }
                    GridCell available_cell = line[i + 1];
                    DoMove(candidateCell, available_cell);
                    available_cell.SetCannotBeMovedThisTurn();
                    return true;
                }
                else if (cell_i.IsHoney() && cell_i.IsEnabled())
                {
                    // Try to damage / destroy the honey 
                    DoDamageToHoney(cell_i);
                    // Check if there is still honey left
                    if (cell_i.IsHoney())
                    {
                        // Considered the asme as a disabled cell
                        if (i + 1 == candidateIdx)
                        {
                            // No movement, already close to another cell
                            candidateCell.SetCannotBeMovedThisTurn();
                            return true;
                        }
                        GridCell available_cell = line[i + 1];
                        DoMove(candidateCell, available_cell);
                        available_cell.SetCannotBeMovedThisTurn();
                        return true;
                    }
                    else
                    {
                        // Honey is gone, however the cell may be non-empty.
                        // Apply general movement to the line once more.
                        return ApplyMergeToLine(line, rd + 1, maxRd);

                        //DoMove(candidateCell, cell_i);
                        //cell_i.SetCannotBeMovedThisTurn();
                        //return true;
                    }
                }

            }

            return false;
        }

        private void ActivateBomb(GridCell gridCell)
        {
            List<GridCell> otherBombs = new List<GridCell>();
            foreach (var cell in GetRadius(gridCell.GetCoords(), true))
            {
                // Priority for the bomb
                if (cell.IsHoney())
                {
                    DoDamageToHoney(cell);
                }
                else if (cell.GetChip() is EggChip eggChip)
                {
                    DoDamageToEggChip(cell, eggChip);
                }
                else if (cell.GetChip() is BoxChip boxChip)
                {
                    DoDamageToBoxChip(cell, boxChip);
                }
                else if (cell.GetChip() is BombChip otherBombChip)
                {
                    // This shall remove bomb from the cell so no
                    // recursive loop occurs.
                    DoDamageToBombChip(cell, otherBombChip);
                    otherBombs.Add(cell);
                }
                else if (cell.IsGrass())
                {
                    DoDamageToGrass(cell);
                }
                else if (cell.GetChip() is NumberChip numberChip)
                {
                    DoDamageToNumberChip(cell, numberChip);
                }
            }

            foreach (var cell in otherBombs)
            {
                ActivateBomb(cell);
            }

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
            // Safer to clear grass after confirmed movement, not during
            // the line traverse (who knows what can block the movement
            // in our futher game design).
            ClearGrass(cellFrom.GetCoords(), cellTo.GetCoords());

            // Move the chip on logical level.
            cellTo.SetChip(chip);
            cellFrom.ClearChip();

            // If we moved a bomb - detonate it!
            if (chip is BombChip bombChip)
            {
                DoDamageToBombChip(cellTo, bombChip);
                ActivateBomb(cellTo);
            }

        }

        private void ClearGrass(Vector2Int cellFromCoords, Vector2Int cellToCoords)
        {
            int minX = Math.Min(cellFromCoords.x, cellToCoords.x);
            int maxX = Math.Max(cellFromCoords.x, cellToCoords.x);
            int minY = Math.Min(cellFromCoords.y, cellToCoords.y);
            int maxY = Math.Max(cellFromCoords.y, cellToCoords.y);

            for (int x=minX; x<=maxX; x++)
            {
                for (int y=minY; y<=maxY; y++)
                {
                    GridCell cell_i = _gridCells[x, y];
                    DoDamageToGrass(cell_i);
                }
            }
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
                // Main case - merge numeric cells
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

                _chipsDeletedThisTurn.Add(numberChipTo.GetChipId());

                // Check merge effects on neighbours - clear honey, eggs, stones
                foreach (GridCell cellNeigh in GetNeighbours(cellTo.GetCoords(), true))
                {
                    // Eggs and stones inside honey are not destroyed the same turn
                    if (cellNeigh.IsHoney())
                    {
                        DoDamageToHoney(cellNeigh);
                    }
                    else if (cellNeigh.GetChip() is EggChip eggChip)
                    {
                        DoDamageToEggChip(cellNeigh, eggChip);
                    }
                    else if (cellNeigh.GetChip() is BoxChip boxChip)
                    {
                        DoDamageToBoxChip(cellNeigh, boxChip);
                    }
                }

                // Clear grass on the way
                ClearGrass(cellFrom.GetCoords(), cellTo.GetCoords());

                _mergedThisTurn.Add(numberChipFrom);
                cellTo.SetChip(numberChipFrom);
                cellFrom.ClearChip();
                return true;
            }
            else if (chipFrom is NumberChip numberChipFromNoMerge &&
                chipTo is EggChip eggChip)
            {
                // Minor case - numbers can be merged into eggs and honey,
                // however, no area effect in this case

                eggChip.DecreaseHealth();

                if (eggChip.IsAlive())
                {
                    // TODO - an event indicating that we're breaking an egg
                    // We'll move the number chip anyway

                    _effects.Add(
                        new ChipHealthChangeEffect(
                            eggChip,
                            eggChip.GetHealth()
                        )
                    );

                    return false;
                }

                _effects.Add(
                    new ChipMoveEffect(
                        numberChipFromNoMerge,
                        cellTo.GetCoords()
                    )
                );

                _effects.Add(
                    new ChipDeletedEffect(
                        eggChip
                    )
                );
                _chipsDeletedThisTurn.Add(eggChip.GetChipId());

                // Clear grass on the way
                ClearGrass(cellFrom.GetCoords(), cellTo.GetCoords());

                cellTo.SetChip(numberChipFromNoMerge);
                cellFrom.ClearChip();
                return true;
            }

            return false;
        }

        private void DoDamageToBombChip(GridCell cell, BombChip bombChip)
        {
            cell.ClearChip();
            _effects.Add(
                new BombActivationEffect(
                    bombChip
                )
            );
            _effects.Add(
                new ChipDeletedEffect(
                    bombChip
                )
            );
            _chipsDeletedThisTurn.Add(bombChip.GetChipId());
        }

        private void DoDamageToNumberChip(GridCell cell, NumberChip numberChip)
        {
            cell.ClearChip();
            _effects.Add(
                new ChipDeletedEffect(
                    numberChip
                )
            );
            _chipsDeletedThisTurn.Add(numberChip.GetChipId());
        }

        private void DoDamageToGrass(GridCell cell)
        {
            if (cell.IsGrass())
            {
                cell.DecreaseGrassHealth();
                _effects.Add(
                    new GrassHealthChangeEffect(
                        cell.GetCoords(),
                        cell.GetGrassHealth()
                    )
                );
            }
        }

        private void DoDamageToHoney(GridCell cell)
        {
            if (cell.IsHoney())
            {
                cell.DecreaseHoneyHealth();
                _effects.Add(
                    new HoneyHealthChangeEffect(
                        cell.GetCoords(),
                        cell.GetHoneyHealth(),
                        false
                    )
                );
            }
        }

        private void DoDamageToEggChip(GridCell cell, EggChip eggChip)
        {
            eggChip.DecreaseHealth();
            if (!eggChip.IsAlive())
            {
                cell.ClearChip();
                _effects.Add(
                    new ChipDeletedEffect(
                        eggChip
                    )
                );
                _chipsDeletedThisTurn.Add(eggChip.GetChipId());
            }
            else
            {
                _effects.Add(
                    new ChipHealthChangeEffect(
                        eggChip,
                        eggChip.GetHealth()
                    )
                );
            }
        }

        private void DoDamageToBoxChip(GridCell cell, BoxChip boxChip)
        {
            boxChip.DecreaseHealth();
            if (!boxChip.IsAlive())
            {
                cell.ClearChip();
                _effects.Add(
                    new ChipDeletedEffect(
                        boxChip
                    )
                );
                _chipsDeletedThisTurn.Add(boxChip.GetChipId());
            }
            else
            {
                _effects.Add(
                    new ChipHealthChangeEffect(
                        boxChip,
                        boxChip.GetHealth()
                    )
                );
            }
        }

        private void ApplySpawnBombRule()
        {
            if (_mergedThisTurn.Count <= 1)
            {
                return;
            }
            // Find highest merges
            int bestNum = 0;
            foreach (NumberChip numbChip in _mergedThisTurn)
            {
                if (numbChip.GetNumericValue() > bestNum && 
                    !_chipsDeletedThisTurn.Contains(numbChip.GetChipId()))
                {
                    bestNum = numbChip.GetNumericValue();
                }
            }

            List<NumberChip> candidates = new List<NumberChip>();
            foreach (NumberChip numbChip in _mergedThisTurn)
            {
                if (numbChip.GetNumericValue() == bestNum &&
                    !_chipsDeletedThisTurn.Contains(numbChip.GetChipId()))
                {
                    candidates.Add(numbChip);
                }
            }

            if (candidates.Count == 0)
            {
                return;  // Every candidate is already destroyed
            }

            int rndIdx = CoreUtils.GlobalCtx
                        .Instance
                        .GetRandom()
                        .Next(candidates.Count);

            NumberChip chipToSwapToBomb = candidates[rndIdx];

            GridCell? cellToSpawnBomb = FindCellByChipId(chipToSwapToBomb.GetChipId());
            if (cellToSpawnBomb == null)
            {
                Debug.LogError("Attempt to spawn a bomb instead of non-existing chip");
                return;
            }

            if (!cellToSpawnBomb.IsEmpty())
            {
                _effects.Add(
                    new ChipDeletedEffect(
                        cellToSpawnBomb.GetChip()
                    )
                );
                cellToSpawnBomb.ClearChip();
            }

            BombChip bombChip = new BombChip();
            bombChip.SetChipId(GetNextChipId());
            cellToSpawnBomb.SetChip(
                bombChip
            );
            _effects.Add(
                new ChipSpawnedEffect(
                    bombChip,
                    cellToSpawnBomb.GetCoords()
                )
            );

        }

        private void ApplySpawnNewHoneyRule()
        {
            Dictionary<Vector2Int, GridCell> candidates = new Dictionary<Vector2Int, GridCell>();
            foreach (GridCell cell in _gridCells)
            {
                foreach (GridCell neighCell in GetNeighbours(cell.GetCoords(), false))
                {
                    if (neighCell.IsEnabled() && !neighCell.IsHoney())
                    {
                        candidates.TryAdd(neighCell.GetCoords(), neighCell);
                    }
                }
            }

            List<GridCell> candidatesList = candidates.Values.ToList();

            if (candidatesList.Count == 0)
            {
                return;  // No place to spawn new honey
            }

            int rndIdx = CoreUtils.GlobalCtx
                        .Instance
                        .GetRandom()
                        .Next(candidatesList.Count);

            GridCell cellToSpawnHoney = candidatesList[rndIdx];

            int newHoneyHealth = 1;
            cellToSpawnHoney.SetHoneyHealth(newHoneyHealth);
            ReportEffect(
                new HoneyHealthChangeEffect(
                    cellToSpawnHoney.GetCoords(),
                    newHoneyHealth,
                    true
                )
            );

        }

        private GridCell? FindCellByChipId(int chipId)
        {
            foreach (GridCell cell in _gridCells)
            {
                if (!cell.IsEmpty() && cell.GetChipEnsure().GetChipId() == chipId)
                {
                    return cell;
                }
            }
            return null;
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

        private List<GridCell> GetNeighbours(Vector2Int coords, bool includeSelf)
        {
            var neigh = new List<GridCell>();
            if (coords.x - 1 >= 0)
            {
                neigh.Add(_gridCells[coords.x - 1, coords.y]);
            }
            if (coords.x + 1 < _X)
            {
                neigh.Add(_gridCells[coords.x + 1, coords.y]);
            }
            if (coords.y - 1 >= 0)
            {
                neigh.Add(_gridCells[coords.x, coords.y - 1]);
            }
            if (coords.y + 1 < _Y)
            {
                neigh.Add(_gridCells[coords.x, coords.y + 1]);
            }
            if (includeSelf)
            {
                neigh.Add(_gridCells[coords.x, coords.y]);
            }
            return neigh;
        }

        private List<GridCell> GetRadius(Vector2Int coords, bool includeSelf)
        {
            var neigh = new List<GridCell>();
            if (coords.x - 1 >= 0 && coords.y - 1 >= 0)
            {
                neigh.Add(_gridCells[coords.x - 1, coords.y - 1]);
            }
            if (coords.x - 1 >= 0)
            {
                neigh.Add(_gridCells[coords.x - 1, coords.y]);
            }
            if (coords.x - 1 >= 0 && coords.y + 1 < _Y)
            {
                neigh.Add(_gridCells[coords.x - 1, coords.y + 1]);
            }

            if (coords.y - 1 >= 0)
            {
                neigh.Add(_gridCells[coords.x, coords.y - 1]);
            }
            if (includeSelf)
            {
                neigh.Add(_gridCells[coords.x, coords.y]);
            }
            if (coords.y + 1 < _Y)
            {
                neigh.Add(_gridCells[coords.x, coords.y + 1]);
            }

            if (coords.x + 1 < _X && coords.y - 1 >= 0)
            {
                neigh.Add(_gridCells[coords.x + 1, coords.y - 1]);
            }
            if (coords.x + 1 < _X)
            {
                neigh.Add(_gridCells[coords.x + 1, coords.y]);
            }
            if (coords.x + 1 < _X && coords.y + 1 < _Y)
            {
                neigh.Add(_gridCells[coords.x + 1, coords.y + 1]);
            }

            return neigh;
        }

    }
}