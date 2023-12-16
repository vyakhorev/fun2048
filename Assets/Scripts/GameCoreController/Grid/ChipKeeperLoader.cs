

using Codice.Client.BaseCommands.BranchExplorer.ExplorerData;
using LevelData;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace GameCoreController
{
    /*
     Stores grid with chips
     */
    public class ChipKeeperLoader
    {

        public static void LoadGoals(GameGoalWatcher watcher, List<LevelGoal> goals)
        {
            foreach (LevelGoal goal in goals)
            {
                if (goal.GoalType == LevelGoalType.CLEAR_GRASS)
                {
                    watcher.SetCleanGrassGoal(goal.Quantity);
                }
                else if (goal.GoalType == LevelGoalType.CLEAR_HONEY)
                {
                    watcher.SetCleanHoneyGoal(goal.Quantity);
                }
                else if (goal.GoalType == LevelGoalType.CLEAR_EGG)
                {
                    watcher.SetCleanEggGoal(goal.Quantity);
                }
                else if (goal.GoalType == LevelGoalType.CLEAR_BOX)
                {
                    watcher.SetCleanBoxGoal(goal.Quantity);
                }
                else if (goal.GoalType == LevelGoalType.COMBINE_NUMBER)
                {
                    watcher.SetCombineNumbersGoal(
                        goal.NumberToCombine, 
                        goal.Quantity
                    );
                }
                else if (goal.GoalType == LevelGoalType.CLEAR_BOMB)
                {
                    watcher.SetCleanBombGoal(goal.Quantity);
                }
            }
        }

        public static void LoadBoard(ChipKeeper chipKeeper, BoardData boardData)
        {
            if (boardData.GridCellList != null)
            {
                foreach (GridCellData cellData in boardData.GridCellList)
                {
                    if (cellData.IsZone)
                    {
                        var zn = cellData.Zone;
                        for (int x = zn.x0; x <= zn.x1; x++)
                        {
                            for (int y = zn.y0; y <= zn.y1; y++)
                            {
                                var cell = chipKeeper.GetGridCell(x, y);
                                LoadCell(chipKeeper, cellData, cell);
                            }
                        }
                    }
                    else
                    {
                        var coords = cellData.Coords;
                        var cell = chipKeeper.GetGridCell(coords.x, coords.y);
                        LoadCell(chipKeeper, cellData, cell);
                    }
                }
            }

            if (boardData.NumberChipList != null)
            {
                foreach (NumberChipData numberData in boardData.NumberChipList)
                {
                    if (numberData.IsZone)
                    {
                        var zn = numberData.Zone;
                        for (int x = zn.x0; x <= zn.x1; x++)
                        {
                            for (int y = zn.y0; y <= zn.y1; y++)
                            {
                                var cell = chipKeeper.GetGridCell(x, y);
                                LoadNumberChip(chipKeeper, numberData, cell);
                            }
                        }
                    }
                    else
                    {
                        var coords = numberData.Coords;
                        var cell = chipKeeper.GetGridCell(coords.x, coords.y);
                        LoadNumberChip(chipKeeper, numberData, cell);
                    }
                }
            }

            if (boardData.BoxChipList != null)
            {
                foreach (BoxChipData boxData in boardData.BoxChipList)
                {
                    if (boxData.IsZone)
                    {
                        var zn = boxData.Zone;
                        for (int x = zn.x0; x <= zn.x1; x++)
                        {
                            for (int y = zn.y0; y <= zn.y1; y++)
                            {
                                var cell = chipKeeper.GetGridCell(x, y);
                                LoadBoxChip(chipKeeper, boxData, cell);
                            }
                        }
                    }
                    else
                    {
                        var coords = boxData.Coords;
                        var cell = chipKeeper.GetGridCell(coords.x, coords.y);
                        LoadBoxChip(chipKeeper, boxData, cell);
                    }
                }
            }

            if (boardData.EggChipList != null)
            {
                foreach (EggChipData eggData in boardData.EggChipList)
                {
                    if (eggData.IsZone)
                    {
                        var zn = eggData.Zone;
                        for (int x = zn.x0; x <= zn.x1; x++)
                        {
                            for (int y = zn.y0; y <= zn.y1; y++)
                            {
                                var cell = chipKeeper.GetGridCell(x, y);
                                LoadEggChip(chipKeeper, eggData, cell);
                            }
                        }
                    }
                    else
                    {
                        var coords = eggData.Coords;
                        var cell = chipKeeper.GetGridCell(coords.x, coords.y);
                        LoadEggChip(chipKeeper, eggData, cell);
                    }
                }
            }

            if (boardData.BubbleChipList != null)
            {
                foreach (BubbleChipData bubbleData in boardData.BubbleChipList)
                {
                    if (bubbleData.IsZone)
                    {
                        var zn = bubbleData.Zone;
                        for (int x = zn.x0; x <= zn.x1; x++)
                        {
                            for (int y = zn.y0; y <= zn.y1; y++)
                            {
                                var cell = chipKeeper.GetGridCell(x, y);
                                LoadBubbleChip(chipKeeper, bubbleData, cell);
                            }
                        }
                    }
                    else
                    {
                        var coords = bubbleData.Coords;
                        var cell = chipKeeper.GetGridCell(coords.x, coords.y);
                        LoadBubbleChip(chipKeeper, bubbleData, cell);
                    }
                }
            }

            if (boardData.BombChipList != null)
            {
                foreach (BombChipData bombData in boardData.BombChipList)
                {
                    if (bombData.IsZone)
                    {
                        var zn = bombData.Zone;
                        for (int x = zn.x0; x <= zn.x1; x++)
                        {
                            for (int y = zn.y0; y <= zn.y1; y++)
                            {
                                var cell = chipKeeper.GetGridCell(x, y);
                                LoadBombChip(chipKeeper, bombData, cell);
                            }
                        }
                    }
                    else
                    {
                        var coords = bombData.Coords;
                        var cell = chipKeeper.GetGridCell(coords.x, coords.y);
                        LoadBombChip(chipKeeper, bombData, cell);
                    }
                }
            }
        }

        private static void LoadCell(ChipKeeper chipKeeper, GridCellData cellData, GridCell cell)
        {
            if (cellData.GrassHealth > 0)
            {
                cell.SetGrassHealth(cellData.GrassHealth);
                chipKeeper.ReportEffect(
                    new GrassHealthChangeEffect(
                        cell.GetCoords(),
                        cellData.GrassHealth
                    )
                );
            }
            if (cellData.HoneyHealth > 0)
            {
                cell.SetHoneyHealth(cellData.HoneyHealth);
                chipKeeper.ReportEffect(
                    new HoneyHealthChangeEffect(
                        cell.GetCoords(),
                        cellData.HoneyHealth
                    )
                );
            }
            if (!cellData.IsEnabled)
            {
                cell.Disable();
                chipKeeper.ReportEffect(
                    new CellEnabledChangeEffect(
                        cell.GetCoords(),
                        false
                    )
                );
            }
        }
    
        private static void LoadNumberChip(ChipKeeper chipKeeper, NumberChipData numberData, GridCell cell)
        {
            if (!cell.IsEmpty()) 
            { 
                throw new Exception("cannot spawn number chip at non-empty cell"); 
            }
            NumberChip newChip = new NumberChip(numberData.NumberValue);
            newChip.SetChipId(chipKeeper.GetNextChipId());
            cell.SetChip(newChip);
            chipKeeper.ReportEffect(
                new ChipSpawnedEffect(newChip, cell.GetCoords())
            );
        }

        private static void LoadBoxChip(ChipKeeper chipKeeper, BoxChipData boxData, GridCell cell)
        {
            if (!cell.IsEmpty())
            {
                throw new Exception("cannot spawn box chip at non-empty cell");
            }
            BoxChip newChip = new BoxChip(boxData.Health);
            newChip.SetChipId(chipKeeper.GetNextChipId());
            cell.SetChip(newChip);
            chipKeeper.ReportEffect(
                new ChipSpawnedEffect(newChip, cell.GetCoords())
            );
        }

        private static void LoadEggChip(ChipKeeper chipKeeper, EggChipData eggData, GridCell cell)
        {
            if (!cell.IsEmpty())
            {
                throw new Exception("cannot spawn egg chip at non-empty cell");
            }
            EggChip newChip = new EggChip(eggData.Health);
            newChip.SetChipId(chipKeeper.GetNextChipId());
            cell.SetChip(newChip);
            chipKeeper.ReportEffect(
                new ChipSpawnedEffect(newChip, cell.GetCoords())
            );
        }

        private static void LoadBubbleChip(ChipKeeper chipKeeper, BubbleChipData bubbleData, GridCell cell)
        {
            if (!cell.IsEmpty())
            {
                throw new Exception("cannot spawn bubble chip at non-empty cell");
            }
            BubbleChip newChip = new BubbleChip(bubbleData.BubbleValue);
            newChip.SetChipId(chipKeeper.GetNextChipId());
            cell.SetChip(newChip);
            chipKeeper.ReportEffect(
                new ChipSpawnedEffect(newChip, cell.GetCoords())
            );
        }

        private static void LoadBombChip(ChipKeeper chipKeeper, BombChipData bombData, GridCell cell)
        {
            if (!cell.IsEmpty())
            {
                throw new Exception("cannot spawn bomb chip at non-empty cell");
            }
            BombChip newChip = new BombChip();
            newChip.SetChipId(chipKeeper.GetNextChipId());
            cell.SetChip(newChip);
            chipKeeper.ReportEffect(
                new ChipSpawnedEffect(newChip, cell.GetCoords())
            );
        }

    }

}