using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkoutsApp
{
    public interface IBaseViewModel
    {
        public Task ManageException(object ex);
        void PrepareModel();
        void ReversePrepareModel(object data = null);
        void OnAppearing();
        void OnDisappearing();
        Task NavigateBack();
    }
}
