using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Pexeso
{
    public class Card
    {

        public int Id { get; set; }

        public int Row { get; set; }

        public int Column { get; set; }

        public Card(int id, int row, int column)
        {
            Id = id;
            Row = row;
            Column = column;
        }
    }


}

