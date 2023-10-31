using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Fun2048
{
    /*
     Stores grid with chips
     */
    public class ChipKeeper
    {

        private GridCell[,] _gridCells;

        //private Dictionary<Vector2Int, AChip> _chips;
        //private Dictionary<int, Vector2Int> _chipsRev;
        private float _cellSize;

        public ChipKeeper(int x, int y)
        {
            //_chips = new Dictionary<Vector2Int, AChip>();
            _gridCells = new GridCell[x, y];
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

        public Vector3 LogicalToWorldPos(Vector2Int logicalPos)
        {
            return new Vector3(
              logicalPos.x * _cellSize,
              0f,
              logicalPos.y * _cellSize
            );
        }

        public void SetChip(Vector2Int xy, AChip chip)
        {
            _gridCells[xy.x, xy.y].SetChip(chip);
        }

        //public bool AddChip(Vector2Int xz, AChip chip)
        //{
        //    if (_chips.TryAdd(xz, chip))
        //    {
        //        _chipsRev.Add(chip.GetChipId(), xz);
        //        return true;
        //    }
        //    return false;
        //}

        //public bool GetChip(Vector2Int xz, out AChip chip)
        //{
        //    return _chips.TryGetValue(xz, out chip);
        //}

        //public bool RemoveChip(AChip chip)
        //{
        //    if (_chipsRev.TryGetValue(chip.GetChipId(), out Vector2Int coords))
        //    {
        //        _chips.Remove(coords);
        //        _chipsRev.Remove(chip.GetChipId());
        //        return true;
        //    }
        //    return false;
        //}

        /*
         * Find neighbours of a cell
         */
        //public List<(GridDirection, AChip)> FindNeighbours(Vector2Int xz)
        //{
        //    var ans = new List<(GridDirection, AChip)>();

        //    if (_chips.TryGetValue(new Vector2Int(xz.x - 1, xz.y), out AChip nghb1))
        //    {
        //        ans.Add((GridDirection.LEFT, nghb1));
        //    }

        //    if (_chips.TryGetValue(new Vector2Int(xz.x + 1, xz.y), out AChip nghb2))
        //    {
        //        ans.Add((GridDirection.RIGHT, nghb2));
        //    }

        //    if (_chips.TryGetValue(new Vector2Int(xz.x, xz.y - 1), out AChip nghb3))
        //    {
        //        ans.Add((GridDirection.DOWN, nghb3));
        //    }

        //    if (_chips.TryGetValue(new Vector2Int(xz.x, xz.y + 1), out AChip nghb4))
        //    {
        //        ans.Add((GridDirection.UP, nghb4));
        //    }

        //    return ans;
        //}

        /*
         * Check first occuring chip in a line by direction
         */
        //public AChip TraceLine(int lineNum, GridDirection gridDirection)
        //{
            
        //}

        

    }
}