using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace projettaquin
{
    public partial class Entrepot : Form
    {
        private List<Chariot> chariots = new List<Chariot>();

        public Entrepot()
        {
            InitializeComponent();
        }
    }
}
