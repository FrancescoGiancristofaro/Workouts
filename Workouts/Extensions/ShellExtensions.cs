using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkoutsApp.Extensions
{
    public static class ShellExtensions
    {
        public static async Task GoToAsync(this Shell shell, string route, string key, object dataToPass)
        {
            var parameters = new Dictionary<string, object>() { { key, dataToPass } };
            await shell.GoToAsync(route, parameters);
        }
    }
}
