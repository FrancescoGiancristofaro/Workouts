using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkoutsApp.Dtos
{
    internal class InfoPopupDto
    {
        public string Title { get; set; }
        public string Message { get; set; }
    }

    internal class EditorPopupDto
    {
        public string Title { get; set; }
        public string Text { get; set; }
    }

}
