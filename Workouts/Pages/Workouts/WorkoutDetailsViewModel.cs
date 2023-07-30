using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using Services.Dtos;

namespace WorkoutsApp.Pages.Workouts
{
    [QueryProperty(nameof(WorkoutsDto), "workout")]
    public partial class WorkoutDetailsViewModel : BaseViewModel
    {
        [ObservableProperty] WorkoutsDto _workoutsDto;

        public override void PrepareModel()
        {
            base.PrepareModel();
        }
    }
}
