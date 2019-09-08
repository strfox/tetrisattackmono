using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace TetrisAttack.Sprites
{
    class Board : Sprite
    {
        readonly int TopMarginWidth = 4;
        readonly int LeftMarginWidth = 4;
        readonly int RightMarginWidth = 4;
        readonly int BottomMarginWidth = 8;
        readonly int InnerWidth = 96;
        readonly int InnerHeight = 194;

        readonly int PanelWidth = 16;
        readonly int PanelHeight = 16;

        readonly int PanelsPerRow = 6;

        private List<List<Panel>> _panels = new List<List<Panel>>();

        public void AddRow(List<Panel> panels)
        {
            if (PanelsPerRow != panels.Count)
            {
                throw new ArgumentException($"Panel array length is {panels.Count} but expected it to be {PanelsPerRow}");
            }
            _panels.Add(panels);
        }

        public void ApplyGravity()
        {
            // this is where all columns that have been applied with gravity
            // are stored, so that the code knows it doesn't have to apply gravity at that column again
            var columnsApplied = new bool[PanelsPerRow];
            var amountColumnsApplied = 0;

            for (int row = _panels.Count - 1; row >= 0; row--)
            {
                for (int col = 0; col < _panels[row].Count; col++)
                {
                    // _panels.Count - 1 = First row (yes, it's reversed)

                    // if we've already iterated over all columns, it's safe to break out
                    if (amountColumnsApplied == PanelsPerRow)
                    {
                        goto break_out_of_forloop;
                    }

                    // skip this column if this column has already been applied by a previous iteration
                    if (columnsApplied[col])
                    {
                        continue;
                    }

                    // ignore this spot if there is a panel here
                    if (_panels[row][col] != null)
                    {
                        continue;
                    }

                    // list of panels that will be affected by gravity
                    var panelsPulledDown = new List<Panel>();

                    // find panels to pull
                    for (int auxRow = row - 1; auxRow >= 0; auxRow--)
                    {
                        // p = row
                        if (_panels[auxRow][col] != null)
                        {
                            panelsPulledDown.Add(_panels[auxRow][col]);
                            // remove the panel from the board (it will be readded in its post-gravity location)
                            _panels[auxRow][col] = null;
                        }
                    }

                    // put the panels in their final positions
                    for (int auxRow = row - 1, i = 0; i < panelsPulledDown.Count; i++, auxRow--)
                    {
                        _panels[auxRow][col] = panelsPulledDown[i];
                    }

                    // mark column k as applied so we don't go over it again
                    columnsApplied[col] = true;
                    amountColumnsApplied++;
                }
            }

        break_out_of_forloop:
            ;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Texture, Position, Color.White);

            foreach (var list in _panels)
            {
                foreach (var panel in list)
                {
                    panel.Draw(spriteBatch);
                }
            }
        }
    }
}
